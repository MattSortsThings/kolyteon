namespace Kolyteon.Common;

/// <summary>
///     An exception to be thrown when the client has attempted to create an invalid instance of a problem type.
/// </summary>
public sealed class InvalidProblemException : Exception
{
    private const string DefaultMessage = "Could not instantiate a valid instance of the problem type.";

    /// <summary>
    ///     Initializes a new <see cref="InvalidProblemException" /> instance with a default <see cref="Exception.Message" />
    ///     value and a <see langword="null" /> <see cref="Exception.InnerException" /> value.
    /// </summary>
    public InvalidProblemException() : base(DefaultMessage) { }

    /// <summary>
    ///     Initializes a new <see cref="InvalidProblemException" /> instance with the specified
    ///     <see cref="Exception.Message" /> value and a <see langword="null" /> <see cref="Exception.InnerException" /> value.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidProblemException(string message) : base(message) { }

    /// <summary>
    ///     Initializes a new <see cref="InvalidProblemException" /> instance with the specified
    ///     <see cref="Exception.Message" /> and <see cref="Exception.InnerException" /> values.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public InvalidProblemException(string message, Exception inner) : base(message, inner) { }
}
