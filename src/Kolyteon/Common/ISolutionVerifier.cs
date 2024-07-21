namespace Kolyteon.Common;

/// <summary>
///     Represents a problem that can verify the correctness of any proposed solution.
/// </summary>
/// <typeparam name="TSolution">The solution type for the problem type.</typeparam>
public interface ISolutionVerifier<in TSolution>
{
    /// <summary>
    ///     Checks the correctness of the proposed solution to the problem represented by this instance and returns a value
    ///     indicating either a successful checking result or the first error encountered.
    /// </summary>
    /// <param name="solution">The proposed solution to the problem represented by this instance.</param>
    /// <returns>
    ///     A successful <see cref="CheckingResult" /> instance if the <paramref name="solution" /> parameter is a correct
    ///     solution to the problem represented by this instance; otherwise, an unsuccessful <see cref="CheckingResult" />
    ///     instance.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="solution" /> is <see langword="null" />.</exception>
    public CheckingResult VerifyCorrect(TSolution solution);
}
