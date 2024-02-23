using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Shikaku;

namespace Mjt85.Kolyteon.UnitTests.Shikaku;

/// <summary>
///     Unit tests for the <see cref="ConversionExtensions" /> extension method class.
/// </summary>
public sealed class ConversionExtensionsTests
{
    [UnitTest]
    public sealed class ToPuzzleSolution_Method
    {
        [Fact]
        public void InstanceIsNonEmptyEnumerable_ReturnsListOfDomainValues()
        {
            // Arrange
            Rectangle R02 = new(0, 0, 1, 2);
            Rectangle R03 = new(0, 2, 1, 3);
            Rectangle R20 = new(1, 0, 4, 5);

            IEnumerable<Assignment<Hint, Rectangle>> sut =
            [
                new Assignment<Hint, Rectangle>(new Hint(0, 0, 2), R02),
                new Assignment<Hint, Rectangle>(new Hint(0, 2, 3), R03),
                new Assignment<Hint, Rectangle>(new Hint(1, 0, 20), R20)
            ];

            // Act
            IReadOnlyList<Rectangle> result = sut.ToPuzzleSolution();

            // Assert
            result.Should().Equal(R02, R03, R20);
        }

        [Fact]
        public void InstanceIsEmptyEnumerable_ReturnsEmptyList()
        {
            // Arrange
            IEnumerable<Assignment<Hint, Rectangle>> sut = Array.Empty<Assignment<Hint, Rectangle>>();

            // Act
            IReadOnlyList<Rectangle> result = sut.ToPuzzleSolution();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
