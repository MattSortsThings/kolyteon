namespace Mjt85.Kolyteon.Solving.OrderingStrategies;

internal interface IOrderingStrategyFactory
{
    public IOrderingStrategy CreateInstance(Ordering strategy);
}
