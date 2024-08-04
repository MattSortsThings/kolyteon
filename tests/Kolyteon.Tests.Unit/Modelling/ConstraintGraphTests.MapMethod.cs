using Kolyteon.Modelling;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class MapMethod
    {
        [Theory]
        [InlineData(0, 0, A, 1)]
        [InlineData(0, 1, A, 2)]
        [InlineData(1, 0, B, 0)]
        [InlineData(1, 1, B, 5)]
        [InlineData(1, 2, B, 9)]
        public void Map_GivenAssignment_ReturnsMappedAssignment(int variableIndex,
            int domainValueIndex,
            char expectedVariable,
            int expectedDomainValue)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1, 2], [B] = [0, 5, 9] });

            IAssignment assignment = new FakeAssignment { VariableIndex = variableIndex, DomainValueIndex = domainValueIndex };

            // Act
            Assignment<char, int> result = sut.Map(assignment);

            // Assert
            using (new AssertionScope())
            {
                result.Variable.Should().Be(expectedVariable);
                result.DomainValue.Should().Be(expectedDomainValue);
            }
        }

        [Fact]
        public void Map_AssignmentArgIsNull_Throws()
        {
            // Arrange
            TestConstraintGraph sut = new();

            // Act
            Action act = () => sut.Map(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'assignment')");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void Map_AssignmentArgVariableIndexIsNegativeOrEqualToOrGreaterThanNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            IAssignment assignment = new FakeAssignment { VariableIndex = index };

            // Act
            Action act = () => sut.Map(assignment);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void Map_AssignmentArgDomainValueIndexIsNegativeOrEqualToOrGreaterThanDomainSizeOfVariable_Throws(int index)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            IAssignment assignment = new FakeAssignment { VariableIndex = 0, DomainValueIndex = index };

            // Act
            Action act = () => sut.Map(assignment);

            // Assert
            act.Should().Throw<DomainValueIndexOutOfRangeException>()
                .WithMessage("Domain value index must be non-negative " +
                             "and less than the number of values in the binary CSP variable's domain.");
        }

        [Fact]
        public void Map_NotModellingAProblem_Throws()
        {
            // Arrange
            TestConstraintGraph sut = new();

            IAssignment arbitraryAssignment = new FakeAssignment();

            // Act
            Action act = () => sut.Map(arbitraryAssignment);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }
    }
}
