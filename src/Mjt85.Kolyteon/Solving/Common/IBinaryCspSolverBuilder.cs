using Mjt85.Kolyteon.Solving.Silent;
using Mjt85.Kolyteon.Solving.Verbose;

namespace Mjt85.Kolyteon.Solving.Common;

public interface IBinaryCspSolverBuilder
{
    public interface ISearchStrategySetter
    {
        public IOrderingStrategySetter AndInitialSearchStrategy(Search strategy);
    }

    public interface IOrderingStrategySetter
    {
        public IModeSetter AndInitialOrderingStrategy(Ordering strategy);
    }

    public interface IModeSetter
    {
        public ISilentTerminal Silent();

        public IVerboseStepDelaySetter Verbose();
    }

    public interface IVerboseStepDelaySetter
    {
        public IVerboseTerminal WithInitialStepDelay(TimeSpan delay);
    }

    public interface ISilentTerminal
    {
        public SilentBinaryCspSolver<V, D> Build<V, D>()
            where V : struct, IComparable<V>, IEquatable<V>
            where D : struct, IComparable<D>, IEquatable<D>;
    }

    public interface IVerboseTerminal
    {
        public VerboseBinaryCspSolver<V, D> Build<V, D>()
            where V : struct, IComparable<V>, IEquatable<V>
            where D : struct, IComparable<D>, IEquatable<D>;
    }
}
