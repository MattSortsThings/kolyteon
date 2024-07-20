using System.Reflection;
using Kolyteon.Placeholders;
using Kolyteon.Tests.Architecture.TestUtils;
using NetArchTest.Rules;

namespace Kolyteon.Tests.Architecture;

[ArchitectureTest]
public sealed class LibraryArchitectureTests
{
    private static readonly Assembly AssemblyUnderTest = typeof(PlaceholderEnum).Assembly;

    [Fact]
    public void PublicNonAnonymousTypes_ShouldNotResideInNamespaceContainingDotInternals()
    {
        // Arrange
        ConditionList condition = Types.InAssembly(AssemblyUnderTest)
            .That()
            .ArePublic()
            .And()
            .DoNotHaveNameMatching("Anonymous")
            .ShouldNot()
            .ResideInNamespaceContaining(".Internals");

        // Act
        TestResult result = condition.GetResult();

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public void NonPublicTypes_ShouldResideInNamespaceContainingDotInternals()
    {
        // Arrange
        ConditionList condition = Types.InAssembly(AssemblyUnderTest)
            .That()
            .AreNotPublic()
            .Should()
            .ResideInNamespaceContaining(".Internals");

        // Act
        TestResult result = condition.GetResult();

        // Assert
        result.Should().BeSuccessful();
    }

    [Fact]
    public void NonAbstractNonStaticTypes_ShouldBeSealed()
    {
        // Arrange
        ConditionList conditions = Types.InAssembly(AssemblyUnderTest)
            .That()
            .AreNotAbstract()
            .And()
            .AreNotStatic()
            .Should()
            .BeSealed();

        // Act
        TestResult result = conditions.GetResult();

        // Assert
        result.Should().BeSuccessful();
    }
}
