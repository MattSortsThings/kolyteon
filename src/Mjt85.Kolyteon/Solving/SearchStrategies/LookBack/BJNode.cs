using System.Diagnostics.CodeAnalysis;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Solving.SearchTrees;

namespace Mjt85.Kolyteon.Solving.SearchStrategies.LookBack;

internal sealed class BJNode<V, D> : LookBackNode<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    private int _backtrackLevel = RootLevel;

    public BJNode(ISolvableBinaryCsp<V, D> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
    }

    [SuppressMessage("ReSharper", "ConvertToAutoPropertyWithPrivateSetter")]
    public override int BacktrackLevel => _backtrackLevel;

    public void UpdateBacktrackLevel(IVisitableNode ancestorNode) =>
        _backtrackLevel = Math.Max(_backtrackLevel, ancestorNode.SearchTreeLevel);

    public void SetBacktrackLevelToMax() => _backtrackLevel = SearchTreeLevel - 1;

    public void ResetBacktrackLevel() => _backtrackLevel = RootLevel;
}
