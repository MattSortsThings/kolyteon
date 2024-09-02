using Kolyteon.Modelling;

namespace Kolyteon.Solving.Internals.Strategies.Checking.LookAhead;

internal sealed class FcNode<TVariable, TDomainValue> : ProspectiveNode<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public FcNode(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
    }
}
