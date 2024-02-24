using FluentAssertions.Execution;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.SearchStrategies.LookAhead;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Solving.Internals.SearchStrategies.LookAhead;

/// <summary>
///     Unit tests for the internal <see cref="FCNode{V,D}" /> class, parametrized over the Map Colouring problem types.
/// </summary>
public sealed class FCNodeTests
{
    private static readonly Region R0 = Region.FromId("R0");
    private static readonly Region R1 = Region.FromId("R1");
    private static readonly Region R2 = Region.FromId("R2");
    private static readonly Region R3 = Region.FromId("R3");
    private static readonly Region R4 = Region.FromId("R4");

    private static FCNodeBuilder GetNode() => new();

    private class FCNodeBuilder
    {
        private ISolvableBinaryCsp<Region, Colour>? _binaryCsp;
        private int _variableIndex;

        public FCNodeBuilder WithBinaryCsp(ISolvableBinaryCsp<Region, Colour> binaryCsp)
        {
            _binaryCsp = binaryCsp;

            return this;
        }

        public FCNodeBuilder WithVariableIndex(int index)
        {
            _variableIndex = index;

            return this;
        }

        public FCNode<Region, Colour> Build() => new(_binaryCsp!, _variableIndex);
    }

    [UnitTest]
    public sealed class Candidates_Property
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void IsInitiallyAllSequentialDomainValueIndexesForVariable(MapColouringPuzzle puzzle,
            int variableIndex,
            IEnumerable<int> expected)
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode()
                .WithBinaryCsp(GetBinaryCsp.ModellingProblem(puzzle))
                .WithVariableIndex(variableIndex)
                .Build();

            // Assert
            sut.Candidates.Should().Equal(expected);
        }

        private sealed class TestCases : TheoryData<MapColouringPuzzle, int, IEnumerable<int>>
        {
            public TestCases()
            {
                MapColouringPuzzle fixedPuzzle = MapColouringPuzzle.Create()
                    .WithRegionSpecificColours()
                    .AddRegionWithColours(R0, Colour.Black, Colour.White)
                    .AddRegionWithColours(R1)
                    .AddRegionWithColours(R2, Colour.Black, Colour.Red, Colour.White)
                    .AddRegionWithColours(R3, Colour.White)
                    .Build();

                Add(fixedPuzzle, 0, [0, 1]);
                Add(fixedPuzzle, 1, []);
                Add(fixedPuzzle, 2, [0, 1, 2]);
                Add(fixedPuzzle, 3, [0]);
            }
        }
    }

    [UnitTest]
    public sealed class RejectedCandidates_Property
    {
        [Fact]
        public void IsInitiallyEmpty()
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode()
                .WithBinaryCsp(GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.White)
                    .AddRegion(R0)
                    .Build()))
                .WithVariableIndex(0)
                .Build();

            // Assert
            sut.RejectedCandidates.Should().BeEmpty();
        }
    }

    [UnitTest]
    public sealed class BacktrackLevel_Property
    {
        [Fact]
        public void IsAlwaysOneLessThanSearchTreeLevelOfInstance()
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode()
                .WithBinaryCsp(GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.White)
                    .AddRegion(R0)
                    .Build()))
                .WithVariableIndex(0)
                .Build();

            // Assert
            sut.BacktrackLevel.Should().Be(-1, "search tree level is 0");

            // Act
            sut.SearchTreeLevel = 5;

            // Assert
            sut.BacktrackLevel.Should().Be(4, "search tree level is 5");
        }
    }

    [UnitTest]
    public sealed class DomainValueIndex_Property
    {
        [Fact]
        public void IsInitiallyMinusOne()
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode()
                .WithBinaryCsp(GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.White)
                    .AddRegion(R0)
                    .Build()))
                .WithVariableIndex(0)
                .Build();

            // Assert
            sut.DomainValueIndex.Should().Be(-1, "no assignment");
        }
    }

    [UnitTest]
    public sealed class SearchTreeLevel_Property
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void IsInitiallySameAsVariableIndex(int variableIndex)
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode()
                .WithBinaryCsp(GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.White)
                    .AddRegions([R0, R1, R2])
                    .Build()))
                .WithVariableIndex(variableIndex)
                .Build();

            // Assert
            sut.SearchTreeLevel.Should().Be(variableIndex);
        }
    }

    [UnitTest]
    public sealed class RemainingCandidates_Property
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void IsInitiallyDomainSizeOfVariable(MapColouringPuzzle puzzle, int variableIndex, int expected)
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode()
                .WithBinaryCsp(GetBinaryCsp.ModellingProblem(puzzle))
                .WithVariableIndex(variableIndex)
                .Build();

            // Assert
            sut.RemainingCandidates.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<MapColouringPuzzle, int, int>
        {
            public TestCases()
            {
                MapColouringPuzzle fixedPuzzle = MapColouringPuzzle.Create()
                    .WithRegionSpecificColours()
                    .AddRegionWithColours(R0, Colour.Black, Colour.White)
                    .AddRegionWithColours(R1)
                    .AddRegionWithColours(R2, Colour.Black, Colour.Red, Colour.White)
                    .AddRegionWithColours(R3, Colour.White)
                    .Build();

                Add(fixedPuzzle, 0, 2);
                Add(fixedPuzzle, 1, 0);
                Add(fixedPuzzle, 2, 3);
                Add(fixedPuzzle, 3, 1);
            }
        }
    }

    [UnitTest]
    public sealed class Degree_Property
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void IsDegreeOfVariable(MapColouringPuzzle puzzle, int variableIndex, int expected)
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode()
                .WithBinaryCsp(GetBinaryCsp.ModellingProblem(puzzle))
                .WithVariableIndex(variableIndex)
                .Build();

            // Assert
            sut.Degree.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<MapColouringPuzzle, int, int>
        {
            public TestCases()
            {
                MapColouringPuzzle fixedPuzzle = MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.White)
                    .AddRegions([R0, R1, R2, R3, R4])
                    .SetAsNeighbours(R0, R1)
                    .SetAsNeighbours(R0, R2)
                    .SetAsNeighbours(R0, R4)
                    .SetAsNeighbours(R2, R4)
                    .Build();

                Add(fixedPuzzle, 0, 3);
                Add(fixedPuzzle, 1, 1);
                Add(fixedPuzzle, 2, 2);
                Add(fixedPuzzle, 3, 0);
                Add(fixedPuzzle, 4, 2);
            }
        }
    }

    [UnitTest]
    public sealed class SumTightness_Property
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void IsSumTightnessOfVariable(MapColouringPuzzle puzzle, int variableIndex, double expectedTo6DP)
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode()
                .WithBinaryCsp(GetBinaryCsp.ModellingProblem(puzzle))
                .WithVariableIndex(variableIndex)
                .Build();

            // Assert
            sut.SumTightness.Should().BeApproximately(expectedTo6DP, Invariants.SixDecimalPlacesPrecision);
        }

        private sealed class TestCases : TheoryData<MapColouringPuzzle, int, double>
        {
            public TestCases()
            {
                MapColouringPuzzle fixedPuzzle = MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.White)
                    .AddRegions([R0, R1, R2, R3, R4])
                    .SetAsNeighbours(R0, R1)
                    .SetAsNeighbours(R0, R2)
                    .SetAsNeighbours(R0, R4)
                    .SetAsNeighbours(R2, R4)
                    .Build();

                Add(fixedPuzzle, 0, 1.5);
                Add(fixedPuzzle, 1, 0.5);
                Add(fixedPuzzle, 2, 1);
                Add(fixedPuzzle, 3, 0);
                Add(fixedPuzzle, 4, 1);
            }
        }
    }

    [UnitTest]
    public sealed class Successors_Property
    {
        [Fact]
        public void IsInitiallyEmpty()
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode()
                .WithBinaryCsp(GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.White)
                    .AddRegion(R0)
                    .AddRegion(R1)
                    .SetAsNeighbours(R0, R1)
                    .Build()))
                .WithVariableIndex(1)
                .Build();

            // Assert
            sut.Successors.Should().BeEmpty();
        }
    }

    [UnitTest]
    public sealed class PruningMemos_Property
    {
        [Fact]
        public void IsInitiallyEmpty()
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode()
                .WithBinaryCsp(GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.White)
                    .AddRegion(R0)
                    .AddRegion(R1)
                    .SetAsNeighbours(R0, R1)
                    .Build()))
                .WithVariableIndex(1)
                .Build();

            // Assert
            sut.PruningMemos.Should().BeEmpty();
        }
    }

    [UnitTest]
    public sealed class AdjacentTo_Method
    {
        [Fact]
        public void InstanceAndOtherHaveAdjacentVariables_ReturnsTrue()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegion(R0)
                .AddRegion(R1)
                .SetAsNeighbours(R0, R1)
                .Build());

            FCNode<Region, Colour> nodeAt0 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(0).Build();
            FCNode<Region, Colour> nodeAt1 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(1).Build();

            // Assert
            using (new AssertionScope())
            {
                nodeAt0.AdjacentTo(nodeAt1).Should().BeTrue();
                nodeAt1.AdjacentTo(nodeAt0).Should().BeTrue();
            }
        }

        [Fact]
        public void InstanceAndOtherHaveNonAdjacentVariables_ReturnsTrue()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegion(R0)
                .AddRegion(R1)
                .Build());

            FCNode<Region, Colour> nodeAt0 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(0).Build();
            FCNode<Region, Colour> nodeAt1 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(1).Build();

            // Assert
            using (new AssertionScope())
            {
                nodeAt0.AdjacentTo(nodeAt1).Should().BeFalse();
                nodeAt1.AdjacentTo(nodeAt0).Should().BeFalse();
            }
        }
    }

    [UnitTest]
    public sealed class AssignNextCandidate_Method
    {
        [Fact]
        public void UpdatesDomainValueIndex()
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode().WithBinaryCsp(GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.White)
                    .AddRegion(R0)
                    .Build()))
                .WithVariableIndex(0)
                .Build();

            // Assert
            using (new AssertionScope())
            {
                sut.DomainValueIndex.Should().Be(-1, "no assignment");
                sut.Candidates.Should().Equal(0, 1);
            }

            // Act
            sut.AssignNextCandidate();

            // Assert
            using (new AssertionScope())
            {
                sut.DomainValueIndex.Should().Be(0, "updated with first candidate");
                sut.Candidates.Should().Equal(0, 1);
            }
        }
    }

    [UnitTest]
    public sealed class AssignmentSupports_Method
    {
        [Fact]
        public void InstanceAndOtherHaveAdjacentVariables_AssignmentsAreConsistent_ReturnsTrue()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black, Colour.Red)
                .AddRegionWithColours(R1, Colour.Red, Colour.White)
                .SetAsNeighbours(R0, R1)
                .Build());

            FCNode<Region, Colour> nodeAt0 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(0).Build();
            FCNode<Region, Colour> nodeAt1 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(1).Build();

            nodeAt0.AssignNextCandidate();
            nodeAt1.AssignNextCandidate();

            // Assert
            using (new AssertionScope())
            {
                nodeAt0.AssignmentSupports(nodeAt1).Should().BeTrue();
                nodeAt1.AssignmentSupports(nodeAt0).Should().BeTrue();
            }
        }

        [Fact]
        public void InstanceAndOtherHaveAdjacentVariables_AssignmentsAreNotConsistent_ReturnsFalse()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegion(R0)
                .AddRegion(R1)
                .SetAsNeighbours(R0, R1)
                .Build());

            FCNode<Region, Colour> nodeAt0 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(0).Build();
            FCNode<Region, Colour> nodeAt1 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(1).Build();

            nodeAt0.AssignNextCandidate();
            nodeAt1.AssignNextCandidate();

            // Assert
            using (new AssertionScope())
            {
                nodeAt0.AssignmentSupports(nodeAt1).Should().BeFalse();
                nodeAt1.AssignmentSupports(nodeAt0).Should().BeFalse();
            }
        }

        [Fact]
        public void InstanceAndOtherHaveNonAdjacentVariables_ReturnsTrue()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegion(R0)
                .AddRegion(R1)
                .Build());

            FCNode<Region, Colour> nodeAt0 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(0).Build();
            FCNode<Region, Colour> nodeAt1 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(1).Build();

            nodeAt0.AssignNextCandidate();
            nodeAt1.AssignNextCandidate();

            // Assert
            using (new AssertionScope())
            {
                nodeAt0.AssignmentSupports(nodeAt1).Should().BeTrue();
                nodeAt1.AssignmentSupports(nodeAt0).Should().BeTrue();
            }
        }
    }

    [UnitTest]
    public sealed class RejectAssignment_Method
    {
        [Fact]
        public void RejectsFirstCandidate_ResetsDomainValueIndexToNoAssignment()
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode().WithBinaryCsp(GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.White)
                    .AddRegion(R0)
                    .Build()))
                .WithVariableIndex(0)
                .Build();

            sut.AssignNextCandidate();

            // Assert
            using (new AssertionScope())
            {
                sut.DomainValueIndex.Should().Be(0);
                sut.Candidates.Should().Equal(0, 1);
                sut.RejectedCandidates.Should().BeEmpty();
            }

            // Act
            sut.RejectAssignment();

            // Assert
            using (new AssertionScope())
            {
                sut.DomainValueIndex.Should().Be(-1, "updated, no assignment");
                sut.Candidates.Should().Equal(1);
                sut.RejectedCandidates.Should().Equal(0);
            }
        }
    }

    [UnitTest]
    public sealed class RestoreRejectedCandidates_Method
    {
        [Fact]
        public void EnqueuesAllRejectedCandidatesInCandidatesQueueThenClearsRejectedCandidates()
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode().WithBinaryCsp(GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.Red, Colour.White)
                    .AddRegion(R0)
                    .Build()))
                .WithVariableIndex(0)
                .Build();

            sut.AssignNextCandidate();
            sut.RejectAssignment();
            sut.AssignNextCandidate();
            sut.RejectAssignment();

            // Assert
            using (new AssertionScope())
            {
                sut.Candidates.Should().Equal(2);
                sut.RejectedCandidates.Should().Equal(0, 1);
            }

            // Act
            sut.RestoreRejectedCandidates();

            // Assert
            using (new AssertionScope())
            {
                sut.Candidates.Should().Equal(2, 0, 1);
                sut.RejectedCandidates.Should().BeEmpty();
            }
        }

        [Fact]
        public void RejectedCandidatesIsEmpty_DoesNothing()
        {
            // Arrange
            FCNode<Region, Colour> sut = GetNode().WithBinaryCsp(GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Black, Colour.White)
                    .AddRegion(R0)
                    .Build()))
                .WithVariableIndex(0)
                .Build();

            // Assert
            using (new AssertionScope())
            {
                sut.Candidates.Should().Equal(0, 1);
                sut.RejectedCandidates.Should().BeEmpty();
            }

            // Act
            sut.RestoreRejectedCandidates();

            // Assert
            using (new AssertionScope())
            {
                sut.Candidates.Should().Equal(0, 1);
                sut.RejectedCandidates.Should().BeEmpty();
            }
        }
    }

    [UnitTest]
    public sealed class RepopulateSuccessors_Method
    {
        [Fact]
        public void AddsAllAdjacentNodesAtLaterSearchTreeLevelsToAncestors_OrderedBySearchTreeLevel()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1, R2, R3, R4])
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R1, R4)
                .SetAsNeighbours(R2, R3)
                .Build());

            FCNode<Region, Colour> nodeAt0 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(0).Build();
            FCNode<Region, Colour> nodeAt1 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(1).Build();
            FCNode<Region, Colour> nodeAt2 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(2).Build();
            FCNode<Region, Colour> nodeAt3 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(3).Build();
            FCNode<Region, Colour> nodeAt4 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(4).Build();

            FCNode<Region, Colour>[] searchTree = [nodeAt0, nodeAt1, nodeAt2, nodeAt3, nodeAt4];

            // Assert
            nodeAt1.Successors.Should().BeEmpty();

            // Act
            nodeAt1.RepopulateSuccessors(searchTree);

            // Assert
            nodeAt1.Successors.Should().Equal(nodeAt2, nodeAt4);
        }
    }

    [UnitTest]
    public sealed class Prune_Method
    {
        [Fact]
        public void PrunesAllOperandNodeCandidatesNotSupportedByInstanceAssignment_AddsPruningMemoForEach()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Red)
                .AddRegionWithColours(R1, Colour.Black, Colour.Red, Colour.White)
                .SetAsNeighbours(R0, R1)
                .Build());

            FCNode<Region, Colour> sut = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(0).Build();
            FCNode<Region, Colour> operand = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(1).Build();

            sut.RepopulateSuccessors([sut, operand]);
            sut.AssignNextCandidate();

            // Assert
            using (new AssertionScope())
            {
                sut.DomainValueIndex.Should().Be(0);
                sut.PruningMemos.Should().BeEmpty();
                operand.DomainValueIndex.Should().Be(-1);
                operand.Candidates.Should().Equal(0, 1, 2);
            }

            // Act
            sut.Prune(operand);

            // Assert
            using (new AssertionScope())
            {
                sut.DomainValueIndex.Should().Be(0);
                sut.PruningMemos.Should().SatisfyRespectively(memo =>
                {
                    memo.PrunedNode.Should().BeSameAs(operand);
                    memo.PrunedCandidate.Should().Be(1);
                });
                operand.DomainValueIndex.Should().Be(-1);
                operand.Candidates.Should().Equal(0, 2);
            }
        }

        [Fact]
        public void InstanceAssignmentSupportsAllOperandNodeCandidates_DoesNotPrune()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black, Colour.Red)
                .AddRegionWithColours(R1, Colour.Black, Colour.White)
                .SetAsNeighbours(R0, R1)
                .Build());

            FCNode<Region, Colour> sut = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(0).Build();
            FCNode<Region, Colour> operand = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(1).Build();

            sut.RepopulateSuccessors([sut, operand]);
            sut.AssignNextCandidate();
            sut.RejectAssignment();
            sut.AssignNextCandidate();

            // Assert
            using (new AssertionScope())
            {
                sut.DomainValueIndex.Should().Be(1);
                sut.PruningMemos.Should().BeEmpty();
                operand.DomainValueIndex.Should().Be(-1);
                operand.Candidates.Should().Equal(0, 1);
            }

            // Act
            sut.Prune(operand);

            // Assert
            using (new AssertionScope())
            {
                sut.DomainValueIndex.Should().Be(1);
                sut.PruningMemos.Should().BeEmpty();
                operand.DomainValueIndex.Should().Be(-1);
                operand.Candidates.Should().Equal(0, 1);
            }
        }
    }

    [UnitTest]
    public sealed class UndoAllPruning_Method
    {
        [Fact]
        public void EnqueuesAllPrunedCandidatesToPrunedNodes_ClearsPruningMemos()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Red)
                .AddRegionWithColours(R1, Colour.Black, Colour.Red, Colour.White)
                .AddRegionWithColours(R2, Colour.Red)
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R0, R2)
                .Build());

            FCNode<Region, Colour> sut = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(0).Build();
            FCNode<Region, Colour> operandAt1 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(1).Build();
            FCNode<Region, Colour> operandAt2 = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(2).Build();

            sut.RepopulateSuccessors([sut, operandAt1, operandAt2]);
            sut.AssignNextCandidate();
            sut.Prune(operandAt1);
            sut.Prune(operandAt2);

            // Assert
            using (new AssertionScope())
            {
                sut.DomainValueIndex.Should().Be(0);
                sut.PruningMemos.Should().SatisfyRespectively(memo =>
                {
                    memo.PrunedNode.Should().BeSameAs(operandAt1);
                    memo.PrunedCandidate.Should().Be(1);
                }, memo =>
                {
                    memo.PrunedNode.Should().BeSameAs(operandAt2);
                    memo.PrunedCandidate.Should().Be(0);
                });
                operandAt1.Candidates.Should().Equal(0, 2);
                operandAt2.Candidates.Should().BeEmpty();
            }

            // Act
            sut.UndoAllPruning();

            // Assert
            using (new AssertionScope())
            {
                sut.DomainValueIndex.Should().Be(0);
                sut.PruningMemos.Should().BeEmpty();
                operandAt1.Candidates.Should().Equal(0, 2, 1);
                operandAt2.Candidates.Should().Equal(0);
            }
        }
    }
}
