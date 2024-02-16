using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Mjt85.Kolyteon.NQueens;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.NQueens;

/// <summary>
///     Unit tests for the <see cref="NQueensPuzzle" /> record type.
/// </summary>
public sealed class NQueensPuzzleTests
{
    [UnitTest]
    public sealed class Equals_Method
    {
        [Fact]
        public void InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);

            // Act
            var result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualNValues_ReturnsTrue()
        {
            // Arrange
            const int sharedN = 1;

            NQueensPuzzle sut = NQueensPuzzle.FromN(sharedN);
            NQueensPuzzle other = NQueensPuzzle.FromN(sharedN);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalNValues_ReturnsFalse()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);
            NQueensPuzzle other = NQueensPuzzle.FromN(99);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);

            // Act
            var result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ValidSolution_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ValidSolution_ReturnsSuccess(int n, IReadOnlyList<Queen> solution)
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(n);

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionHasTooFewItems_ReturnsFailure()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(2);

            IReadOnlyList<Queen> solution = [new Queen(0, 0)];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Solution size is 1, should be 2.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionHasTooManyItems_ReturnsFailure()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);

            IReadOnlyList<Queen> solution =
            [
                new Queen(0, 0),
                new Queen(1, 1),
                new Queen(2, 2)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Solution size is 3, should be 1.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void QueenOutsideChessBoard_ReturnsFailure()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(2);

            IReadOnlyList<Queen> solution =
            [
                new Queen(0, 0),
                new Queen(1, 2)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Queen (1,2) outside chess board.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void QueensCanCaptureEachOther_ReturnsFailure()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(3);

            IReadOnlyList<Queen> solution =
            [
                new Queen(0, 0),
                new Queen(1, 2),
                new Queen(2, 0)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Queens (0,0) and (2,0) can capture each other.");
        }

        [Fact]
        public void SolutionArgIsNull_Throws()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);

            // Act
            Action act = () => sut.ValidSolution(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'solution')");
        }

        private sealed class TestCases : TheoryData<int, IReadOnlyList<Queen>>
        {
            public TestCases()
            {
                Add(1,
                [
                    new Queen(0, 0)
                ]);

                Add(4,
                [
                    new Queen(0, 1),
                    new Queen(1, 3),
                    new Queen(2, 0),
                    new Queen(3, 2)
                ]);

                Add(8,
                [
                    new Queen(0, 3),
                    new Queen(1, 6),
                    new Queen(2, 2),
                    new Queen(3, 7),
                    new Queen(4, 1),
                    new Queen(5, 4),
                    new Queen(6, 0),
                    new Queen(7, 5)
                ]);

                Add(8,
                [
                    new Queen(0, 4),
                    new Queen(1, 1),
                    new Queen(2, 3),
                    new Queen(3, 6),
                    new Queen(4, 2),
                    new Queen(5, 7),
                    new Queen(6, 5),
                    new Queen(7, 0)
                ]);

                Add(8,
                [
                    new Queen(0, 3),
                    new Queen(1, 1),
                    new Queen(2, 6),
                    new Queen(3, 2),
                    new Queen(4, 5),
                    new Queen(5, 7),
                    new Queen(6, 4),
                    new Queen(7, 0)
                ]);

                Add(8,
                [
                    new Queen(0, 3),
                    new Queen(1, 5),
                    new Queen(2, 7),
                    new Queen(3, 2),
                    new Queen(4, 0),
                    new Queen(5, 6),
                    new Queen(6, 4),
                    new Queen(7, 1)
                ]);

                Add(8,
                [
                    new Queen(0, 2),
                    new Queen(1, 5),
                    new Queen(2, 7),
                    new Queen(3, 0),
                    new Queen(4, 3),
                    new Queen(5, 6),
                    new Queen(6, 4),
                    new Queen(7, 1)
                ]);
            }
        }
    }

    [UnitTest]
    public sealed class FromN_StaticMethod
    {
        [Theory]
        [InlineData(1)]
        [InlineData(8)]
        public void ReturnsInstanceWithSpecifiedNValue(int n)
        {
            // Act
            NQueensPuzzle result = NQueensPuzzle.FromN(n);

            // Assert
            result.Should().BeOfType<NQueensPuzzle>().Which.N.Should().Be(n);
        }

        [Fact]
        public void NArgIsLessThanOne_Throws()
        {
            // Act
            Action act = () => NQueensPuzzle.FromN(0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value of N must be greater than or equal to 1. (Parameter 'n')\nActual value was 0.");
        }
    }

    [UnitTest]
    public sealed class Equality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualNValues_ReturnsTrue()
        {
            // Arrange
            const int sharedN = 1;

            NQueensPuzzle sut = NQueensPuzzle.FromN(sharedN);
            NQueensPuzzle other = NQueensPuzzle.FromN(sharedN);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalNValues_ReturnsFalse()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);
            NQueensPuzzle other = NQueensPuzzle.FromN(99);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class Inequality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualNValues_ReturnsFalse()
        {
            // Arrange
            const int sharedN = 1;

            NQueensPuzzle sut = NQueensPuzzle.FromN(sharedN);
            NQueensPuzzle other = NQueensPuzzle.FromN(sharedN);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalNValues_ReturnsTrue()
        {
            // Arrange
            NQueensPuzzle sut = NQueensPuzzle.FromN(1);
            NQueensPuzzle other = NQueensPuzzle.FromN(99);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [InlineData(8)]
        [InlineData(22)]
        public void CanSerializeToJson_ThenDeserialize(int n)
        {
            // Arrange
            NQueensPuzzle original = NQueensPuzzle.FromN(n);

            JsonSerializerOptions jsonOptions = Invariants.JsonSerializerOptions();

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<NQueensPuzzle>(json, jsonOptions);

            // Assert
            deserialized.Should().NotBeNull().And.Be(original);
        }
    }
}
