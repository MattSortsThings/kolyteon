using System.Diagnostics.CodeAnalysis;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.Internals.SearchTrees;

namespace Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookBack;

internal sealed class CBJNode<V, D> : LookBackNode<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public CBJNode(ISolvableBinaryCsp<V, D> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
        ConflictLevels = new HashSet<int>(Degree);
    }

    public HashSet<int> ConflictLevels { get; }

    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    public override int BacktrackLevel
    {
        get
        {
            var backtrackLevel = RootLevel;
            foreach (var l in ConflictLevels)
            {
                if (l > backtrackLevel)
                {
                    backtrackLevel = l;
                }
            }

            return backtrackLevel;
        }
    }

    public void UpdateBacktrackLevel(IVisitableNode ancestorNode) =>
        ConflictLevels.Add(ancestorNode.SearchTreeLevel);

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public void UnionMergeBacktrackDataFrom(CBJNode<V, D> futureNode)
    {
        foreach (var l in futureNode.ConflictLevels)
        {
            if (l != SearchTreeLevel)
            {
                ConflictLevels.Add(l);
            }
        }
    }

    public void ResetBacktrackLevel() => ConflictLevels.Clear();
}
