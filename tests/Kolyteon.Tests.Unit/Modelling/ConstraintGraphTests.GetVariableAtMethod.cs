using Kolyteon.Modelling;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class GetVariableAtMethod
    {
        [Theory]
        [InlineData(0, A)]
        [InlineData(1, B)]
        [InlineData(2, C)]
        [InlineData(3, D)]
        [InlineData(4, E)]
        public void GetVariableAt_GivenIndex_ReturnsVariableAtIndex(int index, char expectedVariable)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2], [B] = [], [C] = [0, 5], [D] = [0, 1, 5, 9], [E] = [1]
            });

            // Act
            char result = sut.GetVariableAt(index);

            // Assert
            result.Should().Be(expectedVariable);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetVariableAt_IndexArgIsNegativeOrEqualToOrGreaterThanNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            // Act
            Action act = () => sut.GetVariableAt(index);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }

        [Fact]
        public void GetVariableAt_NotModellingAProblem_Throws()
        {
            // Arrange
            TestConstraintGraph sut = new();

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.GetVariableAt(arbitraryIndex);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }
    }
}
