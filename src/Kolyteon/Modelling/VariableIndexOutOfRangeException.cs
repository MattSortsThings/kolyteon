namespace Kolyteon.Modelling;

/// <summary>
///     The exception that is thrown when an attempt is made to access a binary CSP variable with an index that is outside
///     the bounds of the binary CSP.
/// </summary>
public sealed class VariableIndexOutOfRangeException : Exception
{
    private const string DefaultMessage =
        "Variable index must be non-negative and less than the number of binary CSP variables.";

    /// <summary>
    ///     Initializes a new <see cref="VariableIndexOutOfRangeException" /> instance with a default
    ///     <see cref="Exception.Message" /> value and a <see langword="null" /> <see cref="Exception.InnerException" /> value.
    /// </summary>
    public VariableIndexOutOfRangeException() : base(DefaultMessage) { }

    /// <summary>
    ///     Initializes a new <see cref="VariableIndexOutOfRangeException" /> instance with the specified
    ///     <see cref="Exception.Message" /> value and a <see langword="null" /> <see cref="Exception.InnerException" /> value.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public VariableIndexOutOfRangeException(string message) : base(message) { }

    /// <summary>
    ///     Initializes a new <see cref="VariableIndexOutOfRangeException" /> instance with the specified
    ///     <see cref="Exception.Message" /> and <see cref="Exception.InnerException" /> values.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public VariableIndexOutOfRangeException(string message, Exception inner) : base(message, inner) { }
}
