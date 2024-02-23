using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.UnitTests.MapColouring;

/// <summary>
///     Unit tests for the <see cref="ConversionExtensions" /> extension method class.
/// </summary>
public sealed class ConversionExtensionsTests
{
    [UnitTest]
    public sealed class ToPuzzleSolution_Method
    {
        [Fact]
        public void InstanceIsNonEmptyEnumerable_ReturnsDictionaryOfKeyValuePairs()
        {
            // Arrange
            Region R0 = Region.FromId("R0");
            Region R1 = Region.FromId("R1");
            Region R2 = Region.FromId("R2");

            IEnumerable<Assignment<Region, Colour>> sut =
            [
                new Assignment<Region, Colour>(R0, Colour.Cyan),
                new Assignment<Region, Colour>(R1, Colour.Magenta),
                new Assignment<Region, Colour>(R2, Colour.Yellow)
            ];

            // Act
            IReadOnlyDictionary<Region, Colour> result = sut.ToPuzzleSolution();

            // Assert
            result.Should().BeEquivalentTo(new Dictionary<Region, Colour>
            {
                [R0] = Colour.Cyan,
                [R1] = Colour.Magenta,
                [R2] = Colour.Yellow
            }, options => options.WithoutStrictOrdering());
        }

        [Fact]
        public void InstanceIsEmptyEnumerable_ReturnsEmptyDictionary()
        {
            // Arrange
            IEnumerable<Assignment<Region, Colour>> sut = Array.Empty<Assignment<Region, Colour>>();

            // Act
            IReadOnlyDictionary<Region, Colour> result = sut.ToPuzzleSolution();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
