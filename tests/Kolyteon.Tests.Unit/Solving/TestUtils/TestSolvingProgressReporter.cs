using Kolyteon.Solving;

namespace Kolyteon.Tests.Unit.Solving.TestUtils;

public sealed class TestSolvingProgressReporter<TVariable, TDomainValue> : SolvingProgressReporter<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    protected internal override void OnSetup() { }

    protected internal override void OnReport() { }
}
