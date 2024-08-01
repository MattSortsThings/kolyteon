using System.Reflection;
using Kolyteon.Common;
using Kolyteon.Tests.Architecture.TestUtils;

namespace Kolyteon.Tests.Architecture;

[ArchitectureTest]
public sealed class LibraryArchitectureTests
{
    private static readonly Assembly AssemblyUnderTest = typeof(Colour).Assembly;

    [Theory]
    [InlineData("Kolyteon.GraphColouring")]
    [InlineData("Kolyteon.MapColouring")]
    [InlineData("Kolyteon.NQueens")]
    [InlineData("Kolyteon.Shikaku")]
    [InlineData("Kolyteon.Sudoku")]
    public void PublicProblemTypes_ShouldBeImmutable(string problemNamespace)
    {
        // Arrange
        ConditionList conditions = Types.InAssembly(AssemblyUnderTest)
            .That()
            .ResideInNamespace(problemNamespace)
            .And()
            .ArePublic()
            .Should()
            .BeImmutable();

        // Act
        TestResult result = conditions.GetResult();

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public void PublicNonAnonymousTypes_ShouldNotResideInNamespaceContainingDotInternals()
    {
        // Arrange
        ConditionList conditions = Types.InAssembly(AssemblyUnderTest)
            .That()
            .ArePublic()
            .And()
            .DoNotHaveNameMatching("Anonymous")
            .ShouldNot()
            .ResideInNamespaceContaining(".Internals");

        // Act
        TestResult result = conditions.GetResult();

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public void NonPublicNonNestedNonAnonymousTypes_ShouldResideInNamespaceContainingDotInternals()
    {
        // Arrange
        ConditionList conditions = Types.InAssembly(AssemblyUnderTest)
            .That()
            .AreNotPublic()
            .And()
            .AreNotNested()
            .And()
            .DoNotHaveNameMatching("Anonymous")
            .And()
            .DoNotHaveNameMatching("<PrivateImplementationDetails>")
            .Should()
            .ResideInNamespaceContaining(".Internals");

        // Act
        TestResult result = conditions.GetResult();

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public void NonAbstractNonStaticNonAnonymousTypes_ShouldBeSealed()
    {
        // Arrange
        ConditionList conditions = Types.InAssembly(AssemblyUnderTest)
            .That()
            .AreNotAbstract()
            .And()
            .AreNotStatic()
            .And()
            .DoNotHaveNameMatching("Anonymous")
            .Should()
            .BeSealed();

        // Act
        TestResult result = conditions.GetResult();

        // Assert
        result.Should().BeSuccessful();
    }
}
