namespace Mjt85.Kolyteon.Modelling;

/// <summary>
///     Represents an ordered pair of values.
/// </summary>
/// <param name="Item1">The first value of the pair.</param>
/// <param name="Item2">The second value of the pair.</param>
/// <typeparam name="T">The pair value type.</typeparam>
public readonly record struct Pair<T>(T Item1, T Item2)
    where T : struct, IEquatable<T>, IComparable<T>;
