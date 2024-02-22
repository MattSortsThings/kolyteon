using FluentAssertions.Execution;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.MapColouring;

/// <summary>
///     Unit tests for the <see cref="MapColouringBinaryCsp" /> class.
/// </summary>
public sealed class MapColouringBinaryCspTests
{
    private static readonly Region R0 = Region.FromId("R0");
    private static readonly Region R1 = Region.FromId("R1");
    private static readonly Region R2 = Region.FromId("R2");
    private static readonly Region R3 = Region.FromId("R3");
    private static readonly Region R4 = Region.FromId("R4");

    [UnitTest]
    public sealed class Model_Method
    {
        [Fact]
        public void Models_VariablesAreRegionsOrderedById()
        {
            // Arrange
            MapColouringBinaryCsp sut = new(5);

            MapColouringPuzzle puzzle = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R4, R3, R2, R1, R0])
                .Build();

            // Act
            sut.Model(puzzle);

            // Assert
            sut.GetAllVariables().Should().Equal(R0, R1, R2, R3, R4);
        }

        [Fact]
        public void Models_DomainsArePermittedColoursForEachRegion_DuplicateValuesRemoved_OrderedById()
        {
            // Arrange
            MapColouringBinaryCsp sut = new(4);

            MapColouringPuzzle puzzle = MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0)
                .AddRegionWithColours(R1, Colour.Black, Colour.Black)
                .AddRegionWithColours(R2, Colour.Red, Colour.White, Colour.Black, Colour.White)
                .AddRegionWithColours(R3, Colour.Black)
                .Build();

            // Act
            sut.Model(puzzle);

            // Assert
            IEnumerable<IReadOnlyList<Colour>> expectedDomains =
            [
                [],
                [Colour.Black],
                [Colour.Black, Colour.Red, Colour.White],
                [Colour.Black]
            ];

            sut.GetAllDomains().Should().BeEquivalentTo(expectedDomains, options => options.WithStrictOrdering());
        }

        [Fact]
        public void Models_AddsConstraintForEachPairOfNeighbouringRegionsWithAtLeastOneCommonPermittedColour()
        {
            // Arrange
            MapColouringBinaryCsp sut = new(4);

            MapColouringPuzzle puzzle = MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black)
                .AddRegionWithColours(R1, Colour.Black, Colour.White)
                .AddRegionWithColours(R2, Colour.White)
                .AddRegionWithColours(R3, Colour.Black)
                .AddRegionWithColours(R4, Colour.Black)
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R2, R3)
                .SetAsNeighbours(R0, R2)
                .SetAsNeighbours(R0, R3)
                .Build();

            // Act
            sut.Model(puzzle);

            // Assert
            sut.GetAllAdjacentVariables().Should().Equal(new Pair<Region>(R0, R1),
                new Pair<Region>(R0, R3),
                new Pair<Region>(R1, R2));
        }

        [Fact]
        public void Models_UpdatesAllProblemMetricsProperties()
        {
            // Arrange
            MapColouringBinaryCsp sut = new(5);

            MapColouringPuzzle puzzle = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1, R2, R3, R4])
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R2, R3)
                .Build();

            // Act
            sut.Model(puzzle);

            // Assert
            using (new AssertionScope())
            {
                sut.Variables.Should().Be(5);
                sut.Constraints.Should().Be(3);
                sut.ConstraintDensity.Should().BeApproximately(0.3, Invariants.SixDecimalPlacesPrecision);
                sut.ConstraintTightness.Should().BeApproximately(0.5, Invariants.SixDecimalPlacesPrecision);
            }
        }
    }
}
