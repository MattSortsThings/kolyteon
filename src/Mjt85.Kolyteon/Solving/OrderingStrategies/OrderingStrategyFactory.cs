namespace Mjt85.Kolyteon.Solving.OrderingStrategies;

internal sealed class OrderingStrategyFactory : IOrderingStrategyFactory
{
    public IOrderingStrategy CreateInstance(Ordering strategy) => new NOStrategy();
}
