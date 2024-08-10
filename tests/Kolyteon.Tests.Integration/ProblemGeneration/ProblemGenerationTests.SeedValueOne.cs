using System.Diagnostics.CodeAnalysis;

namespace Kolyteon.Tests.Integration.ProblemGeneration;

public abstract partial class ProblemGenerationTests
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class SeedValueOne : ProblemGenerationTests
    {
        private protected override int Seed => 1;
    }
}
