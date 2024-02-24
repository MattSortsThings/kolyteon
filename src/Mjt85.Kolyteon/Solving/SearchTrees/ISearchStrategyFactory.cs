using Mjt85.Kolyteon.Solving.SearchStrategies;

namespace Mjt85.Kolyteon.Solving.SearchTrees;

internal interface ISearchStrategyFactory<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public ISearchStrategy<V, D> CreateInstance(Search strategy, int capacity);
}
