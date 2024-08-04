using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class ClearMethod
    {
        [Fact]
        public void Clear_ModellingAProblem_ResetsAllPropertiesExceptCapacityToZero()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(GetProblemWithTwoVariablesAndOneConstraint());

            const int expectedCapacity = 2;

            // Assert
            using (new AssertionScope())
            {
                sut.Variables.Should().BePositive();
                sut.Constraints.Should().BePositive();
                sut.ConstraintDensity.Should().BePositive();
                sut.MeanTightness.Should().BePositive();
                sut.Capacity.Should().Be(expectedCapacity);
            }

            // Act
            sut.Clear();

            // Assert
            using (new AssertionScope())
            {
                sut.Variables.Should().Be(0);
                sut.Constraints.Should().Be(0);
                sut.ConstraintDensity.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
                sut.MeanTightness.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
                sut.Capacity.Should().Be(expectedCapacity);
            }
        }

        [Fact]
        public void Clear_NotModellingAProblem_DoesNothing()
        {
            // Arrange
            const int capacity = 2;

            TestConstraintGraph sut = new(capacity);

            // Assert
            using (new AssertionScope())
            {
                sut.Variables.Should().Be(0);
                sut.Constraints.Should().Be(0);
                sut.ConstraintDensity.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
                sut.MeanTightness.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
                sut.Capacity.Should().Be(capacity);
            }

            // Act
            sut.Clear();

            // Assert
            using (new AssertionScope())
            {
                sut.Variables.Should().Be(0);
                sut.Constraints.Should().Be(0);
                sut.ConstraintDensity.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
                sut.MeanTightness.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
                sut.Capacity.Should().Be(capacity);
            }
        }
    }
}
