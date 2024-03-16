using Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookAhead;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookBack;

namespace Mjt85.Kolyteon.Solving.Internals.SearchStrategies;

internal sealed class SearchStrategyFactory<V, D> : ISearchStrategyFactory<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public ISearchStrategy<V, D> CreateInstance(Search strategy, int capacity)
    {
        return strategy switch
        {
            Search.MaintainingArcConsistency => new MACStrategy<V, D>(capacity),
            Search.FullLookingAhead => new FLAStrategy<V, D>(capacity),
            Search.PartialLookingAhead => new PLAStrategy<V, D>(capacity),
            Search.ForwardChecking => new FCStrategy<V, D>(capacity),
            Search.ConflictDirectedBackjumping => new CBJStrategy<V, D>(capacity),
            Search.GraphBasedBackjumping => new GBJStrategy<V, D>(capacity),
            Search.Backjumping => new BJStrategy<V, D>(capacity),
            _ => new BTStrategy<V, D>(capacity)
        };
    }
}
