using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.NQueens;
using Kolyteon.Tests.Utils.TestAssertions;

namespace Kolyteon.Tests.Unit.NQueens;

public static class NQueensProblemTests
{
    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(5);

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualQueensValues_ReturnsTrue()
        {
            // Arrange
            const int sharedN = 5;

            NQueensProblem sut = NQueensProblem.FromN(sharedN);
            NQueensProblem other = NQueensProblem.FromN(sharedN);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalQueensValues_ReturnsFalse()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(1);
            NQueensProblem other = NQueensProblem.FromN(99);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(5);

            // Act
            bool result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class VerifyCorrectMethod
    {
        public static TheoryData<NQueensProblem, IReadOnlyList<Square>> PositiveTestCases => new()
        {
            {
                NQueensProblem.FromN(1), [
                    Square.FromColumnAndRow(0, 0)
                ]
            },
            {
                NQueensProblem.FromN(4), [
                    Square.FromColumnAndRow(0, 1),
                    Square.FromColumnAndRow(1, 3),
                    Square.FromColumnAndRow(2, 0),
                    Square.FromColumnAndRow(3, 2)
                ]
            },
            {
                NQueensProblem.FromN(5), [
                    Square.FromColumnAndRow(0, 4),
                    Square.FromColumnAndRow(1, 1),
                    Square.FromColumnAndRow(2, 3),
                    Square.FromColumnAndRow(3, 0),
                    Square.FromColumnAndRow(4, 2)
                ]
            },
            {
                NQueensProblem.FromN(5), [
                    Square.FromColumnAndRow(0, 2),
                    Square.FromColumnAndRow(1, 0),
                    Square.FromColumnAndRow(2, 3),
                    Square.FromColumnAndRow(3, 1),
                    Square.FromColumnAndRow(4, 4)
                ]
            },
            {
                NQueensProblem.FromN(5), [
                    Square.FromColumnAndRow(0, 0),
                    Square.FromColumnAndRow(1, 3),
                    Square.FromColumnAndRow(2, 1),
                    Square.FromColumnAndRow(3, 4),
                    Square.FromColumnAndRow(4, 2)
                ]
            },
            {
                NQueensProblem.FromN(5), [
                    Square.FromColumnAndRow(0, 0),
                    Square.FromColumnAndRow(1, 2),
                    Square.FromColumnAndRow(2, 4),
                    Square.FromColumnAndRow(3, 1),
                    Square.FromColumnAndRow(4, 3)
                ]
            },
            {
                NQueensProblem.FromN(5), [
                    Square.FromColumnAndRow(0, 2),
                    Square.FromColumnAndRow(1, 4),
                    Square.FromColumnAndRow(2, 1),
                    Square.FromColumnAndRow(3, 3),
                    Square.FromColumnAndRow(4, 0)
                ]
            },
            {
                NQueensProblem.FromN(5), [
                    Square.FromColumnAndRow(0, 1),
                    Square.FromColumnAndRow(1, 3),
                    Square.FromColumnAndRow(2, 0),
                    Square.FromColumnAndRow(3, 2),
                    Square.FromColumnAndRow(4, 4)
                ]
            },
            {
                NQueensProblem.FromN(5), [
                    Square.FromColumnAndRow(0, 4),
                    Square.FromColumnAndRow(1, 2),
                    Square.FromColumnAndRow(2, 0),
                    Square.FromColumnAndRow(3, 3),
                    Square.FromColumnAndRow(4, 1)
                ]
            },
            {
                NQueensProblem.FromN(5), [
                    Square.FromColumnAndRow(0, 3),
                    Square.FromColumnAndRow(1, 1),
                    Square.FromColumnAndRow(2, 4),
                    Square.FromColumnAndRow(3, 2),
                    Square.FromColumnAndRow(4, 0)
                ]
            },
            {
                NQueensProblem.FromN(5), [
                    Square.FromColumnAndRow(0, 3),
                    Square.FromColumnAndRow(1, 0),
                    Square.FromColumnAndRow(2, 2),
                    Square.FromColumnAndRow(3, 4),
                    Square.FromColumnAndRow(4, 1)
                ]
            },
            {
                NQueensProblem.FromN(5), [
                    Square.FromColumnAndRow(0, 1),
                    Square.FromColumnAndRow(1, 4),
                    Square.FromColumnAndRow(2, 2),
                    Square.FromColumnAndRow(3, 0),
                    Square.FromColumnAndRow(4, 3)
                ]
            }
        };

        [Theory]
        [MemberData(nameof(PositiveTestCases), MemberType = typeof(VerifyCorrectMethod))]
        public void VerifyCorrect_GivenCorrectSolution_ReturnsSuccessfulResult(NQueensProblem sut,
            IReadOnlyList<Square> solution)
        {
            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeSuccessful().And.HaveNullFirstError();
        }

        [Fact]
        public void VerifyCorrect_SolutionIsEmptyList_ReturnsUnsuccessfulResult()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(1);

            IReadOnlyList<Square> solution = [];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Solution has 0 squares, but problem has 1 queen.");
        }

        [Fact]
        public void VerifyCorrect_SolutionHasTooFewItems_ReturnsUnsuccessfulResult()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(2);

            IReadOnlyList<Square> solution = [Square.FromColumnAndRow(0, 0)];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Solution has 1 square, but problem has 2 queens.");
        }

        [Fact]
        public void VerifyCorrect_SolutionHasTooManyItems_ReturnsUnsuccessfulResult()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(2);

            IReadOnlyList<Square> solution =
            [
                Square.FromColumnAndRow(0, 0),
                Square.FromColumnAndRow(0, 1),
                Square.FromColumnAndRow(0, 2)
            ];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Solution has 3 squares, but problem has 2 queens.");
        }

        [Fact]
        public void VerifyCorrect_SquareOutsideChessBoard_ReturnsUnsuccessfulResult()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(2);

            IReadOnlyList<Square> solution =
            [
                Square.FromColumnAndRow(0, 0),
                Square.FromColumnAndRow(1, 2)
            ];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Square (1,2) is not inside chess board (0,0) [2x2].");
        }

        [Fact]
        public void VerifyCorrect_DuplicateSquares_ReturnsUnsuccessfulResult()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(3);

            IReadOnlyList<Square> solution =
            [
                Square.FromColumnAndRow(0, 0),
                Square.FromColumnAndRow(1, 2),
                Square.FromColumnAndRow(0, 0)
            ];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Square (0,0) occurs more than once.");
        }

        [Fact]
        public void VerifyCorrect_CapturingSquares_ReturnsUnsuccessfulResult()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(4);

            IReadOnlyList<Square> solution =
            [
                Square.FromColumnAndRow(0, 0),
                Square.FromColumnAndRow(1, 2),
                Square.FromColumnAndRow(2, 3),
                Square.FromColumnAndRow(3, 0)
            ];

            // Act
            Result result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Squares (1,2) and (2,3) capture each other.");
        }

        [Fact]
        public void VerifyCorrect_SolutionArgIsNull_Throws()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(1);

            // Act
            Action act = () => sut.VerifyCorrect(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'solution')");
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        [Fact]
        public void Equality_InstanceAndOtherHaveEqualQueensValues_ReturnsTrue()
        {
            // Arrange
            const int sharedN = 5;

            NQueensProblem sut = NQueensProblem.FromN(sharedN);
            NQueensProblem other = NQueensProblem.FromN(sharedN);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalQueensValues_ReturnsFalse()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(1);
            NQueensProblem other = NQueensProblem.FromN(99);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class InequalityOperator
    {
        [Fact]
        public void Inequality_InstanceAndOtherHaveEqualQueensValues_ReturnsFalse()
        {
            // Arrange
            const int sharedN = 5;

            NQueensProblem sut = NQueensProblem.FromN(sharedN);
            NQueensProblem other = NQueensProblem.FromN(sharedN);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalQueensValues_ReturnsTrue()
        {
            // Arrange
            NQueensProblem sut = NQueensProblem.FromN(1);
            NQueensProblem other = NQueensProblem.FromN(99);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class FromNStaticFactoryMethod
    {
        public static TheoryData<int, Block, int> TestCases => new()
        {
            { 1, Dimensions.FromWidthAndHeight(1, 1).ToBlock(), 1 },
            { 4, Dimensions.FromWidthAndHeight(4, 4).ToBlock(), 4 },
            { 100, Dimensions.FromWidthAndHeight(100, 100).ToBlock(), 100 }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(FromNStaticFactoryMethod))]
        public void FromNMethod_GivenNValue_ReturnsInstanceWithSquareChessBoardAndNQueens(int n, Block expectedChessBoard,
            int expectedQueens)
        {
            // Act
            NQueensProblem result = NQueensProblem.FromN(n);

            // Assert
            using (new AssertionScope())
            {
                result.ChessBoard.Should().Be(expectedChessBoard);
                result.Queens.Should().Be(expectedQueens);
            }
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(100)]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(int n)
        {
            // Arrange
            NQueensProblem originalProblem = NQueensProblem.FromN(n);

            string json = JsonSerializer.Serialize(originalProblem, JsonSerializerOptions.Default);

            // Act
            NQueensProblem? deserializedProblem =
                JsonSerializer.Deserialize<NQueensProblem>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedProblem.Should().NotBeNull().And.Be(originalProblem);
        }
    }
}
