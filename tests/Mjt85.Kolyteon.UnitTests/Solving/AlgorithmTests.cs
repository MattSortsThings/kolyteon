using Mjt85.Kolyteon.Solving;

namespace Mjt85.Kolyteon.UnitTests.Solving;

public sealed class AlgorithmTests
{
    [UnitTest]
    public sealed class ToShortCode_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsStrategyShortCodesConcatenatedWithPlusSign(Search search, Ordering ordering, string expected)
        {
            // Arrange
            Algorithm sut = new(search, ordering);

            // Act
            var result = sut.ToShortCode();

            // Assert
            result.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<Search, Ordering, string>
        {
            public TestCases()
            {
                Add(Search.Backtracking, Ordering.None, "BT+NO");
                Add(Search.Backjumping, Ordering.Brelaz, "BJ+BZ");
                Add(Search.ForwardChecking, Ordering.MaxCardinality, "FC+MC");
                Add(Search.MaintainingArcConsistency, Ordering.MaxTightness, "MAC+MT");
            }
        }
    }
}
