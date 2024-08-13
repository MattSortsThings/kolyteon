namespace Kolyteon.Solving.Internals.Strategies.Ordering;

internal interface IOrderingStrategyFactory
{
    public IOrderingStrategy Create(OrderingStrategy strategy);
}
