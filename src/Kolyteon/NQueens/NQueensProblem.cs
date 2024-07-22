using System.Text.Json.Serialization;
using Kolyteon.Common;
using Kolyteon.NQueens.Internals;

namespace Kolyteon.NQueens;

/// <summary>
///     Represents a valid (but not necessarily solvable) <i>N</i>-Queens problem.
/// </summary>
[Serializable]
public sealed record NQueensProblem : ISolutionVerifier<IReadOnlyList<Square>>
{
    [JsonConstructor]
    internal NQueensProblem(Block chessBoard, int queens)
    {
        ChessBoard = chessBoard;
        Queens = queens;
    }

    /// <summary>
    ///     Gets a <see cref="Block" /> representing the chess board dimensions for the problem.
    /// </summary>
    public Block ChessBoard { get; }

    /// <summary>
    ///     Gets the number of queens for the problem.
    /// </summary>
    public int Queens { get; }

    /// <summary>
    ///     Indicates whether this <see cref="NQueensProblem" /> instance has equal value to another instance of the same type,
    ///     that is, they represent logically identical problems.
    /// </summary>
    /// <remarks>
    ///     Two <see cref="NQueensProblem" /> instances have equal value if their <see cref="Queens" /> values are equal.
    /// </remarks>
    /// <param name="other">The <see cref="NQueensProblem" /> instance against which this instance is to be compared.</param>
    /// <returns>
    ///     <see langword="true" /> if this instance and the <paramref name="other" /> parameter have equal value;
    ///     otherwise, <see langword="false" />. If the <paramref name="other" /> parameter is <see langword="null" />, the
    ///     method returns <see langword="false" />.
    /// </returns>
    public bool Equals(NQueensProblem? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Queens == other.Queens;
    }

    /// <inheritdoc />
    /// <remarks>
    ///     <para>
    ///         The solution to an <see cref="NQueensProblem" /> instance is a list of <see cref="Square" /> values,
    ///         representing the chess board squares where the queens are to be positioned.
    ///     </para>
    /// </remarks>
    public CheckingResult VerifyCorrect(IReadOnlyList<Square> solution)
    {
        ArgumentNullException.ThrowIfNull(solution);

        return SolutionVerification.OneSquarePerQueen
            .Then(SolutionVerification.AllSquaresInChessBoard)
            .Then(SolutionVerification.AllSquaresUnique)
            .Then(SolutionVerification.NoCapturingSquares)
            .VerifyCorrect(solution, this);
    }

    /// <summary>
    ///     Deconstructs this <see cref="NQueensProblem" /> instance.
    /// </summary>
    /// <param name="chessBoard">Represents the chess board dimensions for the problem.</param>
    /// <param name="queens">The number of queens in the problem.</param>
    public void Deconstruct(out Block chessBoard, out int queens)
    {
        chessBoard = ChessBoard;
        queens = Queens;
    }

    /// <summary>
    ///     Returns the hash code for this <see cref="NQueensProblem" /> instance.
    /// </summary>
    /// <returns>A 32-bit signed integer hash code.</returns>
    public override int GetHashCode() => Queens;

    /// <summary>
    ///     Creates and returns a new <see cref="NQueensProblem" /> for the specified value of <i>N</i>.
    /// </summary>
    /// <param name="n">
    ///     An integer greater than or equal to 1. The number of queens in the problem, and the width and height of
    ///     the chess board.
    /// </param>
    /// <returns>A new <see cref="NQueensProblem" /> instance.</returns>
    /// <exception cref="InvalidProblemException"><paramref name="n" /> is less than 1.</exception>
    public static NQueensProblem FromN(int n)
    {
        if (n < 1)
        {
            throw new InvalidProblemException("Value of N must not be less than 1.");
        }

        return new NQueensProblem(Dimensions.FromWidthAndHeight(n, n).ToBlock(), n);
    }
}
