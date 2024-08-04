using Kolyteon.Modelling;

namespace Kolyteon.Tests.Unit.TestUtils;

/// <summary>
///     Represents the assignment of a domain value to a binary CSP variable, using the indexes of the variable and the
///     domain value.
/// </summary>
internal sealed record FakeAssignment : IAssignment
{
    /// <summary>
    ///     Gets or initializes the zero-based index of the variable in the binary CSP.
    /// </summary>
    /// <value>A non-negative 32-bit signed integer. The zero-based index of the variable in the binary CSP.</value>
    public int VariableIndex { get; init; }

    /// <summary>
    ///     Gets or initializes the zero-based index of the assigned value in the variable's domain.
    /// </summary>
    /// <value>A non-negative 32-bit signed integer. The zero-based index of the assigned value in the variable's domain.</value>
    public int DomainValueIndex { get; init; }
}
