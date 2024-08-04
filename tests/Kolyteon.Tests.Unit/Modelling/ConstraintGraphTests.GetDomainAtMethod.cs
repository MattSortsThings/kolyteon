using Kolyteon.Modelling;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class GetDomainAtMethod
    {
        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(1)]
        [InlineData(2, 0, 5)]
        [InlineData(3, 0, 1, 5, 8, 9)]
        [InlineData(4, 1)]
        public void GetDomainAt_GivenIndex_ReturnsDomainOfVariableAtIndex(int index, params int[] expectedDomain)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2], [B] = [], [C] = [0, 5], [D] = [0, 1, 5, 8, 9], [E] = [1]
            });

            // Act
            IReadOnlyList<int> result = sut.GetDomainAt(index);

            // Assert
            result.Should().Equal(expectedDomain);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void GetDomainAt_IndexArgIsNegativeOrEqualToOrGreaterThanNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            // Act
            Action act = () => sut.GetDomainAt(index);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }

        [Fact]
        public void GetDomainAt_NotModellingAProblem_Throws()
        {
            // Arrange
            TestConstraintGraph sut = new();

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.GetDomainAt(arbitraryIndex);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }
    }
}
