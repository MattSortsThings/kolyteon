using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookAhead;

internal sealed class MACStrategy<V, D> : SearchStrategy<MACNode<V, D>, V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private readonly MACQueue _arcTaskQueue;

    public MACStrategy(int capacity)
    {
        SearchTree = new MACTree(capacity);
        _arcTaskQueue = new MACQueue(capacity);
    }

    public override Search Identifier => Search.MaintainingArcConsistency;

    protected internal override SearchTree<MACNode<V, D>, V, D> SearchTree { get; }

    protected override void PopulateSearchTree(ISolvableBinaryCsp<V, D> binaryCsp)
    {
        var leafLevel = binaryCsp.Variables;
        SearchTree.EnsureCapacity(leafLevel);
        for (var i = 0; i < leafLevel; i++)
        {
            SearchTree.Add(new MACNode<V, D>(binaryCsp, i));
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
        MACNode<V, D> presentNode = GetPresentNode();

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

        while (noNodeExhausted && _arcTaskQueue.TryDequeue(out MACNode<V, D>? operandNode, out MACNode<V, D>? contextNode))
        {
            var initialCandidates = operandNode.RemainingCandidates;
            presentNode.ArcPrune(operandNode, contextNode);

            if (operandNode.RemainingCandidates == initialCandidates)
            {
                continue;
            }

            _arcTaskQueue.Update(SearchTree, SearchLevel, operandNode, contextNode);
            noNodeExhausted = operandNode.RemainingCandidates > 0;
        }

        _arcTaskQueue.Clear();

        SearchState = noNodeExhausted ? SearchState.Safe : SearchState.Unsafe;
    }

    private sealed class MACTree : SearchTree<MACNode<V, D>, V, D>
    {
        public MACTree(int capacity) : base(capacity)
        {
        }
    }

    private sealed class MACQueue : ArcTaskQueue<MACNode<V, D>, V, D>
    {
        public MACQueue(int capacity) : base(capacity)
        {
        }

        public override void Populate(IReadOnlyList<MACNode<V, D>> searchTree, int searchLevel)
        {
            var startLevel = searchLevel + 1;
            var upperLimit = searchTree.Count;

            for (var operandLevel = startLevel; operandLevel < upperLimit; operandLevel++)
            {
                MACNode<V, D> operandNode = searchTree[operandLevel];
                for (var contextLevel = startLevel; contextLevel < upperLimit; contextLevel++)
                {
                    if (operandLevel == contextLevel)
                    {
                        continue;
                    }

                    MACNode<V, D> contextNode = searchTree[contextLevel];

                    if (!operandNode.AdjacentTo(contextNode))
                    {
                        continue;
                    }

                    Enqueue(operandNode, contextNode);
                }
            }
        }

        public void Update(IReadOnlyList<MACNode<V, D>> searchTree, int searchLevel, MACNode<V, D> oldOperandNode,
            IVisitableNode oldContextNode)
        {
            var oldContextLevel = oldContextNode.SearchTreeLevel;
            var upperLimit = searchTree.Count;

            for (var newOperandLevel = searchLevel + 1; newOperandLevel < upperLimit; newOperandLevel++)
            {
                if (newOperandLevel == oldContextLevel)
                {
                    continue;
                }

                MACNode<V, D> newOperandNode = searchTree[newOperandLevel];
                if (oldOperandNode.AdjacentTo(oldOperandNode))
                {
                    Enqueue(newOperandNode, oldOperandNode);
                }
            }
        }
    }
}
