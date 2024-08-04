using Kolyteon.Modelling;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class GetDegreeAtMethod
    {
        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 3)]
        [InlineData(4, 2)]
        public void GetDegreeAt_GivenIndex_ReturnsDegreeOfVariableAtIndex(int index, int expectedDegree)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2], [B] = [], [C] = [0, 5], [D] = [0, 1, 5, 8, 9], [E] = [1]
            });

            // Act
            int result = sut.GetDegreeAt(index);

            // Assert
            result.Should().Be(expectedDegree);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetDegreeAt_IndexArgIsNegativeOrEqualToOrGreaterThanNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            // Act
            Action act = () => sut.GetDegreeAt(index);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }

        [Fact]
        public void GetDegreeAt_NotModellingAProblem_Throws()
        {
            // Arrange
            TestConstraintGraph sut = new();

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.GetDegreeAt(arbitraryIndex);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }
    }
}
