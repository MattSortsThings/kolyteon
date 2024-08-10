using System.Diagnostics.CodeAnalysis;

namespace Kolyteon.Tests.Integration.ProblemGeneration;

public abstract partial class ProblemGenerationTests
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public sealed class SeedValueZero : ProblemGenerationTests
    {
        private protected override int Seed => 0;
    }
}
