using Reqnroll;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class Transformations
{
    [Binding]
    internal static class CheckingStrategy
    {
        [StepArgumentTransformation]
        internal static Solving.CheckingStrategy ToCheckingStrategy(string code) => Solving.CheckingStrategy.FromCode(code);
    }
}
