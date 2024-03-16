namespace Mjt85.Kolyteon.Solving.Common;

public sealed record Algorithm(Search SearchStrategy, Ordering OrderingStrategy)
{
    /// <summary>
    ///     Creates and returns the unique short code representation of this instance.
    /// </summary>
    /// <remarks>
    ///     An <see cref="Algorithm" /> instance is short code is comprised of its strategy short codes concatenated with
    ///     a single plus sign.
    /// </remarks>
    /// <example>
    ///     <code>
    /// class Example
    /// {
    ///   public static void Main()
    ///   {
    ///     Algorithm a0 = new(Search.Backtracking, Ordering.None);
    ///     Algorithm a1 = new(Search.FullLookingAhead, Ordering.MaxTightness);
    /// 
    ///     Console.WriteLine(a0.ToShortCode());
    ///     Console.WriteLine(a1.ToShortCode());
    ///   }
    /// }
    /// // This example produces the following console output:
    /// // BT+NO
    /// // FLA+MT
    /// </code>
    /// </example>
    /// <returns>A string containing the unique short code for this instance.</returns>
    public string ToShortCode() => $"{SearchStrategy.ToShortCode()}+{OrderingStrategy.ToShortCode()}";
}
