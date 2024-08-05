using Kolyteon.Common;

namespace Kolyteon.Shikaku.Internals;

internal static class NumberedSquareQueueExtensions
{
    internal static void Remove(this Queue<NumberedSquare> queue, in NumberedSquare target)
    {
        while (queue.TryDequeue(out NumberedSquare dequeued) && dequeued != target)
        {
            queue.Enqueue(dequeued);
        }
    }
}
