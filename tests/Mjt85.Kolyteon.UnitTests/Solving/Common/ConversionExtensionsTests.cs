using Mjt85.Kolyteon.Solving.Common;

namespace Mjt85.Kolyteon.UnitTests.Solving.Common;

/// <summary>
///     Unit tests for extension methods to convert <see cref="Search" /> and <see cref="Ordering" /> enum values to and
///     from their string short codes.
/// </summary>
public sealed class ConversionExtensionsTests
{
    [UnitTest]
    public sealed class Search_ToShortCode_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ConvertsSearchEnumValueToCorrectShortCode(Search sut, string expected)
        {
            // Act
            var result = sut.ToShortCode();

            // Assert
            result.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<Search, string>
        {
            public TestCases()
            {
                Add(Search.Backtracking, "BT");
                Add(Search.Backjumping, "BJ");
                Add(Search.GraphBasedBackjumping, "GBJ");
                Add(Search.ConflictDirectedBackjumping, "CBJ");
                Add(Search.ForwardChecking, "FC");
                Add(Search.PartialLookingAhead, "PLA");
                Add(Search.FullLookingAhead, "FLA");
                Add(Search.MaintainingArcConsistency, "MAC");
            }
        }
    }

    [UnitTest]
    public sealed class Ordering_ToShortCode_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ConvertsOrderingEnumValueToCorrectShortCode(Ordering sut, string expected)
        {
            // Act
            var result = sut.ToShortCode();

            // Assert
            result.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<Ordering, string>
        {
            public TestCases()
            {
                Add(Ordering.None, "NO");
                Add(Ordering.Brelaz, "BZ");
                Add(Ordering.MaxCardinality, "MC");
                Add(Ordering.MaxTightness, "MT");
            }
        }
    }
}
