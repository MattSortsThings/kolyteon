using Kolyteon.Modelling;

namespace Kolyteon.Solving.Internals.Strategies.Checking.LookBack;

internal sealed class BtNode<TVariable, TDomainValue> : RetrospectiveNode<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public BtNode(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
    }

    public override int BacktrackLevel => SearchTreeLevel - 1;
}
