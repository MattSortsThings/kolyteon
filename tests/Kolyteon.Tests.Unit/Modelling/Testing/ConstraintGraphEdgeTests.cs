using Kolyteon.Modelling.Testing;

namespace Kolyteon.Tests.Unit.Modelling.Testing;

public static class ConstraintGraphEdgeTests
{
    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = 'A',
                SecondVariable = 'B',
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 0, false),
                    new AssignmentPair<int>(0, 1, true)
                ]
            };

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualFirstAndSecondVariablesAndTightnessAndAssignmentPairs_ReturnsTrue()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;
            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalFirstVariableValues_ReturnsFalse()
        {
            // Arrange
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;
            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = 'A',
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = sut with { FirstVariable = 'Z' };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalSecondVariableValues_ReturnsFalse()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const double sharedTightness = 0.5;
            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = 'B',
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = sut with { SecondVariable = 'Z' };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalTightnessValues_ReturnsFalse()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';
            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = 0.5,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = sut with { Tightness = 0.499999 };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalAssignmentPairs_ReturnsFalse()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 0, false),
                    new AssignmentPair<int>(0, 1, true)
                ]
            };

            ConstraintGraphEdge<char, int> other = sut with
            {
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 0, false)
                ]
            };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherSameAssignmentPairsInDifferentOrder_ReturnsFalse()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 0, false),
                    new AssignmentPair<int>(0, 1, true)
                ]
            };

            ConstraintGraphEdge<char, int> other = sut with
            {
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 1, true),
                    new AssignmentPair<int>(0, 0, false)
                ]
            };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsTrue()
        {
            // Arrange
            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = 'A',
                SecondVariable = 'B',
                Tightness = 0.5,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 0, false),
                    new AssignmentPair<int>(0, 1, true)
                ]
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
        [Fact]
        public void Equality_InstanceAndOtherHaveEqualFirstAndSecondVariablesAndTightnessAndAssignmentPairs_ReturnsTrue()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;

            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalFirstVariableValues_ReturnsFalse()
        {
            // Arrange
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;

            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = 'A',
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = sut with { FirstVariable = 'Z' };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalSecondVariableValues_ReturnsFalse()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const double sharedTightness = 0.5;

            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = 'B',
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = sut with { SecondVariable = 'Z' };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalTightnessValues_ReturnsFalse()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';

            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = 0.5,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = sut with { Tightness = 0.499999 };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalAssignmentPairs_ReturnsFalse()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 0, false),
                    new AssignmentPair<int>(0, 1, true)
                ]
            };

            ConstraintGraphEdge<char, int> other = sut with
            {
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 0, false)
                ]
            };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherSameAssignmentPairsInDifferentOrder_ReturnsFalse()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 0, false),
                    new AssignmentPair<int>(0, 1, true)
                ]
            };

            ConstraintGraphEdge<char, int> other = sut with
            {
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 1, true),
                    new AssignmentPair<int>(0, 0, false)
                ]
            };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class InequalityOperator
    {
        [Fact]
        public void Inequality_InstanceAndOtherHaveEqualFirstAndSecondVariablesAndTightnessAndAssignmentPairs_ReturnsFalse()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;

            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalFirstVariableValues_ReturnsTrue()
        {
            // Arrange
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;

            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = 'A',
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = sut with { FirstVariable = 'Z' };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalSecondVariableValues_ReturnsTrue()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const double sharedTightness = 0.5;

            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = 'B',
                Tightness = sharedTightness,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = sut with { SecondVariable = 'Z' };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalTightnessValues_ReturnsTrue()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';

            AssignmentPair<int>[] sharedAssignmentPairs =
            [
                new AssignmentPair<int>(0, 0, false),
                new AssignmentPair<int>(0, 1, true)
            ];

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = 0.5,
                AssignmentPairs = sharedAssignmentPairs
            };

            ConstraintGraphEdge<char, int> other = sut with { Tightness = 0.499999 };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalAssignmentPairs_ReturnsTrue()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 0, false),
                    new AssignmentPair<int>(0, 1, true)
                ]
            };

            ConstraintGraphEdge<char, int> other = sut with
            {
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 0, false)
                ]
            };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherSameAssignmentPairsInDifferentOrder_ReturnsTrue()
        {
            // Arrange
            const char sharedFirstVariable = 'A';
            const char sharedSecondVariable = 'B';
            const double sharedTightness = 0.5;

            ConstraintGraphEdge<char, int> sut = new()
            {
                FirstVariable = sharedFirstVariable,
                SecondVariable = sharedSecondVariable,
                Tightness = sharedTightness,
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 0, false),
                    new AssignmentPair<int>(0, 1, true)
                ]
            };

            ConstraintGraphEdge<char, int> other = sut with
            {
                AssignmentPairs =
                [
                    new AssignmentPair<int>(0, 1, true),
                    new AssignmentPair<int>(0, 0, false)
                ]
            };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }
}
