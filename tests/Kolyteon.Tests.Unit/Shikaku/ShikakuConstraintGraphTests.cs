using Kolyteon.Common;
using Kolyteon.Modelling.Testing;
using Kolyteon.Shikaku;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Shikaku;

public static partial class ShikakuConstraintGraphTests
{
    [UnitTest]
    public sealed class ParameterlessConstructor
    {
        [Fact]
        public void ParameterlessConstructor_Initializes_AllPropertiesIncludingCapacityAreZero()
        {
            // Act
            ShikakuConstraintGraph result = new();

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
            ShikakuConstraintGraph result = new(capacity);

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
            Action act = () => _ = new ShikakuConstraintGraph(-1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("capacity ('-1') must be a non-negative value. (Parameter 'capacity')\nActual value was -1.");
        }
    }

    [UnitTest]
    public sealed class CapacityProperty
    {
        private static ShikakuProblem GetProblemWithTwoVariables() => TestCaseTwo.Problem;

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CapacitySetter_GivenValueGreaterThanOrEqualToNumberOfVariables_UpdatesCapacity(int requiredCapacity)
        {
            // Arrange
            const int initialCapacity = 3;

            ShikakuConstraintGraph sut = new(initialCapacity);

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
            ShikakuConstraintGraph sut =
                ShikakuConstraintGraph.ModellingProblem(GetProblemWithTwoVariables());

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
            ShikakuConstraintGraph sut = new();

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
        public static TheoryData<ShikakuProblem, IList<ConstraintGraphNodeDatum<NumberedSquare, Block>>> NodeTestCases => new()
        {
            { TestCaseOne.Problem, TestCaseOne.ExpectedNodes },
            { TestCaseTwo.Problem, TestCaseTwo.ExpectedNodes },
            { TestCaseThree.Problem, TestCaseThree.ExpectedNodes },
            { TestCaseFour.Problem, TestCaseFour.ExpectedNodes },
            { TestCaseFive.Problem, TestCaseFive.ExpectedNodes },
            { TestCaseSix.Problem, TestCaseSix.ExpectedNodes }
        };

        public static TheoryData<ShikakuProblem, IList<ConstraintGraphEdgeDatum<NumberedSquare, Block>>> EdgeTestCases => new()
        {
            { TestCaseOne.Problem, TestCaseOne.ExpectedEdges },
            { TestCaseTwo.Problem, TestCaseTwo.ExpectedEdges },
            { TestCaseThree.Problem, TestCaseThree.ExpectedEdges },
            { TestCaseFour.Problem, TestCaseFour.ExpectedEdges },
            { TestCaseFive.Problem, TestCaseFive.ExpectedEdges },
            { TestCaseSix.Problem, TestCaseSix.ExpectedEdges }
        };

        [Theory]
        [MemberData(nameof(NodeTestCases), MemberType = typeof(ModelMethod))]
        public void Model_GivenProblem_PopulatesNodes(ShikakuProblem problem,
            IList<ConstraintGraphNodeDatum<NumberedSquare, Block>> expectedNodes)
        {
            // Arrange
            ShikakuConstraintGraph sut = new(4);

            // Act
            sut.Model(problem);

            // Assert
            sut.GetNodeData().Should().Equal(expectedNodes);
        }

        [Theory]
        [MemberData(nameof(EdgeTestCases), MemberType = typeof(ModelMethod))]
        public void Model_GivenProblem_PopulatesEdges(ShikakuProblem problem,
            IList<ConstraintGraphEdgeDatum<NumberedSquare, Block>> expectedEdges)
        {
            // Arrange
            ShikakuConstraintGraph sut = new(4);

            // Act
            sut.Model(problem);

            // Assert
            sut.GetEdgeData().Should().Equal(expectedEdges);
        }

        [Fact]
        public void CanModelProblem_ThenClear_ThenModelAnotherProblem()
        {
            // Arrange
            (ShikakuProblem initialProblem,
                IList<ConstraintGraphNodeDatum<NumberedSquare, Block>> expectedInitialNodes,
                IList<ConstraintGraphEdgeDatum<NumberedSquare, Block>> expectedInitialEdges) = TestCaseFive;

            (ShikakuProblem finalProblem,
                IList<ConstraintGraphNodeDatum<NumberedSquare, Block>> expectedFinalNodes,
                IList<ConstraintGraphEdgeDatum<NumberedSquare, Block>> expectedFinalEdges) = TestCaseTwo;

            ShikakuConstraintGraph sut = ShikakuConstraintGraph.ModellingProblem(initialProblem);

            // Assert
            using (new AssertionScope())
            {
                sut.GetNodeData().Should().Equal(expectedInitialNodes);
                sut.GetEdgeData().Should().Equal(expectedInitialEdges);
            }

            // Act
            sut.Clear();
            sut.Model(finalProblem);

            // Assert
            using (new AssertionScope())
            {
                sut.GetNodeData().Should().Equal(expectedFinalNodes);
                sut.GetEdgeData().Should().Equal(expectedFinalEdges);
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
            (ShikakuProblem problem,
                IList<ConstraintGraphNodeDatum<NumberedSquare, Block>> expectedNodes,
                IList<ConstraintGraphEdgeDatum<NumberedSquare, Block>> expectedEdges) = TestCaseFive;

            // Act
            ShikakuConstraintGraph result = ShikakuConstraintGraph.ModellingProblem(problem);

            // Assert
            using (new AssertionScope())
            {
                result.GetNodeData().Should().Equal(expectedNodes);
                result.GetEdgeData().Should().Equal(expectedEdges);
            }
        }

        [Fact]
        public void ModellingProblem_ProblemArgIsNull_Throws()
        {
            // Act
            Action act = () => ShikakuConstraintGraph.ModellingProblem(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'problem')");
        }
    }
}
