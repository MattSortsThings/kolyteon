using Kolyteon.Tests.Unit.TestUtils;

namespace Kolyteon.Tests.Unit.Modelling;

public static partial class ConstraintGraphTests
{
    [UnitTest]
    public sealed class VariablesProperty
    {
        [Fact]
        public void Variables_ReturnsNumberOfVariables()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2, 3], [B] = [1, 2, 3], [C] = [1, 2, 3], [D] = [4, 5, 6], [E] = []
            });

            // Act
            int result = sut.Variables;

            // Assert
            result.Should().Be(5);
        }

        [Fact]
        public void Variables_NotModellingAProblem_ReturnsZero()
        {
            // Arrange
            TestConstraintGraph sut = new();

            // Act
            int result = sut.Variables;

            // Assert
            result.Should().Be(0);
        }
    }

    [UnitTest]
    public sealed class ConstraintsProperty
    {
        [Fact]
        public void Constraints_ReturnsNumberOfBinaryConstraints()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2, 3], [B] = [1, 2, 3], [C] = [1, 2, 3], [D] = [4, 5, 6], [E] = []
            });

            // Act
            int result = sut.Constraints;

            // Assert
            result.Should().Be(3);
        }

        [Fact]
        public void Constraints_NotModellingAProblem_ReturnsZero()
        {
            // Arrange
            TestConstraintGraph sut = new();

            // Act
            int result = sut.Variables;

            // Assert
            result.Should().Be(0);
        }
    }

    [UnitTest]
    public sealed class ConstraintDensityProperty
    {
        [Fact]
        public void ConstraintDensity_OneVariable_ReturnsZero()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            // Act
            double result = sut.ConstraintDensity;

            // Assert
            result.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
        }

        [Fact]
        public void ConstraintDensity_MultipleVariablesAndZeroConstraints_ReturnsZero()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1], [D] = [2] });

            // Act
            double result = sut.ConstraintDensity;

            // Assert
            result.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
        }

        [Fact]
        public void ConstraintDensity_MultipleVariablesAtLeastOneConstraint_ReturnsRatioOfActualToMaxConstraints()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2, 3], [B] = [1, 2, 3], [C] = [1, 2, 3], [D] = [4, 5, 6], [E] = []
            });

            // Act
            double result = sut.ConstraintDensity;

            // Assert
            result.Should().BeApproximately(0.3, Constants.Precision.SixDecimalPlaces);
        }

        [Fact]
        public void ConstraintDensity_MultipleVariablesAllPairsConstrained_ReturnsOne()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2, 3], [B] = [1, 2, 3], [C] = [1, 2, 3]
            });

            // Act
            double result = sut.ConstraintDensity;

            // Assert
            result.Should().BeApproximately(1.0, Constants.Precision.SixDecimalPlaces);
        }

        [Fact]
        public void ConstraintDensity_NotModellingAProblem_ReturnsZero()
        {
            // Arrange
            TestConstraintGraph sut = new();

            // Act
            double result = sut.ConstraintDensity;

            // Assert
            result.Should().BeApproximately(0.0, Constants.Precision.SixDecimalPlaces);
        }
    }

    [UnitTest]
    public sealed class MeanTightnessProperty
    {
        [Fact]
        public void MeanTightness_OneVariable_ReturnsZero()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1] });

            // Act
            double result = sut.MeanTightness;

            // Assert
            result.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
        }

        [Fact]
        public void MeanTightness_MultipleVariablesAndZeroConstraints_ReturnsZero()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem { [A] = [1], [D] = [2] });

            // Act
            double result = sut.MeanTightness;

            // Assert
            result.Should().BeApproximately(0, Constants.Precision.SixDecimalPlaces);
        }

        [Fact]
        public void MeanTightness_OneConstraint_ReturnsTightnessOfOnlyConstraint()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2], [B] = [1, 2], [C] = [3]
            });

            // Act
            double result = sut.MeanTightness;

            // Assert
            result.Should().BeApproximately(0.5, Constants.Precision.SixDecimalPlaces);
        }

        [Fact]
        public void MeanTightness_MultipleConstraints_ReturnsHarmonicMeanTightnessOfAllConstraints()
        {
            // Arrange
            TestConstraintGraph sut = TestConstraintGraph.ModellingProblem(new TestProblem
            {
                [A] = [1, 2, 3, 4], [B] = [1, 2], [C] = [8, 9], [D] = [9]
            });

            // Act
            double result = sut.MeanTightness;

            // Assert
            result.Should().BeApproximately(0.333333, Constants.Precision.SixDecimalPlaces);
        }

        [Fact]
        public void MeanTightness_NotModellingAProblem_ReturnsZero()
        {
            // Arrange
            TestConstraintGraph sut = new();

            // Act
            double result = sut.MeanTightness;

            // Assert
            result.Should().BeApproximately(0.0, Constants.Precision.SixDecimalPlaces);
        }
    }
}
