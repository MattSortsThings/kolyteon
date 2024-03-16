using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Solving.Internals.OrderingStrategies;

/// <summary>
///     Unit tests for the internal <see cref="MTStrategy" /> class.
/// </summary>
public sealed class MTStrategyTests
{
    [UnitTest]
    public sealed class Identifier_Property
    {
        [Fact]
        public void Is_MaxTightness_OrderingEnumValue()
        {
            // Arrange
            MTStrategy sut = new();

            // Assert
            sut.Identifier.Should().Be(Ordering.MaxTightness);
        }
    }

    [UnitTest]
    public sealed class GetLevelOfOptimalNode_Method
    {
        [Fact]
        public void PresentAndFutureNodesAllHaveSameSumTightnessTo9DPAndSameDegree_ReturnsSearchLevel()
        {
            // Arrange
            MTStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [1, 2, 3, 4, 5],
                    SumTightness = 0.000000005
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, AdjacentVariableIndexes = [0], SumTightness = 0.000000001
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, AdjacentVariableIndexes = [0], SumTightness = 0.000000001
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, AdjacentVariableIndexes = [0], SumTightness = 0.000000001
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, AdjacentVariableIndexes = [0], SumTightness = 0.000000001
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, AdjacentVariableIndexes = [0], SumTightness = 0.000000001
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void PresentNodeHasMaxSumTightness_ReturnsSearchLevel()
        {
            // Arrange
            MTStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, SumTightness = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, AdjacentVariableIndexes = [2, 4], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, AdjacentVariableIndexes = [1], SumTightness = 0.02
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, SumTightness = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, AdjacentVariableIndexes = [1], SumTightness = 0.08
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, SumTightness = 0
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void FutureNodeHasMaxSumTightness_ReturnsLevelOfFutureNode()
        {
            // Arrange
            MTStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [1, 2, 4], SumTightness = 1.999
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, AdjacentVariableIndexes = [0], SumTightness = 0.5
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, AdjacentVariableIndexes = [0], SumTightness = 0.5
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, SumTightness = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, AdjacentVariableIndexes = [0], SumTightness = 0.999
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, SumTightness = 0
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(4);
        }

        [Fact]
        public void PresentAndFutureNodesTiedOnMaxSumTightness_PresentNodeHasMaxDegree_ReturnsSearchLevel()
        {
            // Arrange
            MTStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [1, 5], SumTightness = 2
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, AdjacentVariableIndexes = [0, 2], SumTightness = 1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, AdjacentVariableIndexes = [1], SumTightness = 0.5
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, SumTightness = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, SumTightness = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, AdjacentVariableIndexes = [0], SumTightness = 1
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void PresentAndFutureNodesTiedOnMaxSumTightness_FutureNodeHasMaxDegree_ReturnsLevelOfFutureNode()
        {
            // Arrange
            MTStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [1, 5], SumTightness = 0.75
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, AdjacentVariableIndexes = [0], SumTightness = 0.5
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, SumTightness = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, SumTightness = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, AdjacentVariableIndexes = [5], SumTightness = 0.25
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, AdjacentVariableIndexes = [0, 4], SumTightness = 0.5
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(5);
        }

        [Fact]
        public void PresentAndFutureNodesTiedOnMaxSumTightnessAndTiedOnMaxDegree_ReturnsSearchLevel()
        {
            // Arrange
            MTStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [1], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, AdjacentVariableIndexes = [0], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, AdjacentVariableIndexes = [3], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, AdjacentVariableIndexes = [2], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, SumTightness = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, SumTightness = 0
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void FutureNodesTiedOnMaxSumTightnessAndMaxDegree_BreaksTieUsingMinLevel_ReturnsLevelOfWinningNode()
        {
            // Arrange
            MTStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [2, 3, 4], SumTightness = 0.3
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, SumTightness = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, AdjacentVariableIndexes = [0], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, AdjacentVariableIndexes = [0], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, AdjacentVariableIndexes = [0], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, SumTightness = 0.0
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void SearchTreeHasOneNode_ReturnsSearchLevel()
        {
            // Arrange
            MTStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0
                }
            ];

            const int searchLevel = 0;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void SearchTreeHasMultipleNodes_SearchLevelIsLastNonLeafLevel_ReturnsSearchLevel()
        {
            // Arrange
            MTStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2
                }
            ];

            const int searchLevel = 2;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }
    }
}
