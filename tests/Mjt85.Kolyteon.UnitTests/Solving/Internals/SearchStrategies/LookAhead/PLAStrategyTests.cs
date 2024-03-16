using FluentAssertions.Execution;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookAhead;
using Mjt85.Kolyteon.UnitTests.Helpers;
using Moq;

namespace Mjt85.Kolyteon.UnitTests.Solving.Internals.SearchStrategies.LookAhead;

/// <summary>
///     Unit tests for the internal <see cref="PLAStrategy{V,D}" /> class, parametrized over the Map Colouring problem
///     types.
/// </summary>
public static class PLAStrategyTests
{
    private static readonly Region R0 = Region.FromId("R0");
    private static readonly Region R1 = Region.FromId("R1");
    private static readonly Region R2 = Region.FromId("R2");
    private static readonly Region R3 = Region.FromId("R3");

    private static Mock<IOrderingStrategy> MockOrderingStrategyThatNeverReorders()
    {
        Mock<IOrderingStrategy> mock = new();
        mock.Setup(m => m.GetLevelOfOptimalNode(It.IsAny<IList<PLANode<Region, Colour>>>(), It.IsAny<int>()))
            .Returns((IList<PLANode<Region, Colour>> _, int searchLevel) => searchLevel);

        return mock;
    }

    [UnitTest]
    public sealed class Identifier_Property
    {
        [Fact]
        public void Is_PartialLookingAhead_SearchEnumValue()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(0);

            // Assert
            sut.Identifier.Should().Be(Search.PartialLookingAhead);
        }
    }

    [UnitTest]
    [Category("ClearBoxTest")]
    public sealed class StateTransitions
    {
        [Fact]
        public void StateIsInitial_SetupInvoked_NodeHasZeroCandidates_SearchLevelIsRootLevel_StateIsFinal()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(3);
            Mock<IOrderingStrategy> mockOrderingStrategy = MockOrderingStrategyThatNeverReorders();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black)
                .AddRegionWithColours(R1)
                .AddRegionWithColours(R2, Colour.Black)
                .Build());

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(0);
                sut.SearchState.Should().Be(SearchState.Initial);
                sut.SearchLevel.Should().Be(-1);
                sut.SearchTree.Should().BeEmpty();
            }

            // Act
            sut.Setup(binaryCsp, mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(3);
                sut.SearchState.Should().Be(SearchState.Final);
                sut.SearchLevel.Should().Be(-1);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(-1);
                    at0.Candidates.Should().Equal(0);
                    at0.RejectedCandidates.Should().BeEmpty();
                    at0.Successors.Should().BeEmpty();
                    at0.PruningMemos.Should().BeEmpty();
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().BeEmpty();
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(0);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                });
            }
        }

        [Fact]
        public void StateIsInitial_SetupInvoked_RootArcConsistencyEnforcementExhaustsNode_SearchLevelIsRootLevel_StateIsFinal()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(3);
            Mock<IOrderingStrategy> mockOrderingStrategy = MockOrderingStrategyThatNeverReorders();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black, Colour.White)
                .AddRegionWithColours(R1, Colour.Black)
                .AddRegionWithColours(R2, Colour.Black)
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R1, R2)
                .Build());

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(0);
                sut.SearchState.Should().Be(SearchState.Initial);
                sut.SearchLevel.Should().Be(-1);
                sut.SearchTree.Should().BeEmpty();
            }

            // Act
            sut.Setup(binaryCsp, mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(3);
                sut.SearchState.Should().Be(SearchState.Final);
                sut.SearchLevel.Should().Be(-1);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(-1);
                    at0.Candidates.Should().Equal(1);
                    at0.RejectedCandidates.Should().BeEmpty();
                    at0.Successors.Should().BeEmpty();
                    at0.PruningMemos.Should().BeEmpty();
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().BeEmpty();
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(0);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                });
            }
        }

        [Fact]
        public void StateIsInitial_SetupInvoked_RootLevelSafetyCheckPasses_SearchLevelIsZero_StateIsSafe()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(3);
            Mock<IOrderingStrategy> mockOrderingStrategy = MockOrderingStrategyThatNeverReorders();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1, R2])
                .SetAsNeighbours(R0, R2)
                .SetAsNeighbours(R1, R2)
                .Build());

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(0);
                sut.SearchState.Should().Be(SearchState.Initial);
                sut.SearchLevel.Should().Be(-1);
                sut.SearchTree.Should().BeEmpty();
            }

            // Act
            sut.Setup(binaryCsp, mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(3);
                sut.SearchState.Should().Be(SearchState.Safe);
                sut.SearchLevel.Should().Be(0);
                mockOrderingStrategy.Verify(m => m.GetLevelOfOptimalNode(sut.SearchTree, 0), Times.Once);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(-1);
                    at0.Candidates.Should().Equal(0, 1);
                    at0.RejectedCandidates.Should().BeEmpty();
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(2));
                    at0.PruningMemos.Should().BeEmpty();
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().Equal(0, 1);
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(0, 1);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                });
            }
        }

        [Fact]
        public void StateIsSafe_VisitInvoked_AssignmentMade_SearchLevelIsNextNonLeafLevel_StateIsSafe()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(4);
            Mock<IOrderingStrategy> mockOrderingStrategy = MockOrderingStrategyThatNeverReorders();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1, R2, R3])
                .SetAsNeighbours(R0, R2)
                .SetAsNeighbours(R1, R3)
                .SetAsNeighbours(R2, R3)
                .Build());

            sut.Setup(binaryCsp, mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(4);
                sut.SearchState.Should().Be(SearchState.Safe);
                sut.SearchLevel.Should().Be(0);
                mockOrderingStrategy.Verify(m => m.GetLevelOfOptimalNode(sut.SearchTree, 0), Times.Once);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(-1);
                    at0.Candidates.Should().Equal(0, 1);
                    at0.RejectedCandidates.Should().BeEmpty();
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(2));
                    at0.PruningMemos.Should().BeEmpty();
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().Equal(0, 1);
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(0, 1);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                }, at3 =>
                {
                    at3.VariableIndex.Should().Be(3);
                    at3.SearchTreeLevel.Should().Be(3);
                    at3.BacktrackLevel.Should().Be(2);
                    at3.DomainValueIndex.Should().Be(-1);
                    at3.Candidates.Should().Equal(0, 1);
                    at3.RejectedCandidates.Should().BeEmpty();
                    at3.Successors.Should().BeEmpty();
                    at3.PruningMemos.Should().BeEmpty();
                });
            }

            // Act
            sut.Visit(mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(4);
                sut.SearchState.Should().Be(SearchState.Safe);
                sut.SearchLevel.Should().Be(1);
                mockOrderingStrategy.Verify(m => m.GetLevelOfOptimalNode(sut.SearchTree, 1), Times.Once);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(0);
                    at0.Candidates.Should().Equal(0, 1);
                    at0.RejectedCandidates.Should().BeEmpty();
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(2));
                    at0.PruningMemos.Should().SatisfyRespectively(first =>
                    {
                        first.PrunedNode.VariableIndex.Should().Be(2);
                        first.PrunedCandidate.Should().Be(0);
                    });
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().Equal(0, 1);
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(3));
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(1);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                }, at3 =>
                {
                    at3.VariableIndex.Should().Be(3);
                    at3.SearchTreeLevel.Should().Be(3);
                    at3.BacktrackLevel.Should().Be(2);
                    at3.DomainValueIndex.Should().Be(-1);
                    at3.Candidates.Should().Equal(0, 1);
                    at3.RejectedCandidates.Should().BeEmpty();
                    at3.Successors.Should().BeEmpty();
                    at3.PruningMemos.Should().BeEmpty();
                });
            }
        }

        [Fact]
        public void StateIsSafe_VisitInvoked_AssignmentMade_SearchLevelIsLeafLevel_StateIsFinal()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(3);
            Mock<IOrderingStrategy> mockOrderingStrategy = MockOrderingStrategyThatNeverReorders();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1])
                .SetAsNeighbours(R0, R1)
                .Build());

            sut.Setup(binaryCsp, mockOrderingStrategy.Object);
            sut.Visit(mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.SearchState.Should().Be(SearchState.Safe);
                sut.SearchLevel.Should().Be(1);
                mockOrderingStrategy.Verify(m => m.GetLevelOfOptimalNode(sut.SearchTree, 0), Times.Once);
                mockOrderingStrategy.Verify(m => m.GetLevelOfOptimalNode(sut.SearchTree, 1), Times.Once);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(0);
                    at0.Candidates.Should().Equal(0, 1);
                    at0.RejectedCandidates.Should().BeEmpty();
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(1));
                    at0.PruningMemos.Should().SatisfyRespectively(first =>
                    {
                        first.PrunedNode.VariableIndex.Should().Be(1);
                        first.PrunedCandidate.Should().Be(0);
                    });
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().Equal(1);
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                });
            }

            // Act
            sut.Visit(mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.SearchState.Should().Be(SearchState.Final);
                sut.SearchLevel.Should().Be(2);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(0);
                    at0.Candidates.Should().Equal(0, 1);
                    at0.RejectedCandidates.Should().BeEmpty();
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(1));
                    at0.PruningMemos.Should().SatisfyRespectively(first =>
                    {
                        first.PrunedNode.VariableIndex.Should().Be(1);
                        first.PrunedCandidate.Should().Be(0);
                    });
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(1);
                    at1.Candidates.Should().Equal(1);
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                });
            }
        }

        [Fact]
        public void StateIsSafe_VisitInvoked_CandidatesExhausted_SearchLevelIsUnchanged_StateIsUnsafe()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(4);
            Mock<IOrderingStrategy> mockOrderingStrategy = MockOrderingStrategyThatNeverReorders();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1, R2, R3])
                .SetAsNeighbours(R0, R2)
                .SetAsNeighbours(R0, R3)
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R2, R3)
                .Build());

            sut.Setup(binaryCsp, mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(4);
                sut.SearchState.Should().Be(SearchState.Safe);
                sut.SearchLevel.Should().Be(0);
                mockOrderingStrategy.Verify(m => m.GetLevelOfOptimalNode(sut.SearchTree, 0), Times.Once);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(-1);
                    at0.Candidates.Should().Equal(0, 1);
                    at0.RejectedCandidates.Should().BeEmpty();
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(2), second =>
                        second.VariableIndex.Should().Be(3));
                    at0.PruningMemos.Should().BeEmpty();
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().Equal(0, 1);
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(0, 1);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                }, at3 =>
                {
                    at3.VariableIndex.Should().Be(3);
                    at3.SearchTreeLevel.Should().Be(3);
                    at3.BacktrackLevel.Should().Be(2);
                    at3.DomainValueIndex.Should().Be(-1);
                    at3.Candidates.Should().Equal(0, 1);
                    at3.RejectedCandidates.Should().BeEmpty();
                    at3.Successors.Should().BeEmpty();
                    at3.PruningMemos.Should().BeEmpty();
                });
            }

            // Act
            sut.Visit(mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(4);
                sut.SearchState.Should().Be(SearchState.Unsafe);
                sut.SearchLevel.Should().Be(0);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(-1);
                    at0.Candidates.Should().BeEmpty();
                    at0.RejectedCandidates.Should().Equal(0, 1);
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(2), second =>
                        second.VariableIndex.Should().Be(3));
                    at0.PruningMemos.Should().BeEmpty();
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().Equal(1, 0);
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(1, 0);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                }, at3 =>
                {
                    at3.VariableIndex.Should().Be(3);
                    at3.SearchTreeLevel.Should().Be(3);
                    at3.BacktrackLevel.Should().Be(2);
                    at3.DomainValueIndex.Should().Be(-1);
                    at3.Candidates.Should().Equal(0, 1);
                    at3.RejectedCandidates.Should().BeEmpty();
                    at3.Successors.Should().BeEmpty();
                    at3.PruningMemos.Should().BeEmpty();
                });
            }
        }

        [Fact]
        public void StateIsUnsafe_BacktrackInvoked_SearchLevelIsEarlierNonLeafLevel_CandidatesNotExhausted_StateIsSafe()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(4);
            Mock<IOrderingStrategy> mockOrderingStrategy = MockOrderingStrategyThatNeverReorders();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Red, Colour.White)
                .AddRegionWithColours(R1, Colour.Black, Colour.White)
                .AddRegionWithColours(R2, Colour.Black, Colour.White)
                .AddRegionWithColours(R3, Colour.Black, Colour.Red, Colour.White)
                .SetAsNeighbours(R0, R3)
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R1, R3)
                .SetAsNeighbours(R2, R3)
                .Build());

            sut.Setup(binaryCsp, mockOrderingStrategy.Object);
            sut.Visit(mockOrderingStrategy.Object);
            sut.Visit(mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(4);
                sut.SearchState.Should().Be(SearchState.Unsafe);
                sut.SearchLevel.Should().Be(1);
                mockOrderingStrategy.Verify(m => m.GetLevelOfOptimalNode(sut.SearchTree, 0), Times.Once);
                mockOrderingStrategy.Verify(m => m.GetLevelOfOptimalNode(sut.SearchTree, 1), Times.Once);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(0);
                    at0.Candidates.Should().Equal(0, 1);
                    at0.RejectedCandidates.Should().BeEmpty();
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(3));
                    at0.PruningMemos.Should().SatisfyRespectively(first =>
                    {
                        first.PrunedNode.VariableIndex.Should().Be(3);
                        first.PrunedCandidate.Should().Be(1);
                    });
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().BeEmpty();
                    at1.RejectedCandidates.Should().Equal(0, 1);
                    at1.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(2), second =>
                        second.VariableIndex.Should().Be(3));
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(1, 0);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                }, at3 =>
                {
                    at3.VariableIndex.Should().Be(3);
                    at3.SearchTreeLevel.Should().Be(3);
                    at3.BacktrackLevel.Should().Be(2);
                    at3.DomainValueIndex.Should().Be(-1);
                    at3.Candidates.Should().Equal(0, 2);
                    at3.RejectedCandidates.Should().BeEmpty();
                    at3.Successors.Should().BeEmpty();
                    at3.PruningMemos.Should().BeEmpty();
                });
            }

            // Act
            sut.Backtrack();

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(4);
                sut.SearchState.Should().Be(SearchState.Safe);
                sut.SearchLevel.Should().Be(0);
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(-1);
                    at0.Candidates.Should().Equal(1);
                    at0.RejectedCandidates.Should().Equal(0);
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(3));
                    at0.PruningMemos.Should().BeEmpty();
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().Equal(0, 1);
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(1, 0);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                }, at3 =>
                {
                    at3.VariableIndex.Should().Be(3);
                    at3.SearchTreeLevel.Should().Be(3);
                    at3.BacktrackLevel.Should().Be(2);
                    at3.DomainValueIndex.Should().Be(-1);
                    at3.Candidates.Should().Equal(0, 2, 1);
                    at3.RejectedCandidates.Should().BeEmpty();
                    at3.Successors.Should().BeEmpty();
                    at3.PruningMemos.Should().BeEmpty();
                });
            }
        }

        [Fact]
        public void StateIsUnsafe_BacktrackInvoked_SearchLevelIsEarlierNonLeafLevel_CandidatesExhausted_StateIsUnsafe()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(4);
            Mock<IOrderingStrategy> mockOrderingStrategy = MockOrderingStrategyThatNeverReorders();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Red)
                .AddRegionWithColours(R1, Colour.Black, Colour.White)
                .AddRegionWithColours(R2, Colour.Black, Colour.White)
                .AddRegionWithColours(R3, Colour.Black, Colour.Red, Colour.White)
                .SetAsNeighbours(R0, R3)
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R2, R3)
                .SetAsNeighbours(R1, R3)
                .Build());

            sut.Setup(binaryCsp, mockOrderingStrategy.Object);
            sut.Visit(mockOrderingStrategy.Object);
            sut.Visit(mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(4);
                sut.SearchState.Should().Be(SearchState.Unsafe);
                sut.SearchLevel.Should().Be(1);
                mockOrderingStrategy.Verify(m => m.GetLevelOfOptimalNode(sut.SearchTree, 0), Times.Once);
                mockOrderingStrategy.Verify(m => m.GetLevelOfOptimalNode(sut.SearchTree, 1), Times.Once);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(0);
                    at0.Candidates.Should().Equal(0);
                    at0.RejectedCandidates.Should().BeEmpty();
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(3));
                    at0.PruningMemos.Should().SatisfyRespectively(first =>
                    {
                        first.PrunedNode.VariableIndex.Should().Be(3);
                        first.PrunedCandidate.Should().Be(1);
                    });
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().BeEmpty();
                    at1.RejectedCandidates.Should().Equal(0, 1);
                    at1.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(2), second =>
                        second.VariableIndex.Should().Be(3));
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(1, 0);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                }, at3 =>
                {
                    at3.VariableIndex.Should().Be(3);
                    at3.SearchTreeLevel.Should().Be(3);
                    at3.BacktrackLevel.Should().Be(2);
                    at3.DomainValueIndex.Should().Be(-1);
                    at3.Candidates.Should().Equal(0, 2);
                    at3.RejectedCandidates.Should().BeEmpty();
                    at3.Successors.Should().BeEmpty();
                    at3.PruningMemos.Should().BeEmpty();
                });
            }

            // Act
            sut.Backtrack();

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(4);
                sut.SearchState.Should().Be(SearchState.Unsafe);
                sut.SearchLevel.Should().Be(0);
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(-1);
                    at0.Candidates.Should().BeEmpty();
                    at0.RejectedCandidates.Should().Equal(0);
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(3));
                    at0.PruningMemos.Should().BeEmpty();
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().Equal(0, 1);
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(1, 0);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                }, at3 =>
                {
                    at3.VariableIndex.Should().Be(3);
                    at3.SearchTreeLevel.Should().Be(3);
                    at3.BacktrackLevel.Should().Be(2);
                    at3.DomainValueIndex.Should().Be(-1);
                    at3.Candidates.Should().Equal(0, 2, 1);
                    at3.RejectedCandidates.Should().BeEmpty();
                    at3.Successors.Should().BeEmpty();
                    at3.PruningMemos.Should().BeEmpty();
                });
            }
        }

        [Fact]
        public void StateIsUnsafe_BacktrackInvoked_SearchLevelIsRootLevel_StateIsFinal()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(3);
            Mock<IOrderingStrategy> mockOrderingStrategy = MockOrderingStrategyThatNeverReorders();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1, R2])
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R0, R2)
                .SetAsNeighbours(R1, R2)
                .Build());

            sut.Setup(binaryCsp, mockOrderingStrategy.Object);
            sut.Visit(mockOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(3);
                sut.SearchState.Should().Be(SearchState.Unsafe);
                sut.SearchLevel.Should().Be(0);
                mockOrderingStrategy.Verify(m => m.GetLevelOfOptimalNode(sut.SearchTree, 0), Times.Once);
                mockOrderingStrategy.VerifyNoOtherCalls();
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(-1);
                    at0.Candidates.Should().BeEmpty();
                    at0.RejectedCandidates.Should().Equal(0, 1);
                    at0.Successors.Should().SatisfyRespectively(first =>
                        first.VariableIndex.Should().Be(1), second =>
                        second.VariableIndex.Should().Be(2));
                    at0.PruningMemos.Should().BeEmpty();
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().Equal(1, 0);
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(0, 1);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                });
            }

            // Act
            sut.Backtrack();

            // Assert
            using (new AssertionScope())
            {
                sut.SearchTreeLeafLevel.Should().Be(3);
                sut.SearchState.Should().Be(SearchState.Final);
                sut.SearchLevel.Should().Be(-1);
                sut.SearchTree.Should().SatisfyRespectively(at0 =>
                {
                    at0.VariableIndex.Should().Be(0);
                    at0.SearchTreeLevel.Should().Be(0);
                    at0.BacktrackLevel.Should().Be(-1);
                    at0.DomainValueIndex.Should().Be(-1);
                    at0.Candidates.Should().Equal(0, 1);
                    at0.RejectedCandidates.Should().BeEmpty();
                    at0.Successors.Should().BeEmpty();
                    at0.PruningMemos.Should().BeEmpty();
                }, at1 =>
                {
                    at1.VariableIndex.Should().Be(1);
                    at1.SearchTreeLevel.Should().Be(1);
                    at1.BacktrackLevel.Should().Be(0);
                    at1.DomainValueIndex.Should().Be(-1);
                    at1.Candidates.Should().Equal(1, 0);
                    at1.RejectedCandidates.Should().BeEmpty();
                    at1.Successors.Should().BeEmpty();
                    at1.PruningMemos.Should().BeEmpty();
                }, at2 =>
                {
                    at2.VariableIndex.Should().Be(2);
                    at2.SearchTreeLevel.Should().Be(2);
                    at2.BacktrackLevel.Should().Be(1);
                    at2.DomainValueIndex.Should().Be(-1);
                    at2.Candidates.Should().Equal(0, 1);
                    at2.RejectedCandidates.Should().BeEmpty();
                    at2.Successors.Should().BeEmpty();
                    at2.PruningMemos.Should().BeEmpty();
                });
            }
        }
    }

    [UnitTest]
    [Category("ClearBoxTest")]
    public sealed class GetAssignments_Method
    {
        [Fact]
        public void StateIsSafe_SearchLevelIsZero_ReturnsEmptyArray()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(1);
            Mock<IOrderingStrategy> stubOrderingStrategy = new();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegion(R0)
                .Build());

            sut.Setup(binaryCsp, stubOrderingStrategy.Object);

            // Act
            Assignment<Region, Colour>[] result = sut.GetAssignments();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void StateIsSafe_SearchLevelIsGreaterThanZeroAndLessThanLeafLevel_ReturnsArrayOfAllPastAssignments()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(3);
            Mock<IOrderingStrategy> stubOrderingStrategy = new();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R0, R1, R2])
                .Build());

            sut.Setup(binaryCsp, stubOrderingStrategy.Object);
            sut.Visit(stubOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchState.Should().Be(SearchState.Safe);
                sut.SearchLevel.Should().Be(1);
            }

            // Act
            Assignment<Region, Colour>[] result = sut.GetAssignments();

            // Assert
            result.Should().Equal(GetAssignment.WithVariable(R0).AndDomainValue(Colour.Black));
        }

        [Fact]
        public void StateIsFinal_SearchLevelIsLeafLevel_ReturnsArrayOfOneAssignmentForEveryBinaryCspVariable()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(3);
            Mock<IOrderingStrategy> stubOrderingStrategy = new();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black)
                .AddRegionWithColours(R1, Colour.White)
                .Build());

            sut.Setup(binaryCsp, stubOrderingStrategy.Object);
            sut.Visit(stubOrderingStrategy.Object);
            sut.Visit(stubOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchState.Should().Be(SearchState.Final);
                sut.SearchLevel.Should().Be(2);
            }

            // Act
            Assignment<Region, Colour>[] result = sut.GetAssignments();

            // Assert
            result.Should().Equal(GetAssignment.WithVariable(R0).AndDomainValue(Colour.Black),
                GetAssignment.WithVariable(R1).AndDomainValue(Colour.White));
        }

        [Fact]
        public void StateIsFinal_SearchLevelIsRootLevel_ReturnsEmptyArray()
        {
            PLAStrategy<Region, Colour> sut = new(3);
            Mock<IOrderingStrategy> stubOrderingStrategy = new();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black)
                .AddRegionWithColours(R1)
                .Build());

            sut.Setup(binaryCsp, stubOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchState.Should().Be(SearchState.Final);
                sut.SearchLevel.Should().Be(-1);
            }

            // Act
            Assignment<Region, Colour>[] result = sut.GetAssignments();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void StateIsInitial_ReturnsEmptyArray()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(1);

            // Act
            Assignment<Region, Colour>[] result = sut.GetAssignments();

            // Assert
            result.Should().BeEmpty();
        }
    }

    [UnitTest]
    [Category("ClearBoxTest")]
    public sealed class Reset_Method
    {
        [Fact]
        public void StateIsFinal_SearchLevelIsLeafLevel_SetsSearchStateToInitial_SetsSearchLevelToRoot_ClearsSearchTree()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(3);
            Mock<IOrderingStrategy> stubOrderingStrategy = new();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R0, R1])
                .Build());

            sut.Setup(binaryCsp, stubOrderingStrategy.Object);
            sut.Visit(stubOrderingStrategy.Object);
            sut.Visit(stubOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchState.Should().Be(SearchState.Final);
                sut.SearchLevel.Should().Be(2);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.SearchTree.Should().HaveCount(2);
            }

            // Act
            sut.Reset();

            // Assert
            using (new AssertionScope())
            {
                sut.SearchState.Should().Be(SearchState.Initial, "updated");
                sut.SearchLevel.Should().Be(-1, "updated");
                sut.SearchTreeLeafLevel.Should().Be(0, "search tree cleared");
                sut.SearchTree.Should().BeEmpty("search tree cleared");
            }
        }

        [Fact]
        public void SearchLevelIsBetweenLeafAndRootLevels_SetsSearchStateToInitial_SetsSearchLevelToRoot_ClearsSearchTree()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(3);
            Mock<IOrderingStrategy> stubOrderingStrategy = new();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R0, R1])
                .Build());

            sut.Setup(binaryCsp, stubOrderingStrategy.Object);
            sut.Visit(stubOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchState.Should().Be(SearchState.Safe);
                sut.SearchLevel.Should().Be(1);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.SearchTree.Should().HaveCount(2);
            }

            // Act
            sut.Reset();

            // Assert
            using (new AssertionScope())
            {
                sut.SearchState.Should().Be(SearchState.Initial, "updated");
                sut.SearchLevel.Should().Be(-1, "updated");
                sut.SearchTreeLeafLevel.Should().Be(0, "search tree cleared");
                sut.SearchTree.Should().BeEmpty("search tree cleared");
            }
        }

        [Fact]
        public void StateIsFinal_SearchLevelIsRootLevel_SetsSearchStateToInitial_ClearsSearchTree()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(1);
            Mock<IOrderingStrategy> stubOrderingStrategy = new();

            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black)
                .AddRegionWithColours(R1)
                .Build());

            sut.Setup(binaryCsp, stubOrderingStrategy.Object);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchState.Should().Be(SearchState.Final);
                sut.SearchLevel.Should().Be(-1);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.SearchTree.Should().HaveCount(2);
            }

            // Act
            sut.Reset();

            // Assert
            using (new AssertionScope())
            {
                sut.SearchState.Should().Be(SearchState.Initial, "updated");
                sut.SearchLevel.Should().Be(-1, "unchanged");
                sut.SearchTreeLeafLevel.Should().Be(0, "search tree cleared");
                sut.SearchTree.Should().BeEmpty("search tree cleared");
            }
        }

        [Fact]
        public void StateIsInitial_DoesNothing()
        {
            // Arrange
            PLAStrategy<Region, Colour> sut = new(1);

            // Assert
            using (new AssertionScope())
            {
                sut.SearchState.Should().Be(SearchState.Initial);
                sut.SearchLevel.Should().Be(-1);
                sut.SearchTreeLeafLevel.Should().Be(0);
                sut.SearchTree.Should().BeEmpty();
            }

            // Act
            sut.Reset();

            // Assert
            using (new AssertionScope())
            {
                sut.SearchState.Should().Be(SearchState.Initial, "unchanged");
                sut.SearchLevel.Should().Be(-1, "unchanged");
                sut.SearchTreeLeafLevel.Should().Be(0, "unchanged");
                sut.SearchTree.Should().BeEmpty("unchanged");
            }
        }
    }
}
