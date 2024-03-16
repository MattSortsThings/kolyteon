using Mjt85.Kolyteon.Solving.Common;

namespace Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;

internal interface IOrderingStrategyFactory
{
    public IOrderingStrategy CreateInstance(Ordering strategy);
}
