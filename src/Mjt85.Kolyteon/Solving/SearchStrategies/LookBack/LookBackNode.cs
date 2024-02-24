using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookBack;

internal abstract class LookBackNode<V, D> : SearchTreeNode<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    protected LookBackNode(ISolvableBinaryCsp<V, D> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
        Ancestors = new List<LookBackNode<V, D>>(Degree);
    }

    public List<LookBackNode<V, D>> Ancestors { get; }

    public void RepopulateAncestors(IReadOnlyList<LookBackNode<V, D>> searchTree)
    {
        for (var level = 0; level < SearchTreeLevel && Ancestors.Count < Degree; level++)
        {
            LookBackNode<V, D> pastNode = searchTree[level];

            if (AdjacentTo(pastNode))
            {
                Ancestors.Add(pastNode);
            }
        }
    }
}
