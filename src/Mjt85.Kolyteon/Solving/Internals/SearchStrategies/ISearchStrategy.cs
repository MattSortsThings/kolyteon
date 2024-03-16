using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;

namespace Mjt85.Kolyteon.Solving.Internals.SearchStrategies;

internal interface ISearchStrategy<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public Search Identifier { get; }

    public int Capacity { get; }

    public SearchState SearchState { get; }

    public int SearchLevel { get; }

    public int SearchTreeLeafLevel { get; }

    public int SearchTreeRootLevel { get; }

    public void Setup(ISolvableBinaryCsp<V, D> binaryCsp, IOrderingStrategy orderingStrategy);

    public void Visit(IOrderingStrategy orderingStrategy);

    public void Backtrack();

    public Assignment<V, D>[] GetAssignments();

    public Assignment<V, D> GetLatestAssignment();

    public void Reset();

    public int EnsureCapacity(int capacity);

    public void TrimExcess(int capacity);
}
