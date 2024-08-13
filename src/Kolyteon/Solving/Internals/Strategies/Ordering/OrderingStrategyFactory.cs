namespace Kolyteon.Solving.Internals.Strategies.Ordering;

internal sealed class OrderingStrategyFactory : IOrderingStrategyFactory
{
    public IOrderingStrategy Create(OrderingStrategy strategy)
    {
        if (strategy == OrderingStrategy.NaturalOrdering)
        {
            return new NoStrategy();
        }

        return new BzStrategy();
    }
}
