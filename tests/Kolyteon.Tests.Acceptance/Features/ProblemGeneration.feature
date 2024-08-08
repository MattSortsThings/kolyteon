Feature: Problem Generation

Generate a random, solvable instance of a problem type from a specified size.

    @F/4
    Scenario: Generate a Sudoku problem
        Given I have set the Sudoku generator seed value to 1701
        When I ask the Sudoku generator for a problem with 70 empty squares
        Then the Sudoku problem should have 11 filled squares
