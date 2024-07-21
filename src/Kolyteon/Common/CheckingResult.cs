namespace Kolyteon.Common;

/// <summary>
///     Contains the result of a checking operation.
/// </summary>
public readonly record struct CheckingResult
{
    public CheckingResult() : this(true, null) { }

    private CheckingResult(bool isSuccessful, string? firstError)
    {
        IsSuccessful = isSuccessful;
        FirstError = firstError;
    }

    /// <summary>
    ///     Gets a boolean indicating whether the checking operation was successful.
    /// </summary>
    public bool IsSuccessful { get; }

    /// <summary>
    ///     Gets a string indicating the first error encountered during the checking operation, if any.
    /// </summary>
    /// <remarks>
    ///     The value of this property is <see langword="null" /> when the value of <see cref="IsSuccessful" /> is
    ///     <see langword="true" />.
    /// </remarks>
    public string? FirstError { get; }

    public static CheckingResult Success() => new(true, null);

    public static CheckingResult Failure(string firstError) => new(false, firstError);
}
