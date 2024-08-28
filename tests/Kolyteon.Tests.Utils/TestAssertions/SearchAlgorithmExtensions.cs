using Kolyteon.Solving;

namespace Kolyteon.Tests.Utils.TestAssertions;

public static class SearchAlgorithmExtensions
{
    public static SearchAlgorithmAssertions Should(this SearchAlgorithm subject) => new(subject);
}
