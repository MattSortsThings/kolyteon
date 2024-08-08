using System.Diagnostics.CodeAnalysis;

namespace Kolyteon.Tests.Integration;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public abstract partial class ProblemGenerationTests
{
    public sealed class SeedZero : ProblemGenerationTests
    {
        private protected override int Seed => 0;
    }

    public sealed class SeedOne : ProblemGenerationTests
    {
        private protected override int Seed => 1;
    }

    public sealed class SeedEleven : ProblemGenerationTests
    {
        private protected override int Seed => 11;
    }

    public sealed class SeedFiveThousand : ProblemGenerationTests
    {
        private protected override int Seed => 5000;
    }
}
