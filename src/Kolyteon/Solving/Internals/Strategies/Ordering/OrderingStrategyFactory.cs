namespace Kolyteon.Solving.Internals.Strategies.Ordering;

internal sealed class OrderingStrategyFactory : IOrderingStrategyFactory
{
    public IOrderingStrategy Create(OrderingStrategy strategy)
    {
        if (strategy == OrderingStrategy.NaturalOrdering)
        {
            return new NoStrategy();
        }

        if (strategy == OrderingStrategy.BrelazHeuristic)
        {
            return new BzStrategy();
        }

        if (strategy == OrderingStrategy.MaxCardinality)
        {
            return new McStrategy();
        }

        if (strategy == OrderingStrategy.MaxTightness)
        {
            return new MtStrategy();
        }

        throw new ArgumentException($"No implementation exists for Ordering Strategy value '{strategy}'.");
    }
}
