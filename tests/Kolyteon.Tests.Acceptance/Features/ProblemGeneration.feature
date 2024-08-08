Feature: Problem Generation

Generate a random, solvable instance of a problem type from a specified size.

    @B/4
    Scenario: Generate a Graph Colouring problem
        Given I have set the Graph Colouring generator seed value to 1701
        When I ask the Graph Colouring generator for a problem with 20 nodes and the colours Black, Fuchsia, Aqua, Lime
        Then the Graph Colouring problem should have 20 nodes
        And the Graph Colouring problem should have at least 1 edge
        And every node in the Graph Colouring problem should have the colours Black, Fuchsia, Aqua, Lime

    @C/4
    Scenario: Generate a Map Colouring problem
        Given I have set the Map Colouring generator seed value to 1701
        When I ask the Map Colouring generator for a problem with 20 blocks and the colours Red, Green, Yellow, Blue
        Then the Map Colouring problem should have 20 blocks
        And every block in the Map Colouring problem should have the colours Red, Green, Yellow, Blue

    @E/4
    Scenario: Generate a Shikaku problem
        Given I have set the Shikaku generator seed value to 1701
        When I ask the Shikaku generator for a problem with a grid side length of 7 and 13 hints
        Then the Shikaku problem should have 13 hints
        And the Shikaku problem should have a 7x7 grid

    @F/4
    Scenario: Generate a Sudoku problem
        Given I have set the Sudoku generator seed value to 1701
        When I ask the Sudoku generator for a problem with 70 empty squares
        Then the Sudoku problem should have 11 filled squares
