using Mjt85.Kolyteon.Solving;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;

namespace Mjt85.Kolyteon.UnitTests.Solving.Internals.OrderingStrategies;

/// <summary>
///     Unit tests for the internal <see cref="OrderingStrategyFactory" /> class.
/// </summary>
public sealed class OrderingStrategyFactoryTests
{
    [UnitTest]
    public sealed class CreateInstance_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsInstanceOfExpectedType(Ordering strategy, Type expected)
        {
            // Arrange
            OrderingStrategyFactory sut = new();

            // Act
            IOrderingStrategy result = sut.CreateInstance(strategy);

            // Assert
            result.Should().BeOfType(expected)
                .And.BeAssignableTo<IOrderingStrategy>()
                .Which.Identifier.Should().Be(strategy);
        }

        private sealed class TestCases : TheoryData<Ordering, Type>
        {
            public TestCases()
            {
                Add(Ordering.None, typeof(NOStrategy));
                Add(Ordering.Brelaz, typeof(BZStrategy));
                Add(Ordering.MaxCardinality, typeof(MCStrategy));
                Add(Ordering.MaxTightness, typeof(MTStrategy));
            }
        }
    }
}
