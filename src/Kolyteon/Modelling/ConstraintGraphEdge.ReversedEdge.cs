namespace Kolyteon.Modelling;

public abstract partial class ConstraintGraph<TVariable, TDomainValue, TProblem>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
    where TProblem : class
{
    private sealed record ReversedEdge(Edge Edge) : IAdjacency
    {
        public bool Adjacent => true;

        public bool Consistent(IAssignment assignmentA, IAssignment assignmentB) =>
            Edge.Consistent(assignmentB.DomainValueIndex, assignmentA.DomainValueIndex);
    }
}