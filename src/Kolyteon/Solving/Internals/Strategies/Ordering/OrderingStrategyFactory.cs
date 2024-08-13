namespace Kolyteon.Solving.Internals.Strategies.Ordering;

internal sealed class OrderingStrategyFactory : IOrderingStrategyFactory
{
    private static readonly Dictionary<OrderingStrategy, Func<IOrderingStrategy>> OrderingStrategyInitializerLookup = new()
    {
        [OrderingStrategy.NaturalOrdering] = () => new NoStrategy(),
        [OrderingStrategy.BrelazHeuristic] = () => new BzStrategy(),
        [OrderingStrategy.MaxCardinality] = () => new McStrategy(),
        [OrderingStrategy.MaxTightness] = () => new MtStrategy()
    };

    public IOrderingStrategy Create(OrderingStrategy strategy) => OrderingStrategyInitializerLookup[strategy].Invoke();
}
