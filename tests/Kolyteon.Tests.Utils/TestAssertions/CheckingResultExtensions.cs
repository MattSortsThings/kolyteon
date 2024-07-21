using Kolyteon.Common;

namespace Kolyteon.Tests.Utils.TestAssertions;

public static class CheckingResultExtensions
{
    public static CheckingResultAssertions Should(this CheckingResult subject) => new(subject);
}
