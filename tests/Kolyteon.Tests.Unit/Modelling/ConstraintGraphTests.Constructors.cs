using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class ParameterlessConstructor
    {
        [Fact]
        public void ParameterlessConstructor_Initializes_AllPropertiesHaveValueZero()
        {
            // Act
            TestConstraintGraph sut = new();

            // Assert
            using (new AssertionScope())
            {
                sut.Capacity.Should().Be(0);
                sut.Variables.Should().Be(0);
                sut.Constraints.Should().Be(0);
                sut.ConstraintDensity.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
                sut.MeanTightness.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
            }
        }
    }

    [UnitTest]
    public sealed class CapacityArgConstructor
    {
        [Fact]
        public void CapacityArgConstructor_Initializes_HasGivenCapacityAndAllOtherPropertiesZero()
        {
            // Arrange
            const int requiredCapacity = 3;

            // Act
            TestConstraintGraph sut = new(requiredCapacity);

            // Assert
            using (new AssertionScope())
            {
                sut.Capacity.Should().Be(requiredCapacity);
                sut.Variables.Should().Be(0);
                sut.Constraints.Should().Be(0);
                sut.ConstraintDensity.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
                sut.MeanTightness.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
            }
        }

        [Fact]
        public void CapacityArgConstructor_CapacityArgIsNegative_Throws()
        {
            // Act
            Action act = () => _ = new TestConstraintGraph(-1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("capacity ('-1') must be a non-negative value. (Parameter 'capacity')\n" +
                             "Actual value was -1.");
        }
    }
}
