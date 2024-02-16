using System.Text.Json;
using FluentAssertions.Execution;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.MapColouring;

/// <summary>
///     Unit tests for the <see cref="NeighbourPair" /> record type.
/// </summary>
public sealed class NeighbourPairTests
{
    private static readonly Region R0 = Region.FromId("R0");
    private static readonly Region R1 = Region.FromId("R1");
    private static readonly Region R8 = Region.FromId("R8");
    private static readonly Region R9 = Region.FromId("R9");

    [UnitTest]
    public sealed class Constructor_TwoArgs
    {
        [Fact]
        public void Initializes_SetsFirstAndSecondFromArgs()
        {
            // Act
            NeighbourPair result = new(R0, R1);

            // Assert
            using (new AssertionScope())
            {
                result.First.Should().Be(R0);
                result.Second.Should().Be(R1);
            }
        }

        [Fact]
        public void FirstAndSecondArgsAreEqual_Throws()
        {
            // Act
            Action act = () => _ = new NeighbourPair(R0, R0);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("First must precede Second.");
        }

        [Fact]
        public void FirstArgFollowsSecondArg_Throws()
        {
            // Act
            Action act = () => _ = new NeighbourPair(R1, R0);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("First must precede Second.");
        }
    }

    [UnitTest]
    public sealed class CompareTo_Method
    {
        [Fact]
        public void InstanceFirstValuePrecedesOtherFirstValue_ReturnsNegativeValue()
        {
            // Arrange
            NeighbourPair sut = new(R0, R9);
            NeighbourPair other = new(R1, R9);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualFirstValues_InstanceSecondValuePrecedesOtherSecondValue_ReturnsNegativeValue()
        {
            // Arrange
            NeighbourPair sut = new(R0, R8);
            NeighbourPair other = new(R0, R9);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BeNegative();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualFirstValuesAndEqualSecondValues_ReturnsZero()
        {
            // Arrange
            NeighbourPair sut = new(R0, R1);
            NeighbourPair other = new(R0, R1);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().Be(0);
        }

        [Fact]
        public void InstanceFirstValueFollowsOtherFirstValue_ReturnsPositiveValue()
        {
            // Arrange
            NeighbourPair sut = new(R1, R9);
            NeighbourPair other = new(R0, R9);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualFirstValues_InstanceSecondValueFollowsOtherSecondValue_ReturnsPositiveValue()
        {
            // Arrange
            NeighbourPair sut = new(R0, R9);
            NeighbourPair other = new(R0, R8);

            // Act
            var result = sut.CompareTo(other);

            // Assert
            result.Should().BePositive();
        }

        [Fact]
        public void OtherArgIsNull_ReturnsPositiveValue()
        {
            // Arrange
            NeighbourPair sut = new(R0, R1);

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
            NeighbourPair sut = new(R0, R1);

            // Act
            var result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveEqualFirstValuesAndEqualSecondValues_ReturnsTrue()
        {
            // Arrange
            NeighbourPair sut = new(R0, R1);
            NeighbourPair other = new(R0, R1);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalFirstValues_ReturnsFalse()
        {
            // Arrange
            NeighbourPair sut = new(R0, R9);
            NeighbourPair other = new(R1, R9);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalSecondValues_ReturnsFalse()
        {
            // Arrange
            NeighbourPair sut = new(R0, R8);
            NeighbourPair other = new(R0, R9);

            // Act
            var result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            NeighbourPair sut = new(R0, R1);

            // Act
            var result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class Of_StaticMethod
    {
        [Fact]
        public void RegionAArgPrecedesRegionBArg_ReturnsInstance_FirstIsRegionA_SecondIsRegionB()
        {
            // Act
            NeighbourPair result = NeighbourPair.Of(R0, R1);

            // Assert
            using (new AssertionScope())
            {
                result.First.Should().Be(R0);
                result.Second.Should().Be(R1);
            }
        }

        [Fact]
        public void RegionAArgFollowsRegionBArg_ReturnsInstance_FirstIsRegionB_SecondIsRegionA()
        {
            // Act
            NeighbourPair result = NeighbourPair.Of(R1, R0);

            // Assert
            using (new AssertionScope())
            {
                result.First.Should().Be(R0);
                result.Second.Should().Be(R1);
            }
        }

        [Fact]
        public void RegionAArgIsEqualToRegionBArg_Throws()
        {
            // Act
            Action act = () => _ = NeighbourPair.Of(R0, R0);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Region cannot be neighbour of itself.");
        }
    }

    [UnitTest]
    public sealed class Equality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveEqualFirstValuesAndEqualSecondValues_ReturnsTrue()
        {
            // Arrange
            NeighbourPair sut = new(R0, R1);
            NeighbourPair other = new(R0, R1);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalFirstValues_ReturnsFalse()
        {
            // Arrange
            NeighbourPair sut = new(R0, R9);
            NeighbourPair other = new(R1, R9);

            // Act
            var result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalSecondValues_ReturnsFalse()
        {
            // Arrange
            NeighbourPair sut = new(R0, R8);
            NeighbourPair other = new(R0, R9);

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
        public void InstanceAndOtherHaveEqualFirstValuesAndEqualSecondValues_ReturnsFalse()
        {
            // Arrange
            NeighbourPair sut = new(R0, R1);
            NeighbourPair other = new(R0, R1);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalFirstValues_ReturnsTrue()
        {
            // Arrange
            NeighbourPair sut = new(R0, R9);
            NeighbourPair other = new(R1, R9);

            // Act
            var result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveUnequalSecondValues_ReturnsTrue()
        {
            // Arrange
            NeighbourPair sut = new(R0, R8);
            NeighbourPair other = new(R0, R9);

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
            Region first = Region.FromId("FairIsle");
            Region second = Region.FromId("Hebrides");
            NeighbourPair original = new(first, second);
            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<NeighbourPair>(json, jsonOptions);

            // Assert
            deserialized.Should().Be(original);
        }
    }
}
