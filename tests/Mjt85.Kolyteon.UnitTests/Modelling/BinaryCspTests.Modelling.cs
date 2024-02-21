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
    public sealed class Model_Method
    {
        [Fact]
        public void Models_VariablesOrderedByVariableTypeComparisonRules()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(3);

            TestProblem problem = new()
            {
                [Letter.C] = DigitsFromOneToFive,
                [Letter.A] = DigitsFromOneToFive,
                [Letter.E] = DigitsFromOneToFive
            };

            // Act
            sut.Model(problem);

            // Assert
            sut.GetAllVariables().Should().Equal(Letter.A, Letter.C, Letter.E);
        }

        [Fact]
        public void Models_DomainsOrderedByDomainValueTypeComparisonRules_DuplicateValuesIgnored()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(3);

            TestProblem problem = new()
            {
                [Letter.A] = [Digit.One],
                [Letter.B] = [],
                [Letter.C] = [Digit.Three, Digit.Two, Digit.Two, Digit.One],
                [Letter.D] = [Digit.One, Digit.Two, Digit.Three]
            };

            // Act
            sut.Model(problem);

            // Assert
            sut.GetAllDomains().Should().SatisfyRespectively(first =>
                first.Should().Equal(Digit.One), second =>
                second.Should().BeEmpty(), third =>
                third.Should().Equal(Digit.One, Digit.Two, Digit.Three), fourth =>
                fourth.Should().Equal(Digit.One, Digit.Two, Digit.Three));
        }

        [Fact]
        public void Models_AddsOnlyConstraintsWithNonZeroTightness()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(3);

            TestProblem problem = new()
            {
                [Letter.A] = [Digit.One],
                [Letter.B] = [],
                [Letter.C] = [Digit.One, Digit.Two, Digit.Three],
                [Letter.D] = [Digit.One, Digit.Two, Digit.Three],
                [Letter.E] = [Digit.Four]
            };

            // Act
            sut.Model(problem);

            // Assert
            sut.GetAllAdjacentVariables().Should().Equal(new Pair<Letter>(Letter.A, Letter.C),
                new Pair<Letter>(Letter.A, Letter.D),
                new Pair<Letter>(Letter.C, Letter.D));
        }

        [Fact]
        public void ProblemArgIsNull_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            Action act = () => sut.Model(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'problem')");
        }

        [Fact]
        public void InstanceAlreadyModellingProblem_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem { [Letter.A] = [Digit.One] });

            TestProblem problem = new()
            {
                [Letter.A] = DigitsFromOneToFive,
                [Letter.B] = DigitsFromOneToFive
            };

            // Act
            Action act = () => sut.Model(problem);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Binary CSP is already modelling a problem.");
        }

        [Fact]
        public void InstanceHasZeroVariablesAfterModellingProblemAsBinaryCsp_Throws()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            TestProblem problem = new();

            // Act
            Action act = () => sut.Model(problem);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Binary CSP has zero variables when modelling problem.");
        }
    }

    [UnitTest]
    public sealed class Clear_Method
    {
        [Fact]
        public void ClearsAllBinaryCspData()
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

            // Assert
            using (new AssertionScope())
            {
                sut.GetAllVariables().Should().NotBeEmpty();
                sut.GetAllDomains().Should().NotBeEmpty();
                sut.GetAllAdjacentVariables().Should().NotBeEmpty();
            }

            // Act
            sut.Clear();

            // Assert
            using (new AssertionScope())
            {
                sut.GetAllVariables().Should().BeEmpty();
                sut.GetAllDomains().Should().BeEmpty();
                sut.GetAllAdjacentVariables().Should().BeEmpty();
            }
        }

        [Fact]
        public void Clears_CapacityUnchanged_AllOtherPropertiesReturnZero()
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

            // Assert
            using (new AssertionScope())
            {
                sut.Capacity.Should().Be(5);
                sut.Variables.Should().Be(5);
                sut.Constraints.Should().Be(10);
                sut.ConstraintDensity.Should().BeApproximately(1, Invariants.SixDecimalPlacesPrecision);
                sut.ConstraintTightness.Should().BeApproximately(0.2, Invariants.SixDecimalPlacesPrecision);
            }

            // Act
            sut.Clear();

            // Assert
            using (new AssertionScope())
            {
                sut.Capacity.Should().Be(5);
                sut.Variables.Should().Be(0);
                sut.Constraints.Should().Be(0);
                sut.ConstraintDensity.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
                sut.ConstraintTightness.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
            }
        }

        [Fact]
        public void InstanceNotModellingProblem_DoesNothing()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Assert
            using (new AssertionScope())
            {
                sut.Capacity.Should().Be(1);
                sut.Variables.Should().Be(0);
                sut.Constraints.Should().Be(0);
                sut.ConstraintDensity.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
                sut.ConstraintTightness.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
            }

            // Act
            sut.Clear();

            // Assert
            using (new AssertionScope())
            {
                sut.Capacity.Should().Be(1);
                sut.Variables.Should().Be(0);
                sut.Constraints.Should().Be(0);
                sut.ConstraintDensity.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
                sut.ConstraintTightness.Should().BeApproximately(0, Invariants.SixDecimalPlacesPrecision);
            }
        }
    }
}
