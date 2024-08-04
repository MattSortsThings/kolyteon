using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class CapacityProperty
    {
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CapacitySetter_GivenValueGreaterThanOrEqualToNumberOfVariables_UpdatesCapacity(int requiredCapacity)
        {
            // Arrange
            const int initialCapacity = 3;

            TestConstraintGraph sut = new(initialCapacity);

            sut.Model(GetProblemWithTwoVariablesAndOneConstraint());

            // Assert
            sut.Capacity.Should().Be(initialCapacity);

            // Act
            sut.Capacity = requiredCapacity;

            // Assert
            sut.Capacity.Should().Be(requiredCapacity);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void CapacitySetter_GivenValueLessThanNumberOfVariables_Throws(int requiredCapacity)
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(GetProblemWithTwoVariablesAndOneConstraint());

            // Assert
            sut.Capacity.Should().Be(2);

            // Act
            Action act = () => sut.Capacity = requiredCapacity;

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("capacity was less than the current size. (Parameter 'value')");
        }

        [Fact]
        public void CapacitySetter_GivenNegativeValue_Throws()
        {
            // Arrange
            TestConstraintGraph sut = new();

            // Act
            Action act = () => sut.Capacity = -1;

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("capacity was less than the current size. (Parameter 'value')");
        }
    }
}
