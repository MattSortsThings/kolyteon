using System.Diagnostics.CodeAnalysis;
using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Checking.LookAhead;

internal abstract class ArcTaskQueue<TNode, TVariable, TDomainValue>
    where TNode : ProspectiveNode<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
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

    public abstract void Populate(SearchTree<TNode, TVariable, TDomainValue> searchTree);

    public bool TryDequeue([NotNullWhen(true)] out TNode? operandNode, [NotNullWhen(true)] out TNode? contextNode)
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

    protected void Enqueue(TNode operandNode, TNode contextNode)
    {
        ArcTask task = new(operandNode, contextNode);
        if (_hashSet.Add(task))
        {
            _queue.Enqueue(task);
        }
    }

    private readonly record struct ArcTask(TNode OperandNode, TNode ContextNode)
    {
        public bool Equals(ArcTask other) => OperandNode.VariableIndex == other.OperandNode.VariableIndex
                                             && ContextNode.VariableIndex == other.ContextNode.VariableIndex;

        public override int GetHashCode() => HashCode.Combine(OperandNode.VariableIndex, ContextNode.VariableIndex);
    }
}
