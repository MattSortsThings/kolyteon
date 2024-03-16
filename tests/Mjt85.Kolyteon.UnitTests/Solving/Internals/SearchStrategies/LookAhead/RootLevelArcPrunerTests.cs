using FluentAssertions.Execution;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookAhead;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Solving.Internals.SearchStrategies.LookAhead;

/// <summary>
///     Unit tests for the internal <see cref="RootLevelArcPruner{V,D}" /> class, parametrized over the Map Colouring
///     problem types and using <see cref="PLANode{V,D}" /> as the search tree node type.
/// </summary>
public sealed class RootLevelArcPrunerTests
{
    private static readonly Region R0 = Region.FromId("R0");
    private static readonly Region R1 = Region.FromId("R1");
    private static readonly Region R2 = Region.FromId("R2");

    private static PLANodeBuilder GetNode() => new();

    private class PLANodeBuilder
    {
        private ISolvableBinaryCsp<Region, Colour>? _binaryCsp;
        private int _variableIndex;

        public PLANodeBuilder WithBinaryCsp(ISolvableBinaryCsp<Region, Colour> binaryCsp)
        {
            _binaryCsp = binaryCsp;

            return this;
        }

        public PLANodeBuilder WithVariableIndex(int index)
        {
            _variableIndex = index;

            return this;
        }

        public PLANode<Region, Colour> Build() => new(_binaryCsp!, _variableIndex);
    }

    [UnitTest]
    public sealed class ArcPrune_Method
    {
        [Fact]
        public void InstancePrunesAnyOperandNodeCandidateWithoutSupportingContextNodeCandidate()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R0, R1, R2])
                .SetAsNeighbours(R1, R2)
                .Build());

            RootLevelArcPruner<Region, Colour> sut = new();

            PLANode<Region, Colour> operandNode = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(1).Build();
            PLANode<Region, Colour> contextNode = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(2).Build();

            // Assert
            using (new AssertionScope())
            {
                operandNode.Candidates.Should().Equal(0);
                operandNode.RejectedCandidates.Should().BeEmpty();
                operandNode.PruningMemos.Should().BeEmpty();
                contextNode.Candidates.Should().Equal(0);
                contextNode.RejectedCandidates.Should().BeEmpty();
                contextNode.PruningMemos.Should().BeEmpty();
            }

            // Act
            sut.ArcPrune(operandNode, contextNode);

            // Assert
            using (new AssertionScope())
            {
                operandNode.Candidates.Should().BeEmpty();
                operandNode.RejectedCandidates.Should().BeEmpty();
                operandNode.PruningMemos.Should().BeEmpty();
                contextNode.Candidates.Should().Equal(0);
                contextNode.RejectedCandidates.Should().BeEmpty();
                contextNode.PruningMemos.Should().BeEmpty();
            }
        }

        [Fact]
        public void AllOperandNodeCandidatesHaveSupportingContextNodeCandidate_DoesNotPrune()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1, R2])
                .SetAsNeighbours(R1, R2)
                .Build());

            RootLevelArcPruner<Region, Colour> sut = new();

            PLANode<Region, Colour> operandNode = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(1).Build();
            PLANode<Region, Colour> contextNode = GetNode().WithBinaryCsp(binaryCsp).WithVariableIndex(2).Build();

            // Assert
            using (new AssertionScope())
            {
                operandNode.Candidates.Should().Equal(0, 1);
                operandNode.RejectedCandidates.Should().BeEmpty();
                operandNode.PruningMemos.Should().BeEmpty();
                contextNode.Candidates.Should().Equal(0, 1);
                contextNode.RejectedCandidates.Should().BeEmpty();
                contextNode.PruningMemos.Should().BeEmpty();
            }

            // Act
            sut.ArcPrune(operandNode, contextNode);

            // Assert
            using (new AssertionScope())
            {
                operandNode.Candidates.Should().Equal(0, 1);
                operandNode.RejectedCandidates.Should().BeEmpty();
                operandNode.PruningMemos.Should().BeEmpty();
                contextNode.Candidates.Should().Equal(0, 1);
                contextNode.RejectedCandidates.Should().BeEmpty();
                contextNode.PruningMemos.Should().BeEmpty();
            }
        }
    }
}
