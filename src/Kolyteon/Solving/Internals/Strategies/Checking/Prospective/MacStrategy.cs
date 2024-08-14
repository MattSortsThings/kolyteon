using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Prospective;

internal sealed class MacStrategy<TVariable, TDomainValue> :
    CheckingStrategy<MacNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private readonly MacArcTasksQueue _arcTasks;
    private readonly RootLevelArcPruner<TVariable, TDomainValue> _rootLevelArcPruner;

    public MacStrategy(int capacity)
    {
        SearchTree = new MacTree(capacity);
        _arcTasks = new MacArcTasksQueue(capacity);
        _rootLevelArcPruner = new RootLevelArcPruner<TVariable, TDomainValue>();
    }

    public override CheckingStrategy Identifier => CheckingStrategy.MaintainingArcConsistency;

    private protected override SearchTree<MacNode<TVariable, TDomainValue>, TVariable, TDomainValue> SearchTree { get; }

    private protected override void ReduceSearchTree() => EnforceArcConsistency();

    private protected override void SetupForAssigning() => SearchTree.GetPresentNode().PopulateSuccessors(SearchTree);

    private protected override void SetupForBacktracking(int backtrackLevel)
    {
        for (int level = SearchTree.SearchLevel; level > backtrackLevel; level--)
        {
            SearchTree[level].ClearSuccessors();
        }
    }

    private protected override void AddSafetyCheck()
    {
        PruneFutureNodesBasedOnPresentAssignment();
        if (Safe)
        {
            EnforceArcConsistency();
        }
    }

    private protected override void UndoLastSafetyCheck() => SearchTree.GetPresentNode().UndoAllPruning();

    private void PruneFutureNodesBasedOnPresentAssignment()
    {
        bool noNodeExhausted = true;
        MacNode<TVariable, TDomainValue> presentNode = SearchTree.GetPresentNode();

        for (int i = 0; noNodeExhausted && i < presentNode.Successors.Count; i++)
        {
            ProspectiveNode<TVariable, TDomainValue> successorNode = presentNode.Successors[i];
            presentNode.Prune(successorNode);
            noNodeExhausted = !successorNode.Exhausted;
        }

        Safe = noNodeExhausted;
    }

    private void EnforceArcConsistency()
    {
        bool noNodeExhausted = true;
        _arcTasks.Populate(SearchTree);

        IArcPruner<TVariable, TDomainValue> arcPruner = GetArcPrunerForThisSearchLevel();

        while (noNodeExhausted && _arcTasks.TryDequeue(out MacNode<TVariable, TDomainValue>? operandNode,
                   out MacNode<TVariable, TDomainValue>? contextNode))
        {
            int initialCandidates = operandNode.RemainingCandidates;
            arcPruner.ArcPrune(operandNode, contextNode);

            if (operandNode.RemainingCandidates == initialCandidates)
            {
                continue;
            }

            _arcTasks.Update(SearchTree, operandNode, contextNode);
            noNodeExhausted = !operandNode.Exhausted;
        }

        _arcTasks.Clear();

        Safe = noNodeExhausted;
    }

    private IArcPruner<TVariable, TDomainValue> GetArcPrunerForThisSearchLevel() =>
        SearchTree.SearchLevel > SearchTree.RootLevel
            ? SearchTree.GetPresentNode()
            : _rootLevelArcPruner;

    private sealed class MacTree : SearchTree<MacNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    {
        public MacTree(int capacity) : base(capacity)
        {
        }

        private protected override MacNode<TVariable, TDomainValue> GetNode(int variableIndex,
            IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp) =>
            new(binaryCsp, variableIndex);
    }

    private sealed class MacArcTasksQueue : ArcTaskQueue<MacNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    {
        public MacArcTasksQueue(int capacity) : base(capacity)
        {
        }

        public override void Populate(SearchTree<MacNode<TVariable, TDomainValue>, TVariable, TDomainValue> searchTree)
        {
            int startLevel = searchTree.SearchLevel + 1;
            int leafLevel = searchTree.LeafLevel;

            for (int operandLevel = startLevel; operandLevel < leafLevel; operandLevel++)
            {
                MacNode<TVariable, TDomainValue> operandNode = searchTree[operandLevel];
                int remaining = operandNode.Degree;

                for (int contextLevel = startLevel; remaining > 0 && contextLevel < leafLevel; contextLevel++)
                {
                    if (operandLevel == contextLevel)
                    {
                        continue;
                    }

                    MacNode<TVariable, TDomainValue> contextNode = searchTree[contextLevel];

                    if (!operandNode.AdjacentTo(contextNode))
                    {
                        continue;
                    }

                    Enqueue(operandNode, contextNode);
                    remaining--;
                }
            }
        }

        public void Update(SearchTree<MacNode<TVariable, TDomainValue>, TVariable, TDomainValue> searchTree,
            MacNode<TVariable, TDomainValue> oldOperandNode, IVisitableNode oldContextNode)
        {
            int oldContextLevel = oldContextNode.SearchTreeLevel;
            int leafLevel = searchTree.LeafLevel;

            int remaining = oldOperandNode.Degree;

            for (int operandLevel = searchTree.SearchLevel + 1; remaining > 0 && operandLevel < leafLevel; operandLevel++)
            {
                if (operandLevel == oldContextLevel)
                {
                    continue;
                }

                MacNode<TVariable, TDomainValue> newOperandNode = searchTree[operandLevel];
                if (oldOperandNode.AdjacentTo(newOperandNode))
                {
                    Enqueue(newOperandNode, oldOperandNode);
                    remaining--;
                }
            }
        }
    }
}
