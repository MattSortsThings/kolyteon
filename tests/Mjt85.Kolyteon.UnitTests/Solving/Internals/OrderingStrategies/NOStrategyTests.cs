using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Solving.Internals.OrderingStrategies;

/// <summary>
///     Unit tests for the internal <see cref="NOStrategy" /> class.
/// </summary>
public sealed class NOStrategyTests
{
    [UnitTest]
    public sealed class Identifier_Property
    {
        [Fact]
        public void Is_None_OrderingEnumValue()
        {
            // Arrange
            NOStrategy sut = new();

            // Assert
            sut.Identifier.Should().Be(Ordering.None);
        }
    }

    [UnitTest]
    public sealed class GetLevelOfOptimalNode_Method
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void AlwaysReturnsSearchLevel(int searchLevel)
        {
            // Arrange
            NOStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    SearchTreeLevel = 0, VariableIndex = 0
                },
                new FakeVisitableNode
                {
                    SearchTreeLevel = 1, VariableIndex = 1
                },
                new FakeVisitableNode
                {
                    SearchTreeLevel = 2, VariableIndex = 2
                },
                new FakeVisitableNode
                {
                    SearchTreeLevel = 3, VariableIndex = 3
                }
            ];

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }
    }
}
