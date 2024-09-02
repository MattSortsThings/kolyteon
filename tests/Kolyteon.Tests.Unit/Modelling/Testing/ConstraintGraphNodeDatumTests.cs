using Kolyteon.Modelling.Testing;

namespace Kolyteon.Tests.Unit.Modelling.Testing;

public static class ConstraintGraphNodeDatumTests
{
    [UnitTest]
    public sealed class EqualsMethod
    {
        [Fact]
        public void Equals_InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = 'A', Domain = [0, 1, 2], Degree = 2, SumTightness = 0.5
            };

            // Act
            bool result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveEqualVariableAndDomainAndDegreeAndSumTightnessValues_ReturnsTrue()
        {
            // Arrange
            const char sharedVariable = 'A';
            int[] sharedDomain = [0, 1, 2];
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalVariableValues_ReturnsFalse()
        {
            // Arrange
            int[] sharedDomain = [0, 1, 2];
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = 'A', Domain = sharedDomain, Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Variable = 'Z' };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalDegreeValues_ReturnsFalse()
        {
            // Arrange
            const char sharedVariable = 'A';
            int[] sharedDomain = [0, 1, 2];
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = 1, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Degree = 2 };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalSumTightnessValues_ReturnsFalse()
        {
            // Arrange
            const char sharedVariable = 'A';
            int[] sharedDomain = [0, 1, 2];
            const int sharedDegree = 2;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = sharedDegree, SumTightness = 0.5
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { SumTightness = 0.499999 };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveUnequalDomains_ReturnsFalse()
        {
            // Arrange
            const char sharedVariable = 'A';
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = [0, 1, 2], Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Domain = [0] };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_InstanceAndOtherHaveSameDomainValuesInDifferentOrder_ReturnsFalse()
        {
            // Arrange
            const char sharedVariable = 'A';
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = [0, 1, 2], Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Domain = [2, 1, 0] };

            // Act
            bool result = sut.Equals(other);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equals_OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = 'A', Domain = [0, 1, 2], Degree = 2, SumTightness = 0.5
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
        public void Equality_InstanceAndOtherHaveEqualVariableAndDomainAndDegreeAndSumTightnessValues_ReturnsTrue()
        {
            // Arrange
            const char sharedVariable = 'A';
            int[] sharedDomain = [0, 1, 2];
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalVariableValues_ReturnsFalse()
        {
            // Arrange
            int[] sharedDomain = [0, 1, 2];
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = 'A', Domain = sharedDomain, Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Variable = 'Z' };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalDegreeValues_ReturnsFalse()
        {
            // Arrange
            const char sharedVariable = 'A';
            int[] sharedDomain = [0, 1, 2];
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = 1, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Degree = 2 };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalSumTightnessValues_ReturnsFalse()
        {
            // Arrange
            const char sharedVariable = 'A';
            int[] sharedDomain = [0, 1, 2];
            const int sharedDegree = 2;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = sharedDegree, SumTightness = 0.5
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { SumTightness = 0.499999 };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveUnequalDomains_ReturnsFalse()
        {
            // Arrange
            const char sharedVariable = 'A';
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = [0, 1, 2], Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Domain = [0] };

            // Act
            bool result = sut == other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Equality_InstanceAndOtherHaveSameDomainValuesInDifferentOrder_ReturnsFalse()
        {
            // Arrange
            const char sharedVariable = 'A';
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = [0, 1, 2], Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Domain = [2, 1, 0] };

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
        public void Inequality_InstanceAndOtherHaveEqualVariableAndDomainAndDegreeAndSumTightnessValues_ReturnsFalse()
        {
            // Arrange
            const char sharedVariable = 'A';
            int[] sharedDomain = [0, 1, 2];
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalVariableValues_ReturnsTrue()
        {
            // Arrange
            int[] sharedDomain = [0, 1, 2];
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = 'A', Domain = sharedDomain, Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Variable = 'Z' };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalDegreeValues_ReturnsTrue()
        {
            // Arrange
            const char sharedVariable = 'A';
            int[] sharedDomain = [0, 1, 2];
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = 1, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Degree = 2 };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalSumTightnessValues_ReturnsTrue()
        {
            // Arrange
            const char sharedVariable = 'A';
            int[] sharedDomain = [0, 1, 2];
            const int sharedDegree = 2;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = sharedDomain, Degree = sharedDegree, SumTightness = 0.5
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { SumTightness = 0.499999 };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveUnequalDomains_ReturnsTrue()
        {
            // Arrange
            const char sharedVariable = 'A';
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = [0, 1, 2], Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Domain = [0] };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void Inequality_InstanceAndOtherHaveSameDomainValuesInDifferentOrder_ReturnsTrue()
        {
            // Arrange
            const char sharedVariable = 'A';
            const int sharedDegree = 2;
            const double sharedSumTightness = 0.5;

            ConstraintGraphNodeDatum<char, int> sut = new()
            {
                Variable = sharedVariable, Domain = [0, 1, 2], Degree = sharedDegree, SumTightness = sharedSumTightness
            };

            ConstraintGraphNodeDatum<char, int> other = sut with { Domain = [2, 1, 0] };

            // Act
            bool result = sut != other;

            // Assert
            result.Should().BeTrue();
        }
    }
}
