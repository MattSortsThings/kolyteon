using FluentAssertions.Execution;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.Sudoku;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Sudoku;

/// <summary>
///     Unit tests for the <see cref="SudokuBinaryCsp" /> class.
/// </summary>
public sealed class SudokuBinaryCspTests
{
    private static readonly EmptyCell Col0Row0 = new(0, 0);
    private static readonly EmptyCell Col0Row5 = new(0, 5);
    private static readonly EmptyCell Col0Row6 = new(0, 6);
    private static readonly EmptyCell Col1Row1 = new(1, 1);
    private static readonly EmptyCell Col1Row5 = new(1, 5);
    private static readonly EmptyCell Col2Row1 = new(2, 1);
    private static readonly EmptyCell Col7Row6 = new(7, 6);
    private static readonly EmptyCell Col8Row8 = new(8, 8);

    [UnitTest]
    public sealed class Model_Method
    {
        [Fact]
        public void Models_VariablesAreEmptyCellsOrderedByColumnThenByRow()
        {
            // Arrange
            SudokuBinaryCsp sut = SudokuBinaryCsp.WithInitialCapacity(8);

            SudokuPuzzle puzzle = SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, null, null, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { null, null, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { null, 0007, 0008, 0009, 0001, 0002, 0003, null, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, null }
            });

            // Act
            sut.Model(puzzle);

            // Assert
            sut.GetAllVariables().Should().Equal(Col0Row0, Col0Row5, Col0Row6, Col1Row1, Col1Row5, Col2Row1, Col7Row6, Col8Row8);
        }

        [Fact]
        public void Models_DomainsAreIntersectionOfFreeNumbersForColumnAndRowAndSector_InAscendingOrder()
        {
            // Arrange
            SudokuBinaryCsp sut = SudokuBinaryCsp.WithInitialCapacity(7);

            SudokuPuzzle puzzle = SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, null, null, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { null, null, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { null, 0007, 0008, 0009, 0001, 0002, 0003, null, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, null }
            });

            // Act
            sut.Model(puzzle);

            // Assert
            IEnumerable<IReadOnlyList<int>> expectedDomains =
            [
                [1],
                [5, 6],
                [6],
                [5, 6],
                [5, 6],
                [6],
                [4],
                [2]
            ];

            sut.GetAllDomains().Should().BeEquivalentTo(expectedDomains, options => options.WithStrictOrdering());
        }

        [Fact]
        public void Models_AddsConstraintForEveryPairOfObstructingEmptyCellsWithAtLeastOneSharedNumberInTheirDomains()
        {
            // Arrange
            SudokuBinaryCsp sut = SudokuBinaryCsp.WithInitialCapacity(7);

            SudokuPuzzle puzzle = SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, null, null, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { null, null, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { null, 0007, 0008, 0009, 0001, 0002, 0003, null, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, null }
            });

            // Act
            sut.Model(puzzle);

            // Assert
            sut.GetAllAdjacentVariables().Should().Equal(
                new Pair<EmptyCell>(Col0Row5, Col0Row6),
                new Pair<EmptyCell>(Col0Row5, Col1Row5),
                new Pair<EmptyCell>(Col1Row1, Col1Row5),
                new Pair<EmptyCell>(Col1Row1, Col2Row1)
            );
        }

        [Fact]
        public void Models_UpdatesAllProblemMetricsProperties()
        {
            // Arrange
            SudokuBinaryCsp sut = SudokuBinaryCsp.WithInitialCapacity(7);

            SudokuPuzzle puzzle = SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, null, null, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { null, null, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { null, 0007, 0008, 0009, 0001, 0002, 0003, null, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, null }
            });


            // Act
            sut.Model(puzzle);

            // Assert
            using (new AssertionScope())
            {
                sut.Variables.Should().Be(8);
                sut.Constraints.Should().Be(4);
                sut.ConstraintDensity.Should().BeApproximately(0.142857, Invariants.SixDecimalPlacesPrecision,
                    "4 constraints out of max possible 28");
                sut.ConstraintTightness.Should().BeApproximately(0.5, Invariants.SixDecimalPlacesPrecision);
            }
        }
    }
}
