using System.Text.Json;
using Kolyteon.Common;
using Kolyteon.GraphColouring;
using Kolyteon.Tests.Utils.TestAssertions;

namespace Kolyteon.Tests.Unit.GraphColouring;

public static class GraphColouringProblemTests
{
    [UnitTest]
    public sealed class EqualsMethod
    {
        private static readonly Node N0 = Node.FromName("N0");
        private static readonly Node N1 = Node.FromName("N1");

        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0).Build();

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHAveEqualNodeDataAndEqualEdges_ReturnsTrue()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            GraphColouringProblem other = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalNodeData_ReturnsFalse()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            GraphColouringProblem other = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.Red, Colour.Blue, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalEdges_ReturnsFalse()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            GraphColouringProblem other = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1).Build();

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0).Build();

            // Act
            bool result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class VerifyCorrectMethod
    {
        private static readonly Node N0 = Node.FromName("N0");
        private static readonly Node N1 = Node.FromName("N1");
        private static readonly Node N2 = Node.FromName("N2");
        private static readonly Node N3 = Node.FromName("N3");

        public static TheoryData<GraphColouringProblem, IReadOnlyDictionary<Node, Colour>> PositiveTestCases => new()
        {
            {
                GraphColouringProblem.Create()
                    .UseGlobalColours(Colour.Black)
                    .AddNode(N0).Build(),
                new Dictionary<Node, Colour> { [N0] = Colour.Black }
            },
            {
                GraphColouringProblem.Create()
                    .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                    .AddNode(N0)
                    .AddNode(N1)
                    .AddNode(N2)
                    .AddNode(N3)
                    .AddEdge(Edge.Between(N0, N1))
                    .AddEdge(Edge.Between(N2, N3))
                    .AddEdge(Edge.Between(N0, N2))
                    .AddEdge(Edge.Between(N1, N3))
                    .Build(),
                new Dictionary<Node, Colour> { [N0] = Colour.Red, [N1] = Colour.Blue, [N2] = Colour.Green, [N3] = Colour.Red }
            },
            {
                GraphColouringProblem.Create()
                    .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                    .AddNode(N0)
                    .AddNode(N1)
                    .AddNode(N2)
                    .AddNode(N3)
                    .AddEdge(Edge.Between(N0, N1))
                    .AddEdge(Edge.Between(N2, N3))
                    .AddEdge(Edge.Between(N0, N2))
                    .AddEdge(Edge.Between(N1, N3))
                    .Build(),
                new Dictionary<Node, Colour> { [N0] = Colour.Red, [N1] = Colour.Green, [N2] = Colour.Blue, [N3] = Colour.Red }
            },
            {
                GraphColouringProblem.Create()
                    .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                    .AddNode(N0)
                    .AddNode(N1)
                    .AddNode(N2)
                    .AddNode(N3)
                    .AddEdge(Edge.Between(N0, N1))
                    .AddEdge(Edge.Between(N2, N3))
                    .AddEdge(Edge.Between(N0, N2))
                    .AddEdge(Edge.Between(N1, N3))
                    .Build(),
                new Dictionary<Node, Colour> { [N0] = Colour.Red, [N1] = Colour.Green, [N2] = Colour.Green, [N3] = Colour.Blue }
            },
            {
                GraphColouringProblem.Create()
                    .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                    .AddNode(N0)
                    .AddNode(N1)
                    .AddNode(N2)
                    .AddNode(N3)
                    .AddEdge(Edge.Between(N0, N1))
                    .AddEdge(Edge.Between(N2, N3))
                    .AddEdge(Edge.Between(N0, N2))
                    .AddEdge(Edge.Between(N1, N3))
                    .Build(),
                new Dictionary<Node, Colour> { [N0] = Colour.Red, [N1] = Colour.Blue, [N2] = Colour.Blue, [N3] = Colour.Green }
            },
            {
                GraphColouringProblem.Create()
                    .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                    .AddNode(N0)
                    .AddNode(N1)
                    .AddNode(N2)
                    .AddNode(N3)
                    .AddEdge(Edge.Between(N0, N1))
                    .AddEdge(Edge.Between(N2, N3))
                    .AddEdge(Edge.Between(N0, N2))
                    .AddEdge(Edge.Between(N1, N3))
                    .Build(),
                new Dictionary<Node, Colour> { [N0] = Colour.Green, [N1] = Colour.Blue, [N2] = Colour.Red, [N3] = Colour.Green }
            },
            {
                GraphColouringProblem.Create()
                    .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                    .AddNode(N0)
                    .AddNode(N1)
                    .AddNode(N2)
                    .AddNode(N3)
                    .AddEdge(Edge.Between(N0, N1))
                    .AddEdge(Edge.Between(N2, N3))
                    .AddEdge(Edge.Between(N0, N2))
                    .AddEdge(Edge.Between(N1, N3))
                    .Build(),
                new Dictionary<Node, Colour> { [N0] = Colour.Green, [N1] = Colour.Red, [N2] = Colour.Blue, [N3] = Colour.Green }
            },
            {
                GraphColouringProblem.Create()
                    .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                    .AddNode(N0)
                    .AddNode(N1)
                    .AddNode(N2)
                    .AddNode(N3)
                    .AddEdge(Edge.Between(N0, N1))
                    .AddEdge(Edge.Between(N2, N3))
                    .AddEdge(Edge.Between(N0, N2))
                    .AddEdge(Edge.Between(N1, N3))
                    .Build(),
                new Dictionary<Node, Colour> { [N0] = Colour.Blue, [N1] = Colour.Red, [N2] = Colour.Green, [N3] = Colour.Blue }
            },
            {
                GraphColouringProblem.Create()
                    .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                    .AddNode(N0)
                    .AddNode(N1)
                    .AddNode(N2)
                    .AddNode(N3)
                    .AddEdge(Edge.Between(N0, N1))
                    .AddEdge(Edge.Between(N2, N3))
                    .AddEdge(Edge.Between(N0, N2))
                    .AddEdge(Edge.Between(N1, N3))
                    .Build(),
                new Dictionary<Node, Colour> { [N0] = Colour.Blue, [N1] = Colour.Green, [N2] = Colour.Red, [N3] = Colour.Blue }
            }
        };

        [Theory]
        [MemberData(nameof(PositiveTestCases), MemberType = typeof(VerifyCorrectMethod))]
        public void VerifyCorrect_GivenCorrectSolution_ReturnsSuccessfulResult(GraphColouringProblem sut,
            IReadOnlyDictionary<Node, Colour> solution)
        {
            // Act
            CheckingResult result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeSuccessful().And.HaveNullFirstError();
        }

        [Fact]
        public void VerifyCorrect_SolutionIsEmptyDictionary_ReturnsUnsuccessfulResult()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                .AddNode(N0)
                .Build();

            Dictionary<Node, Colour> solution = [];

            // Act
            CheckingResult result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Solution has 0 entries, but problem has 1 node.");
        }

        [Fact]
        public void VerifyCorrect_SolutionHasTooFewEntries_ReturnsUnsuccessfulResult()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                .AddNode(N0)
                .AddNode(N1)
                .AddNode(N2)
                .AddNode(N3)
                .AddEdge(Edge.Between(N0, N1))
                .AddEdge(Edge.Between(N2, N3))
                .AddEdge(Edge.Between(N0, N2))
                .AddEdge(Edge.Between(N1, N3)).Build();

            Dictionary<Node, Colour> solution = new() { [N0] = Colour.Red };

            // Act
            CheckingResult result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Solution has 1 entry, but problem has 4 nodes.");
        }

        [Fact]
        public void VerifyCorrect_SolutionHasTooManyEntries_ReturnsUnsuccessfulResult()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            Dictionary<Node, Colour> solution = new()
            {
                [N0] = Colour.Red, [N1] = Colour.Blue, [N2] = Colour.Green, [N3] = Colour.Blue
            };

            // Act
            CheckingResult result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Solution has 4 entries, but problem has 2 nodes.");
        }

        [Fact]
        public void VerifyCorrect_NodeIsNotSolutionKey_ReturnsUnsuccessfulResult()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            Dictionary<Node, Colour> solution = new() { [N0] = Colour.Red, [N3] = Colour.Blue };

            // Act
            CheckingResult result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Node (N1) is not a key in the solution.");
        }

        [Fact]
        public void VerifyCorrect_NodeAssignedNonPermittedColour_ReturnsUnsuccessfulResult()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            Dictionary<Node, Colour> solution = new() { [N0] = Colour.Fuchsia, [N1] = Colour.Red };

            // Act
            CheckingResult result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Node (N0) is assigned the colour 'Fuchsia', " +
                                    "which is not a member of its permitted colours set.");
        }

        [Fact]
        public void VerifyCorrect_AdjacentNodesAssignedSameColour_ReturnsUnsuccessfulResult()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Red, Colour.Blue, Colour.Green)
                .AddNode(N0)
                .AddNode(N1)
                .AddNode(N2)
                .AddNode(N3)
                .AddEdge(Edge.Between(N0, N1))
                .AddEdge(Edge.Between(N2, N3))
                .AddEdge(Edge.Between(N0, N2))
                .AddEdge(Edge.Between(N1, N3)).Build();

            Dictionary<Node, Colour> solution = new()
            {
                [N0] = Colour.Green, [N1] = Colour.Red, [N2] = Colour.Green, [N3] = Colour.Red
            };

            // Act
            CheckingResult result = sut.VerifyCorrect(solution);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError("Adjacent nodes (N0) and (N2) are both assigned the colour 'Green'.");
        }

        [Fact]
        public void VerifyCorrect_SolutionArgIsNull_Throws()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black)
                .AddNode(N0).Build();

            // Act
            Action act = () => sut.VerifyCorrect(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'solution')");
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        private static readonly Node N0 = Node.FromName("N0");
        private static readonly Node N1 = Node.FromName("N1");

        [Fact]
        public void Equality_InstanceAndOtherHAveEqualNodeDataAndEqualEdges_ReturnsTrue()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            GraphColouringProblem other = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalNodeData_ReturnsFalse()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            GraphColouringProblem other = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.Red, Colour.Blue, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalEdges_ReturnsFalse()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            GraphColouringProblem other = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1).Build();

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class InequalityOperator
    {
        private static readonly Node N0 = Node.FromName("N0");
        private static readonly Node N1 = Node.FromName("N1");

        [Fact]
        public void Inequality_InstanceAndOtherHAveEqualNodeDataAndEqualEdges_ReturnsFalse()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            GraphColouringProblem other = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalNodeData_ReturnsTrue()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            GraphColouringProblem other = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.Red, Colour.Blue, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalEdges_ReturnsTrue()
        {
            // Arrange
            GraphColouringProblem sut = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1)
                .AddEdge(Edge.Between(N0, N1)).Build();

            GraphColouringProblem other = GraphColouringProblem.Create()
                .UseGlobalColours(Colour.Black, Colour.White)
                .AddNode(N0)
                .AddNode(N1).Build();

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

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

    [UnitTest]
    public sealed class Serialization
    {
        [Fact]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue()
        {
            // Arrange
            GraphColouringProblem originalProblem = GraphColouringProblem.Create()
                .UseNodeSpecificColours()
                .AddNodeAndColours(Node.FromName("N0"), Colour.Red, Colour.Blue, Colour.Green)
                .AddNodeAndColours(Node.FromName("N1"), Colour.Black)
                .AddNodeAndColours(Node.FromName("N2"))
                .AddNodeAndColours(Node.FromName("N3"), Colour.Red, Colour.Blue, Colour.Green, Colour.Yellow)
                .AddEdge(Edge.Between(Node.FromName("N0"), Node.FromName("N3")))
                .AddEdge(Edge.Between(Node.FromName("N0"), Node.FromName("N2")))
                .Build();

            string json = JsonSerializer.Serialize(originalProblem, JsonSerializerOptions.Default);

            // Act
            GraphColouringProblem? deserializedProblem =
                JsonSerializer.Deserialize<GraphColouringProblem>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedProblem.Should().NotBeNull().And.Be(originalProblem);
        }
    }
}
