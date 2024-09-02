using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Checking.LookBack;

internal sealed class CbjNode<TVariable, TDomainValue> : RetrospectiveNode<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public CbjNode(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
        ConflictLevels = new HashSet<int>(Degree);
    }

    private HashSet<int> ConflictLevels { get; }

    public override int BacktrackLevel
    {
        get
        {
            int backtrackLevel = Constants.Levels.Root;
            foreach (int level in ConflictLevels)
            {
                if (level > backtrackLevel)
                {
                    backtrackLevel = level;
                }
            }

            return backtrackLevel;
        }
    }

    public void UpdateBacktrackLevel(IVisitableNode ancestorNode) =>
        ConflictLevels.Add(ancestorNode.SearchTreeLevel);

    public void UnionMergeBacktrackDataFrom(CbjNode<TVariable, TDomainValue> futureNode)
    {
        foreach (int level in futureNode.ConflictLevels)
        {
            if (level != SearchTreeLevel)
            {
                ConflictLevels.Add(level);
            }
        }
    }

    public void ResetBacktrackLevel() => ConflictLevels.Clear();
}
