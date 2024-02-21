namespace Mjt85.Kolyteon.UnitTests.Helpers;

/// <summary>
///     Represents a very simple problem comprising a dictionary of letters and their permitted digits, from which no pair
///     of letters may be assigned equal digits.
/// </summary>
public sealed class TestProblem : Dictionary<Letter, Digit[]>;
