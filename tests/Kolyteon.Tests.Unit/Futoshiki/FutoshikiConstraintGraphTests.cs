using Kolyteon.Common;
using Kolyteon.Futoshiki;
using Kolyteon.Modelling.Testing;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Futoshiki;

public static partial class FutoshikiConstraintGraphTests
{
    [UnitTest]
    public sealed class ParameterlessConstructor
    {
        [Fact]
        public void ParameterlessConstructor_Initializes_AllPropertiesIncludingCapacityAreZero()
        {
            // Act
            FutoshikiConstraintGraph result = new();

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
            FutoshikiConstraintGraph result = new(capacity);

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
            Action act = () => _ = new FutoshikiConstraintGraph(-1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("capacity ('-1') must be a non-negative value. (Parameter 'capacity')\nActual value was -1.");
        }
    }

    [UnitTest]
    public sealed class CapacityProperty
    {
        private static FutoshikiProblem GetProblemWithTwoVariables() => TestCaseTwo.Problem;

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CapacitySetter_GivenValueGreaterThanOrEqualToNumberOfVariables_UpdatesCapacity(int requiredCapacity)
        {
            // Arrange
            const int initialCapacity = 3;

            FutoshikiConstraintGraph sut = new(initialCapacity);

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
            FutoshikiConstraintGraph sut =
                FutoshikiConstraintGraph.ModellingProblem(GetProblemWithTwoVariables());

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
            FutoshikiConstraintGraph sut = new();

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
        public static TheoryData<FutoshikiProblem, IList<ConstraintGraphNode<Square, int>>> NodeTestCases => new()
        {
            { TestCaseOne.Problem, TestCaseOne.ExpectedNodes },
            { TestCaseTwo.Problem, TestCaseTwo.ExpectedNodes },
            { TestCaseThree.Problem, TestCaseThree.ExpectedNodes },
            { TestCaseFour.Problem, TestCaseFour.ExpectedNodes },
            { TestCaseFive.Problem, TestCaseFive.ExpectedNodes },
            { TestCaseSix.Problem, TestCaseSix.ExpectedNodes },
            { TestCaseSeven.Problem, TestCaseSeven.ExpectedNodes },
            { TestCaseEight.Problem, TestCaseEight.ExpectedNodes },
            { TestCaseNine.Problem, TestCaseNine.ExpectedNodes }
        };

        public static TheoryData<FutoshikiProblem, IList<ConstraintGraphEdge<Square, int>>> EdgeTestCases => new()
        {
            { TestCaseOne.Problem, TestCaseOne.ExpectedEdges },
            { TestCaseTwo.Problem, TestCaseTwo.ExpectedEdges },
            { TestCaseThree.Problem, TestCaseThree.ExpectedEdges },
            { TestCaseFour.Problem, TestCaseFour.ExpectedEdges },
            { TestCaseFive.Problem, TestCaseFive.ExpectedEdges },
            { TestCaseSix.Problem, TestCaseSix.ExpectedEdges },
            { TestCaseSeven.Problem, TestCaseSeven.ExpectedEdges },
            { TestCaseEight.Problem, TestCaseEight.ExpectedEdges },
            { TestCaseNine.Problem, TestCaseNine.ExpectedEdges }
        };

        [Theory]
        [MemberData(nameof(NodeTestCases), MemberType = typeof(ModelMethod))]
        public void Model_GivenProblem_PopulatesNodes(FutoshikiProblem problem,
            IList<ConstraintGraphNode<Square, int>> expectedNodes)
        {
            // Arrange
            FutoshikiConstraintGraph sut = new(4);

            // Act
            sut.Model(problem);

            // Assert
            sut.GetConstraintGraphNodes().Should().Equal(expectedNodes);
        }

        [Theory]
        [MemberData(nameof(EdgeTestCases), MemberType = typeof(ModelMethod))]
        public void Model_GivenProblem_PopulatesEdges(FutoshikiProblem problem,
            IList<ConstraintGraphEdge<Square, int>> expectedEdges)
        {
            // Arrange
            FutoshikiConstraintGraph sut = new(4);

            // Act
            sut.Model(problem);

            // Assert
            sut.GetConstraintGraphEdges().Should().Equal(expectedEdges);
        }

        [Fact]
        public void CanModelProblem_ThenClear_ThenModelAnotherProblem()
        {
            // Arrange
            (FutoshikiProblem initialProblem,
                IList<ConstraintGraphNode<Square, int>> expectedInitialNodes,
                IList<ConstraintGraphEdge<Square, int>> expectedInitialEdges) = TestCaseOne;

            (FutoshikiProblem finalProblem,
                IList<ConstraintGraphNode<Square, int>> expectedFinalNodes,
                IList<ConstraintGraphEdge<Square, int>> expectedFinalEdges) = TestCaseNine;

            FutoshikiConstraintGraph sut = FutoshikiConstraintGraph.ModellingProblem(initialProblem);

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
            (FutoshikiProblem problem,
                IList<ConstraintGraphNode<Square, int>> expectedNodes,
                IList<ConstraintGraphEdge<Square, int>> expectedEdges) = TestCaseNine;

            // Act
            FutoshikiConstraintGraph result = FutoshikiConstraintGraph.ModellingProblem(problem);

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
            Action act = () => FutoshikiConstraintGraph.ModellingProblem(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'problem')");
        }
    }
}
