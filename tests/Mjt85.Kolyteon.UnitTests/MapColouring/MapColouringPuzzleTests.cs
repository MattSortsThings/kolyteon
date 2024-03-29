﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using FluentAssertions.Execution;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.MapColouring;

/// <summary>
///     Unit tests for the <see cref="MapColouringPuzzle" /> record type.
/// </summary>
public sealed class MapColouringPuzzleTests
{
    private static readonly Region R0 = Region.FromId("R0");
    private static readonly Region R1 = Region.FromId("R1");
    private static readonly Region R2 = Region.FromId("R2");
    private static readonly Region R3 = Region.FromId("R3");

    [UnitTest]
    public sealed class Equals_Method
    {
        [Fact]
        public void InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            MapColouringPuzzle sut = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1, R2])
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R1, R2)
                .Build();

            // Act
            var result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveEquivalentRegionDataAndNeighbourPairsLists_ReturnsTrue()
        {
            // Arrange
            MapColouringPuzzle sut = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1, R2])
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R1, R2)
                .Build();

            MapColouringPuzzle other = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.White, Colour.Black)
                .AddRegions([R2, R1, R0])
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R0, R1)
                .Build();

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveNonEquivalentRegionDataLists_ReturnsFalse()
        {
            // Arrange
            MapColouringPuzzle sut = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1])
                .Build();

            MapColouringPuzzle other = MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black, Colour.White)
                .AddRegionWithColours(R1, Colour.White)
                .Build();

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveNonEquivalentNeighbourPairsLists_ReturnsTrue()
        {
            // Arrange
            MapColouringPuzzle sut = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegions([R0, R1, R2])
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R1, R2)
                .Build();

            MapColouringPuzzle other = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.White, Colour.Black)
                .AddRegions([R2, R1, R0])
                .SetAsNeighbours(R0, R2)
                .Build();

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ValidSolution_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ValidSolution_ReturnsSuccess(MapColouringPuzzle sut, IReadOnlyDictionary<Region, Colour> solution)
        {
            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionSizeIsTooSmall_ReturnsFailure()
        {
            // Arrange
            MapColouringPuzzle sut = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R0, R1])
                .Build();

            Dictionary<Region, Colour> solution = new()
            {
                [R0] = Colour.Black
            };

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Solution size is 1, should be 2.");
        }


        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionSizeIsTooLarge_ReturnsFailure()
        {
            // Arrange
            MapColouringPuzzle sut = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R0, R1])
                .Build();

            Dictionary<Region, Colour> solution = new()
            {
                [R0] = Colour.Black,
                [R1] = Colour.Black,
                [R2] = Colour.Black
            };

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Solution size is 3, should be 2.");
        }

        [Fact]
        public void SolutionArgIsNull_Throws()
        {
            // Arrange
            MapColouringPuzzle sut = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegion(R0)
                .Build();

            // Act
            Action act = () => sut.ValidSolution(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'solution')");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void RegionNotPresentAsSolutionKey_ReturnsFailure()
        {
            // Arrange
            MapColouringPuzzle sut = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R0, R1, R2])
                .Build();

            Dictionary<Region, Colour> solution = new()
            {
                [R0] = Colour.Black,
                [R3] = Colour.Black,
                [R2] = Colour.Black
            };

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Region R1 not present as solution key.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void RegionAssignedIllegalColour_ReturnsFailure()
        {
            // Arrange
            MapColouringPuzzle sut = MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black, Colour.White)
                .AddRegionWithColours(R1, Colour.White)
                .Build();

            Dictionary<Region, Colour> solution = new()
            {
                [R0] = Colour.Blue,
                [R1] = Colour.White
            };

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Region R0 assigned colour (Blue) outside its permitted colours.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void NeighbouringRegionsAssignedSameColour_ReturnsFailure()
        {
            // Arrange
            MapColouringPuzzle sut = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R0, R1, R2])
                .SetAsNeighbours(R1, R2)
                .Build();

            Dictionary<Region, Colour> solution = new()
            {
                [R0] = Colour.Black,
                [R1] = Colour.Black,
                [R2] = Colour.Black
            };

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Neighbouring regions R1 and R2 assigned same colour (Black).");
        }

        private sealed class TestCases : TheoryData<MapColouringPuzzle, IReadOnlyDictionary<Region, Colour>>
        {
            public TestCases()
            {
                Add(MapColouringPuzzle.Create()
                    .WithRegionSpecificColours()
                    .AddRegionWithColours(R0, Colour.Black)
                    .AddRegionWithColours(R1, Colour.Black, Colour.White)
                    .Build(), new Dictionary<Region, Colour>
                {
                    [R0] = Colour.Black,
                    [R1] = Colour.Black
                });

                Add(MapColouringPuzzle.Create()
                    .WithRegionSpecificColours()
                    .AddRegionWithColours(R0, Colour.Black)
                    .AddRegionWithColours(R1, Colour.Black, Colour.White)
                    .Build(), new Dictionary<Region, Colour>
                {
                    [R0] = Colour.Black,
                    [R1] = Colour.White
                });

                Add(MapColouringPuzzle.Create()
                    .WithRegionSpecificColours()
                    .AddRegionWithColours(R0, Colour.Black)
                    .AddRegionWithColours(R1, Colour.Black, Colour.White)
                    .SetAsNeighbours(R0, R1)
                    .Build(), new Dictionary<Region, Colour>
                {
                    [R0] = Colour.Black,
                    [R1] = Colour.White
                });

                Add(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Red, Colour.Green, Colour.Blue)
                    .AddRegions([R0, R1, R2, R3])
                    .SetAsNeighbours(R0, R1)
                    .SetAsNeighbours(R1, R2)
                    .Build(), new Dictionary<Region, Colour>
                {
                    [R0] = Colour.Red,
                    [R1] = Colour.Green,
                    [R2] = Colour.Red,
                    [R3] = Colour.Red
                });

                Add(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Red, Colour.Green, Colour.Blue)
                    .AddRegions([R0, R1, R2, R3])
                    .SetAsNeighbours(R0, R1)
                    .SetAsNeighbours(R1, R2)
                    .Build(), new Dictionary<Region, Colour>
                {
                    [R0] = Colour.Red,
                    [R1] = Colour.Blue,
                    [R2] = Colour.Red,
                    [R3] = Colour.Red
                });

                Add(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Red, Colour.Green, Colour.Blue)
                    .AddRegions([R0, R1, R2, R3])
                    .SetAsNeighbours(R0, R1)
                    .SetAsNeighbours(R1, R2)
                    .Build(), new Dictionary<Region, Colour>
                {
                    [R0] = Colour.Red,
                    [R1] = Colour.Green,
                    [R2] = Colour.Blue,
                    [R3] = Colour.Red
                });

                Add(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Red, Colour.Green, Colour.Blue)
                    .AddRegions([R0, R1, R2, R3])
                    .SetAsNeighbours(R0, R1)
                    .SetAsNeighbours(R1, R2)
                    .Build(), new Dictionary<Region, Colour>
                {
                    [R0] = Colour.Red,
                    [R1] = Colour.Green,
                    [R2] = Colour.Blue,
                    [R3] = Colour.Green
                });

                Add(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Red, Colour.Green, Colour.Blue)
                    .AddRegions([R0, R1, R2, R3])
                    .SetAsNeighbours(R0, R1)
                    .SetAsNeighbours(R1, R2)
                    .Build(), new Dictionary<Region, Colour>
                {
                    [R0] = Colour.Red,
                    [R1] = Colour.Green,
                    [R2] = Colour.Blue,
                    [R3] = Colour.Blue
                });

                Add(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                    .AddRegions([R0, R1, R2, R3])
                    .SetAsNeighbours(R0, R1)
                    .SetAsNeighbours(R1, R2)
                    .SetAsNeighbours(R0, R3)
                    .SetAsNeighbours(R1, R3)
                    .SetAsNeighbours(R2, R3)
                    .Build(), new Dictionary<Region, Colour>
                {
                    [R0] = Colour.Red,
                    [R1] = Colour.Green,
                    [R2] = Colour.Blue,
                    [R3] = Colour.Yellow
                });

                Add(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                    .AddRegions([R0, R1, R2, R3])
                    .SetAsNeighbours(R0, R1)
                    .SetAsNeighbours(R1, R2)
                    .SetAsNeighbours(R0, R3)
                    .SetAsNeighbours(R1, R3)
                    .SetAsNeighbours(R2, R3)
                    .Build(), new Dictionary<Region, Colour>
                {
                    [R0] = Colour.Blue,
                    [R1] = Colour.Green,
                    [R2] = Colour.Red,
                    [R3] = Colour.Yellow
                });

                Add(MapColouringPuzzle.Create()
                    .WithGlobalColours(Colour.Red, Colour.Green, Colour.Blue, Colour.Yellow)
                    .AddRegions([R0, R1, R2, R3])
                    .SetAsNeighbours(R0, R1)
                    .SetAsNeighbours(R1, R2)
                    .SetAsNeighbours(R0, R3)
                    .SetAsNeighbours(R1, R3)
                    .SetAsNeighbours(R2, R3)
                    .Build(), new Dictionary<Region, Colour>
                {
                    [R0] = Colour.Yellow,
                    [R1] = Colour.Green,
                    [R2] = Colour.Red,
                    [R3] = Colour.Blue
                });
            }
        }
    }

    [UnitTest]
    public sealed class FluentBuilder_HappyPath
    {
        [Fact]
        public void CanBuildUsingGlobalColours()
        {
            // Act
            MapColouringPuzzle result = MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black, Colour.White)
                .AddRegion(R0)
                .AddRegion("R1")
                .AddRegion(R2)
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours("R1", "R2")
                .Build();

            // Assert
            using (new AssertionScope())
            {
                result.RegionData.Should().BeEquivalentTo([
                    new RegionDatum(R0, [Colour.Black, Colour.White]),
                    new RegionDatum(R1, [Colour.Black, Colour.White]),
                    new RegionDatum(R2, [Colour.Black, Colour.White])
                ], options => options.WithoutStrictOrdering());
                result.NeighbourPairs.Should().BeEquivalentTo([
                    new NeighbourPair(R0, R1),
                    new NeighbourPair(R1, R2)
                ], options => options.WithoutStrictOrdering());
            }
        }

        [Fact]
        public void CanBuildUsingPresetMapAndGlobalColours()
        {
            // Arrange
            PresetMap presetMap = new()
            {
                Regions = [R0, R1, R2, R3],
                NeighbourPairs = [new NeighbourPair(R0, R3)]
            };

            // Act
            MapColouringPuzzle result = MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(presetMap, [Colour.Black, Colour.Red])
                .Build();

            // Assert
            using (new AssertionScope())
            {
                result.RegionData.Should().BeEquivalentTo([
                    new RegionDatum(R0, [Colour.Black, Colour.Red]),
                    new RegionDatum(R1, [Colour.Black, Colour.Red]),
                    new RegionDatum(R2, [Colour.Black, Colour.Red]),
                    new RegionDatum(R3, [Colour.Black, Colour.Red])
                ], options => options.WithoutStrictOrdering());
                result.NeighbourPairs.Should().BeEquivalentTo([
                    new NeighbourPair(R0, R3)
                ]);
            }
        }

        [Fact]
        public void CanBuildUsingRegionSpecificColours()
        {
            // Act
            MapColouringPuzzle result = MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours("R0")
                .AddRegionWithColours(R1, Colour.Black)
                .AddRegionWithColours(R2, Colour.Red, Colour.White)
                .SetAsNeighbours(R0, R2)
                .SetAsNeighbours("R1", "R2")
                .Build();

            // Assert
            using (new AssertionScope())
            {
                result.RegionData.Should().BeEquivalentTo([
                    new RegionDatum(R0, []),
                    new RegionDatum(R1, [Colour.Black]),
                    new RegionDatum(R2, [Colour.Red, Colour.White])
                ], options => options.WithoutStrictOrdering());
                result.NeighbourPairs.Should().BeEquivalentTo([
                    new NeighbourPair(R0, R2),
                    new NeighbourPair(R1, R2)
                ], options => options.WithoutStrictOrdering());
            }
        }
    }

    [UnitTest]
    public sealed class FluentBuilder_SadPath
    {
        [Fact]
        public void WithPresetMapAndGlobalColours_PresetMapArgIsNull_Throws()
        {
            // Act
            Action act = () => MapColouringPuzzle.Create()
                .WithPresetMapAndGlobalColours(null!, Colour.Black)
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'presetMap')");
        }

        [Fact]
        public void AddRegion_StringOverload_RegionIdArgIsNull_Throws()
        {
            // Act
            Action act = () => MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegion(null!)
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'regionId')");
        }

        [Fact]
        public void AddRegions_RegionsArgIsNull_Throws()
        {
            // Act
            Action act = () => MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions(null!)
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'regions')");
        }

        [Fact]
        public void AddRegionWithColours_StringOverload_RegionIdArgIsNull_Throws()
        {
            // Act
            Action act = () => MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(null!, Colour.Black)
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'regionId')");
        }

        [Fact]
        public void SetAsNeighbours_StringOverload_RegionAIdArgIsNull_Throws()
        {
            // Act
            Action act = () => MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours("R0", Colour.Black)
                .AddRegionWithColours("R1", Colour.White)
                .SetAsNeighbours(null!, "R1")
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'regionAId')");
        }

        [Fact]
        public void SetAsNeighbours_StringOverload_RegionBIdArgIsNull_Throws()
        {
            // Act
            Action act = () => MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours("R0", Colour.Black)
                .AddRegionWithColours("R1", Colour.White)
                .SetAsNeighbours("R0", null!)
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'regionBId')");
        }

        [Fact]
        public void SetAsNeighbours_IEnumerableOverload_NeighbourPairsArgIsNull_Throws()
        {
            // Act
            Action act = () => MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours("R0", Colour.Black)
                .AddRegionWithColours("R1", Colour.White)
                .SetAsNeighbours(null!)
                .Build();

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'neighbourPairs')");
        }

        [Fact]
        public void InstantiatedPuzzleHasNoRegionData_Throws()
        {
            // Act
            Action act = () => MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .Build();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("RegionData is empty.");
        }

        [Fact]
        public void InstantiatedPuzzleHasRegionDataWithDuplicateRegionValues_Throws()
        {
            // Act
            Action act = () => MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegion(R0)
                .AddRegion(R1)
                .AddRegion(R2)
                .AddRegion(R1)
                .Build();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("RegionData has multiple items with same Region \"R1\".");
        }

        [Fact]
        public void InstantiatedPuzzleHasNeighbourPairWithNoRegionDatumMatchingFirstValue_Throws()
        {
            // Act
            Action act = () => MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R1, R2])
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R0, R1)
                .Build();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("NeighbourPairs has item without matching RegionData item for one or both Regions: " +
                             "NeighbourPair { First = R0, Second = R1 }.");
        }

        [Fact]
        public void InstantiatedPuzzleHasNeighbourPairWithNoRegionDatumMatchingSecondValue_Throws()
        {
            // Act
            Action act = () => MapColouringPuzzle.Create()
                .WithGlobalColours(Colour.Black)
                .AddRegions([R1, R2])
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R1, R3)
                .Build();

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("NeighbourPairs has item without matching RegionData item for one or both Regions: " +
                             "NeighbourPair { First = R1, Second = R3 }.");
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Fact]
        public void CanSerializeToJson_ThenDeserialize()
        {
            // Arrange
            MapColouringPuzzle original = MapColouringPuzzle.Create()
                .WithRegionSpecificColours()
                .AddRegionWithColours(R0, Colour.Black, Colour.White)
                .AddRegionWithColours(R1)
                .AddRegionWithColours(R2, Colour.Red, Colour.Green, Colour.White)
                .AddRegionWithColours(R3, Colour.Red, Colour.Green, Colour.Blue, Colour.White)
                .SetAsNeighbours(R0, R1)
                .SetAsNeighbours(R1, R2)
                .SetAsNeighbours(R2, R3)
                .Build();

            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<MapColouringPuzzle>(json, jsonOptions);

            // Act
            deserialized.Should().Be(original);
        }
    }
}
