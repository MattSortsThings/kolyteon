using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;

namespace Mjt85.Kolyteon.UnitTests.NQueens;

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
            Queen Col0Row2 = new(0, 2);
            Queen Col1Row0 = new(1, 0);
            Queen Col2Row3 = new(2, 3);
            Queen Col3Row1 = new(3, 1);

            IEnumerable<Assignment<int, Queen>> sut =
            [
                new Assignment<int, Queen>(0, Col0Row2),
                new Assignment<int, Queen>(1, Col1Row0),
                new Assignment<int, Queen>(2, Col2Row3),
                new Assignment<int, Queen>(3, Col3Row1)
            ];

            // Act
            IReadOnlyList<Queen> result = sut.ToPuzzleSolution();

            // Assert
            result.Should().Equal(Col0Row2, Col1Row0, Col2Row3, Col3Row1);
        }

        [Fact]
        public void InstanceIsEmptyEnumerable_ReturnsEmptyList()
        {
            // Arrange
            IEnumerable<Assignment<int, Queen>> sut = Array.Empty<Assignment<int, Queen>>();

            // Act
            IReadOnlyList<Queen> result = sut.ToPuzzleSolution();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
