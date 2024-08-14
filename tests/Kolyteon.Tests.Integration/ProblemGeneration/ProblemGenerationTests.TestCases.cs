using System.Diagnostics.CodeAnalysis;

namespace Kolyteon.Tests.Integration.ProblemGeneration;

public abstract partial class ProblemGenerationTests
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class TestCases
    {
        public sealed class SeedValueZero : ProblemGenerationTests
        {
            private protected override int Seed => 0;
        }

        public sealed class SeedValueOne : ProblemGenerationTests
        {
            private protected override int Seed => 1;
        }

        public sealed class SeedValueEleven : ProblemGenerationTests
        {
            private protected override int Seed => 1;
        }

        public sealed class SeedValueFiveThousand : ProblemGenerationTests
        {
            private protected override int Seed => 5000;
        }

        public sealed class SeedValueFiveThousandAndOne : ProblemGenerationTests
        {
            private protected override int Seed => 5001;
        }
    }
}
