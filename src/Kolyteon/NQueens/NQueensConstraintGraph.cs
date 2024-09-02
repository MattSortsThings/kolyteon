using System.Diagnostics.CodeAnalysis;
using Kolyteon.Common;
using Kolyteon.Modelling;

namespace Kolyteon.NQueens;

/// <summary>
///     Models a <see cref="NQueensProblem" /> instance as a generic binary CSP.
/// </summary>
public sealed class NQueensConstraintGraph : ConstraintGraph<int, Square, NQueensProblem>
{
    private int _chessBoardHeightInSquares;
    private int _queens;

    /// <summary>
    ///     Initializes a new <see cref="NQueensConstraintGraph" /> instance with a default initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" /> of 0.
    /// </summary>
    public NQueensConstraintGraph() { }

    /// <summary>
    ///     Initializes a new <see cref="NQueensConstraintGraph" /> instance with the specified initial
    ///     <see cref="ConstraintGraph{TVariable,TDomainValue,TProblem}.Capacity" />.
    /// </summary>
    /// <param name="capacity">
    ///     A non-negative integer. The initial capacity of the <see cref="NQueensConstraintGraph" /> instance.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity" /> is negative.</exception>
    public NQueensConstraintGraph(int capacity) : base(capacity) { }

    /// <summary>
    ///     Creates and returns a new <see cref="NQueensConstraintGraph" /> instance that is modelling the specified
    ///     <i>N</i>-Queens problem as a binary CSP.
    /// </summary>
    /// <param name="problem">The problem to be modelled.</param>
    /// <returns>A new <see cref="NQueensConstraintGraph" /> instance.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="problem" /> is <see langword="null" />.</exception>
    public static NQueensConstraintGraph ModellingProblem(NQueensProblem problem)
    {
        ArgumentNullException.ThrowIfNull(problem);

        NQueensConstraintGraph constraintGraph = new(problem.Queens);
        constraintGraph.Model(problem);

        return constraintGraph;
    }

    /// <inheritdoc />
    protected override void PopulateProblemData(NQueensProblem problem) =>
        ((_, (_, _chessBoardHeightInSquares)), _queens) = problem;

    /// <inheritdoc />
    protected override IEnumerable<int> GetVariables() => Enumerable.Range(0, _queens);

    /// <inheritdoc />
    protected override IEnumerable<Square> GetDomainValues(int presentVariable)
    {
        for (int row = 0; row < _chessBoardHeightInSquares; row++)
        {
            yield return Square.FromColumnAndRow(presentVariable, row);
        }
    }

    /// <inheritdoc />
    protected override bool TryGetBinaryPredicate(int firstVariable,
        int secondVariable,
        [NotNullWhen(true)] out Func<Square, Square, bool>? binaryPredicate)
    {
        binaryPredicate = SquaresDoNotCapture;

        return true;
    }

    /// <inheritdoc />
    protected override void ClearProblemData()
    {
        _chessBoardHeightInSquares = default;
        _queens = default;
    }

    private static bool SquaresDoNotCapture(Square firstSquare, Square secondSquare) => !firstSquare.Captures(in secondSquare);
}
