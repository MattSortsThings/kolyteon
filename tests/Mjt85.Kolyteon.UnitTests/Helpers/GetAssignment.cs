using Mjt85.Kolyteon.Modelling;

namespace Mjt85.Kolyteon.UnitTests.Helpers;

public static class GetAssignment
{
    public static AssignmentBuilder<V> WithVariable<V>(V variable) where V : struct, IComparable<V>, IEquatable<V> =>
        new(variable);


    public class AssignmentBuilder<V>(V variable)
        where V : struct, IComparable<V>, IEquatable<V>
    {
        public Assignment<V, D> AndDomainValue<D>(D domainValue) where D : struct, IComparable<D>, IEquatable<D> =>
            new(variable, domainValue);
    }
}
