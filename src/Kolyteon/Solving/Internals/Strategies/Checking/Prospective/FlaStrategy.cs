using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;
using Kolyteon.Solving.Internals.Strategies.Checking.Common;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Prospective;

internal sealed class FlaStrategy<TVariable, TDomainValue> :
    CheckingStrategy<FlaNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private readonly FlaArcTasksQueue _arcTasks;
    private readonly RootLevelArcPruner<TVariable, TDomainValue> _rootLevelArcPruner;

    public FlaStrategy(int capacity)
    {
        SearchTree = new FlaTree(capacity);
        _arcTasks = new FlaArcTasksQueue(capacity);
        _rootLevelArcPruner = new RootLevelArcPruner<TVariable, TDomainValue>();
    }

    public override CheckingStrategy Identifier => CheckingStrategy.FullLookingAhead;

    private protected override SearchTree<FlaNode<TVariable, TDomainValue>, TVariable, TDomainValue> SearchTree { get; }

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
        FlaNode<TVariable, TDomainValue> presentNode = SearchTree.GetPresentNode();

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

        while (noNodeExhausted && _arcTasks.TryDequeue(out FlaNode<TVariable, TDomainValue>? operandNode,
                   out FlaNode<TVariable, TDomainValue>? contextNode))
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

    private sealed class FlaTree : SearchTree<FlaNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    {
        public FlaTree(int capacity) : base(capacity)
        {
        }

        private protected override FlaNode<TVariable, TDomainValue> GetNode(int variableIndex,
            IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp) =>
            new(binaryCsp, variableIndex);
    }

    private sealed class FlaArcTasksQueue : ArcTaskQueue<FlaNode<TVariable, TDomainValue>, TVariable, TDomainValue>
    {
        public FlaArcTasksQueue(int capacity) : base(capacity)
        {
        }

        public override void Populate(SearchTree<FlaNode<TVariable, TDomainValue>, TVariable, TDomainValue> searchTree)
        {
            int startLevel = searchTree.SearchLevel + 1;
            int upperLimit = searchTree.Count;

            for (int operandLevel = startLevel; operandLevel < upperLimit; operandLevel++)
            {
                FlaNode<TVariable, TDomainValue> operandNode = searchTree[operandLevel];
                int possibleContextNodes = operandNode.Degree;

                for (int contextLevel = startLevel; possibleContextNodes > 0 && contextLevel < upperLimit; contextLevel++)
                {
                    if (operandLevel == contextLevel)
                    {
                        continue;
                    }

                    FlaNode<TVariable, TDomainValue> contextNode = searchTree[contextLevel];

                    if (!operandNode.AdjacentTo(contextNode))
                    {
                        continue;
                    }

                    Enqueue(operandNode, contextNode);
                    possibleContextNodes--;
                }
            }
        }
    }
}
