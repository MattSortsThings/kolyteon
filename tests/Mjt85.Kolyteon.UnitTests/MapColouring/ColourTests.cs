using System.Text.Json;
using FluentAssertions.Execution;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.MapColouring;

/// <summary>
///     Unit tests for the <see cref="Colour" /> struct type.
/// </summary>
public sealed class ColourTests
{
    [UnitTest]
    public sealed class Constructor_ZeroArgs
    {
        [Fact]
        public void Initializes_IdIsZero_NameIsBlack()
        {
            // Act
            Colour result = new();

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(0);
                result.Name.Should().Be("Black");
            }
        }
    }

    [UnitTest]
    public sealed class StaticFields
    {
        [Fact]
        public void Black_IdIsZero_NameIsBlack()
        {
            // Act
            Colour result = Colour.Black;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(0);
                result.Name.Should().Be("Black");
            }
        }

        [Fact]
        public void Blue_IdIsNine_NameIsBlue()
        {
            // Act
            Colour result = Colour.Blue;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(9);
                result.Name.Should().Be("Blue");
            }
        }

        [Fact]
        public void Cyan_IdIsEleven_NameIsCyan()
        {
            // Act
            Colour result = Colour.Cyan;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(11);
                result.Name.Should().Be("Cyan");
            }
        }

        [Fact]
        public void DarkBlue_IdIsOne_NameIsDarkBlue()
        {
            // Act
            Colour result = Colour.DarkBlue;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(1);
                result.Name.Should().Be("DarkBlue");
            }
        }

        [Fact]
        public void DarkCyan_IdIsThree_NameIsDarkCyan()
        {
            // Act
            Colour result = Colour.DarkCyan;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(3);
                result.Name.Should().Be("DarkCyan");
            }
        }

        [Fact]
        public void DarkGreen_IdIsTwo_NameIsDarkGreen()
        {
            // Act
            Colour result = Colour.DarkGreen;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(2);
                result.Name.Should().Be("DarkGreen");
            }
        }

        [Fact]
        public void DarkGrey_IdIsEight_NameIsDarkGrey()
        {
            // Act
            Colour result = Colour.DarkGrey;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(8);
                result.Name.Should().Be("DarkGrey");
            }
        }

        [Fact]
        public void DarkMagenta_IdIsFive_NameIsDarkMagenta()
        {
            // Act
            Colour result = Colour.DarkMagenta;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(5);
                result.Name.Should().Be("DarkMagenta");
            }
        }

        [Fact]
        public void DarkRed_IdIsFour_NameIsDarkRed()
        {
            // Act
            Colour result = Colour.DarkRed;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(4);
                result.Name.Should().Be("DarkRed");
            }
        }

        [Fact]
        public void DarkYellow_IdIsSix_NameIsDarkYellow()
        {
            // Act
            Colour result = Colour.DarkYellow;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(6);
                result.Name.Should().Be("DarkYellow");
            }
        }

        [Fact]
        public void Green_IdIsTen_NameIsGreen()
        {
            // Act
            Colour result = Colour.Green;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(10);
                result.Name.Should().Be("Green");
            }
        }

        [Fact]
        public void Grey_IdIsSeven_NameIsGrey()
        {
            // Act
            Colour result = Colour.Grey;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(7);
                result.Name.Should().Be("Grey");
            }
        }

        [Fact]
        public void Magenta_IdIsThirteen_NameIsMagenta()
        {
            // Act
            Colour result = Colour.Magenta;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(13);
                result.Name.Should().Be("Magenta");
            }
        }

        [Fact]
        public void Red_IdIsTwelve_NameIsRed()
        {
            // Act
            Colour result = Colour.Red;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(12);
                result.Name.Should().Be("Red");
            }
        }

        [Fact]
        public void White_IdIsFifteen_NameIsWhite()
        {
            // Act
            Colour result = Colour.White;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(15);
                result.Name.Should().Be("White");
            }
        }

        [Fact]
        public void Yellow_IdIsFourteen_NameIsYellow()
        {
            // Act
            Colour result = Colour.Yellow;

            // Assert
            using (new AssertionScope())
            {
                result.Id.Should().Be(14);
                result.Name.Should().Be("Yellow");
            }
        }
    }

    [UnitTest]
    public sealed class CompareTo_Method
    {
        [Fact]
        public void InstanceAndOtherHaveEqualIdValues_ReturnsZero()
        {
            // Arrange
            const int sharedId = 4;
            Colour sut = Colour.FromId(sharedId);
            Colour other = Colour.FromId(sharedId);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void InstanceHasSmallerIdValueThanOther_ReturnsNegativeValue()
        {
            // Arrange
            Colour sut = Colour.FromId(4);
            Colour other = Colour.FromId(15);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceHasGreaterIdValueThanOther_ReturnsPositiveValue()
        {
            // Arrange
            Colour sut = Colour.FromId(4);
            Colour other = Colour.FromId(0);

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
        public void InstanceAndOtherHaveEqualIdValues_ReturnsTrue()
        {
            // Arrange
            const int sharedId = 1;
            Colour sut = Colour.FromId(sharedId);
            Colour other = Colour.FromId(sharedId);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalIdValues_ReturnsFalse()
        {
            // Arrange
            Colour sut = Colour.FromId(0);
            Colour other = Colour.FromId(15);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsName(Colour sut, string expected)
        {
            // Act
            var result = sut.ToString();

            // Assert
            result.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<Colour, string>
        {
            public TestCases()
            {
                Add(new Colour(), "Black");
                Add(Colour.Black, "Black");
                Add(Colour.Magenta, "Magenta");
                Add(Colour.DarkMagenta, "DarkMagenta");
            }
        }
    }

    [UnitTest]
    public sealed class FromConsoleColor_StaticFactoryMethod
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsMatchingColourValue(ConsoleColor value, Colour expected)
        {
            // Act
            Colour result = Colour.FromConsoleColor(value);

            // Assert
            result.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<ConsoleColor, Colour>
        {
            public TestCases()
            {
                Add(ConsoleColor.Black, Colour.Black);
                Add(ConsoleColor.DarkBlue, Colour.DarkBlue);
                Add(ConsoleColor.DarkGreen, Colour.DarkGreen);
                Add(ConsoleColor.DarkCyan, Colour.DarkCyan);
                Add(ConsoleColor.DarkRed, Colour.DarkRed);
                Add(ConsoleColor.DarkMagenta, Colour.DarkMagenta);
                Add(ConsoleColor.DarkYellow, Colour.DarkYellow);
                Add(ConsoleColor.Gray, Colour.Grey);
                Add(ConsoleColor.DarkGray, Colour.DarkGrey);
                Add(ConsoleColor.Blue, Colour.Blue);
                Add(ConsoleColor.Green, Colour.Green);
                Add(ConsoleColor.Cyan, Colour.Cyan);
                Add(ConsoleColor.Red, Colour.Red);
                Add(ConsoleColor.Magenta, Colour.Magenta);
                Add(ConsoleColor.Yellow, Colour.Yellow);
                Add(ConsoleColor.White, Colour.White);
            }
        }
    }

    [UnitTest]
    public sealed class FromId_StaticFactoryMethod
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsMatchingColourValue(int id, Colour expected)
        {
            // Act
            Colour result = Colour.FromId(id);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-1, "No Colour value with Id = -1.")]
        [InlineData(16, "No Colour value with Id = 16.")]
        [InlineData(99, "No Colour value with Id = 99.")]
        public void IdArgIsNegativeOrGreaterThanFifteen_Throws(int id, string expectedMessage)
        {
            // Act
            Action act = () => Colour.FromId(id);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage);
        }

        private sealed class TestCases : TheoryData<int, Colour>
        {
            public TestCases()
            {
                Add(0, Colour.Black);
                Add(1, Colour.DarkBlue);
                Add(2, Colour.DarkGreen);
                Add(3, Colour.DarkCyan);
                Add(4, Colour.DarkRed);
                Add(5, Colour.DarkMagenta);
                Add(6, Colour.DarkYellow);
                Add(7, Colour.Grey);
                Add(8, Colour.DarkGrey);
                Add(9, Colour.Blue);
                Add(10, Colour.Green);
                Add(11, Colour.Cyan);
                Add(12, Colour.Red);
                Add(13, Colour.Magenta);
                Add(14, Colour.Yellow);
                Add(15, Colour.White);
            }
        }
    }

    [UnitTest]
    public sealed class FromName_StaticFactoryMethod
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsMatchingColourValue(string name, Colour expected)
        {
            // Act
            Colour result = Colour.FromName(name);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("", """No Colour value with Name = "".""")]
        [InlineData(" ", """No Colour value with Name = " ".""")]
        [InlineData("NotAColour", """No Colour value with Name = "NotAColour".""")]
        [InlineData("Blue ", """No Colour value with Name = "Blue ".""")]
        public void NameArgIsNotNameOfColourValue_Throws(string name, string expectedMessage)
        {
            // Act
            Action act = () => _ = Colour.FromName(name);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage);
        }

        private sealed class TestCases : TheoryData<string, Colour>
        {
            public TestCases()
            {
                Add("Black", Colour.Black);
                Add("DarkBlue", Colour.DarkBlue);
                Add("DarkGreen", Colour.DarkGreen);
                Add("DarkCyan", Colour.DarkCyan);
                Add("DarkRed", Colour.DarkRed);
                Add("DarkMagenta", Colour.DarkMagenta);
                Add("DarkYellow", Colour.DarkYellow);
                Add("Grey", Colour.Grey);
                Add("DarkGrey", Colour.DarkGrey);
                Add("Blue", Colour.Blue);
                Add("Green", Colour.Green);
                Add("Cyan", Colour.Cyan);
                Add("Red", Colour.Red);
                Add("Magenta", Colour.Magenta);
                Add("Yellow", Colour.Yellow);
                Add("White", Colour.White);
            }
        }
    }

    [UnitTest]
    public sealed class GetNames_StaticMethod
    {
        [Fact]
        public void ReturnsNamesOfAllSixteenColourValuesOrderedById()
        {
            // Act
            IEnumerable<string> result = Colour.GetNames();

            // Assert
            string[] expected =
            [
                "Black", "DarkBlue", "DarkGreen", "DarkCyan",
                "DarkRed", "DarkMagenta", "DarkYellow", "Grey",
                "DarkGrey", "Blue", "Green", "Cyan",
                "Red", "Magenta", "Yellow", "White"
            ];

            result.Should().Equal(expected);
        }
    }

    [UnitTest]
    public sealed class GetValues_StaticMethod
    {
        [Fact]
        public void ReturnsAllSixteenColourValuesOrderedById()
        {
            // Act
            IReadOnlyList<Colour> result = Colour.GetValues();

            // Assert
            Colour[] expected =
            [
                Colour.Black, Colour.DarkBlue, Colour.DarkGreen, Colour.DarkCyan,
                Colour.DarkRed, Colour.DarkMagenta, Colour.DarkYellow, Colour.Grey,
                Colour.DarkGrey, Colour.Blue, Colour.Green, Colour.Cyan,
                Colour.Red, Colour.Magenta, Colour.Yellow, Colour.White
            ];

            result.Should().Equal(expected);
        }
    }

    [UnitTest]
    public sealed class Colour_Operator
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsMatchingColourValue(int id, Colour expected)
        {
            // Act
            var result = (Colour)id;

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(-1, "No Colour value with Id = -1.")]
        [InlineData(16, "No Colour value with Id = 16.")]
        [InlineData(99, "No Colour value with Id = 99.")]
        public void IdArgIsNegativeOrGreaterThanFifteen_Throws(int id, string expectedMessage)
        {
            // Act
            Action act = () => _ = (Colour)id;

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage);
        }

        private sealed class TestCases : TheoryData<int, Colour>
        {
            public TestCases()
            {
                Add(0, Colour.Black);
                Add(1, Colour.DarkBlue);
                Add(2, Colour.DarkGreen);
                Add(3, Colour.DarkCyan);
                Add(4, Colour.DarkRed);
                Add(5, Colour.DarkMagenta);
                Add(6, Colour.DarkYellow);
                Add(7, Colour.Grey);
                Add(8, Colour.DarkGrey);
                Add(9, Colour.Blue);
                Add(10, Colour.Green);
                Add(11, Colour.Cyan);
                Add(12, Colour.Red);
                Add(13, Colour.Magenta);
                Add(14, Colour.Yellow);
                Add(15, Colour.White);
            }
        }
    }

    [UnitTest]
    public sealed class Equality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualIdValues_ReturnsTrue()
        {
            // Arrange
            const int sharedId = 1;
            Colour sut = Colour.FromId(sharedId);
            Colour other = Colour.FromId(sharedId);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalIdValues_ReturnsFalse()
        {
            // Arrange
            Colour sut = Colour.FromId(0);
            Colour other = Colour.FromId(15);

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
        public void InstanceAndOtherHaveEqualIdValues_ReturnsFalse()
        {
            // Arrange
            const int sharedId = 1;
            Colour sut = Colour.FromId(sharedId);
            Colour other = Colour.FromId(sharedId);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalIdValues_ReturnsTrue()
        {
            // Arrange
            Colour sut = Colour.FromId(0);
            Colour other = Colour.FromId(15);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Int_Operator
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsIdValue(Colour sut, int expected)
        {
            // Act
            var result = (int)sut;

            // Assert
            result.Should().Be(expected);
        }

        private sealed class TestCases : TheoryData<Colour, int>
        {
            public TestCases()
            {
                Add(Colour.Black, 0);
                Add(Colour.DarkBlue, 1);
                Add(Colour.DarkGreen, 2);
                Add(Colour.DarkCyan, 3);
                Add(Colour.DarkRed, 4);
                Add(Colour.DarkMagenta, 5);
                Add(Colour.DarkYellow, 6);
                Add(Colour.Grey, 7);
                Add(Colour.DarkGrey, 8);
                Add(Colour.Blue, 9);
                Add(Colour.Green, 10);
                Add(Colour.Cyan, 11);
                Add(Colour.Red, 12);
                Add(Colour.Magenta, 13);
                Add(Colour.Yellow, 14);
                Add(Colour.White, 15);
            }
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Fact]
        public void SerializesToNameStringValue()
        {
            // Arrange
            Colour sut = Colour.DarkMagenta;
            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var result = JsonSerializer.Serialize(sut, jsonOptions);

            // Assert
            const string expected = """
                                    "DarkMagenta"
                                    """;
            result.Should().Be(expected);
        }

        [Fact]
        public void DeserializesFromNameStringValue()
        {
            // Arrange
            const string json = """
                                "DarkCyan"
                                """;
            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var result = JsonSerializer.Deserialize<Colour>(json, jsonOptions);

            // Assert
            result.Should().Be(Colour.DarkCyan);
        }

        [Fact]
        public void Deserializing_JsonStringValueIsNotColourName_Throws()
        {
            // Arrange
            const string json = """
                                "NotAColour"
                                """;
            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            Action act = () => JsonSerializer.Deserialize<Colour>(json, jsonOptions);

            // Assert
            act.Should().Throw<JsonException>()
                .WithMessage("Could not deserialize to Colour.")
                .WithInnerException<ArgumentException>()
                .WithMessage("""No Colour value with Name = "NotAColour".""");
        }
    }
}
