using System.Text.Json;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.MapColouring;

/// <summary>
///     Unit tests for the <see cref="Region" /> struct type.
/// </summary>
public sealed class RegionTests
{
    [UnitTest]
    public sealed class Constructor_ZeroArgs
    {
        [Fact]
        public void Initializes_IdIsDefault()
        {
            // Act
            Region result = new();

            // Assert
            result.Should().BeOfType<Region>().Which.Id.Should().Be("Default");
        }
    }

    [UnitTest]
    public sealed class CompareTo_Method
    {
        [Fact]
        public void InstanceIdValuePrecedesOtherIdValue_OrdinalStringComparison_ReturnsNegativeValue()
        {
            // Arrange
            Region sut = Region.FromId("Forties");
            Region other = Region.FromId("Viking");

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceIdValueEqualToOtherIdValue_OrdinalStringComparison_ReturnsZero()
        {
            // Arrange
            const string sharedId = "Forties";
            Region sut = Region.FromId(sharedId);
            Region other = Region.FromId(sharedId);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void InstanceIdValueFollowsOtherIdValue_OrdinalStringComparison_ReturnsPositiveValue()
        {
            // Arrange
            Region sut = Region.FromId("Forties");
            Region other = Region.FromId("Biscay");

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }
    }

    [UnitTest]
    public sealed class Equals_Method
    {
        [Fact]
        public void InstanceAndOtherHaveEqualIdValues_OrdinalStringComparison_ReturnsTrue()
        {
            // Arrange
            const string sharedId = "Fastnet";
            Region sut = Region.FromId(sharedId);
            Region other = Region.FromId(sharedId);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("FASTNET")]
        [InlineData("FastNet")]
        [InlineData("0")]
        public void InstanceAndOtherHaveUnequalIdValues_OrdinalStringComparison_ReturnsFalse(string id)
        {
            // Arrange
            Region sut = Region.FromId("Fastnet");
            Region other = Region.FromId(id);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToString_Method
    {
        [Theory]
        [InlineData("Lundy")]
        [InlineData("GermanBight")]
        public void ReturnsId(string id)
        {
            // Arrange
            Region sut = Region.FromId(id);

            // Act
            var result = sut.ToString();

            // Assert
            result.Should().Be(id);
        }
    }

    [UnitTest]
    public sealed class FromId_StaticMethod
    {
        [Theory]
        [InlineData("A")]
        [InlineData("00")]
        [InlineData("IrishSea")]
        public void ReturnsInstanceWithSpecifiedId(string id)
        {
            // Act
            Region result = Region.FromId(id);

            // Assert
            result.Should().BeOfType<Region>().Which.Id.Should().Be(id);
        }

        [Fact]
        public void IdArgIsNull_Throws()
        {
            // Act
            Action act = () => Region.FromId(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'id')");
        }

        [Fact]
        public void IdArgIsEmptyString_Throws()
        {
            // Act
            Action act = () => Region.FromId("");

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Value must be a non-empty string of letters and/or digits only. (Parameter 'id')");
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("South-East_Iceland")]
        [InlineData("ABC ")]
        [InlineData("!")]
        [InlineData("A\nB")]
        public void IdArgContainsNonAlphanumericChar_Throws(string id)
        {
            // Act
            Action act = () => Region.FromId(id);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Value must be a non-empty string of letters and/or digits only. (Parameter 'id')");
        }
    }

    [UnitTest]
    public sealed class Equality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualIdValues_OrdinalStringComparison_ReturnsTrue()
        {
            // Arrange
            const string sharedId = "Fastnet";
            Region sut = Region.FromId(sharedId);
            Region other = Region.FromId(sharedId);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("FASTNET")]
        [InlineData("FastNet")]
        [InlineData("0")]
        public void InstanceAndOtherHaveUnequalIdValues_OrdinalStringComparison_ReturnsFalse(string id)
        {
            // Arrange
            Region sut = Region.FromId("Fastnet");
            Region other = Region.FromId(id);

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
        public void InstanceAndOtherHaveEqualIdValues_OrdinalStringComparison_ReturnsFalse()
        {
            // Arrange
            const string sharedId = "Fastnet";
            Region sut = Region.FromId(sharedId);
            Region other = Region.FromId(sharedId);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("FASTNET")]
        [InlineData("FastNet")]
        [InlineData("0")]
        public void InstanceAndOtherHaveUnequalIdValues_OrdinalStringComparison_ReturnsTrue(string id)
        {
            // Arrange
            Region sut = Region.FromId("Fastnet");
            Region other = Region.FromId(id);

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
        public void SerializesToIdStringValue()
        {
            // Arrange
            Region original = Region.FromId("IrishSea");
            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var result = JsonSerializer.Serialize(original, jsonOptions);

            // Assert
            result.Should().Be("""
                               "IrishSea"
                               """);
        }

        [Fact]
        public void DeserializesFromIdStringValue()
        {
            // Arrange
            const string json = """
                                "IrishSea"
                                """;
            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var result = JsonSerializer.Deserialize<Region>(json, jsonOptions);

            // Assert
            result.Should().BeOfType<Region>().Which.Id.Should().Be("IrishSea");
        }

        [Fact]
        public void Deserializing_JsonStringValueIsInvalidId_Throws()
        {
            // Arrange
            const string json = """
                                "Irish___Sea"
                                """;
            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            Action act = () => JsonSerializer.Deserialize<Region>(json, jsonOptions);

            // Assert
            act.Should().Throw<JsonException>()
                .WithMessage("Could not deserialize to Region.")
                .WithInnerException<ArgumentException>()
                .WithMessage("Value must be a non-empty string of letters and/or digits only. (Parameter 'id')");
        }
    }
}
