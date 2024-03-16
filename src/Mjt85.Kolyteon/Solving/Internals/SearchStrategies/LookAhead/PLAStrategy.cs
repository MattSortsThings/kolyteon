using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;

namespace Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookAhead;

internal sealed class PLAStrategy<V, D> : SearchStrategy<PLANode<V, D>, V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private readonly PLAQueue _arcTaskQueue;

    public PLAStrategy(int capacity)
    {
        SearchTree = new PLATree(capacity);
        _arcTaskQueue = new PLAQueue(capacity);
    }

    public override Search Identifier => Search.PartialLookingAhead;

    protected internal override SearchTree<PLANode<V, D>, V, D> SearchTree { get; }

    protected override void PopulateSearchTree(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        var leafLevel = binaryCsp.Variables;
        SearchTree.EnsureCapacity(leafLevel);
        for (var i = 0; i < leafLevel; i++)
        {
            SearchTree.Add(new PLANode<V, D>(binaryCsp, i));
        }
    }

    protected override void AddSafetyCheckAtRootLevel()
    {
        CheckAllNodesForEmptyDomains();
        if (SearchState == SearchState.Safe)
        {
            EnforceArcConsistencyInFutureNodes();
        }
    }

    protected override void AddSafetyCheck()
    {
        PruneSuccessorCandidatesBasedOnPresentAssignment();
        if (SearchState == SearchState.Safe)
        {
            EnforceArcConsistencyInFutureNodes();
        }
    }

    protected override void UndoSafetyCheckPropagation() => GetPresentNode().UndoAllPruning();

    protected override void SetupForVisit() => GetPresentNode().RepopulateSuccessors(SearchTree);

    protected override void SetupForBacktrack(in int backtrackLevel)
    {
        for (var level = SearchLevel; level > backtrackLevel; level--)
        {
            SearchTree[level].Successors.Clear();
        }
    }

    private void PruneSuccessorCandidatesBasedOnPresentAssignment()
    {
        var noNodeExhausted = true;
        PLANode<V, D> presentNode = GetPresentNode();

        for (var i = 0; noNodeExhausted && i < presentNode.Successors.Count; i++)
        {
            LookAheadNode<V, D> successorNode = presentNode.Successors[i];
            presentNode.Prune(successorNode);
            noNodeExhausted = successorNode.RemainingCandidates > 0;
        }

        if (noNodeExhausted)
        {
            SearchState = SearchState.Safe;
        }
    }

    private void EnforceArcConsistencyInFutureNodes()
    {
        var noNodeExhausted = true;
        _arcTaskQueue.Populate(SearchTree, SearchLevel);

        IArcConsistencyEnforcer<V, D> presentNode = SearchLevel > RootLevel ? GetPresentNode() : new RootLevelArcPruner<V, D>();

        while (noNodeExhausted && _arcTaskQueue.TryDequeue(out PLANode<V, D>? operandNode, out PLANode<V, D>? contextNode))
        {
            presentNode.ArcPrune(operandNode, contextNode);
            noNodeExhausted = operandNode.RemainingCandidates > 0;
        }

        _arcTaskQueue.Clear();

        SearchState = noNodeExhausted ? SearchState.Safe : SearchState.Unsafe;
    }

    private sealed class PLATree : SearchTree<PLANode<V, D>, V, D>
    {
        public PLATree(int capacity) : base(capacity)
        {
        }
    }

    private sealed class PLAQueue : ArcTaskQueue<PLANode<V, D>, V, D>
    {
        public PLAQueue(int capacity) : base(capacity)
        {
        }

        public override void Populate(IReadOnlyList<PLANode<V, D>> searchTree, int searchLevel)
        {
            var upperLimit = searchTree.Count;

            for (var operandLevel = searchLevel + 1; operandLevel < upperLimit; operandLevel++)
            {
                PLANode<V, D> operandNode = searchTree[operandLevel];
                var possibleContextNodes = operandNode.Degree;

                for (var contextLevel = operandLevel + 1; possibleContextNodes > 0 && contextLevel < upperLimit; contextLevel++)
                {
                    PLANode<V, D> contextNode = searchTree[contextLevel];

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
