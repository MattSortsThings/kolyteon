using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.SearchTrees;

namespace Kolyteon.Solving.Internals.Strategies.Checking.LookBack;

internal sealed class BjNode<TVariable, TDomainValue> : RetrospectiveNode<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    private int _backtrackLevel;

    public BjNode(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp, int variableIndex) : base(binaryCsp, variableIndex)
    {
        _backtrackLevel = Constants.Levels.Root;
    }

    public override int BacktrackLevel => _backtrackLevel;

    public void UpdateBacktrackLevel(IVisitableNode ancestorNode) =>
        _backtrackLevel = Math.Max(_backtrackLevel, ancestorNode.SearchTreeLevel);

    public void SetBacktrackLevelToMax() => _backtrackLevel = SearchTreeLevel - 1;

    public void ResetBacktrackLevel() => _backtrackLevel = Constants.Levels.Root;
}
