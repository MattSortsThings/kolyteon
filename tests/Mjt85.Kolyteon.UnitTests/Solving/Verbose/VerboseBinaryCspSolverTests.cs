using FluentAssertions.Execution;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies;
using Mjt85.Kolyteon.Solving.Verbose;
using Mjt85.Kolyteon.UnitTests.Helpers;
using Moq;

namespace Mjt85.Kolyteon.UnitTests.Solving.Verbose;

/// <summary>
///     Unit tests for the <see cref="VerboseBinaryCspSolver{V,D}" /> class, parametrized over the Map Colouring problem
///     types, mostly using mocked dependencies.
/// </summary>
public sealed class VerboseBinaryCspSolverTests
{
    private static readonly Region R0 = Region.FromId("R0");
    private static readonly Region R1 = Region.FromId("R1");

    private static VerboseBinaryCspSolverBuilder GetVerboseBinaryCspSolver() => new();

    internal class VerboseBinaryCspSolverBuilder
    {
        private IOrderingStrategy? _orderingStrategy;
        private IOrderingStrategyFactory? _orderingStrategyFactory;
        private ISearchStrategy<Region, Colour>? _searchStrategy;
        private ISearchStrategyFactory<Region, Colour>? _searchStrategyFactory;

        public VerboseBinaryCspSolverBuilder WithSearchStrategyFactory(ISearchStrategyFactory<Region, Colour> factory)
        {
            _searchStrategyFactory = factory;

            return this;
        }

        public VerboseBinaryCspSolverBuilder WithOrderingStrategyFactory(IOrderingStrategyFactory factory)
        {
            _orderingStrategyFactory = factory;

            return this;
        }

        public VerboseBinaryCspSolverBuilder WithSearchStrategy(ISearchStrategy<Region, Colour> strategy)
        {
            _searchStrategy = strategy;

            return this;
        }

        public VerboseBinaryCspSolverBuilder WithOrderingStrategy(IOrderingStrategy strategy)
        {
            _orderingStrategy = strategy;

            return this;
        }

        public VerboseBinaryCspSolver<Region, Colour> Build() =>
            new(_searchStrategyFactory!,
                _orderingStrategyFactory!,
                _searchStrategy!,
                _orderingStrategy!,
                TimeSpan.Zero);
    }

    [UnitTest]
    public sealed class SolveAsync_Method
    {
        [Fact]
        public async Task BinaryCspModelsSolvableMapColouringPuzzle_SendsExpectedNotificationsToProgress()
        {
            // Arrange
            MapColouringPuzzle puzzle = MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black, Colour.White)
                .AddRegionWithColours(R1, Colour.Black)
                .SetAsNeighbours(R0, R1)
                .Build();

            MapColouringBinaryCsp binaryCsp = new(2);
            binaryCsp.Model(puzzle);

            VerboseBinaryCspSolver<Region, Colour> solver = CreateBinaryCspSolver
                .WithInitialCapacity(3)
                .AndInitialSearchStrategy(Search.Backtracking)
                .AndInitialOrderingStrategy(Ordering.None)
                .Verbose()
                .WithInitialStepDelay(TimeSpan.Zero)
                .Build<Region, Colour>();

            FakeProgress fakeProgress = new();

            // Act
            _ = await solver.SolveAsync(binaryCsp, fakeProgress);

            // Assert
            fakeProgress.Notifications.Should().SatisfyRespectively(first =>
            {
                first.StepType.Should().Be(StepType.Setup);
                first.CurrentSearchState.Should().Be(SearchState.Safe);
                first.CurrentSearchLevel.Should().Be(0);
                first.SearchTreeLeafLevel.Should().Be(2);
                first.LatestAssignment.Should().BeNull();
            }, second =>
            {
                second.StepType.Should().Be(StepType.Visiting);
                second.CurrentSearchState.Should().Be(SearchState.Safe);
                second.CurrentSearchLevel.Should().Be(1);
                second.SearchTreeLeafLevel.Should().Be(2);
                second.LatestAssignment.Should().Be(new Assignment<Region, Colour>(R0, Colour.Black));
            }, third =>
            {
                third.StepType.Should().Be(StepType.Visiting);
                third.CurrentSearchState.Should().Be(SearchState.Unsafe);
                third.CurrentSearchLevel.Should().Be(1);
                third.SearchTreeLeafLevel.Should().Be(2);
                third.LatestAssignment.Should().BeNull();
            }, fourth =>
            {
                fourth.StepType.Should().Be(StepType.Backtracking);
                fourth.CurrentSearchState.Should().Be(SearchState.Safe);
                fourth.CurrentSearchLevel.Should().Be(0);
                fourth.SearchTreeLeafLevel.Should().Be(2);
                fourth.LatestAssignment.Should().BeNull();
            }, fifth =>
            {
                fifth.StepType.Should().Be(StepType.Visiting);
                fifth.CurrentSearchState.Should().Be(SearchState.Safe);
                fifth.CurrentSearchLevel.Should().Be(1);
                fifth.SearchTreeLeafLevel.Should().Be(2);
                fifth.LatestAssignment.Should().Be(new Assignment<Region, Colour>(R0, Colour.White));
            }, sixth =>
            {
                sixth.StepType.Should().Be(StepType.Visiting);
                sixth.CurrentSearchState.Should().Be(SearchState.Final);
                sixth.CurrentSearchLevel.Should().Be(2);
                sixth.SearchTreeLeafLevel.Should().Be(2);
                sixth.LatestAssignment.Should().Be(new Assignment<Region, Colour>(R1, Colour.Black));
            });
        }

        [Fact]
        public async Task BinaryCspModelsUnsolvableMapColouringPuzzle_SendsExpectedNotificationsToProgress()
        {
            // Arrange
            MapColouringPuzzle puzzle = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R0, R1])
                .SetAsNeighbours(R0, R1)
                .Build();

            MapColouringBinaryCsp binaryCsp = new(2);
            binaryCsp.Model(puzzle);

            VerboseBinaryCspSolver<Region, Colour> solver = CreateBinaryCspSolver
                .WithInitialCapacity(3)
                .AndInitialSearchStrategy(Search.Backtracking)
                .AndInitialOrderingStrategy(Ordering.None)
                .Verbose()
                .WithInitialStepDelay(TimeSpan.Zero)
                .Build<Region, Colour>();

            FakeProgress fakeProgress = new();

            // Act
            _ = await solver.SolveAsync(binaryCsp, fakeProgress);

            // Assert
            fakeProgress.Notifications.Should().SatisfyRespectively(first =>
            {
                first.StepType.Should().Be(StepType.Setup);
                first.CurrentSearchState.Should().Be(SearchState.Safe);
                first.CurrentSearchLevel.Should().Be(0);
                first.SearchTreeLeafLevel.Should().Be(2);
                first.LatestAssignment.Should().BeNull();
            }, second =>
            {
                second.StepType.Should().Be(StepType.Visiting);
                second.CurrentSearchState.Should().Be(SearchState.Safe);
                second.CurrentSearchLevel.Should().Be(1);
                second.SearchTreeLeafLevel.Should().Be(2);
                second.LatestAssignment.Should().Be(new Assignment<Region, Colour>(R0, Colour.Black));
            }, third =>
            {
                third.StepType.Should().Be(StepType.Visiting);
                third.CurrentSearchState.Should().Be(SearchState.Unsafe);
                third.CurrentSearchLevel.Should().Be(1);
                third.SearchTreeLeafLevel.Should().Be(2);
                third.LatestAssignment.Should().BeNull();
            }, fourth =>
            {
                fourth.StepType.Should().Be(StepType.Backtracking);
                fourth.CurrentSearchState.Should().Be(SearchState.Unsafe);
                fourth.CurrentSearchLevel.Should().Be(0);
                fourth.SearchTreeLeafLevel.Should().Be(2);
                fourth.LatestAssignment.Should().BeNull();
            }, fifth =>
            {
                fifth.StepType.Should().Be(StepType.Backtracking);
                fifth.CurrentSearchState.Should().Be(SearchState.Final);
                fifth.CurrentSearchLevel.Should().Be(-1);
                fifth.SearchTreeLeafLevel.Should().Be(2);
                fifth.LatestAssignment.Should().BeNull();
            });
        }

        [Fact]
        [Category("ClearBoxTest")]
        public async Task BinaryCspFoundToHaveNoSolutionDuringSetupStep_ReturnsResultWithExpectedProperties()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours()
                .AddRegion(R0)
                .Build());

            Mock<ISearchStrategy<Region, Colour>> stubSearchStrategy = new();
            stubSearchStrategy.SetupSequence(m => m.SearchState)
                .Returns(SearchState.Initial) // step 1: Setup
                .Returns(SearchState.Final);

            VerboseBinaryCspSolver<Region, Colour> sut = GetVerboseBinaryCspSolver()
                .WithSearchStrategyFactory(Mock.Of<ISearchStrategyFactory<Region, Colour>>())
                .WithOrderingStrategyFactory(Mock.Of<IOrderingStrategyFactory>())
                .WithSearchStrategy(stubSearchStrategy.Object)
                .WithOrderingStrategy(Mock.Of<IOrderingStrategy>())
                .Build();

            IProgress<StepNotification<Region, Colour>> dummyProgress = Mock.Of<IProgress<StepNotification<Region, Colour>>>();

            // Act
            Result<Region, Colour> result = await sut.SolveAsync(binaryCsp, dummyProgress);

            // Assert
            using (new AssertionScope())
            {
                result.Assignments.Should().BeEmpty();
                result.SetupSteps.Should().Be(1);
                result.VisitingSteps.Should().Be(0);
                result.BacktrackingSteps.Should().Be(0);
                result.TotalSteps.Should().Be(1);
            }
        }

        [Fact]
        [Category("ClearBoxTest")]
        public async Task SearchTerminatesAtRootLevel_ReturnsResultWithExpectedProperties()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegion(R0)
                .AddRegion(R1)
                .SetAsNeighbours(R0, R1)
                .Build());

            Mock<ISearchStrategy<Region, Colour>> stubSearchStrategy = new();
            stubSearchStrategy.SetupSequence(m => m.SearchState)
                .Returns(SearchState.Initial) // step 1: Setup
                .Returns(SearchState.Safe) // step 2: Visit
                .Returns(SearchState.Safe) // step 3: Visit
                .Returns(SearchState.Unsafe) // step 4: Backtrack
                .Returns(SearchState.Unsafe) // step 5: Backtrack
                .Returns(SearchState.Final);

            stubSearchStrategy.Setup(m => m.GetAssignments())
                .Returns(Array.Empty<Assignment<Region, Colour>>());

            VerboseBinaryCspSolver<Region, Colour> sut = GetVerboseBinaryCspSolver()
                .WithSearchStrategyFactory(Mock.Of<ISearchStrategyFactory<Region, Colour>>())
                .WithOrderingStrategyFactory(Mock.Of<IOrderingStrategyFactory>())
                .WithSearchStrategy(stubSearchStrategy.Object)
                .WithOrderingStrategy(Mock.Of<IOrderingStrategy>())
                .Build();

            IProgress<StepNotification<Region, Colour>> dummyProgress = Mock.Of<IProgress<StepNotification<Region, Colour>>>();

            // Act
            Result<Region, Colour> result = await sut.SolveAsync(binaryCsp, dummyProgress);

            // Assert
            using (new AssertionScope())
            {
                result.Assignments.Should().BeEmpty();
                result.SetupSteps.Should().Be(1);
                result.VisitingSteps.Should().Be(2);
                result.BacktrackingSteps.Should().Be(2);
                result.TotalSteps.Should().Be(5);
            }
        }

        [Fact]
        [Category("ClearBoxTest")]
        public async Task SearchTerminatesAtLeafLevel_ReturnsResultWithExpectedProperties()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black, Colour.White)
                .AddRegionWithColours(R1, Colour.Black)
                .SetAsNeighbours(R0, R1)
                .Build());

            Mock<ISearchStrategy<Region, Colour>> stubSearchStrategy = new();
            stubSearchStrategy.SetupSequence(m => m.SearchState)
                .Returns(SearchState.Initial) // step 1: Setup
                .Returns(SearchState.Safe) // step 2: Visit
                .Returns(SearchState.Safe) // step 3: Visit
                .Returns(SearchState.Unsafe) // step 4: Backtrack
                .Returns(SearchState.Safe) // step 5: Visit
                .Returns(SearchState.Safe) // step 6: Visit
                .Returns(SearchState.Final);
            stubSearchStrategy.Setup(m => m.GetAssignments())
                .Returns([
                    GetAssignment.WithVariable(R0).AndDomainValue(Colour.White),
                    GetAssignment.WithVariable(R1).AndDomainValue(Colour.White)
                ]);

            VerboseBinaryCspSolver<Region, Colour> sut = GetVerboseBinaryCspSolver()
                .WithSearchStrategyFactory(Mock.Of<ISearchStrategyFactory<Region, Colour>>())
                .WithOrderingStrategyFactory(Mock.Of<IOrderingStrategyFactory>())
                .WithSearchStrategy(stubSearchStrategy.Object)
                .WithOrderingStrategy(Mock.Of<IOrderingStrategy>())
                .Build();

            IProgress<StepNotification<Region, Colour>> dummyProgress = Mock.Of<IProgress<StepNotification<Region, Colour>>>();

            // Act
            Result<Region, Colour> result = await sut.SolveAsync(binaryCsp, dummyProgress);

            // Assert
            using (new AssertionScope())
            {
                result.Assignments.Should().HaveCount(2);
                result.SetupSteps.Should().Be(1);
                result.VisitingSteps.Should().Be(4);
                result.BacktrackingSteps.Should().Be(1);
                result.TotalSteps.Should().Be(6);
            }
        }

        [Fact]
        public async Task BinaryCspArgIsNull_Throws()
        {
            // Arrange
            VerboseBinaryCspSolver<Region, Colour> sut = GetVerboseBinaryCspSolver()
                .WithSearchStrategyFactory(Mock.Of<ISearchStrategyFactory<Region, Colour>>())
                .WithOrderingStrategyFactory(Mock.Of<IOrderingStrategyFactory>())
                .WithSearchStrategy(Mock.Of<ISearchStrategy<Region, Colour>>())
                .WithOrderingStrategy(Mock.Of<IOrderingStrategy>())
                .Build();

            IProgress<StepNotification<Region, Colour>> dummyProgress = Mock.Of<IProgress<StepNotification<Region, Colour>>>();

            // Act
            Func<Task> act = async () => _ = await sut.SolveAsync(null!, dummyProgress);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'binaryCsp')");
        }

        [Fact]
        public async Task BinaryCspArgIsNotModellingAProblem_Throws()
        {
            // Arrange
            VerboseBinaryCspSolver<Region, Colour> sut = GetVerboseBinaryCspSolver()
                .WithSearchStrategyFactory(Mock.Of<ISearchStrategyFactory<Region, Colour>>())
                .WithOrderingStrategyFactory(Mock.Of<IOrderingStrategyFactory>())
                .WithSearchStrategy(Mock.Of<ISearchStrategy<Region, Colour>>())
                .WithOrderingStrategy(Mock.Of<IOrderingStrategy>())
                .Build();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.EmptyWithCapacity(1);

            IProgress<StepNotification<Region, Colour>> dummyProgress = Mock.Of<IProgress<StepNotification<Region, Colour>>>();

            // Act
            Func<Task> act = async () => _ = await sut.SolveAsync(binaryCsp, dummyProgress);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Binary CSP is not modelling a problem.");
        }

        [Category("ClearBoxTest")]
        [Fact]
        public async Task CancellationRequested_Throws()
        {
            // Arrange
            VerboseBinaryCspSolver<Region, Colour> sut = GetVerboseBinaryCspSolver()
                .WithSearchStrategyFactory(Mock.Of<ISearchStrategyFactory<Region, Colour>>())
                .WithOrderingStrategyFactory(Mock.Of<IOrderingStrategyFactory>())
                .WithSearchStrategy(Mock.Of<ISearchStrategy<Region, Colour>>())
                .WithOrderingStrategy(Mock.Of<IOrderingStrategy>())
                .Build();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegion(R0)
                .AddRegion(R1)
                .Build());

            IProgress<StepNotification<Region, Colour>> dummyProgress = Mock.Of<IProgress<StepNotification<Region, Colour>>>();

            // Act
            Func<Task> act = async () =>
            {
                using var cts = new CancellationTokenSource(TimeSpan.Zero);
                _ = await sut.SolveAsync(binaryCsp, dummyProgress, cts.Token);
            };

            // Assert
            await act.Should().ThrowAsync<OperationCanceledException>()
                .WithMessage("The operation was canceled.");
        }
    }

    [UnitTest]
    public sealed class FluentBuilder
    {
        [Fact]
        public void BuildsInstanceWithSpecifiedInitialSettings()
        {
            // Arrange
            TimeSpan delay = TimeSpan.FromMilliseconds(1);

            // Act
            VerboseBinaryCspSolver<Region, Colour> result = CreateBinaryCspSolver
                .WithInitialCapacity(3)
                .AndInitialSearchStrategy(Search.Backtracking)
                .AndInitialOrderingStrategy(Ordering.None)
                .Verbose()
                .WithInitialStepDelay(delay)
                .Build<Region, Colour>();

            // Assert
            using (new AssertionScope())
            {
                result.Capacity.Should().Be(3);
                result.SearchStrategy.Should().Be(Search.Backtracking);
                result.OrderingStrategy.Should().Be(Ordering.None);
                result.StepDelay.Should().Be(delay);
            }
        }

        [Fact]
        public void WithInitialCapacity_CapacityArgIsNegative_Throws()
        {
            // Act
            Action act = () => CreateBinaryCspSolver
                .WithInitialCapacity(-1)
                .AndInitialSearchStrategy(Search.Backtracking)
                .AndInitialOrderingStrategy(Ordering.None)
                .Verbose()
                .WithInitialStepDelay(TimeSpan.Zero)
                .Build<Region, Colour>();

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be negative. (Parameter 'capacity')\nActual value was -1.");
        }
    }
}
