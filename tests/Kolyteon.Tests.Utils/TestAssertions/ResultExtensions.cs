using Kolyteon.Common;

namespace Kolyteon.Tests.Utils.TestAssertions;

public static class ResultExtensions
{
    public static ResultAssertions Should(this Result subject) => new(subject);
}
