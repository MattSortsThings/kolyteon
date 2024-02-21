using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.UnitTests.Helpers;

/// <summary>
///     Provides invariant properties for unit tests.
/// </summary>
internal static class Invariants
{
    public static readonly double SixDecimalPlacesPrecision = 0.000001;

    public static JsonSerializerOptions GetJsonSerializerOptions() => new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}
