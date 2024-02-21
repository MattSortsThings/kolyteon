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
    public sealed class GetAllVariables_Method
    {
        [Fact]
        public void EnumeratesAllVariablesOrderedByIndex()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = DigitsFromOneToFive,
                [Letter.B] = DigitsFromOneToFive
            });

            // Act
            IEnumerable<Letter> result = sut.GetAllVariables();

            // Assert
            result.Should().Equal(Letter.A, Letter.B);
        }

        [Fact]
        public void InstanceNotModellingProblem_ReturnsEmptySequence()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            IEnumerable<Letter> result = sut.GetAllVariables();

            // Assert
            result.Should().BeEmpty();
        }
    }

    [UnitTest]
    public sealed class GetAllDomains_Method
    {
        [Fact]
        public void EnumeratesAllDomainsOrderedByIndex()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = [Digit.One, Digit.Two],
                [Letter.B] = [Digit.Three, Digit.Four]
            });

            // Act
            IEnumerable<IReadOnlyList<Digit>> result = sut.GetAllDomains();

            // Assert
            result.Should().SatisfyRespectively(first =>
                first.Should().Equal(Digit.One, Digit.Two), second =>
                second.Should().Equal(Digit.Three, Digit.Four));
        }

        [Fact]
        public void InstanceNotModellingProblem_ReturnsEmptySequence()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            IEnumerable<IReadOnlyList<Digit>> result = sut.GetAllDomains();

            // Assert
            result.Should().BeEmpty();
        }
    }

    [UnitTest]
    public sealed class GetAllAdjacentVariables_Method
    {
        [Fact]
        public void EnumeratesAllAdjacentVariablePairsOrderedByIndex()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.ModellingProblem(new TestProblem
            {
                [Letter.A] = DigitsFromOneToFive,
                [Letter.B] = DigitsFromOneToFive,
                [Letter.C] = DigitsFromOneToFive
            });

            // Act
            IEnumerable<Pair<Letter>> result = sut.GetAllAdjacentVariables();

            // Assert
            result.Should().Equal(new Pair<Letter>(Letter.A, Letter.B),
                new Pair<Letter>(Letter.A, Letter.C),
                new Pair<Letter>(Letter.B, Letter.C));
        }

        [Fact]
        public void InstanceNotModellingProblem_ReturnsEmptySequence()
        {
            // Arrange
            TestBinaryCsp sut = TestBinaryCsp.WithCapacity(1);

            // Act
            IEnumerable<Pair<Letter>> result = sut.GetAllAdjacentVariables();

            // Assert
            result.Should().BeEmpty();
        }
    }
}
