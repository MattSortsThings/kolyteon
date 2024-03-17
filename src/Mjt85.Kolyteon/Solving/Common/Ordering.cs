namespace Mjt85.Kolyteon.Solving.Common;

/// <summary>
///     Specifies the ordering strategy component of a binary CSP solving algorithm.
/// </summary>
public enum Ordering
{
    /// <summary>
    ///     Specifies that the solving algorithm uses no ordering strategy (short code <c>"NO"</c>).
    /// </summary>
    None,

    /// <summary>
    ///     Specifies that the solving algorithm uses the Brélaz ordering strategy (short code <c>"BZ"</c>).
    /// </summary>
    Brelaz,

    /// <summary>
    ///     Specifies that the solving algorithm uses the Maximum Cardinality ordering strategy (short code <c>"MC"</c>).
    /// </summary>
    MaxCardinality,

    /// <summary>
    ///     Specifies that the solving algorithm uses the Maximum Tightness ordering strategy (short code <c>"MT"</c>).
    /// </summary>
    MaxTightness
}
