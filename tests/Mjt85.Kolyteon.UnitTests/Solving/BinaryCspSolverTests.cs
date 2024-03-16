using FluentAssertions.Execution;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies;
using Mjt85.Kolyteon.UnitTests.Helpers;
using Moq;

namespace Mjt85.Kolyteon.UnitTests.Solving;

/// <summary>
///     Unit tests for the <see cref="BinaryCspSolver{V,D}" /> class, parametrized over the Map Colouring problem types,
///     mostly using mocked dependencies.
/// </summary>
public static class BinaryCspSolverTests
{
    private static readonly Region R0 = Region.FromId("R0");
    private static readonly Region R1 = Region.FromId("R1");

    private static BinaryCspSolverBuilder GetBinaryCspSolver() => new();

    internal class BinaryCspSolverBuilder
    {
        private IOrderingStrategy? _orderingStrategy;
        private IOrderingStrategyFactory? _orderingStrategyFactory;
        private ISearchStrategy<Region, Colour>? _searchStrategy;
        private ISearchStrategyFactory<Region, Colour>? _searchStrategyFactory;

        public BinaryCspSolverBuilder WithSearchStrategyFactory(ISearchStrategyFactory<Region, Colour> factory)
        {
            _searchStrategyFactory = factory;

            return this;
        }

        public BinaryCspSolverBuilder WithOrderingStrategyFactory(IOrderingStrategyFactory factory)
        {
            _orderingStrategyFactory = factory;

            return this;
        }

        public BinaryCspSolverBuilder WithSearchStrategy(ISearchStrategy<Region, Colour> strategy)
        {
            _searchStrategy = strategy;

            return this;
        }

        public BinaryCspSolverBuilder WithOrderingStrategy(IOrderingStrategy strategy)
        {
            _orderingStrategy = strategy;

            return this;
        }

        public BinaryCspSolver<Region, Colour> Build() =>
            new(_searchStrategyFactory!,
                _orderingStrategyFactory!,
                _searchStrategy!,
                _orderingStrategy!);
    }

    [UnitTest]
    public sealed class Solve_Method
    {
        [Fact]
        [Category("ClearBoxTest")]
        public void BinaryCspFoundToHaveNoSolutionDuringSetupStep_ReturnsResultWithExpectedProperties()
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

            BinaryCspSolver<Region, Colour> sut = GetBinaryCspSolver()
                .WithSearchStrategyFactory(Mock.Of<ISearchStrategyFactory<Region, Colour>>())
                .WithOrderingStrategyFactory(Mock.Of<IOrderingStrategyFactory>())
                .WithSearchStrategy(stubSearchStrategy.Object)
                .WithOrderingStrategy(Mock.Of<IOrderingStrategy>())
                .Build();

            // Act
            Result<Region, Colour> result = sut.Solve(binaryCsp);

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
        public void SearchTerminatesAtRootLevel_ReturnsResultWithExpectedProperties()
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

            BinaryCspSolver<Region, Colour> sut = GetBinaryCspSolver()
                .WithSearchStrategyFactory(Mock.Of<ISearchStrategyFactory<Region, Colour>>())
                .WithOrderingStrategyFactory(Mock.Of<IOrderingStrategyFactory>())
                .WithSearchStrategy(stubSearchStrategy.Object)
                .WithOrderingStrategy(Mock.Of<IOrderingStrategy>())
                .Build();

            // Act
            Result<Region, Colour> result = sut.Solve(binaryCsp);

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
        public void SearchTerminatesAtLeafLevel_ReturnsResultWithExpectedProperties()
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

            BinaryCspSolver<Region, Colour> sut = GetBinaryCspSolver()
                .WithSearchStrategyFactory(Mock.Of<ISearchStrategyFactory<Region, Colour>>())
                .WithOrderingStrategyFactory(Mock.Of<IOrderingStrategyFactory>())
                .WithSearchStrategy(stubSearchStrategy.Object)
                .WithOrderingStrategy(Mock.Of<IOrderingStrategy>())
                .Build();

            // Act
            Result<Region, Colour> result = sut.Solve(binaryCsp);

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
        public void BinaryCspArgIsNull_Throws()
        {
            // Arrange
            BinaryCspSolver<Region, Colour> sut = GetBinaryCspSolver()
                .WithSearchStrategyFactory(Mock.Of<ISearchStrategyFactory<Region, Colour>>())
                .WithOrderingStrategyFactory(Mock.Of<IOrderingStrategyFactory>())
                .WithSearchStrategy(Mock.Of<ISearchStrategy<Region, Colour>>())
                .WithOrderingStrategy(Mock.Of<IOrderingStrategy>())
                .Build();

            // Act
            Action act = () => sut.Solve(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'binaryCsp')");
        }

        [Fact]
        public void BinaryCspArgIsNotModellingAProblem_Throws()
        {
            // Arrange
            BinaryCspSolver<Region, Colour> sut = GetBinaryCspSolver()
                .WithSearchStrategyFactory(Mock.Of<ISearchStrategyFactory<Region, Colour>>())
                .WithOrderingStrategyFactory(Mock.Of<IOrderingStrategyFactory>())
                .WithSearchStrategy(Mock.Of<ISearchStrategy<Region, Colour>>())
                .WithOrderingStrategy(Mock.Of<IOrderingStrategy>())
                .Build();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.EmptyWithCapacity(1);

            // Act
            Action act = () => sut.Solve(binaryCsp);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Binary CSP is not modelling a problem.");
        }

        [Category("ClearBoxTest")]
        [Fact]
        public void CancellationRequested_Throws()
        {
            // Arrange
            BinaryCspSolver<Region, Colour> sut = GetBinaryCspSolver()
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

            // Act
            Action act = () =>
            {
                using var cts = new CancellationTokenSource(TimeSpan.Zero);
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
        public void BuildsInstanceWithSpecifiedInitialSettings()
        {
            // Act
            BinaryCspSolver<Region, Colour> result = BinaryCspSolver<Region, Colour>.Create()
                .WithInitialCapacity(3)
                .AndInitialSearchStrategy(Search.Backtracking)
                .AndInitialOrderingStrategy(Ordering.None)
                .Build();

            // Assert
            using (new AssertionScope())
            {
                result.Capacity.Should().Be(3);
                result.SearchStrategy.Should().Be(Search.Backtracking);
                result.OrderingStrategy.Should().Be(Ordering.None);
            }
        }

        [Fact]
        public void WithInitialCapacity_CapacityArgIsNegative_Throws()
        {
            // Act
            Action act = () => BinaryCspSolver<Region, Colour>.Create()
                .WithInitialCapacity(-1)
                .AndInitialSearchStrategy(Search.Backtracking)
                .AndInitialOrderingStrategy(Ordering.None)
                .Build();

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be negative. (Parameter 'capacity')\nActual value was -1.");
        }
    }
}
