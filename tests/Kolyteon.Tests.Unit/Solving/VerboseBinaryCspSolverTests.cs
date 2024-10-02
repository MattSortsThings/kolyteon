using Kolyteon.Modelling;
using Kolyteon.Solving;
using Kolyteon.Tests.Unit.Solving.TestUtils;
using Kolyteon.Tests.Unit.TestUtils;
using Kolyteon.Tests.Utils.TestAssertions;
using NSubstitute;

namespace Kolyteon.Tests.Unit.Solving;

public static class VerboseBinaryCspSolverTests
{
    [UnitTest]
    public sealed class SolveAsyncMethod
    {
        [Fact]
        public async Task SolveAsync_GivenBinaryCspWithEmptyDomain_ReturnsResultWithZeroAssignments()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1, 2], ['B'] = [], ['C'] = [0, 1, 2]
            });

            ISolvingProgress<char, int> dummyProgress = Substitute.For<ISolvingProgress<char, int>>();

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            // Act
            (IReadOnlyList<Assignment<char, int>> solution, SearchMetrics metrics) =
                await sut.SolveAsync(binaryCsp, dummyProgress, cancellationToken: CancellationToken.None);

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
        public async Task SolveAsync_GivenBinaryCspWithEmptyDomain_SendsCorrectNotificationsToProgress()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1, 2], ['B'] = [], ['C'] = [0, 1, 2]
            });

            ISolvingProgress<char, int> spyProgress = Substitute.For<ISolvingProgress<char, int>>();

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            // Act
            SolvingResult<char, int> _ = await sut.SolveAsync(binaryCsp, spyProgress, cancellationToken: CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                spyProgress.Received(1).Reset(Arg.Any<int>());
                spyProgress.Received(1).Report(Arg.Any<SolvingStepDatum<char, int>>());

                Received.InOrder(() =>
                {
                    spyProgress.Reset(3);

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Simplifying,
                        -1,
                        SolvingState.Finished));
                });
            }
        }

        [Fact]
        public async Task SolveAsync_GivenBinaryCspWithNoSolution_ReturnsResultWithZeroAssignments()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1], ['B'] = [1], ['C'] = [0]
            });

            ISolvingProgress<char, int> dummyProgress = Substitute.For<ISolvingProgress<char, int>>();

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            // Act
            (IReadOnlyList<Assignment<char, int>> solution, SearchMetrics metrics) =
                await sut.SolveAsync(binaryCsp, dummyProgress, cancellationToken: CancellationToken.None);

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
        public async Task SolveAsync_GivenBinaryCspWithNoSolution_SendsCorrectNotificationsToProgress()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1], ['B'] = [1], ['C'] = [0]
            });

            ISolvingProgress<char, int> spyProgress = Substitute.For<ISolvingProgress<char, int>>();

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            // Act
            SolvingResult<char, int> _ = await sut.SolveAsync(binaryCsp, spyProgress, cancellationToken: CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                spyProgress.Received(1).Reset(Arg.Any<int>());
                spyProgress.Received(10).Report(Arg.Any<SolvingStepDatum<char, int>>());
                Received.InOrder(() =>
                {
                    spyProgress.Reset(3);

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Simplifying,
                        0,
                        SolvingState.Assigning));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Assigning,
                        1,
                        SolvingState.Assigning,
                        new Assignment<char, int>('A', 0)));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Assigning,
                        2,
                        SolvingState.Assigning,
                        new Assignment<char, int>('B', 1)));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Assigning,
                        2,
                        SolvingState.Backtracking));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Backtracking,
                        1,
                        SolvingState.Backtracking));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Backtracking,
                        0,
                        SolvingState.Assigning));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Assigning,
                        1,
                        SolvingState.Assigning,
                        new Assignment<char, int>('A', 1)));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Assigning,
                        1,
                        SolvingState.Backtracking));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Backtracking,
                        0,
                        SolvingState.Backtracking));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Backtracking,
                        -1,
                        SolvingState.Finished));
                });
            }
        }

        [Fact]
        public async Task SolveAsync_GivenBinaryCspWithSolution_ReturnsResultWithExpectedAssignments()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1], ['B'] = [0], ['C'] = [2]
            });

            ISolvingProgress<char, int> dummyProgress = Substitute.For<ISolvingProgress<char, int>>();

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            // Act
            (IReadOnlyList<Assignment<char, int>> solution, SearchMetrics metrics) =
                await sut.SolveAsync(binaryCsp, dummyProgress, cancellationToken: CancellationToken.None);

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
        public async Task SolveAsync_GivenBinaryCspWithSolution_SendsCorrectNotificationsToProgress()
        {
            // Arrange
            TestConstraintGraph binaryCsp = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                ['A'] = [0, 1], ['B'] = [0], ['C'] = [2]
            });

            ISolvingProgress<char, int> spyProgress = Substitute.For<ISolvingProgress<char, int>>();

            VerboseBinaryCspSolver<char, int> sut = VerboseBinaryCspSolver<char, int>.Create()
                .WithCapacity(3)
                .AndCheckingStrategy(CheckingStrategy.NaiveBacktracking)
                .AndOrderingStrategy(OrderingStrategy.NaturalOrdering)
                .AndStepDelay(TimeSpan.Zero)
                .Build();

            // Act
            SolvingResult<char, int> _ = await sut.SolveAsync(binaryCsp, spyProgress, cancellationToken: CancellationToken.None);

            // Assert
            using (new AssertionScope())
            {
                spyProgress.Received(1).Reset(Arg.Any<int>());
                spyProgress.Received(7).Report(Arg.Any<SolvingStepDatum<char, int>>());

                Received.InOrder(() =>
                {
                    spyProgress.Reset(3);

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Simplifying,
                        0,
                        SolvingState.Assigning));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Assigning,
                        1,
                        SolvingState.Assigning,
                        new Assignment<char, int>('A', 0)));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Assigning,
                        1,
                        SolvingState.Backtracking));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Backtracking,
                        0,
                        SolvingState.Assigning));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Assigning,
                        1,
                        SolvingState.Assigning,
                        new Assignment<char, int>('A', 1)));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Assigning,
                        2,
                        SolvingState.Assigning,
                        new Assignment<char, int>('B', 0)));

                    spyProgress.Report(new SolvingStepDatum<char, int>(SolvingStepType.Assigning,
                        3,
                        SolvingState.Finished,
                        new Assignment<char, int>('C', 2)));
                });
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
        public async Task SolveAsync_ProgressArgIsNull_Throws()
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
                .WithMessage("Value cannot be null. (Parameter 'progress')");
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
                _ = await sut.SolveAsync(binaryCsp, progressReporter, cancellationToken: cts.Token);
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
                result.SearchAlgorithm.Should().HaveCheckingStrategy(checkingStrategy)
                    .And.HaveOrderingStrategy(orderingStrategy);
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
