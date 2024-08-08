Feature: Problem Generation

Generate a random, solvable instance of a problem type from a specified size.

    @C/4
    Scenario: Generate a Map Colouring problem
        Given I have set the Map Colouring generator seed value to 1701
        When I ask the Map Colouring generator for a problem with 20 blocks and the colours Red, Green, Yellow, Blue
        Then the Map Colouring problem should have 20 blocks
        And every block in the Map Colouring problem should have the colours Red, Green, Yellow, Blue

    @F/4
    Scenario: Generate a Sudoku problem
        Given I have set the Sudoku generator seed value to 1701
        When I ask the Sudoku generator for a problem with 70 empty squares
        Then the Sudoku problem should have 11 filled squares
