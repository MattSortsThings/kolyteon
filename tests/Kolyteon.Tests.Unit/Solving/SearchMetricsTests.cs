using System.Collections;
using System.Text.Json;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Unit.Solving;

public static class SearchMetricsTests
{
    [UnitTest]
    public sealed class EqualsMethod
    {
        private static readonly SearchAlgorithm FixedSearchAlgorithm =
            new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);

        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm);

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualSearchAlgorithmAndStepsValues_ReturnsTrue()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm, 1, 1, 1);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, 1, 1, 1);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualUnequalSearchAlgorithmValues_ReturnsFalse()
        {
            // Arrange
            SearchAlgorithm algorithm1 = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);
            SearchAlgorithm algorithm2 = new(CheckingStrategy.MaintainingArcConsistency, OrderingStrategy.MaxTightness);

            SearchMetrics sut = SearchMetrics.Create(algorithm1);
            SearchMetrics other = SearchMetrics.Create(algorithm2);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalSimplifyingStepsValues_ReturnsFalse()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm, 3);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, 2);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalAssigningStepsValues_ReturnsFalse()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm, assigningSteps: 1);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, assigningSteps: 2);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalBacktrackingStepsValues_ReturnsFalse()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm, backtrackingSteps: 1);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, backtrackingSteps: 2);

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm);

            // Act
            bool result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        private static readonly SearchAlgorithm FixedSearchAlgorithm =
            new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);

        [Fact]
        public void Equality_InstanceAndOtherHaveEqualSearchAlgorithmAndStepsValues_ReturnsTrue()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm, 1, 1, 1);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, 1, 1, 1);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveEqualUnequalSearchAlgorithmValues_ReturnsFalse()
        {
            // Arrange
            SearchAlgorithm algorithm1 = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);
            SearchAlgorithm algorithm2 = new(CheckingStrategy.MaintainingArcConsistency, OrderingStrategy.MaxTightness);

            SearchMetrics sut = SearchMetrics.Create(algorithm1);
            SearchMetrics other = SearchMetrics.Create(algorithm2);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalSimplifyingStepsValues_ReturnsFalse()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, 2);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalAssigningStepsValues_ReturnsFalse()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm, assigningSteps: 1);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, assigningSteps: 2);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalBacktrackingStepsValues_ReturnsFalse()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm, backtrackingSteps: 1);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, backtrackingSteps: 2);

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class InequalityOperator
    {
        private static readonly SearchAlgorithm FixedSearchAlgorithm =
            new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);

        [Fact]
        public void Inequality_InstanceAndOtherHaveEqualSearchAlgorithmAndStepsValues_ReturnsFalse()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm, 1, 1, 1);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, 1, 1, 1);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveEqualUnequalSearchAlgorithmValues_ReturnsTrue()
        {
            // Arrange
            SearchAlgorithm algorithm1 = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);
            SearchAlgorithm algorithm2 = new(CheckingStrategy.MaintainingArcConsistency, OrderingStrategy.MaxTightness);

            SearchMetrics sut = SearchMetrics.Create(algorithm1);
            SearchMetrics other = SearchMetrics.Create(algorithm2);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalSimplifyingStepsValues_ReturnsTrue()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, 2);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalAssigningStepsValues_ReturnsTrue()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm, assigningSteps: 1);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, assigningSteps: 2);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalBacktrackingStepsValues_ReturnsTrue()
        {
            // Arrange
            SearchMetrics sut = SearchMetrics.Create(FixedSearchAlgorithm, backtrackingSteps: 1);
            SearchMetrics other = SearchMetrics.Create(FixedSearchAlgorithm, backtrackingSteps: 2);

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class CreateFactoryMethod
    {
        [Fact]
        public void Create_GivenValues_ReturnsInstanceWithSpecifiedValues()
        {
            // Arrange
            SearchAlgorithm algorithm = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);
            const int simplifyingSteps = 1;
            const int assigningSteps = 5;
            const int backtrackingSteps = 2;

            // Act
            SearchMetrics result = SearchMetrics.Create(algorithm, simplifyingSteps, assigningSteps, backtrackingSteps);

            // Assert
            using (new AssertionScope())
            {
                result.SearchAlgorithm.Should().Be(algorithm);
                result.SimplifyingSteps.Should().Be(simplifyingSteps);
                result.AssigningSteps.Should().Be(assigningSteps);
                result.BacktrackingSteps.Should().Be(backtrackingSteps);
                result.TotalSteps.Should().Be(8);
                result.Efficiency.Should().BeApproximately(0.75, 0.1);
            }
        }

        [Fact]
        public void Create_SearchAlgorithmArgIsNull_Throws()
        {
            // Act
            Action act = () => SearchMetrics.Create(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'searchAlgorithm')");
        }

        [Fact]
        public void Create_SimplifyingStepsArgIsLessThanOne_Throws()
        {
            // Arrange
            SearchAlgorithm arbitraryAlgorithm = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);

            // Act
            Action act = () => SearchMetrics.Create(arbitraryAlgorithm, 0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("simplifyingSteps ('0') must be greater than or equal to '1'. " +
                             "(Parameter 'simplifyingSteps')\nActual value was 0.");
        }

        [Fact]
        public void Create_AssigningStepsArgIsNegative_Throws()
        {
            // Arrange
            SearchAlgorithm arbitraryAlgorithm = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);

            // Act
            Action act = () => SearchMetrics.Create(arbitraryAlgorithm, assigningSteps: -1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("assigningSteps ('-1') must be a non-negative value. " +
                             "(Parameter 'assigningSteps')\nActual value was -1.");
        }

        [Fact]
        public void Create_BacktrackingStepsArgIsNegative_Throws()
        {
            // Arrange
            SearchAlgorithm arbitraryAlgorithm = new(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering);

            // Act
            Action act = () => SearchMetrics.Create(arbitraryAlgorithm, backtrackingSteps: -1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("backtrackingSteps ('-1') must be a non-negative value. " +
                             "(Parameter 'backtrackingSteps')\nActual value was -1.");
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue(SearchMetrics originalMetrics)
        {
            // Arrange
            string json = JsonSerializer.Serialize(originalMetrics, JsonSerializerOptions.Default);

            // Act
            SearchMetrics? deserializedMetrics = JsonSerializer.Deserialize<SearchMetrics>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedMetrics.Should().NotBeNull().And.Be(originalMetrics);
        }

        private sealed class TestCases : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return
                [
                    SearchMetrics.Create(
                        new SearchAlgorithm(CheckingStrategy.NaiveBacktracking, OrderingStrategy.NaturalOrdering))
                ];

                yield return
                [
                    SearchMetrics.Create(
                        new SearchAlgorithm(CheckingStrategy.MaintainingArcConsistency, OrderingStrategy.MaxTightness),
                        assigningSteps: 5,
                        backtrackingSteps: 2)
                ];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
