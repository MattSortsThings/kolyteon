using Kolyteon.Modelling;
using Kolyteon.Solving;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Solving;

public static class SilentBinaryCspSolverTests
{
    [UnitTest]
    public sealed class SolveMethod
    {
        [Fact]
        public void Solve_GivenBinaryCspWithVariableHavingEmptyDomain_ReturnsResultWithZeroAssignmentsAndCorrectMetrics()
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
            SolvingResult<char, int> result = sut.Solve(binaryCsp);

            // Assert
            SearchAlgorithm expectedAlgorithm = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);

            using (new AssertionScope())
            {
                result.Assignments.Should().BeEmpty();
                result.SearchAlgorithm.Should().Be(expectedAlgorithm);
                result.SimplifyingSteps.Should().Be(1);
                result.AssigningSteps.Should().Be(0);
                result.BacktrackingSteps.Should().Be(0);
            }
        }

        [Fact]
        public void Solve_GivenBinaryCspWithNoSolution_ReturnsResultWithZeroAssignmentsAndCorrectMetrics()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1], ['B'] = [0, 1], ['C'] = [0, 1]
            });

            SilentBinaryCspSolver<char, int> sut = SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .Build();

            // Act
            SolvingResult<char, int> result = sut.Solve(binaryCsp);

            // Assert
            SearchAlgorithm expectedAlgorithm = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);

            using (new AssertionScope())
            {
                result.Assignments.Should().BeEmpty();
                result.SearchAlgorithm.Should().Be(expectedAlgorithm);
                result.SimplifyingSteps.Should().Be(1);
                result.AssigningSteps.Should().BePositive();
                result.BacktrackingSteps.Should().BePositive();
            }
        }

        [Fact]
        public void Solve_GivenBinaryCspWithSolution_ReturnsResultWithCorrectAssignmentsAndCorrectMetrics()
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
            SolvingResult<char, int> result = sut.Solve(binaryCsp);

            // Assert
            SearchAlgorithm expectedAlgorithm = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);

            using (new AssertionScope())
            {
                result.Assignments.Should().BeEquivalentTo([
                    new Assignment<char, int>('A', 2),
                    new Assignment<char, int>('B', 1),
                    new Assignment<char, int>('C', 0)
                ]);
                result.SearchAlgorithm.Should().Be(expectedAlgorithm);
                result.SimplifyingSteps.Should().Be(1);
                result.AssigningSteps.Should().BePositive();
                result.BacktrackingSteps.Should().BePositive();
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
                sut.Solve(binaryCsp, cts.Token);
            };

            // Assert
            act.Should().Throw<OperationCanceledException>()
                .WithMessage("The operation was canceled.");
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
            CheckingStrategy checkingStrategy = CheckingStrategy.NaiveBacktracking;
            OrderingStrategy orderingStrategy = OrderingStrategy.NaturalOrdering;

            // Act
            SilentBinaryCspSolver<char, int> result = SilentBinaryCspSolver<char, int>.Create()
                .WithCapacity(capacity)
                .AndCheckingStrategy(checkingStrategy)
                .AndOrderingStrategy(orderingStrategy)
                .Build();

            // Assert
            using (new AssertionScope())
            {
                result.CheckingStrategy.Should().Be(checkingStrategy);
                result.OrderingStrategy.Should().Be(orderingStrategy);
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
