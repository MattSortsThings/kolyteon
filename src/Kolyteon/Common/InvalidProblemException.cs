namespace Kolyteon.Common;

/// <summary>
///     An exception to be thrown when the client has attempted to create an invalid instance of a problem type.
/// </summary>
public sealed class InvalidProblemException : Exception
{
    public InvalidProblemException()
    {
    }

    public InvalidProblemException(string message) : base(message) { }

    public InvalidProblemException(string message, Exception inner) : base(message, inner) { }
}
