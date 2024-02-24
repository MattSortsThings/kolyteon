using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Solving;
using Mjt85.Kolyteon.Solving.SearchStrategies;
using Mjt85.Kolyteon.Solving.SearchStrategies.LookBack;
using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.UnitTests.Solving.Internals.SearchStrategies;

/// <summary>
///     Unit tests for the internal <see cref="SearchStrategyFactory{V,D}" /> class, parametrized over the Map Colouring
///     problem types.
/// </summary>
public static class SearchStrategyFactoryTests
{
    [UnitTest]
    public sealed class CreateInstance_Method
    {
        [Theory]
        [ClassData(typeof(TypeTestCases))]
        public void ReturnsInstanceOfExpectedType(Search strategy, Type expected)
        {
            // Arrange
            SearchStrategyFactory<Region, Colour> sut = new();

            const int arbitraryCapacity = 1;

            // Act
            ISearchStrategy<Region, Colour> result = sut.CreateInstance(strategy, arbitraryCapacity);

            // Assert
            result.Should().BeOfType(expected)
                .And.BeAssignableTo<ISearchStrategy<Region, Colour>>()
                .Which.Identifier.Should().Be(strategy);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(4)]
        public void ReturnsInstanceWithRequestedCapacity(int capacity)
        {
            // Arrange
            SearchStrategyFactory<Region, Colour> sut = new();

            const Search arbitraryStrategy = Search.Backtracking;

            // Act
            ISearchStrategy<Region, Colour> result = sut.CreateInstance(arbitraryStrategy, capacity);

            // Assert
            result.Capacity.Should().Be(capacity);
        }

        private sealed class TypeTestCases : TheoryData<Search, Type>
        {
            public TypeTestCases()
            {
                Add(Search.Backtracking, typeof(BTStrategy<,>));
                Add(Search.Backjumping, typeof(BJStrategy<,>));
                Add(Search.GraphBasedBackjumping, typeof(GBJStrategy<,>));
                Add(Search.ConflictDirectedBackjumping, typeof(CBJStrategy<,>));
            }
        }
    }
}
