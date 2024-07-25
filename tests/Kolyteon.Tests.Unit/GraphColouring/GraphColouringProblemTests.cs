using Kolyteon.Common;
using Kolyteon.GraphColouring;

namespace Kolyteon.Tests.Unit.GraphColouring;

public static class GraphColouringProblemTests
{
    [UnitTest]
    public sealed class FluentBuilder
    {
        private static readonly Node N0 = Node.FromName("N0");
        private static readonly Node N1 = Node.FromName("N1");
        private static readonly Node N2 = Node.FromName("N2");
        private static readonly Node N3 = Node.FromName("N3");

        [Fact]
        public void FluentBuilder_CanBuildWithGlobalPermittedColours_NodeDataAndEdgesAreInAscendingOrder()
        {
            // Act
            GraphColouringProblem result = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White, Colour.Red, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddNode(N3)
                .AddNode(N2)
                .AddEdge(Edge.Between(N0, N3))
                .AddEdge(Edge.Between(N0, N2))
                .Build();

            // Assert
            using (new AssertionScope())
            {
                result.NodeData.Should().SatisfyRespectively(datum =>
                    {
                        datum.Node.Should().Be(N0);
                        datum.PermittedColours.Should().BeEquivalentTo([Colour.Black, Colour.Red, Colour.White]);
                    }, datum =>
                    {
                        datum.Node.Should().Be(N1);
                        datum.PermittedColours.Should().BeEquivalentTo([Colour.Black, Colour.Red, Colour.White]);
                    }, datum =>
                    {
                        datum.Node.Should().Be(N2);
                        datum.PermittedColours.Should().BeEquivalentTo([Colour.Black, Colour.Red, Colour.White]);
                    }, datum =>
                    {
                        datum.Node.Should().Be(N3);
                        datum.PermittedColours.Should().BeEquivalentTo([Colour.Black, Colour.Red, Colour.White]);
                    }
                ).And.BeInAscendingOrder();

                result.Edges.Should().Equal(Edge.Between(N0, N2), Edge.Between(N0, N3))
                    .And.BeInAscendingOrder();
            }
        }

        [Fact]
        public void FluentBuilder_CanBuildWithNodeSpecificPermittedColours_NodeDataAndEdgesAreInAscendingOrder()
        {
            // Act
            GraphColouringProblem result = GraphColouringProblem.Create()
                .UseNodeSpecificColours()
                .AddNodeAndColours(N0)
                .AddNodeAndColours(N2, Colour.White, Colour.Black, Colour.Black)
                .AddNodeAndColours(N1, Colour.Black)
                .AddEdge(Edge.Between(N1, N2))
                .AddEdge(Edge.Between(N0, N2))
                .Build();

            // Assert
            using (new AssertionScope())
            {
                result.NodeData.Should().SatisfyRespectively(datum =>
                    {
                        datum.Node.Should().Be(N0);
                        datum.PermittedColours.Should().BeEmpty();
                    }, datum =>
                    {
                        datum.Node.Should().Be(N1);
                        datum.PermittedColours.Should().BeEquivalentTo([Colour.Black]);
                    }, datum =>
                    {
                        datum.Node.Should().Be(N2);
                        datum.PermittedColours.Should().BeEquivalentTo([Colour.Black, Colour.White]);
                    }
                ).And.BeInAscendingOrder();

                result.Edges.Should().Equal(Edge.Between(N0, N2), Edge.Between(N1, N2))
                    .And.BeInAscendingOrder();
            }
        }

        [Fact]
        public void FluentBuilder_ZeroNodes_Throws()
        {
            // Act
            Action act = () => GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Problem has zero nodes.");
        }

        [Fact]
        public void FluentBuilder_DuplicateNodes_Throws()
        {
            // Act
            Action act = () => GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N2)
                .AddNode(N1)
                .AddNode(N3)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N3))
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Node (N1) occurs more than once.");
        }

        [Fact]
        public void FluentBuilder_EdgeWithFirstNodeNotInGraph_Throws()
        {
            // Act
            Action act = () => GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N2)
                .AddEdge(Edge.Between(N0, N2))
                .AddEdge(Edge.Between(N1, N2))
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Edge (N1)-(N2) is invalid: node (N1) not present in graph.");
        }

        [Fact]
        public void FluentBuilder_EdgeWithSecondNodeNotInGraph_Throws()
        {
            // Act
            Action act = () => GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N2)
                .AddEdge(Edge.Between(N0, N3))
                .AddEdge(Edge.Between(N0, N2))
                .Build();

            // Assert
            act.Should().Throw<InvalidProblemException>()
                .WithMessage("Edge (N0)-(N3) is invalid: node (N3) not present in graph.");
        }
    }
}
