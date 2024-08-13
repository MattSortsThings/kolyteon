using Kolyteon.Modelling;
using Kolyteon.Solving.Internals.Strategies.Ordering;

namespace Kolyteon.Solving.Internals.Strategies.Checking.Common;

internal interface ICheckingStrategy<TVariable, TDomainValue>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
{
    /// <summary>
    ///     Gets the checking strategy's identifier.
    /// </summary>
    public CheckingStrategy Identifier { get; }

    /// <summary>
    ///     Gets or sets the search tree capacity.
    /// </summary>
    public int Capacity { get; set; }

    /// <summary>
    ///     Gets a boolean value indicating whether the current state of the search tree is safe (i.e. may lead to a solution).
    /// </summary>
    /// <remarks>The default value of this property is <see langword="true" />.</remarks>
    public bool Safe { get; }

    /// <summary>
    ///     Gets an integer denoting the root level of the search tree.
    /// </summary>
    /// <remarks>The value of this property is fixed at -1.</remarks>
    public int RootLevel { get; }

    /// <summary>
    ///     Gets an integer denoting the leaf level of the search tree.
    /// </summary>
    /// <remarks>The value of this property is equal to the number of nodes in the search tree.</remarks>
    public int LeafLevel { get; }

    /// <summary>
    ///     Gets an integer denoting the current level of the search in the search tree.
    /// </summary>
    /// <remarks>The default value of this property is -1.</remarks>
    public int SearchLevel { get; }

    public void Simplify();

    public void TryAssign();

    public void Advance();

    public void Backtrack();

    public void Optimize(IOrderingStrategy orderingStrategy);

    public void Populate(IReadOnlyBinaryCsp<TVariable, TDomainValue> binaryCsp);

    public void Reset();

    public Assignment<TVariable, TDomainValue>[] GetAllAssignments();
}
