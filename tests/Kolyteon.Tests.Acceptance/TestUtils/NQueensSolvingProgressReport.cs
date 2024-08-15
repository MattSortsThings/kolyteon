using Kolyteon.Common;
using Kolyteon.Solving;

namespace Kolyteon.Tests.Acceptance.TestUtils;

public sealed record NQueensSolvingProgressReport
{
    public int TotalSteps { get; init; }

    public int SearchLevel { get; init; }

    public SolvingState SolvingState { get; init; }

    public Square[] Squares { get; init; } = Array.Empty<Square>();

    public bool Equals(NQueensSolvingProgressReport? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return TotalSteps == other.TotalSteps
               && SearchLevel == other.SearchLevel
               && SolvingState == other.SolvingState
               && Squares.Length == other.Squares.Length
               && Squares.SequenceEqual(other.Squares);
    }

    public override int GetHashCode() => HashCode.Combine(TotalSteps, SearchLevel, (int)SolvingState, Squares);
}
