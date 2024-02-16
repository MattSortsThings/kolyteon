using System.Text.Json;
using FluentAssertions.Execution;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.MapColouring;

/// <summary>
///     Unit tests for the <see cref="RegionDatum" /> record type.
/// </summary>
public sealed class RegionDatumTests
{
    [UnitTest]
    public sealed class Constructor_TwoArgs
    {
        [Fact]
        public void Initializes_SetsRegionAndColoursFromArgs()
        {
            // Arrange
            Region R0 = Region.FromId("R0");
            HashSet<Colour> colours = [Colour.Black, Colour.White];

            // Act
            RegionDatum result = new(R0, colours);

            // Assert
            using (new AssertionScope())
            {
                result.Region.Should().Be(R0);
                result.Colours.Should().BeSameAs(colours);
            }
        }

        [Fact]
        public void ColoursArgIsNull_Throws()
        {
            // Arrange
            Region R0 = Region.FromId("R0");

            // Act
            Action act = () => _ = new RegionDatum(R0, null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'colours')");
        }
    }

    [UnitTest]
    public sealed class CompareTo_Method
    {
        [Fact]
        public void InstanceRegionValuePrecedesOtherRegionValue_ReturnsNegativeValue()
        {
            // Arrange
            Region R0 = Region.FromId("R0");
            Region R1 = Region.FromId("R1");
            HashSet<Colour> arbitraryColours = [Colour.Black];

            RegionDatum sut = new(R0, arbitraryColours);
            RegionDatum other = new(R1, arbitraryColours);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualRegionValues_ReturnsZero()
        {
            // Arrange
            Region R0 = Region.FromId("R0");
            HashSet<Colour> arbitraryColours = [Colour.Black];

            RegionDatum sut = new(R0, arbitraryColours);
            RegionDatum other = new(R0, arbitraryColours);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void InstanceRegionValueFollowsOtherRegionValue_ReturnsPositiveValue()
        {
            // Arrange
            Region R0 = Region.FromId("R0");
            Region R1 = Region.FromId("R1");
            HashSet<Colour> arbitraryColours = [Colour.Black];

            RegionDatum sut = new(R1, arbitraryColours);
            RegionDatum other = new(R0, arbitraryColours);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void OtherArgIsNull_ReturnsPositiveValue()
        {
            // Arrange
            RegionDatum sut = new(Region.FromId("R0"), [Colour.Black]);

            // Act
            var result = sut.CompareTo(null);

            // Assert
            result.Should().BePositive();
        }
    }

    [UnitTest]
    public sealed class Equals_Method
    {
        [Fact]
        public void InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            RegionDatum sut = new(Region.FromId("R0"), [Colour.Black]);

            // Act
            var result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualRegionValuesAndColoursCollectionsWithSameItems_ReturnsTrue()
        {
            // Arrange
            Region R0 = Region.FromId("R0");

            RegionDatum sut = new(R0, [Colour.Red, Colour.White, Colour.Blue]);
            RegionDatum other = new(R0, [Colour.Blue, Colour.Red, Colour.White]);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveNonEqualRegionValues_ReturnsFalse()
        {
            // Arrange
            HashSet<Colour> sharedColours = [Colour.Black];

            RegionDatum sut = new(Region.FromId("R0"), sharedColours);
            RegionDatum other = new(Region.FromId("R1"), sharedColours);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveColoursCollectionsWithDifferentItems_ReturnsFalse()
        {
            // Arrange
            Region R0 = Region.FromId("R0");

            RegionDatum sut = new(R0, [Colour.Black, Colour.White]);
            RegionDatum other = new(R0, [Colour.Black, Colour.Red]);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            RegionDatum sut = new(Region.FromId("R0"), [Colour.Black]);

            // Act
            var result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class Equality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualRegionValuesAndColoursCollectionsWithSameItems_ReturnsTrue()
        {
            // Arrange
            Region R0 = Region.FromId("R0");

            RegionDatum sut = new(R0, [Colour.Red, Colour.White, Colour.Blue]);
            RegionDatum other = new(R0, [Colour.Blue, Colour.Red, Colour.White]);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveNonEqualRegionValues_ReturnsFalse()
        {
            // Arrange
            HashSet<Colour> sharedColours = [Colour.Black];

            RegionDatum sut = new(Region.FromId("R0"), sharedColours);
            RegionDatum other = new(Region.FromId("R1"), sharedColours);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class Inequality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualRegionValuesAndColoursCollectionsWithSameItems_ReturnsFalse()
        {
            // Arrange
            Region R0 = Region.FromId("R0");

            RegionDatum sut = new(R0, [Colour.Red, Colour.White, Colour.Blue]);
            RegionDatum other = new(R0, [Colour.Blue, Colour.Red, Colour.White]);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveNonEqualRegionValues_ReturnsTrue()
        {
            // Arrange
            HashSet<Colour> sharedColours = [Colour.Black];

            RegionDatum sut = new(Region.FromId("R0"), sharedColours);
            RegionDatum other = new(Region.FromId("R1"), sharedColours);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Fact]
        public void CanSerializeToJson_ThenDeserialize()
        {
            // Arrange
            RegionDatum original = new(Region.FromId("R0"), new HashSet<Colour> { Colour.Red, Colour.Blue });
            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<RegionDatum>(json, jsonOptions);

            // Assert
            deserialized.Should().Be(original);
        }
    }
}
