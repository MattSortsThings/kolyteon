﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Mjt85.Kolyteon.Sudoku;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Sudoku;

/// <summary>
///     Unit tests for the <see cref="SudokuPuzzle" /> record type.
/// </summary>
public sealed class SudokuPuzzleTests
{
    [UnitTest]
    public sealed class Equals_Method
    {
        [Fact]
        public void InstanceGivenItselfAsOther_ReturnsTrue()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, null, null },
                { null, null, 0003, null, null, null, null, null, null },
                { null, null, null, 0004, null, null, null, null, null },
                { null, null, null, null, 0005, null, null, null, null },
                { null, null, null, null, null, 0006, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            });

            // Act
            var result = sut.Equals(sut);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveSameHints_ReturnsTrue()
        {
            // Arrange
            var sharedGrid = new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, null, null },
                { null, null, 0003, null, null, null, null, null, null },
                { null, null, null, 0004, null, null, null, null, null },
                { null, null, null, null, 0005, null, null, null, null },
                { null, null, null, null, null, 0006, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            };

            SudokuPuzzle sut = SudokuPuzzle.FromGrid(sharedGrid);
            SudokuPuzzle other = SudokuPuzzle.FromGrid(sharedGrid);

            // Act
            var result = sut.Equals(other);

            // Act
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveDifferentHints_ReturnsFalse()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, null, null },
                { null, null, 0003, null, null, null, null, null, null },
                { null, null, null, 0004, null, null, null, null, null },
                { null, null, null, null, 0005, null, null, null, null },
                { null, null, null, null, null, 0006, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            });

            SudokuPuzzle other = SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0009, null, null, null, null, null, null, null, null },
                { null, 0008, null, null, null, null, null, null, null },
                { null, null, 0007, null, null, null, null, null, null },
                { null, null, null, 0009, null, null, null, null, null },
                { null, null, null, null, 0008, null, null, null, null },
                { null, null, null, null, null, 0007, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            });

            // Act
            var result = sut.Equals(other);

            // Act
            result.Should().BeFalse();
        }

        [Fact]
        public void OtherArgIsNull_ReturnsFalse()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, null, null },
                { null, null, 0003, null, null, null, null, null, null },
                { null, null, null, 0004, null, null, null, null, null },
                { null, null, null, null, 0005, null, null, null, null },
                { null, null, null, null, null, 0006, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            });

            // Act
            var result = sut.Equals(null);

            // Assert
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class ValidSolution_Method
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ValidSolution_ReturnsSuccess(int?[,] grid, IReadOnlyList<FilledCell> solution)
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(grid);

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().Be(ValidationResult.Success);
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionHasTooFewItems_ReturnsFailure()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, null, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
            });

            IReadOnlyList<FilledCell> solution = [new FilledCell(0, 0, 1)];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Solution size is 1, should be 2.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionHasTooManyItems_ReturnsFailure()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
            });

            IReadOnlyList<FilledCell> solution =
            [
                new FilledCell(0, 0, 1),
                new FilledCell(1, 1, 5),
                new FilledCell(2, 2, 9)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Solution size is 3, should be 1.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionContainsFilledCellsInSameCell_ReturnsFailure()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, null, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
            });

            IReadOnlyList<FilledCell> solution =
            [
                new FilledCell(1, 1, 1),
                new FilledCell(1, 1, 2)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should().Be("Solution filled cells (1,1) [1] and (1,1) [2] are in same cell.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionContainsObstructingFilledCells_ReturnsFailure()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, null, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
            });

            IReadOnlyList<FilledCell> solution =
            [
                new FilledCell(0, 0, 1),
                new FilledCell(1, 1, 1)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should()
                .Be("Solution filled cells (0,0) [1] and (1,1) [1] obstruct each other.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionContainsFilledCellInSameCellAsPuzzleFilledCell_ReturnsFailure()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, null, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
            });

            IReadOnlyList<FilledCell> solution =
            [
                new FilledCell(0, 0, 1),
                new FilledCell(8, 8, 2)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should()
                .Be("Solution filled cell (8,8) [2] and puzzle filled cell (8,8) [2] are in same cell.");
        }

        [Fact]
        [Category("ClearBoxTest")]
        public void SolutionContainsFilledCellThatObstructsPuzzleFilledCell_ReturnsFailure()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, null, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
            });

            IReadOnlyList<FilledCell> solution =
            [
                new FilledCell(0, 0, 2),
                new FilledCell(1, 1, 1)
            ];

            // Act
            ValidationResult? result = sut.ValidSolution(solution);

            // Assert
            result.Should().BeOfType<ValidationResult>()
                .Which.ErrorMessage.Should()
                .Be("Solution filled cell (0,0) [2] obstructs puzzle filled cell (0,4) [2].");
        }

        [Fact]
        public void SolutionArgIsNull_Throws()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
            });

            // Act
            Action act = () => sut.ValidSolution(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'solution')");
        }

        private sealed class TestCases : TheoryData<int?[,], IReadOnlyList<FilledCell>>
        {
            public TestCases()
            {
                // Add(new int?[,]
                // {
                //     {0001,0002,0003,0004,0005,0006,0007,0008,0009},
                //     {0004,0005,0006,0007,0008,0009,0001,0002,0003},
                //     {0007,0008,0009,0001,0002,0003,0004,0005,0006},
                //     {0008,0009,0001,0002,0003,0004,0005,0006,0007},
                //     {0002,0003,0004,0005,0006,0007,0008,0009,0001},
                //     {0005,0006,0007,0008,0009,0001,0002,0003,0004},
                //     {0006,0007,0008,0009,0001,0002,0003,0004,0005},
                //     {0009,0001,0002,0003,0004,0005,0006,0007,0008},
                //     {0003,0004,0005,0006,0007,0008,0009,0001,0002}
                // }, []);

                Add(new int?[,]
                {
                    { null, 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                    { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                    { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                    { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                    { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                    { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                    { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                    { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                    { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
                }, [
                    new FilledCell(0, 0, 1)
                ]);

                Add(new int?[,]
                {
                    { null, null, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                    { 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                    { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                    { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                    { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                    { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                    { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                    { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                    { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
                }, [
                    new FilledCell(0, 0, 1),
                    new FilledCell(1, 0, 2)
                ]);

                Add(new int?[,]
                {
                    { null, null, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                    { null, null, 0006, 0007, 0008, 0009, 0001, 0002, 0003 },
                    { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                    { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                    { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                    { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                    { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                    { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                    { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
                }, [
                    new FilledCell(0, 0, 1),
                    new FilledCell(0, 1, 4),
                    new FilledCell(1, 0, 2),
                    new FilledCell(1, 1, 5)
                ]);

                Add(new int?[,]
                {
                    { null, null, 0003, 0004, 0005, 0006, 0007, 0008, 0009 },
                    { null, null, 0006, 0007, 0008, 0009, 0001, 0002, null },
                    { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006 },
                    { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                    { 0002, 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001 },
                    { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                    { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                    { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                    { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
                }, [
                    new FilledCell(0, 0, 1),
                    new FilledCell(0, 1, 4),
                    new FilledCell(1, 0, 2),
                    new FilledCell(1, 1, 5),
                    new FilledCell(8, 1, 3)
                ]);

                Add(new int?[,]
                {
                    { null, null, 0003, 0004, 0005, 0006, 0007, 0008, null },
                    { null, null, 0006, 0007, 0008, 0009, 0001, 0002, null },
                    { 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005, null },
                    { 0008, 0009, 0001, 0002, 0003, 0004, 0005, 0006, null },
                    { 0002, 0003, 0004, 0005, null, 0007, 0008, 0009, 0001 },
                    { 0005, 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004 },
                    { 0006, 0007, 0008, 0009, 0001, 0002, 0003, 0004, 0005 },
                    { 0009, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                    { 0003, 0004, 0005, 0006, 0007, 0008, 0009, 0001, 0002 }
                }, [
                    new FilledCell(0, 0, 1),
                    new FilledCell(0, 1, 4),
                    new FilledCell(1, 0, 2),
                    new FilledCell(1, 1, 5),
                    new FilledCell(4, 4, 6),
                    new FilledCell(8, 0, 9),
                    new FilledCell(8, 1, 3),
                    new FilledCell(8, 2, 6),
                    new FilledCell(8, 3, 7)
                ]);

                Add(new int?[,]
                {
                    { 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008, null },
                    { 0004, 0005, 0006, 0007, 0008, null, 0001, 0002, 0003 },
                    { 0007, 0008, null, 0001, 0002, 0003, 0004, 0005, 0006 },
                    { 0008, null, 0001, 0002, 0003, 0004, 0005, 0006, 0007 },
                    { 0002, 0003, 0004, 0005, 0006, 0007, 0008, null, 0001 },
                    { 0005, 0006, 0007, 0008, null, 0001, 0002, 0003, 0004 },
                    { 0006, 0007, 0008, null, 0001, 0002, 0003, 0004, 0005 },
                    { null, 0001, 0002, 0003, 0004, 0005, 0006, 0007, 0008 },
                    { 0003, 0004, 0005, 0006, 0007, 0008, null, 0001, 0002 }
                }, [
                    new FilledCell(0, 7, 9),
                    new FilledCell(1, 3, 9),
                    new FilledCell(2, 2, 9),
                    new FilledCell(3, 6, 9),
                    new FilledCell(4, 5, 9),
                    new FilledCell(5, 1, 9),
                    new FilledCell(6, 8, 9),
                    new FilledCell(7, 4, 9),
                    new FilledCell(8, 0, 9)
                ]);
            }
        }
    }

    [UnitTest]
    public sealed class FromGrid_StaticMethod
    {
        [Theory]
        [ClassData(typeof(TestCases))]
        public void ReturnsInstanceWithExpectedFilledCells(int?[,] grid, IReadOnlyList<FilledCell> expectedFilledCells)
        {
            // Act
            SudokuPuzzle result = SudokuPuzzle.FromGrid(grid);

            // Assert
            result.Should().BeOfType<SudokuPuzzle>()
                .Which.FilledCells.Should().BeEquivalentTo(expectedFilledCells, options => options.WithoutStrictOrdering());
        }

        [Fact]
        public void GridArgIsNull_Throws()
        {
            // Act
            Action act = () => SudokuPuzzle.FromGrid(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'grid')");
        }

        [Fact]
        public void GridIsSmallerThanNineByNine_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null },
                { null, null, null },
                { null, null, null }
            };

            // Act
            Action act = () => SudokuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Grid must be a 9x9 square.");
        }

        [Fact]
        public void GridIsLargerThanNineByNine_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Grid must be a 9x9 square.");
        }

        [Fact]
        public void GridIsNotSquareThrows()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Grid must be a 9x9 square.");
        }

        [Fact]
        public void GridContainsNonNullValueLessThanOne_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, 0001, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, 0000, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Grid has illegal number at index [6,7].");
        }

        [Fact]
        public void GridContainsNonNullValueGreaterThanEight_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, 0010, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, 0001, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Grid has illegal number at index [2,1].");
        }

        [Fact]
        public void PuzzleHasNoEmptyCells_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                { 4, 5, 6, 7, 8, 9, 1, 2, 3 },
                { 7, 8, 9, 1, 2, 3, 4, 5, 6 },
                { 8, 9, 1, 2, 3, 4, 5, 6, 7 },
                { 2, 3, 4, 5, 6, 7, 8, 9, 1 },
                { 5, 6, 7, 8, 9, 1, 2, 3, 4 },
                { 6, 7, 8, 9, 1, 2, 3, 4, 5 },
                { 9, 1, 2, 3, 4, 5, 6, 7, 8 },
                { 3, 4, 5, 6, 7, 8, 9, 1, 2 }
            };

            // Act
            Action act = () => SudokuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("No empty cells.");
        }

        [Fact]
        public void PuzzleHasDuplicateNumberInSameColumn_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, 0005, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, 0005, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Filled cells (1,2) [5] and (1,8) [5] obstruct each other.");
        }

        [Fact]
        public void PuzzleHasDuplicateNumberInSameRow_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, 0001, null, null, null, null, null, 0001, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Filled cells (1,2) [1] and (7,2) [1] obstruct each other.");
        }

        [Fact]
        public void PuzzleHasDuplicateNumberInSameSector_Throws()
        {
            // Arrange
            int?[,] grid =
            {
                { null, null, null, null, null, null, null, null, null },
                { 0009, null, null, null, null, null, null, null, null },
                { null, 0009, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null },
                { null, null, null, null, null, null, null, null, null }
            };

            // Act
            Action act = () => SudokuPuzzle.FromGrid(grid);

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Filled cells (0,1) [9] and (1,2) [9] obstruct each other.");
        }

        private sealed class TestCases : TheoryData<int?[,], IReadOnlyList<FilledCell>>
        {
            public TestCases()
            {
                Add(new int?[,]
                {
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null }
                }, Array.Empty<FilledCell>());

                Add(new int?[,]
                {
                    { 0001, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, 0009 }
                }, [
                    new FilledCell(0, 0, 1),
                    new FilledCell(8, 8, 9)
                ]);

                Add(new int?[,]
                {
                    { null, null, null, null, null, null, null, null, null },
                    { null, 0004, null, null, null, null, null, 0003, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, 0009, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null }
                }, [
                    new FilledCell(1, 1, 4),
                    new FilledCell(2, 6, 9),
                    new FilledCell(7, 1, 3)
                ]);

                Add(new int?[,]
                {
                    { null, null, null, null, 0005, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { 0002, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, null, null, null, null, null, null },
                    { null, null, null, 0007, 0009, 0002, null, null, null }
                }, [
                    new FilledCell(0, 2, 2),
                    new FilledCell(3, 8, 7),
                    new FilledCell(4, 0, 5),
                    new FilledCell(4, 8, 9),
                    new FilledCell(5, 8, 2)
                ]);
            }
        }
    }

    [UnitTest]
    public sealed class Equality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveSameHints_ReturnsTrue()
        {
            // Arrange
            var sharedGrid = new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, null, null },
                { null, null, 0003, null, null, null, null, null, null },
                { null, null, null, 0004, null, null, null, null, null },
                { null, null, null, null, 0005, null, null, null, null },
                { null, null, null, null, null, 0006, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            };

            SudokuPuzzle sut = SudokuPuzzle.FromGrid(sharedGrid);
            SudokuPuzzle other = SudokuPuzzle.FromGrid(sharedGrid);

            // Act
            var result = sut == other;

            // Act
            result.Should().BeTrue();
        }

        [Fact]
        public void InstanceAndOtherHaveDifferentHints_ReturnsFalse()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, null, null },
                { null, null, 0003, null, null, null, null, null, null },
                { null, null, null, 0004, null, null, null, null, null },
                { null, null, null, null, 0005, null, null, null, null },
                { null, null, null, null, null, 0006, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            });

            SudokuPuzzle other = SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0009, null, null, null, null, null, null, null, null },
                { null, 0008, null, null, null, null, null, null, null },
                { null, null, 0007, null, null, null, null, null, null },
                { null, null, null, 0009, null, null, null, null, null },
                { null, null, null, null, 0008, null, null, null, null },
                { null, null, null, null, null, 0007, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            });

            // Act
            var result = sut == other;

            // Act
            result.Should().BeFalse();
        }
    }

    [UnitTest]
    public sealed class Inequality_Operator
    {
        [Fact]
        public void InstanceAndOtherHaveSameHints_ReturnsFalse()
        {
            // Arrange
            var sharedGrid = new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, null, null },
                { null, null, 0003, null, null, null, null, null, null },
                { null, null, null, 0004, null, null, null, null, null },
                { null, null, null, null, 0005, null, null, null, null },
                { null, null, null, null, null, 0006, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            };

            SudokuPuzzle sut = SudokuPuzzle.FromGrid(sharedGrid);
            SudokuPuzzle other = SudokuPuzzle.FromGrid(sharedGrid);

            // Act
            var result = sut != other;

            // Act
            result.Should().BeFalse();
        }

        [Fact]
        public void InstanceAndOtherHaveDifferentHints_ReturnsTrue()
        {
            // Arrange
            SudokuPuzzle sut = SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, null, null },
                { null, null, 0003, null, null, null, null, null, null },
                { null, null, null, 0004, null, null, null, null, null },
                { null, null, null, null, 0005, null, null, null, null },
                { null, null, null, null, null, 0006, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            });

            SudokuPuzzle other = SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0009, null, null, null, null, null, null, null, null },
                { null, 0008, null, null, null, null, null, null, null },
                { null, null, 0007, null, null, null, null, null, null },
                { null, null, null, 0009, null, null, null, null, null },
                { null, null, null, null, 0008, null, null, null, null },
                { null, null, null, null, null, 0007, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            });

            // Act
            var result = sut != other;

            // Act
            result.Should().BeTrue();
        }
    }

    [UnitTest]
    public sealed class Serialization
    {
        [Fact]
        public void CanSerializeToJson_ThenDeserialize()
        {
            // Arrange
            SudokuPuzzle original = SudokuPuzzle.FromGrid(new int?[,]
            {
                { 0001, null, null, null, null, null, null, null, null },
                { null, 0002, null, null, null, null, null, null, null },
                { null, null, 0003, null, null, null, null, null, null },
                { null, null, null, 0004, null, null, null, null, null },
                { null, null, null, null, 0005, null, null, null, null },
                { null, null, null, null, null, 0006, null, null, null },
                { null, null, null, null, null, null, 0007, null, null },
                { null, null, null, null, null, null, null, 0008, null },
                { null, null, null, null, null, null, null, null, 0009 }
            });

            JsonSerializerOptions jsonOptions = Invariants.GetJsonSerializerOptions();

            // Act
            var json = JsonSerializer.Serialize(original, jsonOptions);
            var deserialized = JsonSerializer.Deserialize<SudokuPuzzle>(json, jsonOptions);

            // Assert
            deserialized.Should().NotBeNull().And.Be(original);
        }
    }
}
