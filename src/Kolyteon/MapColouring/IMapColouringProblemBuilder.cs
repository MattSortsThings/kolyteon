using Kolyteon.Common;

namespace Kolyteon.MapColouring;

/// <summary>
///     Fluent builder API for the <see cref="MapColouringProblem" /> record type.
/// </summary>
/// <remarks>
///     Any <see cref="MapColouringProblem" /> instance built using this API is guaranteed to represent a valid (but
///     not necessarily solvable) Map Colouring problem.
/// </remarks>
public interface IMapColouringProblemBuilder
{
    /// <summary>
    ///     Sets the canvas size for the Map Colouring problem.
    /// </summary>
    /// <param name="dimensions">Specifies the size of the canvas for the problem.</param>
    /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
    public IColoursSetter WithCanvasSize(Dimensions dimensions);

    /// <summary>
    ///     Fluent builder API for the <see cref="MapColouringProblem" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="MapColouringProblem" /> instance built using this API is guaranteed to represent a valid (but
    ///     not necessarily solvable) Map Colouring problem.
    /// </remarks>
    public interface IColoursSetter
    {
        /// <summary>
        ///     Configures the builder to use the specified global set of permitted colours for every block added to the problem.
        /// </summary>
        /// <param name="colours">The permitted colours all blocks in the problem. Any duplicate values will be discarded.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public IBlockAdder UseGlobalColours(params Colour[] colours);

        /// <summary>
        ///     Configures the builder to use a different set of permitted colours for each block added to the problem.
        /// </summary>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public IBlockAndColoursAdder UseBlockSpecificColours();
    }

    /// <summary>
    ///     Fluent builder API for the <see cref="MapColouringProblem" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="MapColouringProblem" /> instance built using this API is guaranteed to represent a valid (but
    ///     not necessarily solvable) Map Colouring problem.
    /// </remarks>
    public interface IBlockAdder : ITerminal
    {
        /// <summary>
        ///     Adds the specified block to the problem.
        /// </summary>
        /// <param name="block">The block to be added to the problem.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public IBlockAdder AddBlock(Block block);
    }

    /// <summary>
    ///     Fluent builder API for the <see cref="MapColouringProblem" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="MapColouringProblem" /> instance built using this API is guaranteed to represent a valid (but
    ///     not necessarily solvable) Map Colouring problem.
    /// </remarks>
    public interface IBlockAndColoursAdder : ITerminal
    {
        /// <summary>
        ///     Adds the specified block to the problem, with the specified set of permitted colours.
        /// </summary>
        /// <param name="block">The block to be added to the problem.</param>
        /// <param name="colours">The permitted colours for the block. Any duplicate values will be discarded.</param>
        /// <returns>The same fluent builder instance, so that method invocations can be chained.</returns>
        public IBlockAndColoursAdder AddBlockWithColours(Block block, params Colour[] colours);
    }

    /// <summary>
    ///     Fluent builder API for the <see cref="MapColouringProblem" /> record type.
    /// </summary>
    /// <remarks>
    ///     Any <see cref="MapColouringProblem" /> instance built using this API is guaranteed to represent a valid (but
    ///     not necessarily solvable) Map Colouring problem.
    /// </remarks>
    public interface ITerminal
    {
        /// <summary>
        ///     Creates and returns a new <see cref="MapColouringProblem" /> instance as specified by all the previous fluent
        ///     builder method invocations.
        /// </summary>
        /// <returns>A new <see cref="MapColouringProblem" /> instance.</returns>
        /// <exception cref="InvalidProblemException">
        ///     The <see cref="MapColouringProblem" /> as specified did not represent a valid Map Colouring problem.
        /// </exception>
        public MapColouringProblem Build();
    }
}
