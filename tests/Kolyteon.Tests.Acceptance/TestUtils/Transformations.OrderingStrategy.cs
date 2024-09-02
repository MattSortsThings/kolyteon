using Reqnroll;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class Transformations
{
    [Binding]
    internal static class OrderingStrategy
    {
        [StepArgumentTransformation]
        internal static Solving.OrderingStrategy ToOrderingStrategy(string code) => Solving.OrderingStrategy.FromCode(code);
    }
}
