namespace Kolyteon.Solving.Internals.Strategies.Ordering;

internal sealed class OrderingStrategyFactory : IOrderingStrategyFactory
{
    public IOrderingStrategy Create(OrderingStrategy strategy) => new NoStrategy();
}
