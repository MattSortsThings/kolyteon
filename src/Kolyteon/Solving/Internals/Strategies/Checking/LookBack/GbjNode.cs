using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Checking.LookBack;

internal sealed class GbjNode<TVariable, TDomainValue> : RetrospectiveNode<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public GbjNode(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
        InducedAncestorLevels = new HashSet<int>(Degree);
    }

    private HashSet<int> InducedAncestorLevels { get; }

    public override int BacktrackLevel
    {
        get
        {
            int backtrackLevel = Constants.Levels.Root;
            foreach (int level in InducedAncestorLevels)
            {
                if (level > backtrackLevel)
                {
                    backtrackLevel = level;
                }
            }

            return backtrackLevel;
        }
    }

    public void UnionMergeBacktrackDataFrom(GbjNode<TVariable, TDomainValue> futureNode)
    {
        foreach (int level in futureNode.InducedAncestorLevels)
        {
            if (level != SearchTreeLevel)
            {
                InducedAncestorLevels.Add(level);
            }
        }
    }

    public void ResetBacktrackLevel()
    {
        InducedAncestorLevels.Clear();
        foreach (RetrospectiveNode<TVariable, TDomainValue> ancestor in Ancestors)
        {
            InducedAncestorLevels.Add(ancestor.SearchTreeLevel);
        }
    }
}
