using System.Text.Json;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Unit.Solving;

public static class CheckingStrategyTests
{
    [UnitTest]
    public sealed class StaticProperties
    {
        [Fact]
        public void NaiveBacktracking_ReturnsNaiveBacktrackingInstance()
        {
            // Act
            CheckingStrategy result = CheckingStrategy.NaiveBacktracking;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(0);
                result.Code.Should().Be("BT");
                result.Name.Should().Be("Naive Backtracking");
                result.Type.Should().Be(CheckingStrategyType.Retrospective);
            }
        }

        [Fact]
        public void Backjumping_ReturnsBackjumpingInstance()
        {
            // Act
            CheckingStrategy result = CheckingStrategy.Backjumping;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(1);
                result.Code.Should().Be("BJ");
                result.Name.Should().Be("Backjumping");
                result.Type.Should().Be(CheckingStrategyType.Retrospective);
            }
        }

        [Fact]
        public void GraphBasedBackjumping_ReturnsGraphBasedBackjumpingInstance()
        {
            // Act
            CheckingStrategy result = CheckingStrategy.GraphBasedBackjumping;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(2);
                result.Code.Should().Be("GBJ");
                result.Name.Should().Be("Graph-Based Backjumping");
                result.Type.Should().Be(CheckingStrategyType.Retrospective);
            }
        }

        [Fact]
        public void ConflictDirectedBackjumping_ReturnsConflictDirectedBackjumpingInstance()
        {
            // Act
            CheckingStrategy result = CheckingStrategy.ConflictDirectedBackjumping;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(3);
                result.Code.Should().Be("CBJ");
                result.Name.Should().Be("Conflict-Directed Backjumping");
                result.Type.Should().Be(CheckingStrategyType.Retrospective);
            }
        }

        [Fact]
        public void ForwardChecking_ReturnsForwardCheckingInstance()
        {
            // Act
            CheckingStrategy result = CheckingStrategy.ForwardChecking;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(4);
                result.Code.Should().Be("FC");
                result.Name.Should().Be("Forward Checking");
                result.Type.Should().Be(CheckingStrategyType.Prospective);
            }
        }

        [Fact]
        public void PartialLookingAhead_ReturnsPartialLookingAheadInstance()
        {
            // Act
            CheckingStrategy result = CheckingStrategy.PartialLookingAhead;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(5);
                result.Code.Should().Be("PLA");
                result.Name.Should().Be("Partial Looking Ahead");
                result.Type.Should().Be(CheckingStrategyType.Prospective);
            }
        }

        [Fact]
        public void FullLookingAhead_ReturnsFullLookingAheadInstance()
        {
            // Act
            CheckingStrategy result = CheckingStrategy.FullLookingAhead;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(6);
                result.Code.Should().Be("FLA");
                result.Name.Should().Be("Full Looking Ahead");
                result.Type.Should().Be(CheckingStrategyType.Prospective);
            }
        }

        [Fact]
        public void MaintainingArcConsistency_ReturnsMaintainingArcConsistencyInstance()
        {
            // Act
            CheckingStrategy result = CheckingStrategy.MaintainingArcConsistency;

            // Assert
            using (new AssertionScope())
            {
                result.Number.Should().Be(7);
                result.Code.Should().Be("MAC");
                result.Name.Should().Be("Maintaining Arc Consistency");
                result.Type.Should().Be(CheckingStrategyType.Prospective);
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
            CheckingStrategy sut = CheckingStrategy.ForwardChecking;

            // Act
            int result = sut.CompareTo(sut);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceNumberPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            CheckingStrategy sut = CheckingStrategy.FromNumber(0);
            CheckingStrategy other = CheckingStrategy.FromNumber(7);

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

            CheckingStrategy sut = CheckingStrategy.FromNumber(sharedNumber);
            CheckingStrategy other = CheckingStrategy.FromNumber(sharedNumber);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceNumberFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            CheckingStrategy sut = CheckingStrategy.FromNumber(7);
            CheckingStrategy other = CheckingStrategy.FromNumber(0);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_OtherArgIsNull_ReturnsPositiveValue()
        {
            // Arrange
            CheckingStrategy sut = CheckingStrategy.ForwardChecking;

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
            CheckingStrategy sut = CheckingStrategy.ForwardChecking;

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

            CheckingStrategy sut = CheckingStrategy.FromNumber(sharedNumber);
            CheckingStrategy other = CheckingStrategy.FromNumber(sharedNumber);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalNumberValues_ReturnsFalse()
        {
            // Arrange
            CheckingStrategy sut = CheckingStrategy.FromNumber(7);
            CheckingStrategy other = CheckingStrategy.FromNumber(0);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            CheckingStrategy sut = CheckingStrategy.ForwardChecking;

            // Act
            bool result = sut.Equals(null!);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        public static TheoryData<CheckingStrategy, string> TestCases => new()
        {
            { CheckingStrategy.NaiveBacktracking, "BT" },
            { CheckingStrategy.Backjumping, "BJ" },
            { CheckingStrategy.GraphBasedBackjumping, "GBJ" },
            { CheckingStrategy.ConflictDirectedBackjumping, "CBJ" },
            { CheckingStrategy.ForwardChecking, "FC" },
            { CheckingStrategy.PartialLookingAhead, "PLA" },
            { CheckingStrategy.FullLookingAhead, "FLA" },
            { CheckingStrategy.MaintainingArcConsistency, "MAC" }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(ToStringMethod))]
        public void ToString_ReturnsCode(CheckingStrategy sut, string expected)
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
        public static TheoryData<CheckingStrategy, string> TestCases => new()
        {
            { CheckingStrategy.NaiveBacktracking, "BT (Naive Backtracking, Retrospective)" },
            { CheckingStrategy.Backjumping, "BJ (Backjumping, Retrospective)" },
            { CheckingStrategy.GraphBasedBackjumping, "GBJ (Graph-Based Backjumping, Retrospective)" },
            { CheckingStrategy.ConflictDirectedBackjumping, "CBJ (Conflict-Directed Backjumping, Retrospective)" },
            { CheckingStrategy.ForwardChecking, "FC (Forward Checking, Prospective)" },
            { CheckingStrategy.PartialLookingAhead, "PLA (Partial Looking Ahead, Prospective)" },
            { CheckingStrategy.FullLookingAhead, "FLA (Full Looking Ahead, Prospective)" },
            { CheckingStrategy.MaintainingArcConsistency, "MAC (Maintaining Arc Consistency, Prospective)" }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(ToLongStringMethod))]
        public void ToLongString_ReturnsDescriptiveString(CheckingStrategy sut, string expected)
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

            CheckingStrategy sut = CheckingStrategy.FromNumber(sharedNumber);
            CheckingStrategy other = CheckingStrategy.FromNumber(sharedNumber);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalNumberValues_ReturnsFalse()
        {
            // Arrange
            CheckingStrategy sut = CheckingStrategy.FromNumber(7);
            CheckingStrategy other = CheckingStrategy.FromNumber(0);

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

            CheckingStrategy sut = CheckingStrategy.FromNumber(sharedNumber);
            CheckingStrategy other = CheckingStrategy.FromNumber(sharedNumber);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalNumberValues_ReturnsTrue()
        {
            // Arrange
            CheckingStrategy sut = CheckingStrategy.FromNumber(7);
            CheckingStrategy other = CheckingStrategy.FromNumber(0);

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
        public void GetValues_ReturnsListOfAllEightCheckingStrategiesInAscendingOrder()
        {
            // Act
            IReadOnlyList<CheckingStrategy> result = CheckingStrategy.GetValues();

            // Assert
            result.Should().Equal(CheckingStrategy.NaiveBacktracking,
                CheckingStrategy.Backjumping,
                CheckingStrategy.GraphBasedBackjumping,
                CheckingStrategy.ConflictDirectedBackjumping,
                CheckingStrategy.ForwardChecking,
                CheckingStrategy.PartialLookingAhead,
                CheckingStrategy.FullLookingAhead,
                CheckingStrategy.MaintainingArcConsistency);
        }
    }

    [UnitTest]
    public sealed class FromCodeStaticFactoryMethod
    {
        public static TheoryData<string, CheckingStrategy> HappyPathTestCases => new()
        {
            { "BT", CheckingStrategy.NaiveBacktracking },
            { "BJ", CheckingStrategy.Backjumping },
            { "PLA", CheckingStrategy.PartialLookingAhead },
            { "MAC", CheckingStrategy.MaintainingArcConsistency }
        };

        [Theory]
        [MemberData(nameof(HappyPathTestCases), MemberType = typeof(FromCodeStaticFactoryMethod))]
        public void FromCode_CodeMatchesCheckingStrategy_ReturnsCheckingStrategy(string code, CheckingStrategy expected)
        {
            // Act
            CheckingStrategy result = CheckingStrategy.FromCode(code);

            // Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("BT ")]
        [InlineData("bt")]
        [InlineData("Naive Backtracking")]
        [InlineData("NotACheckingStrategy")]
        public void FromCode_NoCheckingStrategyMatchingCode_Throws(string code)
        {
            // Act
            Action act = () => CheckingStrategy.FromCode(code);

            // Assert
            string expectedMessage = $"No Checking Strategy exists with Code value '{code}'.";

            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage);
        }
    }

    [UnitTest]
    public sealed class FromNumberStaticFactoryMethod
    {
        public static TheoryData<int, CheckingStrategy> HappyPathTestCases => new()
        {
            { 0, CheckingStrategy.NaiveBacktracking },
            { 1, CheckingStrategy.Backjumping },
            { 5, CheckingStrategy.PartialLookingAhead },
            { 7, CheckingStrategy.MaintainingArcConsistency }
        };

        [Theory]
        [MemberData(nameof(HappyPathTestCases), MemberType = typeof(FromNumberStaticFactoryMethod))]
        public void FromNumber_NumberMatchesCheckingStrategy_ReturnsCheckingStrategy(int number, CheckingStrategy expected)
        {
            // Act
            CheckingStrategy result = CheckingStrategy.FromNumber(number);

            // Assert
            result.Should().Be(expected);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(8)]
        [InlineData(99)]
        public void FromNumber_NoCheckingStrategyMatchingNumber_Throws(int number)
        {
            // Act
            Action act = () => CheckingStrategy.FromNumber(number);

            // Assert
            string expectedMessage = $"No Checking Strategy exists with Number value '{number}'.";

            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage);
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [InlineData("BT", "\"BT\"")]
        [InlineData("MAC", "\"MAC\"")]
        public void SerializesToCodeStringValue(string code, string expected)
        {
            // Arrange
            CheckingStrategy originalStrategy = CheckingStrategy.FromCode(code);

            // Act
            string json = JsonSerializer.Serialize(originalStrategy, JsonSerializerOptions.Default);

            // Assert
            json.Should().Be(expected);
        }

        [Theory]
        [InlineData("\"BT\"", "BT")]
        [InlineData("\"MAC\"", "MAC")]
        public void DeserializesFromCodeStringValue(string json, string expectedStrategyCode)
        {
            // Arrange
            CheckingStrategy? deserializedStrategy =
                JsonSerializer.Deserialize<CheckingStrategy>(json, JsonSerializerOptions.Default);

            // Assert
            CheckingStrategy expectedStrategy = CheckingStrategy.FromCode(expectedStrategyCode);

            deserializedStrategy.Should().NotBeNull().And.Be(expectedStrategy);
        }
    }
}
