using Kolyteon.Modelling;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class AdjacentMethod
    {
        [Theory]
        [InlineData(0, 1, false)]
        [InlineData(0, 2, true)]
        [InlineData(1, 2, true)]
        [InlineData(1, 0, false)]
        [InlineData(2, 0, true)]
        [InlineData(2, 1, true)]
        public void Adjacent_GivenIndexesOfDifferentVariables_ReturnsTrueIfAdjacentOrFalseIfNotAdjacent(int indexA,
            int indexB,
            bool expected)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2], [B] = [3, 4], [C] = [1, 2, 3, 4]
            });

            // Act
            bool result = sut.Adjacent(indexA, indexB);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void Adjacent_GivenIndexesOfSameVariable_ReturnsTrue(int indexA, int indexB)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2], [B] = [3, 4], [C] = [1, 2, 3, 4]
            });

            // Act
            bool result = sut.Adjacent(indexA, indexB);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void Adjacent_IndexAArgIsNegativeOrEqualToOrGreaterThanNumberOfVariables_Throws(int indexA)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.Adjacent(indexA, arbitraryIndex);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void Adjacent_IndexBArgIsNegativeOrEqualToOrGreaterThanNumberOfVariables_Throws(int indexB)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.Adjacent(arbitraryIndex, indexB);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }

        [Fact]
        public void Adjacent_NotModellingAProblem_Throws()
        {
            // Arrange
            TestConstraintGraph sut = new();

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.Adjacent(arbitraryIndex, arbitraryIndex);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }
    }
}
