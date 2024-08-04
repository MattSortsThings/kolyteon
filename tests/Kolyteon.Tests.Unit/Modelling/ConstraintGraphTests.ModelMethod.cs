using Kolyteon.Modelling.Testing;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class ModelMethod
    {
        [Fact]
        public void Model_GivenProblem_PopulatesNodesWithVariablesInOrder()
        {
            // Arrange
            TestConstraintGraph sut = new(3);

            TestProblem problem = new() { [C] = [1], [A] = [1], [B] = [1] };

            // Act
            sut.Model(problem);

            // Assert
            sut.GetConstraintGraphNodes().Should()
                .SatisfyRespectively(node => node.Variable.Should().Be(A),
                    node => node.Variable.Should().Be(B),
                    node => node.Variable.Should().Be(C));
        }

        [Fact]
        public void Model_GivenProblem_PopulatesDomainsWithUniqueValuesInOrder()
        {
            // Arrange
            TestConstraintGraph sut = new(3);

            TestProblem problem = new() { [A] = [1, 9], [B] = [], [C] = [3, 2, 1, 1] };

            // Act
            sut.Model(problem);

            // Assert
            sut.GetConstraintGraphNodes().Should()
                .SatisfyRespectively(node => node.Domain.Should().Equal(1, 9),
                    node => node.Domain.Should().BeEmpty(),
                    node => node.Domain.Should().Equal(1, 2, 3));
        }

        [Fact]
        public void Model_GivenProblem_PopulatesEdgesForProvenConstraintsOnly()
        {
            // Arrange
            TestConstraintGraph sut = new(4);

            TestProblem problem = new() { [A] = [1, 2], [B] = [3, 4], [C] = [1, 2, 3], [D] = [4] };

            // Act
            sut.Model(problem);

            // Assert
            sut.GetConstraintGraphEdges().Should().SatisfyRespectively(
                edge =>
                {
                    edge.FirstVariable.Should().Be(A);
                    edge.SecondVariable.Should().Be(C);
                }, edge =>
                {
                    edge.FirstVariable.Should().Be(B);
                    edge.SecondVariable.Should().Be(C);
                }, edge =>
                {
                    edge.FirstVariable.Should().Be(B);
                    edge.SecondVariable.Should().Be(D);
                }
            );
        }

        [Fact]
        public void Model_GivenProblem_CreatesCorrectNodes()
        {
            // Arrange
            TestConstraintGraph sut = new(4);

            TestProblem problem = new() { [A] = [1, 2], [B] = [3, 4], [C] = [1, 2, 3], [D] = [4], [E] = [9] };

            // Act
            sut.Model(problem);

            // Assert
            sut.GetConstraintGraphNodes().Should().Equal(
                new ConstraintGraphNode<char, int> { Variable = A, Degree = 1, SumTightness = 0.333333333, Domain = [1, 2] },
                new ConstraintGraphNode<char, int> { Variable = B, Degree = 2, SumTightness = 0.666666667, Domain = [3, 4] },
                new ConstraintGraphNode<char, int> { Variable = C, Degree = 2, SumTightness = 0.5, Domain = [1, 2, 3] },
                new ConstraintGraphNode<char, int> { Variable = D, Degree = 1, SumTightness = 0.5, Domain = [4] },
                new ConstraintGraphNode<char, int> { Variable = E, Degree = 0, SumTightness = 0.0, Domain = [9] }
            );
        }

        [Fact]
        public void Model_GivenProblem_CreatesCorrectEdges()
        {
            // Arrange
            TestConstraintGraph sut = new(4);

            TestProblem problem = new() { [A] = [1, 2], [B] = [3, 4], [C] = [1, 2, 3], [D] = [4], [E] = [9] };

            // Act
            sut.Model(problem);

            // Assert
            sut.GetConstraintGraphEdges().Should().Equal(
                new ConstraintGraphEdge<char, int>
                {
                    FirstVariable = A,
                    SecondVariable = C,
                    Tightness = 0.333333333,
                    AssignmentPairs =
                    [
                        new AssignmentPair<int>(1, 1, false),
                        new AssignmentPair<int>(1, 2, true),
                        new AssignmentPair<int>(1, 3, true),
                        new AssignmentPair<int>(2, 1, true),
                        new AssignmentPair<int>(2, 2, false),
                        new AssignmentPair<int>(2, 3, true)
                    ]
                }, new ConstraintGraphEdge<char, int>
                {
                    FirstVariable = B,
                    SecondVariable = C,
                    Tightness = 0.166666667,
                    AssignmentPairs =
                    [
                        new AssignmentPair<int>(3, 1, true),
                        new AssignmentPair<int>(3, 2, true),
                        new AssignmentPair<int>(3, 3, false),
                        new AssignmentPair<int>(4, 1, true),
                        new AssignmentPair<int>(4, 2, true),
                        new AssignmentPair<int>(4, 3, true)
                    ]
                },
                new ConstraintGraphEdge<char, int>
                {
                    FirstVariable = B,
                    SecondVariable = D,
                    Tightness = 0.5,
                    AssignmentPairs =
                    [
                        new AssignmentPair<int>(3, 4, true),
                        new AssignmentPair<int>(4, 4, false)
                    ]
                }
            );
        }

        [Fact]
        public void Model_GivenProblemWithMoreVariablesThanCapacity_IncreasesCapacity()
        {
            // Arrange
            TestConstraintGraph sut = new();

            TestProblem problem = GetProblemWithTwoVariablesAndOneConstraint();

            // Assert
            sut.Capacity.Should().Be(0);

            // Act
            sut.Model(problem);

            // Assert
            sut.Capacity.Should().BeGreaterOrEqualTo(2);
        }

        [Fact]
        public void Model_GivenProblemWithFewerVariablesThanCapacity_DoesNotUpdateCapacity()
        {
            // Arrange
            const int initialCapacity = 4;

            TestConstraintGraph sut = new(initialCapacity);

            TestProblem problem = GetProblemWithTwoVariablesAndOneConstraint();

            // Assert
            sut.Capacity.Should().Be(initialCapacity);

            // Act
            sut.Model(problem);

            // Assert
            sut.Capacity.Should().Be(initialCapacity);
        }

        [Fact]
        public void Model_ProblemArgIsNull_Throws()
        {
            // Arrange
            TestConstraintGraph sut = new();

            // Act
            Action act = () => sut.Model(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'problem')");
        }

        [Fact]
        public void Model_AlreadyModellingAProblem_Throws()
        {
            // Arrange
            TestProblem problem = GetProblemWithOneVariable();

            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(problem);

            // Act
            Action act = () => sut.Model(problem);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Already modelling a problem.");
        }
    }
}
