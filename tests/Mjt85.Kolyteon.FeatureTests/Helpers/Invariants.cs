using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.FeatureTests.Helpers;

/// <summary>
///     Provides invariant properties for feature tests.
/// </summary>
internal static class Invariants
{
    public const string DESERIALIZED_PUZZLE = "DESERIALIZED_PUZZLE";
    public const string JSON = "JSON";
    public const string PROPOSED_SOLUTION = "PROPOSED_SOLUTION";
    public const string PUZZLE = "PUZZLE";
    public const string VALIDATION_RESULT = "VALIDATION_RESULT";

    public static JsonSerializerOptions GetJsonSerializerOptions() => new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}
