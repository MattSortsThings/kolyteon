using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mjt85.Kolyteon.FeatureTests.Helpers;

/// <summary>
///     Provides invariant properties for feature tests.
/// </summary>
internal static class Invariants
{
    public const string DEGREE_STATISTICS = "DEGREE_STATISTICS";
    public const string DESERIALIZED_PUZZLE = "DESERIALIZED_PUZZLE";
    public const string DOMAIN_SIZE_STATISTICS = "DOMAIN_SIZE_STATISTICS";
    public const string JSON = "JSON";
    public const string PROBLEM_METRICS = "PROBLEM_METRICS";
    public const string PROPOSED_SOLUTION = "PROPOSED_SOLUTION";
    public const string PUZZLE = "PUZZLE";
    public const string SUM_TIGHTNESS_STATISTICS = "SUM_TIGHTNESS_STATISTICS";
    public const string VALIDATION_RESULT = "VALIDATION_RESULT";

    public const double SixDecimalPlacesPrecision = 0.000001;

    public static JsonSerializerOptions GetJsonSerializerOptions() => new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}
