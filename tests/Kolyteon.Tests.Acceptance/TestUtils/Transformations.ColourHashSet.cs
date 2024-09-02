using Kolyteon.Common;
using Reqnroll;

namespace Kolyteon.Tests.Acceptance.TestUtils;

internal static partial class Transformations
{
    [Binding]
    internal static class ColourHashSet
    {
        [StepArgumentTransformation]
        internal static HashSet<Colour> ToColourHashSet(string text) =>
            text.Split(", ").Select(Colour.FromName).ToHashSet();
    }
}
