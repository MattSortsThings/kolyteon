using System.Text.Json;
using Kolyteon.Modelling;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Unit.Solving;

public static class SolvingResultTests
{
    [UnitTest]
    public sealed class EqualsMethod
    {
        private const int FixedSimplifyingSteps = 1;
        private const int FixedAssigningSteps = 6;
        private const int FixedBacktrackingSteps = 1;
        private static readonly Assignment<char, int>[] FixedAssignments =
        [
            new Assignment<char, int>('A', 0),
            new Assignment<char, int>('C', 2),
            new Assignment<char, int>('B', 1),
            new Assignment<char, int>('D', 3)
        ];

        private static readonly SearchAlgorithm FixedSearchAlgorithm =
            new(CheckingStrategy.ForwardChecking, OrderingStrategy.BrelazHeuristic);

        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualValues_ReturnsTrue()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalAssignments_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = [new Assignment<char, int>('A', 0)],
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = sut with { Assignments = [] };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalSearchAlgorithmValues_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = new SearchAlgorithm(CheckingStrategy.Backjumping, OrderingStrategy.NaturalOrdering),
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = sut with
            {
                SearchAlgorithm = new SearchAlgorithm(CheckingStrategy.ForwardChecking, OrderingStrategy.BrelazHeuristic)
            };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalSimplifyingStepsValues_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = 1,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = sut with { SimplifyingSteps = 0 };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalAssigningStepsValues_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = 6,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = 99,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalBacktrackingStepsValues_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = 1
            };

            SolvingResult<char, int> other = sut with { BacktrackingSteps = 0 };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            // Act
            bool result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class EqualityOperator
    {
        private const int FixedSimplifyingSteps = 1;
        private const int FixedAssigningSteps = 6;
        private const int FixedBacktrackingSteps = 1;
        private static readonly Assignment<char, int>[] FixedAssignments =
        [
            new Assignment<char, int>('A', 0),
            new Assignment<char, int>('C', 2),
            new Assignment<char, int>('B', 1),
            new Assignment<char, int>('D', 3)
        ];

        private static readonly SearchAlgorithm FixedSearchAlgorithm =
            new(CheckingStrategy.ForwardChecking, OrderingStrategy.BrelazHeuristic);

        [Fact]
        public void Equality_InstanceAndOtherHaveEqualValues_ReturnsTrue()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalAssignments_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = [new Assignment<char, int>('A', 0)],
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = sut with { Assignments = [] };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalSearchAlgorithmValues_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = new SearchAlgorithm(CheckingStrategy.Backjumping, OrderingStrategy.NaturalOrdering),
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = sut with
            {
                SearchAlgorithm = new SearchAlgorithm(CheckingStrategy.ForwardChecking, OrderingStrategy.BrelazHeuristic)
            };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalSimplifyingStepsValues_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = 1,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = sut with { SimplifyingSteps = 0 };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalAssigningStepsValues_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = 6,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = 99,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalBacktrackingStepsValues_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = 1
            };

            SolvingResult<char, int> other = sut with { BacktrackingSteps = 0 };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class InequalityOperator
    {
        private const int FixedSimplifyingSteps = 1;
        private const int FixedAssigningSteps = 6;
        private const int FixedBacktrackingSteps = 1;
        private static readonly Assignment<char, int>[] FixedAssignments =
        [
            new Assignment<char, int>('A', 0),
            new Assignment<char, int>('C', 2),
            new Assignment<char, int>('B', 1),
            new Assignment<char, int>('D', 3)
        ];

        private static readonly SearchAlgorithm FixedSearchAlgorithm =
            new(CheckingStrategy.ForwardChecking, OrderingStrategy.BrelazHeuristic);

        [Fact]
        public void Inequality_InstanceAndOtherHaveEqualValues_ReturnsFalse()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalAssignments_ReturnsTrue()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = [new Assignment<char, int>('A', 0)],
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = sut with { Assignments = [] };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalSearchAlgorithmValues_ReturnsTrue()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = new SearchAlgorithm(CheckingStrategy.Backjumping, OrderingStrategy.NaturalOrdering),
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = sut with
            {
                SearchAlgorithm = new SearchAlgorithm(CheckingStrategy.ForwardChecking, OrderingStrategy.BrelazHeuristic)
            };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalSimplifyingStepsValues_ReturnsTrue()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = 1,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = sut with { SimplifyingSteps = 0 };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalAssigningStepsValues_ReturnsTrue()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = 6,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            SolvingResult<char, int> other = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = 99,
                BacktrackingSteps = FixedBacktrackingSteps
            };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalBacktrackingStepsValues_ReturnsTrue()
        {
            // Arrange
            SolvingResult<char, int> sut = new()
            {
                Assignments = FixedAssignments,
                SearchAlgorithm = FixedSearchAlgorithm,
                SimplifyingSteps = FixedSimplifyingSteps,
                AssigningSteps = FixedAssigningSteps,
                BacktrackingSteps = 1
            };

            SolvingResult<char, int> other = sut with { BacktrackingSteps = 0 };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Fact]
        public void CanSerializeToJson_ThenDeserializeToInstanceWithEqualValue()
        {
            // Arrange
            SolvingResult<char, int> originalResult = new()
            {
                Assignments =
                [
                    new Assignment<char, int>('A', 0),
                    new Assignment<char, int>('C', 2),
                    new Assignment<char, int>('B', 1),
                    new Assignment<char, int>('D', 3)
                ],
                SearchAlgorithm = new SearchAlgorithm(CheckingStrategy.ForwardChecking, OrderingStrategy.BrelazHeuristic),
                SimplifyingSteps = 1,
                AssigningSteps = 6,
                BacktrackingSteps = 1
            };

            string json = JsonSerializer.Serialize(originalResult, JsonSerializerOptions.Default);

            // Act
            SolvingResult<char, int>? deserializedResult =
                JsonSerializer.Deserialize<SolvingResult<char, int>>(json, JsonSerializerOptions.Default);

            // Assert
            deserializedResult.Should().NotBeNull().And.Be(deserializedResult);
        }
    }
}
