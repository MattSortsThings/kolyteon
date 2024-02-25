using System.Diagnostics.CodeAnalysis;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookAhead;

internal abstract class ArcTaskQueue<N, V, D>
    where N : LookAheadNode<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private readonly HashSet<ArcTask> _hashSet;
    private readonly Queue<ArcTask> _queue;

    internal ArcTaskQueue(int capacity)
    {
        _hashSet = new HashSet<ArcTask>(capacity);
        _queue = new Queue<ArcTask>(capacity);
    }

    public void Clear()
    {
        _hashSet.Clear();
        _queue.Clear();
    }

    public abstract void Populate(IReadOnlyList<N> searchTree, int searchLevel);

    public bool TryDequeue([NotNullWhen(true)] out N? operandNode, [NotNullWhen(true)] out N? contextNode)
    {
        if (_queue.TryDequeue(out ArcTask d))
        {
            _hashSet.Remove(d);

            (operandNode, contextNode) = d;

            return true;
        }

        operandNode = null;
        contextNode = null;

        return false;
    }

    protected void Enqueue(N operandNode, N contextNode)
    {
        ArcTask task = new(operandNode, contextNode);
        if (_hashSet.Add(task))
        {
            _queue.Enqueue(task);
        }
    }

    private readonly record struct ArcTask(N OperandNode, N ContextNode)
    {
        public bool Equals(ArcTask other) => OperandNode.VariableIndex == other.OperandNode.VariableIndex
                                             && ContextNode.VariableIndex == other.ContextNode.VariableIndex;

        public override int GetHashCode() => HashCode.Combine(OperandNode.VariableIndex, ContextNode.VariableIndex);
    }
}
