using System.Diagnostics.CodeAnalysis;
using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.Solving.Internals.SearchStrategies.LookBack;

internal sealed class GBJNode<V, D> : LookBackNode<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public GBJNode(ISolvableBinaryCsp<V, D> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
        InducedAncestorLevels = new HashSet<int>(Degree);
    }

    public HashSet<int> InducedAncestorLevels { get; }

    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    public override int BacktrackLevel
    {
        get
        {
            var backtrackLevel = RootLevel;
            foreach (var l in InducedAncestorLevels)
            {
                if (l > backtrackLevel)
                {
                    backtrackLevel = l;
                }
            }

            return backtrackLevel;
        }
    }

    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public void UnionMergeBacktrackDataFrom(GBJNode<V, D> futureNode)
    {
        foreach (var l in futureNode.InducedAncestorLevels)
        {
            if (l != SearchTreeLevel)
            {
                InducedAncestorLevels.Add(l);
            }
        }
    }

    public void ResetBacktrackLevel()
    {
        InducedAncestorLevels.Clear();
        foreach (LookBackNode<V, D> ancestor in Ancestors)
        {
            InducedAncestorLevels.Add(ancestor.SearchTreeLevel);
        }
    }
}
