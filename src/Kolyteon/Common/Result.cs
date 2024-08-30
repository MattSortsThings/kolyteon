namespace Kolyteon.Common;

/// <summary>
///     Represents the result of an operation that either succeeded or failed.
/// </summary>
public readonly record struct Result
{
    /// <summary>
    ///     Initializes a new <see cref="Result" /> instance with default <see cref="IsSuccessful" /> and
    ///     <see cref="FirstError" /> values of <see langword="true" /> and <see langword="null" /> respectively.
    /// </summary>
    public Result() : this(true, null) { }

    private Result(bool isSuccessful, string? firstError)
    {
        IsSuccessful = isSuccessful;
        FirstError = firstError;
    }

    /// <summary>
    ///     Gets a boolean indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccessful { get; }

    /// <summary>
    ///     Gets a string describing the first error encountered during the operation, if any.
    /// </summary>
    /// <remarks>
    ///     The value of this property is <see langword="null" /> when the value of <see cref="IsSuccessful" /> is
    ///     <see langword="true" />.
    /// </remarks>
    public string? FirstError { get; }

    /// <summary>
    ///     Creates and returns a new <see cref="Result" /> instance indicating a successful operation.
    /// </summary>
    /// <returns>A new <see cref="Result" /> instance.</returns>
    public static Result Success() => new(true, null);

    /// <summary>
    ///     Creates and returns a new <see cref="Result" /> instance indicating an unsuccessful operation, with the specified
    ///     <see cref="FirstError" />.
    /// </summary>
    /// <param name="firstError">A string describing the first error encountered during the operation.</param>
    /// <returns>A new <see cref="Result" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="firstError" /> is <see langword="null" />.</exception>
    public static Result Failure(string firstError) => firstError is not null
        ? new Result(false, firstError)
        : throw new ArgumentNullException(nameof(firstError));
}
