using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Solving.Internals.OrderingStrategies;

/// <summary>
///     Unit tests for the internal <see cref="BZStrategy" /> class.
/// </summary>
public sealed class BZStrategyTests
{
    [UnitTest]
    public sealed class Identifier_Property
    {
        [Fact]
        public void Is_Brelaz_OrderingEnumValue()
        {
            // Arrange
            BZStrategy sut = new();

            // Assert
            sut.Identifier.Should().Be(Ordering.Brelaz);
        }
    }

    [UnitTest]
    public sealed class GetLevelOfOptimalNode_Method
    {
        [Fact]
        public void PresentAndFutureNodesAllHaveSameRemainingCandidatesAndSameDegree_ReturnsSearchLevel()
        {
            // Arrange
            BZStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, RemainingCandidates = 99
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void PresentNodeHasMinRemainingCandidates_ReturnsSearchLevel()
        {
            // Arrange
            BZStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, RemainingCandidates = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, RemainingCandidates = 99
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void FutureNodeHasMinRemainingCandidates_ReturnsLevelOfFutureNode()
        {
            // Arrange
            BZStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, RemainingCandidates = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, RemainingCandidates = 99
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(4);
        }

        [Fact]
        public void PresentAndFutureNodesTiedOnMinRemainingCandidates_PresentNodeHasMaxDegree_ReturnsSearchLevel()
        {
            // Arrange
            BZStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, RemainingCandidates = 99, AdjacentVariableIndexes = [1],
                    SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, RemainingCandidates = 0, AdjacentVariableIndexes = [0],
                    SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, RemainingCandidates = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, RemainingCandidates = 0
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void PresentAndFutureNodesTiedOnMinRemainingCandidates_FutureNodeHasMaxDegree_ReturnsLevelOfFutureNode()
        {
            // Arrange
            BZStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, RemainingCandidates = 99, AdjacentVariableIndexes = [5],
                    SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, RemainingCandidates = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, RemainingCandidates = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, RemainingCandidates = 0, AdjacentVariableIndexes = [0],
                    SumTightness = 0.1
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(5);
        }

        [Fact]
        public void PresentAndFutureNodesTiedOnMinRemainingCandidatesAndTiedOnMaxDegree_ReturnsSearchLevel()
        {
            // Arrange
            BZStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, RemainingCandidates = 99, AdjacentVariableIndexes = [1, 2, 4],
                    SumTightness = 0.3
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, RemainingCandidates = 0, AdjacentVariableIndexes = [0],
                    SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2,
                    RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, RemainingCandidates = 0, AdjacentVariableIndexes = [0],
                    SumTightness = 0.1
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4,
                    RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, RemainingCandidates = 0, AdjacentVariableIndexes = [0],
                    SumTightness = 0.1
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(searchLevel);
        }

        [Fact]
        public void FutureNodesTiedOnMinRemainingCandidatesAndMaxDegree_BreaksTieUsingMinLevel_ReturnsLevelOfWinningNode()
        {
            // Arrange
            BZStrategy sut = new();

            IVisitableNode[] searchTree =
            [
                new FakeVisitableNode
                {
                    VariableIndex = 0, SearchTreeLevel = 0, RemainingCandidates = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 1, SearchTreeLevel = 1, RemainingCandidates = 99
                },
                new FakeVisitableNode
                {
                    VariableIndex = 2, SearchTreeLevel = 2, RemainingCandidates = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 3, SearchTreeLevel = 3, RemainingCandidates = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 4, SearchTreeLevel = 4, RemainingCandidates = 0
                },
                new FakeVisitableNode
                {
                    VariableIndex = 5, SearchTreeLevel = 5, RemainingCandidates = 0
                }
            ];

            const int searchLevel = 1;

            // Act
            var result = sut.GetLevelOfOptimalNode(searchTree, searchLevel);

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public void SearchTreeHasOneNode_SearchLevelIsZero_ReturnsSearchLevel()
        {
            // Arrange
            BZStrategy sut = new();

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
        public void SearchTreeHasMultipleNodes_SearchLevelIsLastNonLeafLevel_AlwaysReturnsSearchLevel()
        {
            // Arrange
            BZStrategy sut = new();

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
