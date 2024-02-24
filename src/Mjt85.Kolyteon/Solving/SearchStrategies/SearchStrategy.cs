using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.OrderingStrategies;
using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving.SearchStrategies;

internal abstract class SearchStrategy<N, V, D> : ISearchStrategy<V, D>
    where N : SearchTreeNode<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    protected const int RootLevel = -1;

    protected internal abstract SearchTree<N, V, D> SearchTree { get; }

    public abstract Search Identifier { get; }

    public int Capacity => SearchTree.Capacity;

    public SearchState SearchState { get; protected set; }

    public int SearchLevel { get; private set; } = RootLevel;

    public int SearchTreeLeafLevel => SearchTree.Count;

    public int SearchTreeRootLevel => RootLevel;

    public void Setup(ISolvableBinaryCsp<V, D> binaryCsp, IOrderingStrategy orderingStrategy)
    {
        PopulateSearchTree(binaryCsp);
        AddSafetyCheckAtRootLevel();
        if (SearchState == SearchState.Safe)
        {
            StepForwards(orderingStrategy);
        }
        else
        {
            SearchState = SearchState.Final;
        }
    }

    public void Visit(IOrderingStrategy orderingStrategy)
    {
        SearchState = SearchState.Unsafe;
        N presentNode = GetPresentNode();

        while (SearchState == SearchState.Unsafe && presentNode.RemainingCandidates > 0)
        {
            presentNode.AssignNextCandidate();
            AddSafetyCheck();

            if (SearchState == SearchState.Safe)
            {
                continue;
            }

            UndoSafetyCheckPropagation();
            presentNode.RejectAssignment();
        }

        if (SearchState == SearchState.Safe)
        {
            StepForwards(orderingStrategy);
        }
    }

    public void Backtrack()
    {
        N presentNode = GetPresentNode();
        var backtrackLevel = presentNode.BacktrackLevel;
        presentNode.RestoreRejectedCandidates();
        SetupForBacktrack(in backtrackLevel);
        BacktrackToLevel(in backtrackLevel);
    }

    public Assignment<V, D>[] GetAssignments()
    {
        Assignment<V, D>[] pastAssignments = new Assignment<V, D>[Math.Max(0, SearchLevel)];
        for (var i = 0; i < SearchLevel; i++)
        {
            pastAssignments[i] = SearchTree[i].Map();
        }

        return pastAssignments;
    }

    public Assignment<V, D> GetLatestAssignment() => SearchTree[SearchLevel - 1].Map();

    public void Reset()
    {
        SearchTree.Clear();
        SearchLevel = RootLevel;
        SearchState = SearchState.Initial;
    }

    public int EnsureCapacity(int capacity) => SearchTree.EnsureCapacity(capacity);

    public void TrimExcess(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Value must not be negative.");
        }

        SearchTree.Capacity = Math.Max(SearchTree.Count, capacity);
    }

    protected N GetPresentNode() => SearchTree[SearchLevel];

    protected abstract void PopulateSearchTree(ISolvableBinaryCsp<V, D> binaryCsp);

    protected abstract void AddSafetyCheckAtRootLevel();

    protected abstract void AddSafetyCheck();

    protected abstract void UndoSafetyCheckPropagation();

    protected abstract void SetupForVisit();

    protected abstract void SetupForBacktrack(in int backtrackLevel);

    protected void CheckAllNodesForEmptyDomains()
    {
        SearchState = SearchTree.All(node => node.RemainingCandidates > 0) ? SearchState.Safe : SearchState.Unsafe;
    }

    private void BacktrackToLevel(in int backtrackLevel)
    {
        N presentNode;
        StepBackwards();

        while (SearchLevel > backtrackLevel)
        {
            presentNode = GetPresentNode();
            presentNode.RejectAssignment();
            UndoSafetyCheckPropagation();
            presentNode.RestoreRejectedCandidates();
            StepBackwards();
        }

        if (SearchLevel > RootLevel)
        {
            presentNode = GetPresentNode();
            presentNode.RejectAssignment();
            UndoSafetyCheckPropagation();
            SearchState = presentNode.RemainingCandidates > 0 ? SearchState.Safe : SearchState.Unsafe;
        }
        else
        {
            SearchState = SearchState.Final;
        }
    }

    private void StepForwards(IOrderingStrategy orderingStrategy)
    {
        SearchLevel++;
        if (SearchLevel >= SearchTreeLeafLevel)
        {
            SearchState = SearchState.Final;
        }
        else
        {
            SearchTree.ReorderNodes(orderingStrategy, SearchLevel);
            SetupForVisit();
        }
    }

    private void StepBackwards()
    {
        SearchLevel--;
        if (SearchLevel <= RootLevel)
        {
            SearchState = SearchState.Final;
        }
    }
}
