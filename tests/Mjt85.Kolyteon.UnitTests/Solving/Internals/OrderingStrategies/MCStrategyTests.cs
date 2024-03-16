using Mjt85.Kolyteon.Solving;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Solving.Internals.OrderingStrategies;

/// <summary>
///     Unit tests for the internal <see cref="MCStrategy" /> class.
/// </summary>
public sealed class MCStrategyTests
{
    [UnitTest]
    public sealed class Identifier_Property
    {
        [Fact]
        public void Is_MaxCardinality_OrderingEnumValue()
        {
            // Arrange
            MCStrategy sut = new();

            // Assert
            sut.Identifier.Should().Be(Ordering.MaxCardinality);
        }
    }

    [UnitTest]
    public sealed class GetLevelOfOptimalNode_Method
    {
        [Fact]
        public void SearchLevelIsZero_ReturnsSearchLevel()
        {
            // Arrange
            MCStrategy sut = new();

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

            const int searchLevel = 0;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }


        [Fact]
        public void PresentAndFutureNodesAllHaveSameCardinalityAndDegree_ReturnsSearchLevel()
        {
            // Arrange
            MCStrategy sut = new();

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
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5
                }
            ];

            const int searchLevel = 2;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void PresentNodeHasMaxCardinality_ReturnsSearchLevel()
        {
            // Arrange
            MCStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [2], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, AdjacentVariableIndexes = [0, 5], SumTightness = 0.2
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, AdjacentVariableIndexes = [2], SumTightness = 0.1
                }
            ];

            const int searchLevel = 2;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void FutureNodeHasMaxCardinality_ReturnsLevelOfFutureNode()
        {
            // Arrange
            MCStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [5], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1
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
                    VariableIndex = 4, SearchTreeLevel = 4
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, AdjacentVariableIndexes = [0], SumTightness = 0.1
                }
            ];

            const int searchLevel = 2;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(5);
        }

        [Fact]
        public void PresentAndFutureNodesTiedOnMaxCardinality_PresentNodeHasMaxDegree_ReturnsSearchLevel()
        {
            // Arrange
            MCStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [2, 3], SumTightness = 0.2
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, AdjacentVariableIndexes = [0, 4], SumTightness = 0.2
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, AdjacentVariableIndexes = [0], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, AdjacentVariableIndexes = [2], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5
                }
            ];

            const int searchLevel = 2;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void PresentAndFutureNodesTiedOnMaxCardinality_FutureNodeHasMaxDegree_ReturnsLevelOfFutureNode()
        {
            // Arrange
            MCStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [2, 3], SumTightness = 0.2
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, AdjacentVariableIndexes = [0], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, AdjacentVariableIndexes = [0, 4], SumTightness = 0.2
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, AdjacentVariableIndexes = [3], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5
                }
            ];

            const int searchLevel = 2;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(3);
        }

        [Fact]
        public void PresentAndFutureNodesTiedOnMaxCardinalityAndTiedOnMaxDegree_ReturnsSearchLevel()
        {
            // Arrange
            MCStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [2, 5], SumTightness = 0.2
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, AdjacentVariableIndexes = [0], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, AdjacentVariableIndexes = [0], SumTightness = 0.1
                }
            ];

            const int searchLevel = 2;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void FutureNodesTiedOnMinCandidatesCountAndMaxDegree_BreaksTieUsingMinLevel_ReturnsLevelOfWinningNode()
        {
            // Arrange
            MCStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, AdjacentVariableIndexes = [4, 5], SumTightness = 0.2
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, AdjacentVariableIndexes = [4, 5], SumTightness = 0.2
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, AdjacentVariableIndexes = [4], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, AdjacentVariableIndexes = [5], SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, AdjacentVariableIndexes = [0, 1, 2]
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, AdjacentVariableIndexes = [0, 1, 3]
                }
            ];

            const int searchLevel = 2;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(4);
        }

        [Fact]
        public void SearchTreeHasOneNode_SearchLevelIsZero_ReturnsSearchLevel()
        {
            // Arrange
            MCStrategy sut = new();

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
            MCStrategy sut = new();

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
