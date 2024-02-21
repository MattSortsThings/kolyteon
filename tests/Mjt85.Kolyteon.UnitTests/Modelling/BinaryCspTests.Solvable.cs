using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Modelling;

/// <summary>
///     Unit tests for the <see cref="BinaryCsp{P,V,D}" /> abstract base class, using the <see cref="TestBinaryCsp" />
///     derivative.
/// </summary>
public sealed partial class BinaryCspTests
{
    [UnitTest]
    public sealed class Adjacent_Method
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(1, 2)]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        public void IndexAAndIndexBArgsAreForDifferentAdjacentVariables_ReturnTrue(int indexA, int indexB)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = DigitsFromOneToFive,
                [Letter.B] = DigitsFromOneToFive,
                [Letter.C] = DigitsFromOneToFive
            });

            // Act
            var result = sut.Adjacent(indexA, indexB);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(1, 2)]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        public void IndexAAndIndexBArgsAreForDifferentNonAdjacentVariables_ReturnsFalse(int indexA, int indexB)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = [Digit.One],
                [Letter.B] = [Digit.Two],
                [Letter.C] = [Digit.Three]
            });

            // Act
            var result = sut.Adjacent(indexA, indexB);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void IndexAAndIndexBArgsAreForSameVariable_ReturnsTrue(int indexA, int indexB)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = [Digit.One],
                [Letter.B] = [Digit.Two],
                [Letter.C] = [Digit.Three]
            });

            // Act
            var result = sut.Adjacent(indexA, indexB);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IndexAArgIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.Adjacent(-1, arbitraryIndex);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public void IndexAArgIsGreaterThanOrEqualToNumberOfVariables_Throws(int indexA)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.Adjacent(indexA, arbitraryIndex);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void IndexBArgIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.Adjacent(arbitraryIndex, -1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public void IndexBArgIsGreaterThanOrEqualToNumberOfVariables_Throws(int indexB)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.Adjacent(arbitraryIndex, indexB);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void IndexNotModellingProblem_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            const int arbitraryIndex = 0;

            // Act
            Action act = () => sut.Adjacent(arbitraryIndex, arbitraryIndex);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }
    }

    [UnitTest]
    public sealed class Consistent_Method
    {
        [Theory]
        [ClassData(typeof(AdjacentTestCases))]
        public void AssignmentsAreForDifferentAdjacentVariables_ReturnsTrueIfLegal_OtherwiseFalse(TestProblem problem,
            IAssignment assignmentA,
            IAssignment assignmentB,
            bool expected)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(problem);

            // Act
            var result = sut.Consistent(assignmentA, assignmentB);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(NonAdjacentTestCases))]
        public void AssignmentsAreForDifferentNonAdjacentVariables_ReturnsTrue(TestProblem problem,
            IAssignment assignmentA,
            IAssignment assignmentB)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(problem);

            // Act
            var result = sut.Consistent(assignmentA, assignmentB);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(SameVariableTestCases))]
        public void AssignmentsAreForSameVariable_ReturnsTrueIfSameDomainValue_OtherwiseFalse(TestProblem problem,
            IAssignment assignmentA,
            IAssignment assignmentB,
            bool expected)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(problem);

            // Act
            var result = sut.Consistent(assignmentA, assignmentB);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void AssignmentAArgIsNull_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            TestAssignment dummyAssignment = new();

            // Act
            Action act = () => sut.Consistent(null!, dummyAssignment);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'assignmentA')");
        }

        [Fact]
        public void AssignmentBArgIsNull_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            TestAssignment dummyAssignment = new();

            // Act
            Action act = () => sut.Consistent(dummyAssignment, null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'assignmentB')");
        }

        [Fact]
        public void AssignmentAArgVariableIndexIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignmentA = new TestAssignment { VariableIndex = -1, DomainValueIndex = 0 };
            IAssignment assignmentB = new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Consistent(assignmentA, assignmentB);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void AssignmentAArgVariableIndexIsGreaterThanOrEqualToNumberOfVariables_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignmentA = new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 };
            IAssignment assignmentB = new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Consistent(assignmentA, assignmentB);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void AssignmentAArgDomainValueIndexIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignmentA = new TestAssignment { VariableIndex = 0, DomainValueIndex = -1 };
            IAssignment assignmentB = new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Consistent(assignmentA, assignmentB);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Domain value index was out of range. " +
                             "Must be non-negative and less than the number of values in the binary CSP variable's domain.")
                .WithInnerException<IndexOutOfRangeException>();
        }

        [Fact]
        public void AssignmentAArgDomainValueIndexIsGreaterThanOrEqualToNumberOfValuesInDomainOfVariable_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignmentA = new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 };
            IAssignment assignmentB = new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Consistent(assignmentA, assignmentB);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Domain value index was out of range. " +
                             "Must be non-negative and less than the number of values in the binary CSP variable's domain.")
                .WithInnerException<IndexOutOfRangeException>();
        }

        [Fact]
        public void AssignmentBArgVariableIndexIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignmentA = new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 };
            IAssignment assignmentB = new TestAssignment { VariableIndex = -1, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Consistent(assignmentA, assignmentB);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void AssignmentBArgVariableIndexIsGreaterThanOrEqualToNumberOfVariables_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignmentA = new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 };
            IAssignment assignmentB = new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Consistent(assignmentA, assignmentB);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void AssignmentBArgDomainValueIndexIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignmentA = new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 };
            IAssignment assignmentB = new TestAssignment { VariableIndex = 0, DomainValueIndex = -1 };

            // Act
            Action act = () => sut.Consistent(assignmentA, assignmentB);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Domain value index was out of range. " +
                             "Must be non-negative and less than the number of values in the binary CSP variable's domain.")
                .WithInnerException<IndexOutOfRangeException>();
        }

        [Fact]
        public void AssignmentBArgDomainValueIndexIsGreaterThanOrEqualToNumberOfValuesInDomainOfVariable_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignmentA = new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 };
            IAssignment assignmentB = new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 };

            // Act
            Action act = () => sut.Consistent(assignmentA, assignmentB);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Domain value index was out of range. " +
                             "Must be non-negative and less than the number of values in the binary CSP variable's domain.")
                .WithInnerException<IndexOutOfRangeException>();
        }

        [Fact]
        public void InstanceNotModellingProblem_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            IAssignment assignmentA = new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 };
            IAssignment assignmentB = new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Consistent(assignmentA, assignmentB);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        private sealed class AdjacentTestCases : TheoryData<TestProblem, IAssignment, IAssignment, bool>
        {
            public AdjacentTestCases()
            {
                TestProblem fixedProblem = new()
                {
                    [Letter.A] = [Digit.One, Digit.Two],
                    [Letter.B] = [Digit.One, Digit.Two, Digit.Three]
                };

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    true);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    true);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    true);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    true);


                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    true);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    true);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    true);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    true);
            }
        }

        private sealed class NonAdjacentTestCases : TheoryData<TestProblem, IAssignment, IAssignment>
        {
            public NonAdjacentTestCases()
            {
                TestProblem fixedProblem = new()
                {
                    [Letter.A] = [Digit.One, Digit.Two],
                    [Letter.B] = [Digit.Three, Digit.Four, Digit.Five]
                };

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 });

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 });

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 });

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 });

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 });

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 });


                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 });

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 });

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 });

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 });

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 });

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 });
            }
        }

        public sealed class SameVariableTestCases : TheoryData<TestProblem, IAssignment, IAssignment, bool>
        {
            public SameVariableTestCases()
            {
                TestProblem fixedProblem = new()
                {
                    [Letter.A] = [Digit.One, Digit.Two],
                    [Letter.B] = [Digit.One, Digit.Two, Digit.Three]
                };

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    true);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 0, DomainValueIndex = 1 },
                    true);


                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    true);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    true);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 0 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 1 },
                    false);

                Add(fixedProblem,
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    new TestAssignment { VariableIndex = 1, DomainValueIndex = 2 },
                    true);
            }
        }
    }

    [UnitTest]
    public sealed class Map_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsMappedAssignment(TestProblem problem, IAssignment assignment, Assignment<Letter, Digit> expected)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(problem);

            // Act
            Assignment<Letter, Digit> result = sut.Map(assignment);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void AssignmentArgIsNull_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            Action act = () => sut.Map(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'assignment')");
        }

        [Fact]
        public void AssignmentArgVariableIndexIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignment = new TestAssignment { VariableIndex = -1, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Map(assignment);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public void AssignmentArgVariableIndexIsGreaterThanOrEqualToNumberOfVariables_Throws(int variableIndex)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignment = new TestAssignment { VariableIndex = variableIndex, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Map(assignment);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void AssignmentArgDomainValueIndexIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignment = new TestAssignment { VariableIndex = 0, DomainValueIndex = -1 };

            // Act
            Action act = () => sut.Map(assignment);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Domain value index was out of range. " +
                             "Must be non-negative and less than the number of values in the binary CSP variable's domain.")
                .WithInnerException<IndexOutOfRangeException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public void AssignmentArgDomainValueIndexIsGreaterThanOrEqualToDomainSizeOfVariable_Throws(int domainValueIndex)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            IAssignment assignment = new TestAssignment { VariableIndex = 0, DomainValueIndex = domainValueIndex };

            // Act
            Action act = () => sut.Map(assignment);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Domain value index was out of range. " +
                             "Must be non-negative and less than the number of values in the binary CSP variable's domain.")
                .WithInnerException<IndexOutOfRangeException>();
        }

        [Fact]
        public void InstanceNotModellingProblem_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            IAssignment assignment = new TestAssignment { VariableIndex = 0, DomainValueIndex = 0 };

            // Act
            Action act = () => sut.Map(assignment);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        private sealed class TestCases : TheoryData<TestProblem, IAssignment, Assignment<Letter, Digit>>
        {
            public TestCases()
            {
                TestProblem fixedProblem = new()
                {
                    [Letter.B] = [Digit.One, Digit.Two],
                    [Letter.D] = [Digit.Two, Digit.Three, Digit.Four]
                };

                Add(fixedProblem, new TestAssignment
                {
                    VariableIndex = 0, DomainValueIndex = 0
                }, new Assignment<Letter, Digit>(Letter.B, Digit.One));

                Add(fixedProblem, new TestAssignment
                {
                    VariableIndex = 0, DomainValueIndex = 1
                }, new Assignment<Letter, Digit>(Letter.B, Digit.Two));

                Add(fixedProblem, new TestAssignment
                {
                    VariableIndex = 1, DomainValueIndex = 0
                }, new Assignment<Letter, Digit>(Letter.D, Digit.Two));

                Add(fixedProblem, new TestAssignment
                {
                    VariableIndex = 1, DomainValueIndex = 1
                }, new Assignment<Letter, Digit>(Letter.D, Digit.Three));

                Add(fixedProblem, new TestAssignment
                {
                    VariableIndex = 1, DomainValueIndex = 2
                }, new Assignment<Letter, Digit>(Letter.D, Digit.Four));
            }
        }
    }

    [UnitTest]
    public sealed class GetVariableAt_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsVariableAtIndex(TestProblem problem, int index, Letter expected)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(problem);

            // Act
            Letter result = sut.GetVariableAt(index);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void IndexArgIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            Action act = () => sut.GetVariableAt(-1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public void IndexArgIsGreaterThanOrEqualToNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            Action act = () => sut.GetVariableAt(index);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void InstanceNotModellingProblem_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            Action act = () => sut.GetVariableAt(0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        private sealed class TestCases : TheoryData<TestProblem, int, Letter>
        {
            public TestCases()
            {
                TestProblem fixedProblem = new()
                {
                    [Letter.A] = [Digit.One],
                    [Letter.B] = DigitsFromOneToFive,
                    [Letter.C] = DigitsFromOneToFive
                };

                Add(fixedProblem, 0, Letter.A);
                Add(fixedProblem, 1, Letter.B);
                Add(fixedProblem, 2, Letter.C);
            }
        }
    }

    [UnitTest]
    public sealed class GetDomainAt_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsDomainOfVariableAtIndex(TestProblem problem, int index, IReadOnlyList<Digit> expected)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(problem);

            // Act
            IReadOnlyList<Digit> result = sut.GetDomainAt(index);

            // Assert
            result.Should().Equal(expected);
        }

        [Fact]
        public void IndexArgIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            Action act = () => sut.GetDomainAt(-1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public void IndexArgIsGreaterThanOrEqualToNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            Action act = () => sut.GetDomainAt(index);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void InstanceNotModellingProblem_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            Action act = () => sut.GetDomainAt(0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        private sealed class TestCases : TheoryData<TestProblem, int, IReadOnlyList<Digit>>
        {
            public TestCases()
            {
                TestProblem fixedProblem = new()
                {
                    [Letter.A] = [Digit.One, Digit.Two],
                    [Letter.B] = [],
                    [Letter.C] = [Digit.One]
                };

                Add(fixedProblem, 0, [Digit.One, Digit.Two]);
                Add(fixedProblem, 1, Array.Empty<Digit>());
                Add(fixedProblem, 2, [Digit.One]);
            }
        }
    }

    [UnitTest]
    public sealed class GetDomainSizeAt_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsDomainSizeOfVariableAtIndex(TestProblem problem, int index, int expected)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(problem);

            // Act
            var result = sut.GetDomainSizeAt(index);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void IndexArgIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            Action act = () => sut.GetDomainSizeAt(-1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public void IndexArgIsGreaterThanOrEqualToNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            Action act = () => sut.GetDomainSizeAt(index);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void InstanceNotModellingProblem_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            Action act = () => sut.GetDomainSizeAt(0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        private sealed class TestCases : TheoryData<TestProblem, int, int>
        {
            public TestCases()
            {
                TestProblem fixedProblem = new()
                {
                    [Letter.A] = [Digit.One, Digit.Two],
                    [Letter.B] = [],
                    [Letter.C] = [Digit.One]
                };

                Add(fixedProblem, 0, 2);
                Add(fixedProblem, 1, 0);
                Add(fixedProblem, 2, 1);
            }
        }
    }

    [UnitTest]
    public sealed class GetDegreeAt_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsDegreeOfVariableAtIndex(TestProblem problem, int index, int expected)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(problem);

            // Act
            var result = sut.GetDegreeAt(index);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void IndexArgIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            Action act = () => sut.GetDegreeAt(-1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public void IndexArgIsGreaterThanOrEqualToNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            Action act = () => sut.GetDegreeAt(index);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void InstanceNotModellingProblem_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            Action act = () => sut.GetDegreeAt(0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        private sealed class TestCases : TheoryData<TestProblem, int, int>
        {
            public TestCases()
            {
                TestProblem fixedProblem = new()
                {
                    [Letter.A] = [Digit.Four],
                    [Letter.B] = [Digit.One, Digit.Two],
                    [Letter.C] = [Digit.One, Digit.Two, Digit.Three, Digit.Four],
                    [Letter.D] = [Digit.Four],
                    [Letter.E] = [Digit.Five]
                };

                Add(fixedProblem, 0, 2);
                Add(fixedProblem, 1, 1);
                Add(fixedProblem, 2, 3);
                Add(fixedProblem, 3, 2);
                Add(fixedProblem, 4, 0);
            }
        }
    }

    [UnitTest]
    public sealed class GetSumTightnessAt_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsSumTightnessOfVariableAtIndex(TestProblem problem, int index, double expected)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(problem);

            // Act
            var result = sut.GetSumTightnessAt(index);

            // Assert
            result.Should().BeApproximately(expected, Invariants.SixDecimalPlacesPrecision);
        }

        [Fact]
        public void IndexArgIsNegative_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            Action act = () => sut.GetSumTightnessAt(-1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        public void IndexArgIsGreaterThanOrEqualToNumberOfVariables_Throws(int index)
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            Action act = () => sut.GetSumTightnessAt(index);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void InstanceNotModellingProblem_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            Action act = () => sut.GetSumTightnessAt(0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Variable index was out of range. " +
                             "Must be non-negative and less than the number of binary CSP variables.")
                .WithInnerException<ArgumentOutOfRangeException>();
        }

        private sealed class TestCases : TheoryData<TestProblem, int, double>
        {
            public TestCases()
            {
                TestProblem fixedProblem = new()
                {
                    [Letter.A] = [Digit.Four],
                    [Letter.B] = [Digit.One, Digit.Two],
                    [Letter.C] = [Digit.One, Digit.Two, Digit.Three, Digit.Four],
                    [Letter.D] = [Digit.Four],
                    [Letter.E] = [Digit.Five]
                };

                Add(fixedProblem, 0, 1.25);
                Add(fixedProblem, 1, 0.25);
                Add(fixedProblem, 2, 0.75);
                Add(fixedProblem, 3, 1.25);
                Add(fixedProblem, 4, 0);
            }
        }
    }
}
