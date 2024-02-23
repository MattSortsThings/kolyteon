using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Sudoku;

namespace Mjt85.Kolyteon.UnitTests.Sudoku;

/// <summary>
///     Unit tests for the <see cref="ConversionExtensions" /> extension method class.
/// </summary>
public sealed class ConversionExtensionsTests
{
    [UnitTest]
    public sealed class ToPuzzleSolution_Method
    {
        [Fact]
        public void InstanceIsNonEmptyEnumerable_ReturnsListOfFilledCells()
        {
            // Arrange
            IEnumerable<Assignment<EmptyCell, int>> sut =
            [
                new Assignment<EmptyCell, int>(new EmptyCell(0, 0), 1),
                new Assignment<EmptyCell, int>(new EmptyCell(0, 5), 2),
                new Assignment<EmptyCell, int>(new EmptyCell(7, 3), 8)
            ];

            // Act
            IReadOnlyList<FilledCell> result = sut.ToPuzzleSolution();

            // Assert
            result.Should().Equal(new FilledCell(0, 0, 1),
                new FilledCell(0, 5, 2),
                new FilledCell(7, 3, 8));
        }

        [Fact]
        public void InstanceIsEmptyEnumerable_ReturnsEmptyList()
        {
            // Arrange
            IEnumerable<Assignment<EmptyCell, int>> sut = Array.Empty<Assignment<EmptyCell, int>>();

            // Act
            IReadOnlyList<FilledCell> result = sut.ToPuzzleSolution();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
