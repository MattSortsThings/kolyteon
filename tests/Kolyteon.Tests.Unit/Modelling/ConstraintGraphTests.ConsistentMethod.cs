using Kolyteon.Modelling;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class ConsistentMethod
    {
        [Theory]
        [InlineData(0, 0, 1, 0, false)]
        [InlineData(0, 0, 1, 1, true)]
        [InlineData(0, 1, 1, 0, true)]
        [InlineData(0, 1, 1, 1, false)]
        [InlineData(1, 0, 0, 0, false)]
        [InlineData(1, 1, 0, 0, true)]
        [InlineData(1, 0, 0, 1, true)]
        [InlineData(1, 1, 0, 1, false)]
        public void Consistent_GivenAssignmentsForAdjacentVariables_ReturnsTrueIfConsistentElseFalse(int variableIndexA,
            int domainValueIndexA,
            int variableIndexB,
            int domainValueIndexB,
            bool expected)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1, 2], [B] = [1, 2] });

            IAssignment assignmentA = new FakeAssignment
            {
                VariableIndex = variableIndexA, DomainValueIndex = domainValueIndexA
            };

            IAssignment assignmentB = new FakeAssignment
            {
                VariableIndex = variableIndexB, DomainValueIndex = domainValueIndexB
            };

            // Act
            bool result = sut.Consistent(assignmentA, assignmentB);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 0, 1, 0)]
        [InlineData(0, 0, 1, 1)]
        [InlineData(0, 1, 1, 0)]
        [InlineData(0, 1, 1, 1)]
        [InlineData(1, 0, 0, 0)]
        [InlineData(1, 1, 0, 0)]
        [InlineData(1, 0, 0, 1)]
        [InlineData(1, 1, 0, 1)]
        public void Consistent_GivenAssignmentsForNonAdjacentVariables_ReturnsTrue(int variableIndexA,
            int domainValueIndexA,
            int variableIndexB,
            int domainValueIndexB)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1, 2], [B] = [8, 9] });

            IAssignment assignmentA = new FakeAssignment
            {
                VariableIndex = variableIndexA, DomainValueIndex = domainValueIndexA
            };

            IAssignment assignmentB = new FakeAssignment
            {
                VariableIndex = variableIndexB, DomainValueIndex = domainValueIndexB
            };

            // Act
            bool result = sut.Consistent(assignmentA, assignmentB);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(0, 1, false)]
        [InlineData(1, 0, false)]
        [InlineData(1, 1, true)]
        public void Consistent_GivenAssignmentsForSameVariable_ReturnsTrueIfSameDomainValueElseFalse(int domainValueIndexA,
            int domainValueIndexB, bool expected)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1, 2] });

            const int sharedVariableIndex = 0;

            IAssignment assignmentA = new FakeAssignment
            {
                VariableIndex = sharedVariableIndex, DomainValueIndex = domainValueIndexA
            };

            IAssignment assignmentB = new FakeAssignment
            {
                VariableIndex = sharedVariableIndex, DomainValueIndex = domainValueIndexB
            };

            // Act
            bool result = sut.Consistent(assignmentA, assignmentB);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void Consistent_AssignmentAVariableIndexIsNegativeOrEqualToOrGreaterThanNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            IAssignment arbitraryAssignment = new FakeAssignment { VariableIndex = 0, DomainValueIndex = 0 };

            IAssignment illegalAssignment = new FakeAssignment { VariableIndex = index, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Consistent(illegalAssignment, arbitraryAssignment);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void Consistent_AssignmentBVariableIndexIsNegativeOrEqualToOrGreaterThanNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            IAssignment arbitraryAssignment = new FakeAssignment { VariableIndex = 0, DomainValueIndex = 0 };

            IAssignment illegalAssignment = new FakeAssignment { VariableIndex = index, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Consistent(arbitraryAssignment, illegalAssignment);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void Consistent_AssignmentADomainValueIndexIsNegativeOrEqualToOrGreaterThanDomainSizeOfVariable_Throws(int index)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            IAssignment arbitraryAssignment = new FakeAssignment { VariableIndex = 0, DomainValueIndex = 0 };

            IAssignment illegalAssignment = new FakeAssignment { VariableIndex = 0, DomainValueIndex = index };

            // Act
            Action act = () => sut.Consistent(illegalAssignment, arbitraryAssignment);

            // Assert
            act.Should().Throw<DomainValueIndexOutOfRangeException>()
                .WithMessage("Domain value index must be non-negative " +
                             "and less than the number of values in the binary CSP variable's domain.");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(2)]
        public void Consistent_AssignmentBDomainValueIndexIsNegativeOrEqualToOrGreaterThanDomainSizeOfVariable_Throws(int index)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            IAssignment arbitraryAssignment = new FakeAssignment { VariableIndex = 0, DomainValueIndex = 0 };

            IAssignment illegalAssignment = new FakeAssignment { VariableIndex = 0, DomainValueIndex = index };

            // Act
            Action act = () => sut.Consistent(arbitraryAssignment, illegalAssignment);

            // Assert
            act.Should().Throw<DomainValueIndexOutOfRangeException>()
                .WithMessage("Domain value index must be non-negative " +
                             "and less than the number of values in the binary CSP variable's domain.");
        }

        [Fact]
        public void Consistent_AssignmentAArgIsnull_Throws()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            IAssignment arbitraryAssignment = new FakeAssignment();

            // Act
            Action act = () => sut.Consistent(null!, arbitraryAssignment);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'assignmentA')");
        }

        [Fact]
        public void Consistent_AssignmentBArgIsnull_Throws()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            IAssignment arbitraryAssignment = new FakeAssignment();

            // Act
            Action act = () => sut.Consistent(arbitraryAssignment, null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'assignmentB')");
        }

        [Fact]
        public void Consistent_NotModellingAProblem_Throws()
        {
            // Arrange
            TestConstraintGraph sut = new();

            IAssignment arbitraryAssignment = new FakeAssignment();

            // Act
            Action act = () => sut.Consistent(arbitraryAssignment, arbitraryAssignment);

            // Assert
            act.Should().Throw<VariableIndexOutOfRangeException>()
                .WithMessage("Variable index must be non-negative and less than the number of binary CSP variables.");
        }
    }
}
