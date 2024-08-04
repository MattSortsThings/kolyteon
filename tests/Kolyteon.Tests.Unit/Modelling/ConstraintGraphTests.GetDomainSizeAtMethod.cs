using Kolyteon.Modelling;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class GetDomainSizeAtMethod
    {
        [Theory]
        [InlineData(0, 2)]
        [InlineData(1, 0)]
        [InlineData(2, 2)]
        [InlineData(3, 5)]
        [InlineData(4, 1)]
        public void GetDomainSizeAt_GivenIndex_ReturnsDomainSizeOfVariableAtIndex(int index, int expectedDomainSize)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2], [B] = [], [C] = [0, 5], [D] = [0, 1, 5, 8, 9], [E] = [1]
            });

            // Act
            int result = sut.GetDomainSizeAt(index);

            // Assert
            result.Should().Be(expectedDomainSize);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetDomainSizeAt_IndexArgIsNegativeOrEqualToOrGreaterThanNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            // Act
            Action act = () => sut.GetDomainSizeAt(index);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }

        [Fact]
        public void GetDomainSizeAt_NotModellingAProblem_Throws()
        {
            // Arrange
            TestConstraintGraph sut = new();

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.GetDomainSizeAt(arbitraryIndex);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }
    }
}
