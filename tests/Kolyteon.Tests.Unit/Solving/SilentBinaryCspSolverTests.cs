using Kolyteon.Modelling;
using Kolyteon.Solving;
using Kolyteon.Tests.Unit.TestUtils;
using Kolyteon.Tests.Utils.TestAssertions;

namespace Kolyteon.Tests.Unit.Solving;

public static class SilentBinaryCspSolverTests
{
    [UnitTest]
    public sealed class SolveMethod
    {
        [Fact]
        public void Solve_GivenBinaryCspWithEmptyDomain_ReturnsResultWithZeroAssignments()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1, 2], ['B'] = [], ['C'] = [0, 1, 2]
            });

            SilentBinaryCspSolver<char, int> sut = SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .Build();

            // Act
            (IReadOnlyList<Assignment<char, int>> solution, SearchMetrics metrics) =
                sut.Solve(binaryCsp, cancellationToken: CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                solution.Should().BeEmpty();


                metrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                    .And.HaveOrderingStrategy(OrderingStrategy.NaturalOrdering);
                metrics.SimplifyingSteps.Should().Be(1);
                metrics.AssigningSteps.Should().Be(0);
                metrics.BacktrackingSteps.Should().Be(0);
                metrics.TotalSteps.Should().Be(1);
                metrics.Efficiency.Should().BeApproximately(1.0, 0.000001);
            }
        }

        [Fact]
        public void Solve_GivenBinaryCspWithNoSolution_ReturnsResultWithZeroAssignments()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1], ['B'] = [1], ['C'] = [0]
            });

            SilentBinaryCspSolver<char, int> sut = SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .Build();

            // Act
            (IReadOnlyList<Assignment<char, int>> solution, SearchMetrics metrics) =
                sut.Solve(binaryCsp, cancellationToken: CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                solution.Should().BeEmpty();


                metrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                    .And.HaveOrderingStrategy(OrderingStrategy.NaturalOrdering);
                metrics.SimplifyingSteps.Should().Be(1);
                metrics.AssigningSteps.Should().Be(5);
                metrics.BacktrackingSteps.Should().Be(4);
                metrics.TotalSteps.Should().Be(10);
                metrics.Efficiency.Should().BeApproximately(0.6, 0.000001);
            }
        }

        [Fact]
        public void Solve_GivenBinaryCspWithSolution_ReturnsResultWithCorrectAssignments()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1], ['B'] = [0], ['C'] = [2]
            });

            SilentBinaryCspSolver<char, int> sut = SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .Build();

            // Act
            (IReadOnlyList<Assignment<char, int>> solution, SearchMetrics metrics) =
                sut.Solve(binaryCsp, cancellationToken: CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                solution.Should().Equal(new Assignment<char, int>('A', 1),
                    new Assignment<char, int>('B', 0),
                    new Assignment<char, int>('C', 2));


                metrics.SearchAlgorithm.Should().HaveCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                    .And.HaveOrderingStrategy(OrderingStrategy.NaturalOrdering);
                metrics.SimplifyingSteps.Should().Be(1);
                metrics.AssigningSteps.Should().Be(5);
                metrics.BacktrackingSteps.Should().Be(1);
                metrics.TotalSteps.Should().Be(7);
                metrics.Efficiency.Should().BeApproximately(0.857143, 0.000001);
            }
        }

        [Fact]
        public void Solve_BinaryCspArgIsNull_Throws()
        {
            // Arrange
            SilentBinaryCspSolver<char, int> sut = SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(0)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .Build();

            // Act
            Action act = () => sut.Solve(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'binaryCsp')");
        }

        [Fact]
        public void Solve_GivenBinaryCspNotModellingAProblem_Throws()
        {
            // Arrange
            TestConstraintGraph emptyBinaryCsp = new();

            SilentBinaryCspSolver<char, int> sut = SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(0)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .Build();

            // Act
            Action act = () => sut.Solve(emptyBinaryCsp);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Binary CSP is not modelling a problem.");
        }

        [Fact]
        public void Solve_CancellationRequestedViaCancellationToken_Throws()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1, 2], ['B'] = [0, 1], ['C'] = [0]
            });

            SilentBinaryCspSolver<char, int> sut = SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .Build();

            // Act
            Action act = () =>
            {
                using CancellationTokenSource cts = new(TimeSpan.Zero);
                _ = sut.Solve(binaryCsp, cancellationToken: cts.Token);
            };

            // Assert
            act.Should().Throw<OperationCanceledException>()
                .WithMessage("The binary CSP solving operation was cancelled.");
        }
    }

    [UnitTest]
    public sealed class FluentBuilder
    {
        [Fact]
        public void FluentBuilder_CanBuildSolver()
        {
            // Arrange
            const int capacity = 4;
            CheckingStrategy checkingStrategy = CheckingStrategy.ForwardChecking;
            OrderingStrategy orderingStrategy = OrderingStrategy.BrelazHeuristic;

            // Act
            SilentBinaryCspSolver<char, int> result = SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(capacity)
                .AndCheckingStrategy(checkingStrategy)
                .AndOrderingStrategy(orderingStrategy)
                .Build();

            // Assert
            using (new AssertionScope())
            {
                result.SearchAlgorithm.Should().HaveCheckingStrategy(checkingStrategy)
                    .And.HaveOrderingStrategy(orderingStrategy);
                result.Capacity.Should().Be(capacity);
            }
        }

        [Fact]
        public void FluentBuilder_SettingNegativeCapacity_Throws()
        {
            // Arrange
            CheckingStrategy arbitraryCheckingStrategy = CheckingStrategy.NaiveBacktracking;
            OrderingStrategy arbitraryOrderingStrategy = OrderingStrategy.NaturalOrdering;

            // Act
            Action act = () => SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(-1)
                .AndCheckingStrategy(arbitraryCheckingStrategy)
                .AndOrderingStrategy(arbitraryOrderingStrategy)
                .Build();

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("capacity ('-1') must be a non-negative value. (Parameter 'capacity')\n" +
                             "Actual value was -1.");
        }

        [Fact]
        public void FluentBuilder_SettingNullCheckingStrategy_Throws()
        {
            // Arrange
            const int arbitraryCapacity = 4;
            OrderingStrategy arbitraryOrderingStrategy = OrderingStrategy.NaturalOrdering;

            // Act
            Action act = () => SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(arbitraryCapacity)
                .AndCheckingStrategy(null!)
                .AndOrderingStrategy(arbitraryOrderingStrategy)
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'checkingStrategy')");
        }

        [Fact]
        public void FluentBuilder_SettingNullOrderingStrategy_Throws()
        {
            // Arrange
            const int arbitraryCapacity = 4;
            CheckingStrategy arbitraryCheckingStrategy = CheckingStrategy.NaiveBacktracking;

            // Act
            Action act = () => SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(arbitraryCapacity)
                .AndCheckingStrategy(arbitraryCheckingStrategy)
                .AndOrderingStrategy(null!)
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'orderingStrategy')");
        }
    }
}
