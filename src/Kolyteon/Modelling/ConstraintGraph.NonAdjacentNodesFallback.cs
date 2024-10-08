namespace Kolyteon.Modelling;

public abstract partial class ConstraintGraph<TVariable, TDomainValue, TProblem>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
    where TProblem : class
{
    private sealed class NonAdjacentNodesFallback : IAdjacency
    {
        private readonly ConstraintGraph<TVariable, TDomainValue, TProblem> _parent;

        public NonAdjacentNodesFallback(ConstraintGraph<TVariable, TDomainValue, TProblem> parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public bool Adjacent => false;

        public bool Consistent(IAssignment assignmentA, IAssignment assignmentB)
        {
            (Node nodeA, Node nodeB) = (_parent._nodes[assignmentA.VariableIndex], _parent._nodes[assignmentB.VariableIndex]);
            (int domainValueIndexA, int domainValueIndexB) = (assignmentA.DomainValueIndex, assignmentB.DomainValueIndex);

            if (domainValueIndexA < 0 || domainValueIndexA >= nodeA.DomainSize)
            {
                throw new DomainValueIndexOutOfRangeException();
            }

            if (domainValueIndexB < 0 || domainValueIndexB >= nodeB.DomainSize)
            {
                throw new DomainValueIndexOutOfRangeException();
            }

            return true;
        }
    }
}
