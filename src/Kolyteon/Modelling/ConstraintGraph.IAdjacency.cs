namespace Kolyteon.Modelling;

public abstract partial class ConstraintGraph<TVariable, TDomainValue, TProblem>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
    where TProblem : class
{
    private interface IAdjacency
    {
        public bool Adjacent { get; }

        public bool Consistent(IAssignment assignmentA, IAssignment assignmentB);
    }
}
