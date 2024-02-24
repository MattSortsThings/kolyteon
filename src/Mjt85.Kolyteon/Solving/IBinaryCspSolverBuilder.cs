namespace Mjt85.Kolyteon.Solving;

public interface IBinaryCspSolverBuilder<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public ISearchStrategySetter WithInitialCapacity(int capacity);

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
        public BinaryCspSolver<V, D> Build();
    }
}
