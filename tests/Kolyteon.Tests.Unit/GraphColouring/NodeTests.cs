using System.Text.Json;
using Kolyteon.GraphColouring;

namespace Kolyteon.Tests.Unit.GraphColouring;

public static class NodeTests
{
    [UnitTest]
    public sealed class ParameterlessConstructor
    {
        [Fact]
        public void ParameterlessConstructor_Initializes_HasNameUndefinedAllLowerCase()
        {
            // Act
            Node node = new();

            // Assert
            node.Name.Should().Be("undefined");
        }
    }

    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceNamePrecedesOtherUsingOrdinalStringComparison_ReturnsNegativeValue()
        {
            // Arrange
            Node sut = Node.FromName("AA");
            Node other = Node.FromName("ZZ");

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualNameValuesUsingOrdinalStringComparison_ReturnsZero()
        {
            // Arrange
            const string sharedName = "MM";

            Node sut = Node.FromName(sharedName);
            Node other = Node.FromName(sharedName);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceNameFollowsOtherUsingOrdinalStringComparison_ReturnsPositiveValue()
        {
            // Arrange
            Node sut = Node.FromName("ZZ");
            Node other = Node.FromName("AA");

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }
    }

    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceAndOtherHaveEqualNameValuesUsingOrdinalStringComparison_ReturnsTrue()
        {
            // Arrange
            const string sharedName = "MM";

            Node sut = Node.FromName(sharedName);
            Node other = Node.FromName(sharedName);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalNameValuesUsingOrdinalStringComparison_ReturnsFalse()
        {
            // Arrange
            Node sut = Node.FromName("ZZ");
            Node other = Node.FromName("AA");

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        [Theory]
        [InlineData("r0", "(r0)")]
        [InlineData("Lundy", "(Lundy)")]
        [InlineData("Fastnet", "(Fastnet)")]
        [InlineData("Irish Sea", "(Irish Sea)")]
        public void ToString_ReturnsFormattedString(string name, string expected)
        {
            // Arrange
            Node sut = Node.FromName(name);

            // Act
            string result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        [Fact]
        public void Equality_InstanceAndOtherHaveEqualNameValuesUsingOrdinalStringComparison_ReturnsTrue()
        {
            // Arrange
            const string sharedName = "MM";

            Node sut = Node.FromName(sharedName);
            Node other = Node.FromName(sharedName);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalNameValuesUsingOrdinalStringComparison_ReturnsFalse()
        {
            // Arrange
            Node sut = Node.FromName("Aa");
            Node other = Node.FromName("aa");

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class InequalityOperator
    {
        [Fact]
        public void Inequality_InstanceAndOtherHaveEqualNameValuesUsingOrdinalStringComparison_ReturnsFalse()
        {
            // Arrange
            const string sharedName = "MM";

            Node sut = Node.FromName(sharedName);
            Node other = Node.FromName(sharedName);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalNameValuesUsingOrdinalStringComparison_ReturnsTrue()
        {
            // Arrange
            Node sut = Node.FromName("ZZ");
            Node other = Node.FromName("AA");

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class FromNameStaticFactoryMethod
    {
        [Theory]
        [InlineData("A")]
        [InlineData("0")]
        [InlineData("r0")]
        public void FromName_GivenName_ReturnsInstanceWithGivenName(string name)
        {
            // Act
            Node result = Node.FromName(name);

            // Assert
            result.Name.Should().Be(name);
        }

        [Fact]
        public void FromName_NameArgIsNull_Throws()
        {
            // Act
            Action act = () => Node.FromName(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'name')");
        }

        [Fact]
        public void FromName_NameArgIsEmptyString_Throws()
        {
            // Act
            Action act = () => Node.FromName(string.Empty);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("The value cannot be an empty string or composed entirely of whitespace. " +
                             "(Parameter 'name')");
        }

        [Fact]
        public void FromName_NameArgIsAllWhiteSpace_Throws()
        {
            // Act
            Action act = () => Node.FromName("             ");

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("The value cannot be an empty string or composed entirely of whitespace. " +
                             "(Parameter 'name')");
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [InlineData("Lundy")]
        [InlineData("Fastnet")]
        [InlineData("Irish Sea")]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(string name)
        {
            // Arrange
            Node originalNode = Node.FromName(name);

            string json = JsonSerializer.Serialize(originalNode, JsonSerializerOptions.Default);

            // Act
            Node deserializedNode = JsonSerializer.Deserialize<Node>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedNode.Should().Be(originalNode);
        }
    }
}
