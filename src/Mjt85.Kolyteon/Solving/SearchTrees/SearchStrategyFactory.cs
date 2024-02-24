using Mjt85.Kolyteon.Solving.SearchStrategies;
using Mjt85.Kolyteon.Solving.SearchStrategies.LookBack;

namespace Mjt85.Kolyteon.Solving.SearchTrees;

internal sealed class SearchStrategyFactory<V, D> : ISearchStrategyFactory<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public ISearchStrategy<V, D> CreateInstance(Search strategy, int capacity) => new BTStrategy<V, D>(capacity);
}
