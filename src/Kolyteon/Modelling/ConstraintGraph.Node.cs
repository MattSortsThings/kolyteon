using Kolyteon.Modelling.Testing;

namespace Kolyteon.Modelling;

public abstract partial class ConstraintGraph<TVariable, TDomainValue, TProblem>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
    where TProblem : class
{
    private sealed record Node
    {
        public TVariable Variable { get; init; }

        public TDomainValue[] Domain { get; init; } = [];

        public int DomainSize => Domain.Length;

        public int Degree { get; internal set; }

        public double SumTightness { get; internal set; }

        public Assignment<TVariable, TDomainValue> Map(IAssignment assignment)
        {
            try
            {
                return new Assignment<TVariable, TDomainValue>(Variable, Domain[assignment.DomainValueIndex]);
            }
            catch (IndexOutOfRangeException)
            {
                throw new DomainValueIndexOutOfRangeException();
            }
        }

        public ConstraintGraphNodeDatum<TVariable, TDomainValue> ToConstraintGraphNodeDatum() => new()
        {
            Variable = Variable, Domain = Domain, Degree = Degree, SumTightness = SumTightness
        };
    }
}
