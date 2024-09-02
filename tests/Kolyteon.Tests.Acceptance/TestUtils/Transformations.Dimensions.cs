using Reqnroll;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class Transformations
{
    [Binding]
    internal static class Dimensions
    {
        [StepArgumentTransformation]
        internal static Common.Dimensions ToDimensions(string text) => Common.Dimensions.Parse(text);
    }
}
