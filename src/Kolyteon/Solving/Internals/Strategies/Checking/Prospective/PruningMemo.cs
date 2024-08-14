namespace Kolyteon.Solving.Internals.Strategies.Checking.Prospective;

internal readonly record struct PruningMemo<TVariable, TDomainValue>(
    ProspectiveNode<TVariable, TDomainValue> PrunedNode,
    int PrunedCandidate)
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>;
