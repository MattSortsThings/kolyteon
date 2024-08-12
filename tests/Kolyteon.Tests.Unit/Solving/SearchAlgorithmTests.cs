using System.Text.Json;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Unit.Solving;

public static class SearchAlgorithmTests
{
    [UnitTest]
    public sealed class Constructor
    {
        [Fact]
        public void Constructor_CheckingStrategyArgIsNull_Throws()
        {
            // Arrange
            OrderingStrategy arbitraryOrderingStrategy = OrderingStrategy.NaturalOrdering;

            // Act
            Action act = () => _ = new SearchAlgorithm(null!, arbitraryOrderingStrategy);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'checkingStrategy')");
        }

        [Fact]
        public void Constructor_OrderingStrategyArgIsNull_Throws()
        {
            // Arrange
            CheckingStrategy arbitraryCheckingStrategy = CheckingStrategy.NaiveBacktracking;

            // Act
            Action act = () => _ = new SearchAlgorithm(arbitraryCheckingStrategy, null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'orderingStrategy')");
        }
    }

    [UnitTest]
    public sealed class CompareToMethod
    {
        [Fact]
        public void CompareTo_InstanceGivenItselfAsOther_ReturnsZero()
        {
            // Arrange
            SearchAlgorithm sut = new(CheckingStrategy.ForwardChecking, OrderingStrategy.BrelazHeuristic);

            // Act
            int result = sut.CompareTo(sut);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceCheckingStrategyPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            OrderingStrategy orderingStrategy = OrderingStrategy.BrelazHeuristic;

            SearchAlgorithm sut = new(CheckingStrategy.FromNumber(0), orderingStrategy);
            SearchAlgorithm other = new(CheckingStrategy.FromNumber(7), orderingStrategy);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceOrderingStrategyPrecedesOther_ReturnsNegativeValue()
        {
            // Arrange
            CheckingStrategy checkingStrategy = CheckingStrategy.ForwardChecking;

            SearchAlgorithm sut = new(checkingStrategy, OrderingStrategy.FromNumber(0));
            SearchAlgorithm other = new(checkingStrategy, OrderingStrategy.FromNumber(3));

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void CompareTo_InstanceAndOtherHaveEqualCheckingStrategyAndOrderingStrategyValues_ReturnsZero()
        {
            // Arrange
            CheckingStrategy checkingStrategy = CheckingStrategy.ForwardChecking;
            OrderingStrategy orderingStrategy = OrderingStrategy.BrelazHeuristic;

            SearchAlgorithm sut = new(checkingStrategy, orderingStrategy);
            SearchAlgorithm other = new(checkingStrategy, orderingStrategy);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void CompareTo_InstanceCheckingStrategyFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            OrderingStrategy orderingStrategy = OrderingStrategy.BrelazHeuristic;

            SearchAlgorithm sut = new(CheckingStrategy.FromNumber(7), orderingStrategy);
            SearchAlgorithm other = new(CheckingStrategy.FromNumber(0), orderingStrategy);

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_InstanceOrderingStrategyFollowsOther_ReturnsPositiveValue()
        {
            // Arrange
            CheckingStrategy checkingStrategy = CheckingStrategy.ForwardChecking;

            SearchAlgorithm sut = new(checkingStrategy, OrderingStrategy.FromNumber(3));
            SearchAlgorithm other = new(checkingStrategy, OrderingStrategy.FromNumber(0));

            // Act
            int result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void CompareTo_OtherArgIsNull_ReturnsPositiveValue()
        {
            // Arrange
            SearchAlgorithm sut = new(CheckingStrategy.ForwardChecking, OrderingStrategy.BrelazHeuristic);

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
            SearchAlgorithm sut = new(CheckingStrategy.ForwardChecking, OrderingStrategy.BrelazHeuristic);

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualCheckingStrategyAndOrderingStrategyValues_ReturnsTrue()
        {
            // Arrange
            CheckingStrategy checkingStrategy = CheckingStrategy.ForwardChecking;
            OrderingStrategy orderingStrategy = OrderingStrategy.BrelazHeuristic;

            SearchAlgorithm sut = new(checkingStrategy, orderingStrategy);
            SearchAlgorithm other = new(checkingStrategy, orderingStrategy);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalCheckingStrategyValues_ReturnsFalse()
        {
            // Arrange
            OrderingStrategy orderingStrategy = OrderingStrategy.BrelazHeuristic;

            SearchAlgorithm sut = new(CheckingStrategy.FromNumber(0), orderingStrategy);
            SearchAlgorithm other = new(CheckingStrategy.FromNumber(7), orderingStrategy);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalOrderingStrategyValues_ReturnsFalse()
        {
            // Arrange
            CheckingStrategy checkingStrategy = CheckingStrategy.ForwardChecking;

            SearchAlgorithm sut = new(checkingStrategy, OrderingStrategy.FromNumber(0));
            SearchAlgorithm other = new(checkingStrategy, OrderingStrategy.FromNumber(3));

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            SearchAlgorithm sut = new(CheckingStrategy.ForwardChecking, OrderingStrategy.BrelazHeuristic);

            // Act
            bool result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ToStringMethod
    {
        public static TheoryData<CheckingStrategy, OrderingStrategy, string> TestCases => new()
        {
            { CheckingStrategy.FromCode("BT"), OrderingStrategy.FromCode("NO"), "BT+NO" },
            { CheckingStrategy.FromCode("BJ"), OrderingStrategy.FromCode("MC"), "BJ+MC" },
            { CheckingStrategy.FromCode("PLA"), OrderingStrategy.FromCode("MT"), "PLA+MT" }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(ToStringMethod))]
        public void ToString_ReturnsStrategyCodesConcatenatedWithPlusSign(CheckingStrategy checkingStrategy,
            OrderingStrategy orderingStrategy,
            string expected)
        {
            // Arrange
            SearchAlgorithm sut = new(checkingStrategy, orderingStrategy);

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
        public void Equality_InstanceAndOtherHaveEqualCheckingStrategyAndOrderingStrategyValues_ReturnsTrue()
        {
            // Arrange
            CheckingStrategy checkingStrategy = CheckingStrategy.ForwardChecking;
            OrderingStrategy orderingStrategy = OrderingStrategy.BrelazHeuristic;

            SearchAlgorithm sut = new(checkingStrategy, orderingStrategy);
            SearchAlgorithm other = new(checkingStrategy, orderingStrategy);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalCheckingStrategyValues_ReturnsFalse()
        {
            // Arrange
            OrderingStrategy orderingStrategy = OrderingStrategy.BrelazHeuristic;

            SearchAlgorithm sut = new(CheckingStrategy.FromNumber(0), orderingStrategy);
            SearchAlgorithm other = new(CheckingStrategy.FromNumber(7), orderingStrategy);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalOrderingStrategyValues_ReturnsFalse()
        {
            // Arrange
            CheckingStrategy checkingStrategy = CheckingStrategy.ForwardChecking;

            SearchAlgorithm sut = new(checkingStrategy, OrderingStrategy.FromNumber(0));
            SearchAlgorithm other = new(checkingStrategy, OrderingStrategy.FromNumber(3));

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
        public void Inequality_InstanceAndOtherHaveEqualCheckingStrategyAndOrderingStrategyValues_ReturnsFalse()
        {
            // Arrange
            CheckingStrategy checkingStrategy = CheckingStrategy.ForwardChecking;
            OrderingStrategy orderingStrategy = OrderingStrategy.BrelazHeuristic;

            SearchAlgorithm sut = new(checkingStrategy, orderingStrategy);
            SearchAlgorithm other = new(checkingStrategy, orderingStrategy);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalCheckingStrategyValues_ReturnsTrue()
        {
            // Arrange
            OrderingStrategy orderingStrategy = OrderingStrategy.BrelazHeuristic;

            SearchAlgorithm sut = new(CheckingStrategy.FromNumber(0), orderingStrategy);
            SearchAlgorithm other = new(CheckingStrategy.FromNumber(7), orderingStrategy);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalOrderingStrategyValues_ReturnsTrue()
        {
            // Arrange
            CheckingStrategy checkingStrategy = CheckingStrategy.ForwardChecking;

            SearchAlgorithm sut = new(checkingStrategy, OrderingStrategy.FromNumber(0));
            SearchAlgorithm other = new(checkingStrategy, OrderingStrategy.FromNumber(3));

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        public static TheoryData<CheckingStrategy, OrderingStrategy> TestCases => new()
        {
            { CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering },
            { CheckingStrategy.Backjumping, OrderingStrategy.MaxCardinality },
            { CheckingStrategy.PartialLookingAhead, OrderingStrategy.BrelazHeuristic }
        };

        [Theory]
        [MemberData(nameof(TestCases), MemberType = typeof(Serialization))]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(CheckingStrategy checkingStrategy,
            OrderingStrategy orderingStrategy)
        {
            // Arrange
            SearchAlgorithm originalAlgorithm = new(checkingStrategy, orderingStrategy);

            string json = JsonSerializer.Serialize(originalAlgorithm, JsonSerializerOptions.Default);

            // Act
            SearchAlgorithm? deserializedAlgorithm =
                JsonSerializer.Deserialize<SearchAlgorithm>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedAlgorithm.Should().NotBeNull().And.Be(originalAlgorithm);
        }
    }
}
