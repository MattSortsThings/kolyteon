using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Retrospective;

internal abstract class RetrospectiveNode<TVariable, TDomainValue> : SearchTreeNode<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    protected RetrospectiveNode(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp, int variableIndex) :
        base(binaryCsp, variableIndex)
    {
        Ancestors = new List<RetrospectiveNode<TVariable, TDomainValue>>(Degree);
    }

    public List<RetrospectiveNode<TVariable, TDomainValue>> Ancestors { get; }

    public void PopulateAncestors(IReadOnlyList<RetrospectiveNode<TVariable, TDomainValue>> searchTree)
    {
        for (int level = 0; level < SearchTreeLevel && Ancestors.Count < Degree; level++)
        {
            RetrospectiveNode<TVariable, TDomainValue> pastNode = searchTree[level];

            if (AdjacentTo(pastNode))
            {
                Ancestors.Add(pastNode);
            }
        }
    }

    public void ClearAncestors() => Ancestors.Clear();
}
