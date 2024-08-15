using Kolyteon.Modelling;
using Kolyteon.Solving;
using Kolyteon.Tests.Unit.Solving.TestUtils;
using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Solving;

public static class VerboseBinaryCspSolverTests
{
    [UnitTest]
    public sealed class SolveAsyncMethod
    {
        [Fact]
        public async Task SolveAsync_GivenBinaryCspWithEmptyDomain_NotifiesReporterAndReturnsResultWithZeroAssignments()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1, 2], ['B'] = [], ['C'] = [0, 1, 2]
            });

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            TestSolvingProgressReporter<char, int> progressReporter = new();

            // Act
            SolvingResult<char, int> result = await sut.SolveAsync(binaryCsp, progressReporter);

            // Assert
            SearchAlgorithm expectedAlgorithm = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);

            using (new AssertionScope())
            {
                result.Assignments.Should().BeEmpty();
                result.SearchAlgorithm.Should().Be(expectedAlgorithm);
                result.SimplifyingSteps.Should().Be(1);
                result.AssigningSteps.Should().Be(0);
                result.BacktrackingSteps.Should().Be(0);
                progressReporter.VerifyEndStateMatchesResult(result);
            }
        }

        [Fact]
        public async Task SolveAsync_GivenBinaryCspWithNoSolution_NotifiesReporterAndReturnsResultWithZeroAssignments()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1], ['B'] = [0, 1], ['C'] = [0, 1]
            });

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            TestSolvingProgressReporter<char, int> progressReporter = new();

            // Act
            SolvingResult<char, int> result = await sut.SolveAsync(binaryCsp, progressReporter);

            // Assert
            SearchAlgorithm expectedAlgorithm = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);

            using (new AssertionScope())
            {
                result.Assignments.Should().BeEmpty();
                result.SearchAlgorithm.Should().Be(expectedAlgorithm);
                result.SimplifyingSteps.Should().Be(1);
                result.AssigningSteps.Should().BePositive();
                result.BacktrackingSteps.Should().BePositive();
                progressReporter.VerifyEndStateMatchesResult(result);
            }
        }

        [Fact]
        public async Task SolveAsync_GivenBinaryCspWithSolution_NotifiesReporterAndReturnsResultWithCorrectAssignments()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1, 2], ['B'] = [0, 1], ['C'] = [0]
            });

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            TestSolvingProgressReporter<char, int> progressReporter = new();

            // Act
            SolvingResult<char, int> result = await sut.SolveAsync(binaryCsp, progressReporter);

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
                progressReporter.VerifyEndStateMatchesResult(result);
            }
        }

        [Fact]
        public async Task SolveAsync_BinaryCspArgIsNull_Throws()
        {
            // Arrange
            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(1)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            TestSolvingProgressReporter<char, int> progressReporter = new();

            // Act
            Func<Task> act = async () => _ = await sut.SolveAsync(null!, progressReporter);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'binaryCsp')");
        }

        [Fact]
        public async Task SolveAsync_ProgressReporterArgIsNull_Throws()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem { ['A'] = [0] });

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(1)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            // Act
            Func<Task> act = async () => _ = await sut.SolveAsync(binaryCsp, null!);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'progressReporter')");
        }

        [Fact]
        public async Task SolveAsync_GivenBinaryCspNotModellingAProblem_Throws()
        {
            // Arrange
            TestConstraintGraph binaryCsp = new();

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(1)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            TestSolvingProgressReporter<char, int> progressReporter = new();

            // Act
            Func<Task> act = async () => _ = await sut.SolveAsync(binaryCsp, progressReporter);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Binary CSP is not modelling a problem.");
        }

        [Fact]
        public async Task SolveAsync_CancellationRequestedViaCancellationToken_Throws()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1, 2], ['B'] = [0, 1], ['C'] = [0]
            });

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(1)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            TestSolvingProgressReporter<char, int> progressReporter = new();

            // Act
            Func<Task> act = async () =>
            {
                using CancellationTokenSource cts = new(TimeSpan.Zero);
                _ = await sut.SolveAsync(binaryCsp, progressReporter, cts.Token);
            };

            // Assert
            await act.Should().ThrowAsync<OperationCanceledException>()
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
            TimeSpan stepDelay = TimeSpan.FromSeconds(1);

            // Act
            VerboseBinaryCspSolver<char, int> result = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(capacity)
                .AndCheckingStrategy(checkingStrategy)
                .AndOrderingStrategy(orderingStrategy)
                .AndStepDelay(stepDelay)
                .Build();

            // Assert
            using (new AssertionScope())
            {
                result.CheckingStrategy.Should().Be(checkingStrategy);
                result.OrderingStrategy.Should().Be(orderingStrategy);
                result.Capacity.Should().Be(capacity);
                result.StepDelay.Should().Be(stepDelay);
            }
        }

        [Fact]
        public void FluentBuilder_SettingNegativeCapacity_Throws()
        {
            // Arrange
            CheckingStrategy arbitraryCheckingStrategy = CheckingStrategy.NaiveBacktracking;
            OrderingStrategy arbitraryOrderingStrategy = OrderingStrategy.NaturalOrdering;
            TimeSpan arbitraryStepDelay = TimeSpan.FromSeconds(1);

            // Act
            Action act = () => VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(-1)
                .AndCheckingStrategy(arbitraryCheckingStrategy)
                .AndOrderingStrategy(arbitraryOrderingStrategy)
                .AndStepDelay(arbitraryStepDelay)
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
            TimeSpan arbitraryStepDelay = TimeSpan.FromSeconds(1);

            // Act
            Action act = () => VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(arbitraryCapacity)
                .AndCheckingStrategy(null!)
                .AndOrderingStrategy(arbitraryOrderingStrategy)
                .AndStepDelay(arbitraryStepDelay)
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
            TimeSpan arbitraryStepDelay = TimeSpan.FromSeconds(1);

            // Act
            Action act = () => VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(arbitraryCapacity)
                .AndCheckingStrategy(arbitraryCheckingStrategy)
                .AndOrderingStrategy(null!)
                .AndStepDelay(arbitraryStepDelay)
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'orderingStrategy')");
        }
    }
}
