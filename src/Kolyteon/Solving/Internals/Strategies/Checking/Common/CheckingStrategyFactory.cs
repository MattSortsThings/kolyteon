using Kolyteon.Solving.Internals.Strategies.Checking.LookAhead;
using Kolyteon.Solving.Internals.Strategies.Checking.LookBack;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Common;

internal sealed class CheckingStrategyFactory<TVariable, TDomainValue> : ICheckingStrategyFactory<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    public ICheckingStrategy<TVariable, TDomainValue> Create(CheckingStrategy strategy, int capacity)
    {
        if (strategy == CheckingStrategy.MaintainingArcConsistency)
        {
            return new MacStrategy<TVariable, TDomainValue>(capacity);
        }

        if (strategy == CheckingStrategy.FullLookingAhead)
        {
            return new FlaStrategy<TVariable, TDomainValue>(capacity);
        }

        if (strategy == CheckingStrategy.PartialLookingAhead)
        {
            return new PlaStrategy<TVariable, TDomainValue>(capacity);
        }

        if (strategy == CheckingStrategy.ForwardChecking)
        {
            return new FcStrategy<TVariable, TDomainValue>(capacity);
        }

        if (strategy == CheckingStrategy.ConflictBackjumping)
        {
            return new CbjStrategy<TVariable, TDomainValue>(capacity);
        }

        if (strategy == CheckingStrategy.GraphBackjumping)
        {
            return new GbjStrategy<TVariable, TDomainValue>(capacity);
        }

        if (strategy == CheckingStrategy.Backjumping)
        {
            return new BjStrategy<TVariable, TDomainValue>(capacity);
        }

        if (strategy == CheckingStrategy.NaiveBacktracking)
        {
            return new BtStrategy<TVariable, TDomainValue>(capacity);
        }

        throw new ArgumentException($"No implementation exists for Checking Strategy value '{strategy}'.");
    }
}
