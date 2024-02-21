using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.UnitTests.Helpers;

/// <summary>
///     Implements <see cref="IAssignment" /> for use in testing.
/// </summary>
public record TestAssignment : IAssignment
{
    public int VariableIndex { get; init; }

    public int DomainValueIndex { get; init; }
}
