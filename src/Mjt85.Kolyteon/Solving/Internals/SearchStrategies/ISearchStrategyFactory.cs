using Mjt85.Kolyteon.Solving.Common;

namespace Mjt85.Kolyteon.Solving.Internals.SearchStrategies;

internal interface ISearchStrategyFactory<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public ISearchStrategy<V, D> CreateInstance(Search strategy, int capacity);
}
