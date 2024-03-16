using System.Collections.Concurrent;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Solving.Verbose;

namespace Mjt85.Kolyteon.UnitTests.Helpers;

internal sealed class FakeProgress : IProgress<StepNotification<Region, Colour>>
{
    public ConcurrentQueue<StepNotification<Region, Colour>> Notifications { get; } = new();

    public void Report(StepNotification<Region, Colour> value) => Notifications.Enqueue(value);
}
