using Kolyteon.Common;
using Kolyteon.Modelling.Testing;
using Kolyteon.NQueens;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.NQueens;

public static partial class NQueensConstraintGraphTests
{
    [UnitTest]
    public sealed class ParameterlessConstructor
    {
        [Fact]
        public void ParameterlessConstructor_Initializes_AllPropertiesIncludingCapacityAreZero()
        {
            // Act
            NQueensConstraintGraph result = new();

            // Assert
            using (new AssertionScope())
            {
                result.Capacity.Should().Be(0);
                result.Variables.Should().Be(0);
                result.Constraints.Should().Be(0);
                result.ConstraintDensity.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
                result.MeanTightness.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
            }
        }
    }

    [UnitTest]
    public sealed class CapacityArgConstructor
    {
        [Fact]
        public void CapacityArgConstructor_Initializes_HasGivenCapacityAndAllOtherPropertiesAreZero()
        {
            // Arrange
            const int capacity = 4;

            // Act
            NQueensConstraintGraph result = new(capacity);

            // Assert
            using (new AssertionScope())
            {
                result.Capacity.Should().Be(capacity);
                result.Variables.Should().Be(0);
                result.Constraints.Should().Be(0);
                result.ConstraintDensity.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
                result.MeanTightness.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
            }
        }

        [Fact]
        public void CapacityArgConstructor_CapacityArgIsNegative_Throws()
        {
            // Act
            Action act = () => _ = new NQueensConstraintGraph(-1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("capacity ('-1') must be a non-negative value. (Parameter 'capacity')\nActual value was -1.");
        }
    }

    [UnitTest]
    public sealed class CapacityProperty
    {
        private static NQueensProblem GetProblemWithTwoVariables() => TestCaseTwo.Problem;

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CapacitySetter_GivenValueGreaterThanOrEqualToNumberOfVariables_UpdatesCapacity(int requiredCapacity)
        {
            // Arrange
            const int initialCapacity = 3;

            NQueensConstraintGraph sut = new(initialCapacity);

            sut.Model(GetProblemWithTwoVariables());

            // Assert
            sut.Capacity.Should().Be(initialCapacity);

            // Act
            sut.Capacity = requiredCapacity;

            // Assert
            sut.Capacity.Should().Be(requiredCapacity);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void CapacitySetter_GivenValueLessThanNumberOfVariables_Throws(int requiredCapacity)
        {
            // Arrange
            NQueensConstraintGraph sut =
                NQueensConstraintGraph.ModellingProblem(GetProblemWithTwoVariables());

            // Assert
            sut.Capacity.Should().Be(2);

            // Act
            Action act = () => sut.Capacity = requiredCapacity;

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("capacity was less than the current size. (Parameter 'value')");
        }

        [Fact]
        public void CapacitySetter_GivenNegativeValue_Throws()
        {
            // Arrange
            NQueensConstraintGraph sut = new();

            // Act
            Action act = () => sut.Capacity = -1;

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("capacity was less than the current size. (Parameter 'value')");
        }
    }

    [UnitTest]
    public sealed class ModelMethod
    {
        public static TheoryData<NQueensProblem, IList<ConstraintGraphNode<int, Square>>> NodeTestCases => new()
        {
            { TestCaseOne.Problem, TestCaseOne.ExpectedNodes },
            { TestCaseTwo.Problem, TestCaseTwo.ExpectedNodes },
            { TestCaseThree.Problem, TestCaseThree.ExpectedNodes },
            { TestCaseFour.Problem, TestCaseFour.ExpectedNodes }
        };

        public static TheoryData<NQueensProblem, IList<ConstraintGraphEdge<int, Square>>> EdgeTestCases => new()
        {
            { TestCaseOne.Problem, TestCaseOne.ExpectedEdges },
            { TestCaseTwo.Problem, TestCaseTwo.ExpectedEdges },
            { TestCaseThree.Problem, TestCaseThree.ExpectedEdges },
            { TestCaseFour.Problem, TestCaseFour.ExpectedEdges }
        };

        [Theory]
        [MemberData(nameof(NodeTestCases), MemberType = typeof(ModelMethod))]
        public void Model_GivenProblem_PopulatesNodes(NQueensProblem problem,
            IList<ConstraintGraphNode<int, Square>> expectedNodes)
        {
            // Arrange
            NQueensConstraintGraph sut = new(4);

            // Act
            sut.Model(problem);

            // Assert
            sut.GetConstraintGraphNodes().Should().Equal(expectedNodes);
        }

        [Theory]
        [MemberData(nameof(EdgeTestCases), MemberType = typeof(ModelMethod))]
        public void Model_GivenProblem_PopulatesEdges(NQueensProblem problem,
            IList<ConstraintGraphEdge<int, Square>> expectedEdges)
        {
            // Arrange
            NQueensConstraintGraph sut = new(4);

            // Act
            sut.Model(problem);

            // Assert
            sut.GetConstraintGraphEdges().Should().Equal(expectedEdges);
        }

        [Fact]
        public void CanModelProblem_ThenClear_ThenModelAnotherProblem()
        {
            // Arrange
            (NQueensProblem initialProblem,
                IList<ConstraintGraphNode<int, Square>> expectedInitialNodes,
                IList<ConstraintGraphEdge<int, Square>> expectedInitialEdges) = TestCaseThree;

            (NQueensProblem finalProblem,
                IList<ConstraintGraphNode<int, Square>> expectedFinalNodes,
                IList<ConstraintGraphEdge<int, Square>> expectedFinalEdges) = TestCaseFour;

            NQueensConstraintGraph sut = NQueensConstraintGraph.ModellingProblem(initialProblem);

            // Assert
            using (new AssertionScope())
            {
                sut.GetConstraintGraphNodes().Should().Equal(expectedInitialNodes);
                sut.GetConstraintGraphEdges().Should().Equal(expectedInitialEdges);
            }

            // Act
            sut.Clear();
            sut.Model(finalProblem);

            // Assert
            using (new AssertionScope())
            {
                sut.GetConstraintGraphNodes().Should().Equal(expectedFinalNodes);
                sut.GetConstraintGraphEdges().Should().Equal(expectedFinalEdges);
            }
        }
    }

    [UnitTest]
    public sealed class ModellingProblemStaticFactoryMethod
    {
        [Fact]
        public void ModellingProblem_GivenProblem_ReturnsInstanceModellingProblem()
        {
            // Arrange
            (NQueensProblem problem,
                IList<ConstraintGraphNode<int, Square>> expectedNodes,
                IList<ConstraintGraphEdge<int, Square>> expectedEdges) = TestCaseFour;

            // Act
            NQueensConstraintGraph result = NQueensConstraintGraph.ModellingProblem(problem);

            // Assert
            using (new AssertionScope())
            {
                result.GetConstraintGraphNodes().Should().Equal(expectedNodes);
                result.GetConstraintGraphEdges().Should().Equal(expectedEdges);
            }
        }

        [Fact]
        public void ModellingProblem_ProblemArgIsNull_Throws()
        {
            // Act
            Action act = () => NQueensConstraintGraph.ModellingProblem(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'problem')");
        }
    }
}
