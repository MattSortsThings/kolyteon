using System.Text.Json;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Unit.Solving;

public static class OrderingStrategyTests
{
    [UnitTest]
    public sealed class StaticProperties
    {
        [Fact]
        public void NaturalOrdering_ReturnsNaturalOrdering()
        {
            // Act
            OrderingStrategy result = OrderingStrategy.NaturalOrdering;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(0);
                result.Code.Should().Be("NO");
                result.Name.Should().Be("Natural Ordering");
            }
        }

        [Fact]
        public void BrelazHeuristic_ReturnsBrelazHeuristic()
        {
            // Act
            OrderingStrategy result = OrderingStrategy.BrelazHeuristic;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(1);
                result.Code.Should().Be("BZ");
                result.Name.Should().Be("Brelaz Heuristic");
            }
        }

        [Fact]
        public void MaxCardinality_ReturnsMaxCardinality()
        {
            // Act
            OrderingStrategy result = OrderingStrategy.MaxCardinality;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(2);
                result.Code.Should().Be("MC");
                result.Name.Should().Be("Max Cardinality");
            }
        }

        [Fact]
        public void MaxTightness_ReturnsMaxTightness()
        {
            // Act
            OrderingStrategy result = OrderingStrategy.MaxTightness;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(3);
                result.Code.Should().Be("MT");
                result.Name.Should().Be("Max Tightness");
            }
        }
    }

    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceGivenItselfAsOther_ReturnsZero()
        {
            // Arrange
            OrderingStrategy sut = OrderingStrategy.MaxCardinality;

            // Act
            int result = sut.CompareTo(sut);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceNumberPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            OrderingStrategy sut = OrderingStrategy.FromNumber(0);
            OrderingStrategy other = OrderingStrategy.FromNumber(3);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualNumberValues_ReturnsZero()
        {
            // Arrange
            const int sharedNumber = 2;

            OrderingStrategy sut = OrderingStrategy.FromNumber(sharedNumber);
            OrderingStrategy other = OrderingStrategy.FromNumber(sharedNumber);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceNumberFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            OrderingStrategy sut = OrderingStrategy.FromNumber(3);
            OrderingStrategy other = OrderingStrategy.FromNumber(0);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_OtherArgIsNull_ReturnsPositiveValue()
        {
            // Arrange
            OrderingStrategy sut = OrderingStrategy.MaxCardinality;

            // Act
            int result = sut.CompareTo(null);

            // Assert
            result.Should().BePositive();
        }
    }

    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            OrderingStrategy sut = OrderingStrategy.MaxCardinality;

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualNumberValues_ReturnsTrue()
        {
            // Arrange
            const int sharedNumber = 2;

            OrderingStrategy sut = OrderingStrategy.FromNumber(sharedNumber);
            OrderingStrategy other = OrderingStrategy.FromNumber(sharedNumber);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalNumberValues_ReturnsFalse()
        {
            // Arrange
            OrderingStrategy sut = OrderingStrategy.FromNumber(3);
            OrderingStrategy other = OrderingStrategy.FromNumber(0);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            OrderingStrategy sut = OrderingStrategy.MaxCardinality;

            // Act
            bool result = sut.Equals(null!);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        public static TheoryData<OrderingStrategy, string> TestCases => new()
        {
            { OrderingStrategy.NaturalOrdering, "NO" },
            { OrderingStrategy.BrelazHeuristic, "BZ" },
            { OrderingStrategy.MaxCardinality, "MC" },
            { OrderingStrategy.MaxTightness, "MT" }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(ToStringMethod))]
        public void ToString_ReturnsCode(OrderingStrategy sut, string expected)
        {
            // Act
            string result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }
    }

    [UnitTest]
    public sealed class ToLongStringMethod
    {
        public static TheoryData<OrderingStrategy, string> TestCases => new()
        {
            { OrderingStrategy.NaturalOrdering, "NO (Natural Ordering)" },
            { OrderingStrategy.BrelazHeuristic, "BZ (Brelaz Heuristic)" },
            { OrderingStrategy.MaxCardinality, "MC (Max Cardinality)" },
            { OrderingStrategy.MaxTightness, "MT (Max Tightness)" }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(ToLongStringMethod))]
        public void ToLongString_ReturnsDescriptiveString(OrderingStrategy sut, string expected)
        {
            // Act
            string result = sut.ToLongString();

            // Assert
            result.Should().Be(expected);
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        [Fact]
        public void Equality_InstanceAndOtherHaveEqualNumberValues_ReturnsTrue()
        {
            // Arrange
            const int sharedNumber = 2;

            OrderingStrategy sut = OrderingStrategy.FromNumber(sharedNumber);
            OrderingStrategy other = OrderingStrategy.FromNumber(sharedNumber);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalNumberValues_ReturnsFalse()
        {
            // Arrange
            OrderingStrategy sut = OrderingStrategy.FromNumber(3);
            OrderingStrategy other = OrderingStrategy.FromNumber(0);

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
        public void Inequality_InstanceAndOtherHaveEqualNumberValues_ReturnsFalse()
        {
            // Arrange
            const int sharedNumber = 2;

            OrderingStrategy sut = OrderingStrategy.FromNumber(sharedNumber);
            OrderingStrategy other = OrderingStrategy.FromNumber(sharedNumber);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalNumberValues_ReturnsTrue()
        {
            // Arrange
            OrderingStrategy sut = OrderingStrategy.FromNumber(3);
            OrderingStrategy other = OrderingStrategy.FromNumber(0);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class GetValuesStaticMethod
    {
        [Fact]
        public void GetValues_ReturnsListOfAllFourOrderingStrategiesInAscendingOrder()
        {
            // Act
            IReadOnlyList<OrderingStrategy> result = OrderingStrategy.GetValues();

            // Assert
            result.Should().Equal(OrderingStrategy.NaturalOrdering,
                OrderingStrategy.BrelazHeuristic,
                OrderingStrategy.MaxCardinality,
                OrderingStrategy.MaxTightness);
        }
    }

    [UnitTest]
    public sealed class FromCodeStaticFactoryMethod
    {
        public static TheoryData<string, OrderingStrategy> HappyPathTestCases => new()
        {
            { "NO", OrderingStrategy.NaturalOrdering },
            { "BZ", OrderingStrategy.BrelazHeuristic },
            { "MC", OrderingStrategy.MaxCardinality },
            { "MT", OrderingStrategy.MaxTightness }
        };

        [Theory]
        [MemberData(nameof(HappyPathTestCases), MemberType = typeof(FromCodeStaticFactoryMethod))]
        public void FromCode_CodeMatchesOrderingStrategy_ReturnsOrderingStrategy(string code, OrderingStrategy expected)
        {
            // Act
            OrderingStrategy result = OrderingStrategy.FromCode(code);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("NO ")]
        [InlineData("no")]
        [InlineData("Natural Ordering")]
        [InlineData("NotAnOrderingStrategy")]
        public void FromCode_NoOrderingStrategyMatchingCode_Throws(string code)
        {
            // Act
            Action act = () => OrderingStrategy.FromCode(code);

            // Assert
            string expectedMessage = $"No Ordering Strategy exists with Code value '{code}'.";

            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage);
        }
    }

    [UnitTest]
    public sealed class FromNumberStaticFactoryMethod
    {
        public static TheoryData<int, OrderingStrategy> HappyPathTestCases => new()
        {
            { 0, OrderingStrategy.NaturalOrdering },
            { 1, OrderingStrategy.BrelazHeuristic },
            { 2, OrderingStrategy.MaxCardinality },
            { 3, OrderingStrategy.MaxTightness }
        };

        [Theory]
        [MemberData(nameof(HappyPathTestCases), MemberType = typeof(FromNumberStaticFactoryMethod))]
        public void FromNumber_NumberMatchesOrderingStrategy_ReturnsOrderingStrategy(int number, OrderingStrategy expected)
        {
            // Act
            OrderingStrategy result = OrderingStrategy.FromNumber(number);

            // Assert
            result.Should().Be(expected);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(4)]
        [InlineData(99)]
        public void FromNumber_NoOrderingStrategyMatchingNumber_Throws(int number)
        {
            // Act
            Action act = () => OrderingStrategy.FromNumber(number);

            // Assert
            string expectedMessage = $"No Ordering Strategy exists with Number value '{number}'.";

            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage);
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [InlineData("NO", "\"NO\"")]
        [InlineData("MC", "\"MC\"")]
        public void SerializesToCodeStringValue(string code, string expected)
        {
            // Arrange
            OrderingStrategy originalStrategy = OrderingStrategy.FromCode(code);

            // Act
            string json = JsonSerializer.Serialize(originalStrategy, JsonSerializerOptions.Default);

            // Assert
            json.Should().Be(expected);
        }

        [Theory]
        [InlineData("\"NO\"", "NO")]
        [InlineData("\"MC\"", "MC")]
        public void DeserializesFromCodeStringValue(string json, string expectedStrategyCode)
        {
            // Arrange
            OrderingStrategy? deserializedStrategy =
                JsonSerializer.Deserialize<OrderingStrategy>(json, JsonSerializerOptions.Default);

            // Assert
            OrderingStrategy expectedStrategy = OrderingStrategy.FromCode(expectedStrategyCode);

            deserializedStrategy.Should().NotBeNull().And.Be(expectedStrategy);
        }
    }
}
