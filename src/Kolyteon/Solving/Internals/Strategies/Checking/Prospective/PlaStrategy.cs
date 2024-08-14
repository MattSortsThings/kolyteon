using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Prospective;

internal sealed class PlaStrategy<TVariable, TDomainValue> :
    CheckingStrategy<PlaNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private readonly PlaArcTasksQueue _arcTasks;
    private readonly RootLevelArcPruner<TVariable, TDomainValue> _rootLevelArcPruner;

    public PlaStrategy(int capacity)
    {
        SearchTree = new PlaTree(capacity);
        _arcTasks = new PlaArcTasksQueue(capacity);
        _rootLevelArcPruner = new RootLevelArcPruner<TVariable, TDomainValue>();
    }

    public override CheckingStrategy Identifier => CheckingStrategy.PartialLookingAhead;

    private protected override SearchTree<PlaNode<TVariable, TDomainValue>, TVariable, TDomainValue> SearchTree { get; }

    private protected override void ReduceSearchTree() => EnforceArcConsistency();

    private protected override void SetupForAssigning() => SearchTree.GetPresentNode().RepopulateSuccessors(SearchTree);

    private protected override void SetupForBacktracking(int backtrackLevel)
    {
        for (int level = SearchTree.SearchLevel; level > backtrackLevel; level--)
        {
            SearchTree[level].Successors.Clear();
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
        PlaNode<TVariable, TDomainValue> presentNode = SearchTree.GetPresentNode();

        for (int i = 0; noNodeExhausted && i < presentNode.Successors.Count; i++)
        {
            ProspectiveNode<TVariable, TDomainValue> successorNode = presentNode.Successors[i];
            presentNode.Prune(successorNode);
            noNodeExhausted = successorNode.RemainingCandidates > 0;
        }

        Safe = noNodeExhausted;
    }

    private void EnforceArcConsistency()
    {
        bool noNodeExhausted = true;
        _arcTasks.Populate(SearchTree);

        IArcPruner<TVariable, TDomainValue> arcPruner = GetArcPrunerForThisSearchLevel();

        while (noNodeExhausted && _arcTasks.TryDequeue(out PlaNode<TVariable, TDomainValue>? operandNode,
                   out PlaNode<TVariable, TDomainValue>? contextNode))
        {
            arcPruner.ArcPrune(operandNode, contextNode);
            noNodeExhausted = operandNode.RemainingCandidates > 0;
        }

        _arcTasks.Clear();

        Safe = noNodeExhausted;
    }

    private IArcPruner<TVariable, TDomainValue> GetArcPrunerForThisSearchLevel() =>
        SearchTree.SearchLevel > SearchTree.RootLevel
            ? SearchTree.GetPresentNode()
            : _rootLevelArcPruner;

    private sealed class PlaTree : SearchTree<PlaNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    {
        public PlaTree(int capacity) : base(capacity)
        {
        }

        private protected override PlaNode<TVariable, TDomainValue> GetNode(int variableIndex,
            IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp) =>
            new(binaryCsp, variableIndex);
    }

    private sealed class PlaArcTasksQueue : ArcTaskQueue<PlaNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    {
        public PlaArcTasksQueue(int capacity) : base(capacity)
        {
        }

        public override void Populate(SearchTree<PlaNode<TVariable, TDomainValue>, TVariable, TDomainValue> searchTree)
        {
            int leafLevel = searchTree.LeafLevel;

            for (int operandLevel = searchTree.SearchLevel + 1; operandLevel < leafLevel; operandLevel++)
            {
                PlaNode<TVariable, TDomainValue> operandNode = searchTree[operandLevel];
                int remaining = operandNode.Degree;

                for (int contextLevel = operandLevel + 1; remaining > 0 && contextLevel < leafLevel; contextLevel++)
                {
                    PlaNode<TVariable, TDomainValue> contextNode = searchTree[contextLevel];

                    if (!operandNode.AdjacentTo(contextNode))
                    {
                        continue;
                    }

                    Enqueue(operandNode, contextNode);
                    remaining--;
                }
            }
        }
    }
}
