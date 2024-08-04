using Kolyteon.Modelling;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class GetSumTightnessAtMethod
    {
        [Theory]
        [InlineData(0, 0.6)]
        [InlineData(1, 0.0)]
        [InlineData(2, 0.2)]
        [InlineData(3, 0.5)]
        [InlineData(4, 0.7)]
        public void GetSumTightnessAt_GivenIndex_ReturnsSumTightnessOfVariableAtIndex(int index, double expectedSumTightness)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2], [B] = [], [C] = [0, 5], [D] = [0, 1, 5, 8, 9], [E] = [1]
            });

            // Act
            double result = sut.GetSumTightnessAt(index);

            // Assert
            result.Should().BeApproximately(expectedSumTightness, Constants.Precision.SixDecimalPlaces);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetSumTightnessAt_IndexArgIsNegativeOrEqualToOrGreaterThanNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            // Act
            Action act = () => sut.GetSumTightnessAt(index);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }

        [Fact]
        public void GetSumTightnessAt_NotModellingAProblem_Throws()
        {
            // Arrange
            TestConstraintGraph sut = new();

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.GetSumTightnessAt(arbitraryIndex);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }
    }
}
