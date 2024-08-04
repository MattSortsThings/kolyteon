namespace Kolyteon.Modelling;

/// <summary>
///     The exception that is thrown when an attempt is made to access a value in a binary CSP domain with an index that is
///     outside the bounds of the domain.
/// </summary>
public sealed class DomainValueIndexOutOfRangeException : Exception
{
    private const string DefaultMessage =
        "Domain value index must be non-negative and less than the number of values in the binary CSP variable's domain.";

    /// <summary>
    ///     Initializes a new <see cref="DomainValueIndexOutOfRangeException" /> instance with a default
    ///     <see cref="Exception.Message" /> value and a <see langword="null" /> <see cref="Exception.InnerException" /> value.
    /// </summary>
    public DomainValueIndexOutOfRangeException() : base(DefaultMessage) { }

    /// <summary>
    ///     Initializes a new <see cref="DomainValueIndexOutOfRangeException" /> instance with the specified
    ///     <see cref="Exception.Message" /> value and a <see langword="null" /> <see cref="Exception.InnerException" /> value.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DomainValueIndexOutOfRangeException(string message) : base(message) { }

    /// <summary>
    ///     Initializes a new <see cref="DomainValueIndexOutOfRangeException" /> instance with the specified
    ///     <see cref="Exception.Message" /> and <see cref="Exception.InnerException" /> values.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public DomainValueIndexOutOfRangeException(string message, Exception inner) : base(message, inner) { }
}
