using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Solving.Internals.OrderingStrategies;
using Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookBack;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;
using Mjt85.Kolyteon.UnitTests.Helpers;
using Moq;

namespace Mjt85.Kolyteon.UnitTests.Solving.Internals.SearchTrees;

/// <summary>
///     Unit tests for the internal <see cref="SearchTree{N,V,D}" /> class, parametrized over the Map Colouring puzzle
///     types.
/// </summary>
public static class SearchTreeTests
{
    private static Mock<IOrderingStrategy> MockOrderingStrategyWithFixedOptimalNodeAtLevel(int level)
    {
        Mock<IOrderingStrategy> mock = new();
        mock.Setup(m => m.GetLevelOfOptimalNode(It.IsAny<IList<BTNode<Region, Colour>>>(), It.IsAny<int>())).Returns(level);

        return mock;
    }

    [UnitTest]
    public sealed class ReorderNodes_Method
    {
        [Fact]
        public void OptimalNodeIsAtLaterLevelThanSearchLevel_ReordersNodes_And_UpdatesNodeSearchTreeLevelValues()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegion("R0")
                .AddRegion("R1")
                .AddRegion("R2")
                .Build());

            BTNode<Region, Colour> nodeAt0 = new(binaryCsp, 0);
            BTNode<Region, Colour> nodeAt1 = new(binaryCsp, 1);
            BTNode<Region, Colour> nodeAt2 = new(binaryCsp, 2);

            SearchTree<BTNode<Region, Colour>, Region, Colour> sut = [nodeAt0, nodeAt1, nodeAt2];

            const int searchLevel = 0;
            const int swapLevel = 2;

            Mock<IOrderingStrategy> stubOrderingStrategy = MockOrderingStrategyWithFixedOptimalNodeAtLevel(swapLevel);

            // Assert
            sut.Should().SatisfyRespectively(at0 =>
            {
                at0.Should().BeSameAs(nodeAt0);
                at0.SearchTreeLevel.Should().Be(0);
            }, at1 =>
            {
                at1.Should().BeSameAs(nodeAt1);
                at1.SearchTreeLevel.Should().Be(1);
            }, at2 =>
            {
                at2.Should().BeSameAs(nodeAt2);
                at2.SearchTreeLevel.Should().Be(2);
            });

            // Act
            sut.ReorderNodes(stubOrderingStrategy.Object, searchLevel);

            // Assert
            sut.Should().SatisfyRespectively(at0 =>
            {
                at0.Should().BeSameAs(nodeAt2, "swapped");
                at0.SearchTreeLevel.Should().Be(0, "updated");
            }, at1 =>
            {
                at1.Should().BeSameAs(nodeAt1);
                at1.SearchTreeLevel.Should().Be(1);
            }, at2 =>
            {
                at2.Should().BeSameAs(nodeAt0, "swapped");
                at2.SearchTreeLevel.Should().Be(2, "updated");
            });
        }

        [Fact]
        public void OptimalNodeIsAtSearchLevel_DoesNotReorderOrUpdateNodes()
        {
            // Arrange
            MapColouringBinaryCsp binaryCsp = GetBinaryCsp.ModellingProblem(MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegion("R0")
                .AddRegion("R1")
                .AddRegion("R2")
                .Build());

            BTNode<Region, Colour> nodeAt0 = new(binaryCsp, 0);
            BTNode<Region, Colour> nodeAt1 = new(binaryCsp, 1);
            BTNode<Region, Colour> nodeAt2 = new(binaryCsp, 2);

            SearchTree<BTNode<Region, Colour>, Region, Colour> sut = [nodeAt0, nodeAt1, nodeAt2];

            const int searchLevel = 0;

            Mock<IOrderingStrategy> stubOrderingStrategy = MockOrderingStrategyWithFixedOptimalNodeAtLevel(searchLevel);

            // Assert
            sut.Should().SatisfyRespectively(at0 =>
            {
                at0.Should().BeSameAs(nodeAt0);
                at0.SearchTreeLevel.Should().Be(0);
            }, at1 =>
            {
                at1.Should().BeSameAs(nodeAt1);
                at1.SearchTreeLevel.Should().Be(1);
            }, at2 =>
            {
                at2.Should().BeSameAs(nodeAt2);
                at2.SearchTreeLevel.Should().Be(2);
            });

            // Act
            sut.ReorderNodes(stubOrderingStrategy.Object, searchLevel);

            // Assert
            sut.Should().SatisfyRespectively(at0 =>
            {
                at0.Should().BeSameAs(nodeAt0);
                at0.SearchTreeLevel.Should().Be(0);
            }, at1 =>
            {
                at1.Should().BeSameAs(nodeAt1);
                at1.SearchTreeLevel.Should().Be(1);
            }, at2 =>
            {
                at2.Should().BeSameAs(nodeAt2);
                at2.SearchTreeLevel.Should().Be(2);
            });
        }
    }
}
