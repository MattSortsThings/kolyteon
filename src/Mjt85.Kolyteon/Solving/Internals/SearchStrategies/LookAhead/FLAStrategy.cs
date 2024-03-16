using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;

namespace Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookAhead;

internal sealed class FLAStrategy<V, D> : SearchStrategy<FLANode<V, D>, V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private readonly FLAQueue _arcTaskQueue;

    public FLAStrategy(int capacity)
    {
        SearchTree = new FLATree(capacity);
        _arcTaskQueue = new FLAQueue(capacity);
    }

    public override Search Identifier => Search.FullLookingAhead;

    protected internal override SearchTree<FLANode<V, D>, V, D> SearchTree { get; }

    protected override void PopulateSearchTree(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        var leafLevel = binaryCsp.Variables;
        SearchTree.EnsureCapacity(leafLevel);
        for (var i = 0; i < leafLevel; i++)
        {
            SearchTree.Add(new FLANode<V, D>(binaryCsp, i));
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
        FLANode<V, D> presentNode = GetPresentNode();

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

        while (noNodeExhausted && _arcTaskQueue.TryDequeue(out FLANode<V, D>? operandNode, out FLANode<V, D>? contextNode))
        {
            presentNode.ArcPrune(operandNode, contextNode);
            noNodeExhausted = operandNode.RemainingCandidates > 0;
        }

        _arcTaskQueue.Clear();

        SearchState = noNodeExhausted ? SearchState.Safe : SearchState.Unsafe;
    }

    private sealed class FLATree : SearchTree<FLANode<V, D>, V, D>
    {
        public FLATree(int capacity) : base(capacity)
        {
        }
    }

    private sealed class FLAQueue : ArcTaskQueue<FLANode<V, D>, V, D>
    {
        public FLAQueue(int capacity) : base(capacity)
        {
        }

        public override void Populate(IReadOnlyList<FLANode<V, D>> searchTree, int searchLevel)
        {
            var startLevel = searchLevel + 1;
            var upperLimit = searchTree.Count;

            for (var operandLevel = startLevel; operandLevel < upperLimit; operandLevel++)
            {
                FLANode<V, D> operandNode = searchTree[operandLevel];
                var possibleContextNodes = operandNode.Degree;

                for (var contextLevel = startLevel; possibleContextNodes > 0 && contextLevel < upperLimit; contextLevel++)
                {
                    if (operandLevel == contextLevel)
                    {
                        continue;
                    }

                    FLANode<V, D> contextNode = searchTree[contextLevel];

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
