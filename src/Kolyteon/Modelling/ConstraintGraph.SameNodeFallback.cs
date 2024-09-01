namespace Kolyteon.Modelling;

public abstract partial class ConstraintGraph<TVariable, TDomainValue, TProblem>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
    where TProblem : class
{
    private sealed class SameNodeFallback : IAdjacency
    {
        private readonly ConstraintGraph<TVariable, TDomainValue, TProblem> _parent;

        public SameNodeFallback(ConstraintGraph<TVariable, TDomainValue, TProblem> parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public bool Adjacent => true;

        public bool Consistent(IAssignment assignmentA, IAssignment assignmentB)
        {
            Node node = _parent._nodes[assignmentA.VariableIndex];

            (int domainValueIndexA, int domainValueIndexB) = (assignmentA.DomainValueIndex, assignmentB.DomainValueIndex);

            if (domainValueIndexA < 0 || domainValueIndexA >= node.DomainSize)
            {
                throw new DomainValueIndexOutOfRangeException();
            }

            if (domainValueIndexB < 0 || domainValueIndexB >= node.DomainSize)
            {
                throw new DomainValueIndexOutOfRangeException();
            }

            return domainValueIndexA == domainValueIndexB;
        }
    }
}
