using NetArchTest.Rules;

namespace Kolyteon.Tests.Architecture.TestUtils;

internal static class TestResultExtensions
{
    internal static TestResultAssertions Should(this TestResult subject) => new(subject);
}
