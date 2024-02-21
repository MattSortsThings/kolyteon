using FluentAssertions.Execution;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Modelling;

/// <summary>
///     Unit tests for the <see cref="BinaryCsp{P,V,D}" /> abstract base class, using the <see cref="TestBinaryCsp" />
///     derivative.
/// </summary>
public sealed partial class BinaryCspTests
{
    private static readonly Digit[] DigitsFromOneToFive = [Digit.One, Digit.Two, Digit.Three, Digit.Four, Digit.Five];

    [UnitTest]
    public sealed class Constructor_OneArg
    {
        [Fact]
        public void Initializes_SetsCapacityFromArg_AllOtherPropertiesHaveValueOfZero()
        {
            // Arrange
            const int capacity = 5;

            // Act
            TestBinaryCsp result = new(capacity);

            // Assert
            using (new AssertionScope())
            {
                result.Capacity.Should().Be(capacity);
                result.Variables.Should().Be(0);
                result.Constraints.Should().Be(0);
                result.ConstraintDensity.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
                result.ConstraintTightness.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
            }
        }

        [Fact]
        public void CapacityArgIsNegative_Throws()
        {
            // Act
            Action act = () => _ = new TestBinaryCsp(-1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Value must not be negative. (Parameter 'capacity')\nActual value was -1.");
        }
    }
}
