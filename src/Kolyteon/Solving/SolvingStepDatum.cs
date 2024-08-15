using Kolyteon.Modelling;

namespace Kolyteon.Solving;

public readonly record struct SolvingStepDatum<TVariable, TDomainValue>(
    SolvingStepType StepType,
    int SearchLevel,
    SolvingState SolvingState,
    Assignment<TVariable, TDomainValue>? Assignment = null)
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>;
