using System.Text.Json;
using Kolyteon.Common;

namespace Kolyteon.Tests.Unit.Common;

public static class ColourTests
{
    [UnitTest]
    public sealed class ParameterlessConstructor
    {
        [Fact]
        public void ParameterlessConstructor_Initializes_HasNumberZeroAndNameBlack()
        {
            // Act
            Colour result = new();

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(0);
                result.Name.Should().Be("Black");
            }
        }
    }

    [UnitTest]
    public sealed class StaticProperties
    {
        [Fact]
        public void Black_ReturnsInstanceWithNumberZeroAndNameBlack()
        {
            // Act
            Colour result = Colour.Black;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(0);
                result.Name.Should().Be("Black");
            }
        }

        [Fact]
        public void Maroon_ReturnsInstanceWithNumberOneAndNameMaroon()
        {
            // Act
            Colour result = Colour.Maroon;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(1);
                result.Name.Should().Be("Maroon");
            }
        }

        [Fact]
        public void Green_ReturnsInstanceWithNumberTwoAndNameGreen()
        {
            // Act
            Colour result = Colour.Green;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(2);
                result.Name.Should().Be("Green");
            }
        }

        [Fact]
        public void Olive_ReturnsInstanceWithNumberThreeAndNameOlive()
        {
            // Act
            Colour result = Colour.Olive;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(3);
                result.Name.Should().Be("Olive");
            }
        }

        [Fact]
        public void Navy_ReturnsInstanceWithNumberFourAndNameNavy()
        {
            // Act
            Colour result = Colour.Navy;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(4);
                result.Name.Should().Be("Navy");
            }
        }

        [Fact]
        public void Purple_ReturnsInstanceWithNumberFiveAndNamePurple()
        {
            // Act
            Colour result = Colour.Purple;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(5);
                result.Name.Should().Be("Purple");
            }
        }

        [Fact]
        public void Teal_ReturnsInstanceWithNumberSixAndNameTeal()
        {
            // Act
            Colour result = Colour.Teal;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(6);
                result.Name.Should().Be("Teal");
            }
        }

        [Fact]
        public void Silver_ReturnsInstanceWithNumberSevenAndNameSilver()
        {
            // Act
            Colour result = Colour.Silver;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(7);
                result.Name.Should().Be("Silver");
            }
        }

        [Fact]
        public void Grey_ReturnsInstanceWithNumberEightAndNameGrey()
        {
            // Act
            Colour result = Colour.Grey;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(8);
                result.Name.Should().Be("Grey");
            }
        }

        [Fact]
        public void Red_ReturnsInstanceWithNumberNineAndNameRed()
        {
            // Act
            Colour result = Colour.Red;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(9);
                result.Name.Should().Be("Red");
            }
        }

        [Fact]
        public void Lime_ReturnsInstanceWithNumberTenAndNameLime()
        {
            // Act
            Colour result = Colour.Lime;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(10);
                result.Name.Should().Be("Lime");
            }
        }

        [Fact]
        public void Yellow_ReturnsInstanceWithNumberElevenAndNameYellow()
        {
            // Act
            Colour result = Colour.Yellow;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(11);
                result.Name.Should().Be("Yellow");
            }
        }

        [Fact]
        public void Blue_ReturnsInstanceWithNumberTwelveAndNameBlue()
        {
            // Act
            Colour result = Colour.Blue;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(12);
                result.Name.Should().Be("Blue");
            }
        }

        [Fact]
        public void Fuchsia_ReturnsInstanceWithNumberThirteenAndNameFuchsia()
        {
            // Act
            Colour result = Colour.Fuchsia;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(13);
                result.Name.Should().Be("Fuchsia");
            }
        }

        [Fact]
        public void Aqua_ReturnsInstanceWithNumberFourteenAndNameAqua()
        {
            // Act
            Colour result = Colour.Aqua;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(14);
                result.Name.Should().Be("Aqua");
            }
        }

        [Fact]
        public void White_ReturnsInstanceWithNumberFifteenAndNameWhite()
        {
            // Act
            Colour result = Colour.White;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(15);
                result.Name.Should().Be("White");
            }
        }
    }

    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceNumberPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            Colour sut = Colour.FromNumber(0);
            Colour other = Colour.FromNumber(15);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualNumberValues_ReturnsZero()
        {
            // Arrange
            const int sharedNumber = 9;

            Colour sut = Colour.FromNumber(sharedNumber);
            Colour other = Colour.FromNumber(sharedNumber);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceNumberFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            Colour sut = Colour.FromNumber(15);
            Colour other = Colour.FromNumber(0);

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
        public void Equals_InstanceAndOtherHaveEqualNumberValues_ReturnsTrue()
        {
            // Arrange
            const int sharedNumber = 9;

            Colour sut = Colour.FromNumber(sharedNumber);
            Colour other = Colour.FromNumber(sharedNumber);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalNumberValues_ReturnsFalse()
        {
            // Arrange
            Colour sut = Colour.FromNumber(15);
            Colour other = Colour.FromNumber(0);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        public static TheoryData<Colour, string> TestCases => new()
        {
            { Colour.Black, "Black" }, { Colour.Maroon, "Maroon" }, { Colour.Fuchsia, "Fuchsia" }, { Colour.White, "White" }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(ToStringMethod))]
        public void ToString_ReturnsName(Colour sut, string expected)
        {
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
        public void Equality_InstanceAndOtherHaveEqualNumberValues_ReturnsTrue()
        {
            // Arrange
            const int sharedNumber = 9;

            Colour sut = Colour.FromNumber(sharedNumber);
            Colour other = Colour.FromNumber(sharedNumber);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalNumberValues_ReturnsFalse()
        {
            // Arrange
            Colour sut = Colour.FromNumber(15);
            Colour other = Colour.FromNumber(0);

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
            const int sharedNumber = 9;

            Colour sut = Colour.FromNumber(sharedNumber);
            Colour other = Colour.FromNumber(sharedNumber);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalNumberValues_ReturnsTrue()
        {
            // Arrange
            Colour sut = Colour.FromNumber(15);
            Colour other = Colour.FromNumber(0);

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
        public void GetValues_ReturnsListOfAllSixteenColourValuesInAscendingOrder()
        {
            // Act
            IReadOnlyList<Colour> result = Colour.GetValues();

            // Assert
            result.Should().Equal(Colour.Black,
                Colour.Maroon,
                Colour.Green,
                Colour.Olive,
                Colour.Navy,
                Colour.Purple,
                Colour.Teal,
                Colour.Silver,
                Colour.Grey,
                Colour.Red,
                Colour.Lime,
                Colour.Yellow,
                Colour.Blue,
                Colour.Fuchsia,
                Colour.Aqua,
                Colour.White);
        }
    }

    [UnitTest]
    public sealed class FromNameStaticFactoryMethod
    {
        public static TheoryData<string, Colour> HappyPathTestCases => new()
        {
            { "Black", Colour.Black }, { "Teal", Colour.Teal }, { "Fuchsia", Colour.Fuchsia }, { "White", Colour.White }
        };

        [Theory]
        [MemberData(nameof(HappyPathTestCases), MemberType = typeof(FromNameStaticFactoryMethod))]
        public void FromName_NameMatchesColourValue_ReturnsColourValue(string name, Colour expected)
        {
            // Act
            Colour result = Colour.FromName(name);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("BLACK")]
        [InlineData("black")]
        [InlineData("NotAColour")]
        public void FromName_NoColourValueMatchingName_Throws(string name)
        {
            // Act
            Action act = () => Colour.FromName(name);

            // Assert
            string expectedMessage = $"No Colour exists with Name value '{name}'.";

            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage);
        }

        [Fact]
        public void FromName_NameArgIsNull_Throws()
        {
            // Act
            Action act = () => Colour.FromName(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'name')");
        }
    }

    [UnitTest]
    public sealed class FromNumberStaticFactoryMethod
    {
        public static TheoryData<int, Colour> HappyPathTestCases => new()
        {
            { 0, Colour.Black }, { 6, Colour.Teal }, { 13, Colour.Fuchsia }, { 15, Colour.White }
        };

        [Theory]
        [MemberData(nameof(HappyPathTestCases), MemberType = typeof(FromNumberStaticFactoryMethod))]
        public void FromNumber_NumberMatchesColourValue_ReturnsColourValue(int number, Colour expected)
        {
            // Act
            Colour result = Colour.FromNumber(number);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(16)]
        [InlineData(99)]
        public void FromNumber_NoColourMatchingNumber_Throws(int number)
        {
            // Act
            Action act = () => Colour.FromNumber(number);

            // Assert
            string expectedMessage = $"No Colour exists with Number value '{number}'.";

            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage);
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [InlineData("Black", "\"Black\"")]
        [InlineData("White", "\"White\"")]
        public void SerializesToNameStringValue(string name, string expected)
        {
            // Arrange
            Colour originalColour = Colour.FromName(name);

            // Act
            string json = JsonSerializer.Serialize(originalColour, JsonSerializerOptions.Default);

            // Assert
            json.Should().Be(expected);
        }

        [Theory]
        [InlineData("\"Black\"", "Black")]
        [InlineData("\"White\"", "White")]
        public void DeserializesFromNameStringValue(string json, string expectedColourName)
        {
            // Act
            Colour deserializedColour = JsonSerializer.Deserialize<Colour>(json, JsonSerializerOptions.Default);

            // Assert
            Colour expected = Colour.FromName(expectedColourName);

            deserializedColour.Should().Be(expected);
        }

        [Theory]
        [InlineData("Navy")]
        [InlineData("Olive")]
        [InlineData("Maroon")]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(string name)
        {
            // Arrange
            Colour originalColour = Colour.FromName(name);

            string json = JsonSerializer.Serialize(originalColour, JsonSerializerOptions.Default);

            // Act
            Colour deserializedColour = JsonSerializer.Deserialize<Colour>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedColour.Should().Be(originalColour);
        }
    }
}
