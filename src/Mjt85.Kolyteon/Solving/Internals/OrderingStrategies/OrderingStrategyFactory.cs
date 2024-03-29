﻿using Mjt85.Kolyteon.Solving.Common;

namespace Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;

internal sealed class OrderingStrategyFactory : IOrderingStrategyFactory
{
    public IOrderingStrategy CreateInstance(Ordering strategy)
    {
        return strategy switch
        {
            Ordering.MaxTightness => new MTStrategy(),
            Ordering.MaxCardinality => new MCStrategy(),
            Ordering.Brelaz => new BZStrategy(),
            _ => new NOStrategy()
        };
    }
}
