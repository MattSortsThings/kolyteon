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
    [UnitTest]
    public sealed class Variables_Property_Getter
    {
        [Fact]
        public void ReturnsNumberOfVariables()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = DigitsFromOneToFive,
                [Letter.B] = DigitsFromOneToFive,
                [Letter.C] = DigitsFromOneToFive,
                [Letter.D] = DigitsFromOneToFive,
                [Letter.E] = DigitsFromOneToFive
            });

            // Act
            var result = sut.Variables;

            // Assert
            result.Should().Be(5);
        }

        [Fact]
        public void InstanceNotModellingProblem_ReturnsZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            var result = sut.Variables;

            // Assert
            result.Should().Be(0);
        }
    }

    [UnitTest]
    public sealed class Constraints_Property_Getter
    {
        [Fact]
        public void ReturnsNumberOfConstraints()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = DigitsFromOneToFive,
                [Letter.B] = DigitsFromOneToFive,
                [Letter.C] = DigitsFromOneToFive,
                [Letter.D] = DigitsFromOneToFive,
                [Letter.E] = DigitsFromOneToFive
            });

            // Act
            var result = sut.Constraints;

            // Assert
            result.Should().Be(10);
        }

        [Fact]
        public void InstanceNotModellingProblem_ReturnsZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            var result = sut.Constraints;

            // Assert
            result.Should().Be(0);
        }
    }

    [UnitTest]
    public sealed class ConstraintDensity_Property_Getter
    {
        [Fact]
        public void OneVariable_ReturnsZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            var result = sut.ConstraintDensity;

            // Assert
            result.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
        }

        [Fact]
        public void MultipleVariables_ZeroConstraints_ReturnsZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = [Digit.One],
                [Letter.B] = [Digit.Two],
                [Letter.C] = [Digit.Three]
            });

            // Act
            var result = sut.ConstraintDensity;

            // Assert
            result.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
        }

        [Fact]
        public void MultipleVariables_AtLeastOneConstraint_ReturnsRatioOfConstraintsToMaxPossibleConstraints()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = [Digit.One],
                [Letter.B] = [Digit.Two],
                [Letter.C] = [Digit.Three],
                [Letter.D] = [Digit.One, Digit.Four],
                [Letter.E] = [Digit.Five]
            });

            // Act
            var result = sut.ConstraintDensity;

            // Assert
            result.Should().BeApproximately(0.1, Invariants.SixDecimalPlacesPrecision,
                "1 constraint out of max possible 10");
        }

        [Fact]
        public void MultipleVariables_AllPairsAdjacent_ReturnsOne()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = DigitsFromOneToFive,
                [Letter.B] = DigitsFromOneToFive,
                [Letter.C] = DigitsFromOneToFive,
                [Letter.D] = DigitsFromOneToFive,
                [Letter.E] = DigitsFromOneToFive
            });

            // Act
            var result = sut.ConstraintDensity;

            // Assert
            result.Should().BeApproximately(1, Invariants.SixDecimalPlacesPrecision);
        }

        [Fact]
        public void InstanceNotModellingProblem_ReturnsZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            var result = sut.ConstraintDensity;

            // Assert
            result.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
        }
    }

    [UnitTest]
    public sealed class ConstraintTightness_Property_Getter
    {
        [Fact]
        public void OneVariable_ReturnsZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            // Act
            var result = sut.ConstraintTightness;

            // Assert
            result.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
        }

        [Fact]
        public void MultipleVariables_ZeroConstraints_ReturnsZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = [Digit.One],
                [Letter.B] = [Digit.Two],
                [Letter.C] = [Digit.Three]
            });

            // Act
            var result = sut.ConstraintTightness;

            // Assert
            result.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
        }

        [Fact]
        public void MultipleVariables_AtLeastOneConstraint_ReturnsProbabilityOfInconsistentAssignmentsForAdjacentVariablePair()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = DigitsFromOneToFive,
                [Letter.B] = DigitsFromOneToFive,
                [Letter.C] = DigitsFromOneToFive,
                [Letter.D] = DigitsFromOneToFive,
                [Letter.E] = DigitsFromOneToFive
            });

            // Act
            var result = sut.ConstraintTightness;

            // Assert
            result.Should().BeApproximately(0.2, Invariants.SixDecimalPlacesPrecision);
        }

        [Fact]
        public void InstanceNotModellingProblem_ReturnsZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            var result = sut.ConstraintTightness;

            // Assert
            result.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
        }
    }

    [UnitTest]
    public sealed class GetProblemStatistics_Method
    {
        [Fact]
        public void ReturnsObjectWithProblemStatistics()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = DigitsFromOneToFive,
                [Letter.B] = DigitsFromOneToFive,
                [Letter.C] = DigitsFromOneToFive,
                [Letter.D] = DigitsFromOneToFive,
                [Letter.E] = DigitsFromOneToFive
            });

            // Act
            ProblemMetrics result = sut.GetProblemMetrics();

            // Assert
            using (new AssertionScope())
            {
                result.Variables.Should().Be(5);
                result.Constraints.Should().Be(10);
                result.ConstraintDensity.Should().BeApproximately(1, Invariants.SixDecimalPlacesPrecision);
                result.ConstraintTightness.Should().BeApproximately(0.2, Invariants.SixDecimalPlacesPrecision);
            }
        }

        [Fact]
        public void InstanceNotModellingProblem_ReturnsObjectWithAllPropertiesZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            ProblemMetrics result = sut.GetProblemMetrics();

            // Assert
            using (new AssertionScope())
            {
                result.Variables.Should().Be(0);
                result.Constraints.Should().Be(0);
                result.ConstraintDensity.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
                result.ConstraintTightness.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
            }
        }
    }

    [UnitTest]
    public sealed class GetDomainSizeStatistics_Method
    {
        [Fact]
        public void ReturnsObjectWithVariableDomainSizeStatistics()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = [Digit.Four],
                [Letter.B] = [Digit.One, Digit.Two],
                [Letter.C] = [Digit.One, Digit.Two, Digit.Three, Digit.Four],
                [Letter.D] = [Digit.Four],
                [Letter.E] = [Digit.Five]
            });

            // Act
            DomainSizeStatistics result = sut.GetDomainSizeStatistics();

            // Assert
            using (new AssertionScope())
            {
                result.MinimumValue.Should().Be(1);
                result.MeanValue.Should().BeApproximately(1.8, Invariants.SixDecimalPlacesPrecision);
                result.MaximumValue.Should().Be(4);
                result.DistinctValues.Should().Be(3);
            }
        }

        [Fact]
        public void InstanceNotModellingProblem_ReturnsObjectWithAllPropertiesZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            DomainSizeStatistics result = sut.GetDomainSizeStatistics();

            // Assert
            using (new AssertionScope())
            {
                result.MinimumValue.Should().Be(0);
                result.MeanValue.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
                result.MaximumValue.Should().Be(0);
                result.DistinctValues.Should().Be(0);
            }
        }
    }

    [UnitTest]
    public sealed class GetDegreeStatistics_Method
    {
        [Fact]
        public void ReturnsObjectWithVariableDegreeStatistics()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = [Digit.Four],
                [Letter.B] = [Digit.One, Digit.Two],
                [Letter.C] = [Digit.One, Digit.Two, Digit.Three, Digit.Four],
                [Letter.D] = [Digit.Four],
                [Letter.E] = [Digit.Five]
            });

            // Act
            DegreeStatistics result = sut.GetDegreeStatistics();

            // Assert
            using (new AssertionScope())
            {
                result.MinimumValue.Should().Be(0);
                result.MeanValue.Should().BeApproximately(1.6, Invariants.SixDecimalPlacesPrecision);
                result.MaximumValue.Should().Be(3);
                result.DistinctValues.Should().Be(4);
            }
        }

        [Fact]
        public void InstanceNotModellingProblem_ReturnsObjectWithAllPropertiesZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            DegreeStatistics result = sut.GetDegreeStatistics();

            // Assert
            using (new AssertionScope())
            {
                result.MinimumValue.Should().Be(0);
                result.MeanValue.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
                result.MaximumValue.Should().Be(0);
                result.DistinctValues.Should().Be(0);
            }
        }
    }

    [UnitTest]
    public sealed class GetSumTightnessStatistics_Method
    {
        [Fact]
        public void ReturnsObjectWithVariableSumTightnessStatistics()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = [Digit.Four],
                [Letter.B] = [Digit.One, Digit.Two],
                [Letter.C] = [Digit.One, Digit.Two, Digit.Three, Digit.Four],
                [Letter.D] = [Digit.Four],
                [Letter.E] = [Digit.Five]
            });

            // Act
            SumTightnessStatistics result = sut.GetSumTightnessStatistics();

            // Assert
            using (new AssertionScope())
            {
                result.MinimumValue.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
                result.MeanValue.Should().BeApproximately(0.7, Invariants.SixDecimalPlacesPrecision);
                result.MaximumValue.Should().BeApproximately(1.25, Invariants.SixDecimalPlacesPrecision);
                result.DistinctValues.Should().Be(4);
            }
        }

        [Fact]
        public void InstanceNotModellingProblem_ReturnsObjectWithAllPropertiesZero()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            SumTightnessStatistics result = sut.GetSumTightnessStatistics();

            // Assert
            using (new AssertionScope())
            {
                result.MinimumValue.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
                result.MeanValue.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
                result.MaximumValue.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
                result.DistinctValues.Should().Be(0);
            }
        }
    }
}
