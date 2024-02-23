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
            Queen Q0 = new(0, 2);
            Queen Q1 = new(1, 0);
            Queen Q2 = new(2, 3);
            Queen Q3 = new(3, 1);

            IEnumerable<Assignment<int, Queen>> sut =
            [
                new Assignment<int, Queen>(0, Q0),
                new Assignment<int, Queen>(1, Q1),
                new Assignment<int, Queen>(2, Q2),
                new Assignment<int, Queen>(3, Q3)
            ];

            // Act
            IReadOnlyList<Queen> result = sut.ToPuzzleSolution();

            // Assert
            result.Should().Equal(Q0, Q1, Q2, Q3);
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
