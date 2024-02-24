using Mjt85.Kolyteon.Solving.SearchStrategies;
using Mjt85.Kolyteon.Solving.SearchStrategies.LookBack;

namespace Mjt85.Kolyteon.Solving.SearchTrees;

internal sealed class SearchStrategyFactory<V, D> : ISearchStrategyFactory<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public ISearchStrategy<V, D> CreateInstance(Search strategy, int capacity)
    {
        return strategy switch
        {
            Search.ConflictDirectedBackjumping => new CBJStrategy<V, D>(capacity),
            Search.GraphBasedBackjumping => new GBJStrategy<V, D>(capacity),
            Search.Backjumping => new BJStrategy<V, D>(capacity),
            _ => new BTStrategy<V, D>(capacity)
        };
    }
}
