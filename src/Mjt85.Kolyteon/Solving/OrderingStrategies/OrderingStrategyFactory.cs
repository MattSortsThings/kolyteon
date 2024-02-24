namespace Mjt85.Kolyteon.Solving.OrderingStrategies;

internal sealed class OrderingStrategyFactory : IOrderingStrategyFactory
{
    public IOrderingStrategy CreateInstance(Ordering strategy)
    {
        return strategy switch
        {
            Ordering.Brelaz => new BZStrategy(),
            _ => new NOStrategy()
        };
    }
}
