namespace Mjt85.Kolyteon.Solving.OrderingStrategies;

internal sealed class OrderingStrategyFactory : IOrderingStrategyFactory
{
    public IOrderingStrategy CreateInstance(Ordering strategy)
    {
        return strategy switch
        {
            Ordering.MaxCardinality => new MCStrategy(),
            Ordering.Brelaz => new BZStrategy(),
            _ => new NOStrategy()
        };
    }
}
