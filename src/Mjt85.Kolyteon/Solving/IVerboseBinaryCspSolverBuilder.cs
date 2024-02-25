namespace Mjt85.Kolyteon.Solving;

public interface IVerboseBinaryCspSolverBuilder<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public IStepDelaySetter WithInitialCapacity(int capacity);

    public interface IStepDelaySetter
    {
        public ISearchStrategySetter AndInitialStepDelay(TimeSpan stepDelay);
    }

    public interface ISearchStrategySetter
    {
        public IOrderingStrategySetter AndInitialSearchStrategy(Search strategy);
    }

    public interface IOrderingStrategySetter
    {
        public ITerminal AndInitialOrderingStrategy(Ordering strategy);
    }

    public interface ITerminal
    {
        public VerboseBinaryCspSolver<V, D> Build();
    }
}
