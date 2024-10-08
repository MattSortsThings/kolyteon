Feature: Solution Verification

Verify the correctness of any proposed solution to any valid instance of a given problem type.

    @A/2
    Scenario: Verify a Futoshiki problem solution
        Given I have created a Futoshiki problem matching the following diagram
        """
        +---+---+---+---+
        |   | 2 <   |   |
        +-<-+---+---+->-+
        |   <   |   | 1 |
        +---+---+---+---+
        | 3 |   > 1 |   |
        +---+->-+---+---+
        | 4 >   |   | 3 |
        +---+---+---+---+
        """
        And I have proposed the following filled squares as a solution to the Futoshiki problem
          | Filled Square |
          | (0,0) [1]     |
          | (0,1) [2]     |
          | (1,1) [3]     |
          | (1,2) [4]     |
          | (1,3) [1]     |
          | (2,0) [3]     |
          | (2,1) [4]     |
          | (2,3) [2]     |
          | (3,0) [4]     |
          | (3,2) [2]     |
        When I ask the Futoshiki problem to verify the correctness of the proposed solution
        Then the verification result should be successful

    @B/2
    Scenario: Verify a Graph Colouring problem solution
        Given I have created a Graph Colouring problem with the following nodes and edges
          | Node | Permitted Colours  | Adjacent Nodes |
          | A    | Red, Blue, Green   | B, C, D, G     |
          | B    | Blue, Green        | A, F           |
          | C    | Red, Blue          | A, G           |
          | D    | Red, Blue          | A, E, G, H     |
          | E    | Blue, Green        | D, F, G        |
          | F    | Red, Green, Yellow | B, E           |
          | G    | Red, Blue          | A, C, D, E     |
          | H    | Yellow             | D              |
        And I have proposed the following node and colour dictionary as a solution to the Graph Colouring problem
          | Node | Colour |
          | A    | Green  |
          | B    | Blue   |
          | C    | Red    |
          | D    | Red    |
          | E    | Green  |
          | F    | Yellow |
          | G    | Blue   |
          | H    | Yellow |
        When I ask the Graph Colouring problem to verify the correctness of the proposed solution
        Then the verification result should be successful

    @C/2
    Scenario: Verify a Map Colouring problem solution
        Given I have created a Map Colouring problem with a 10x10 canvas and the following blocks
          | Block        | Permitted Colours        |
          | (0,6) [5x2]  | Red, Blue, Green         |
          | (0,8) [10x2] | Red                      |
          | (1,0) [3x1]  | Red, Blue, Green         |
          | (1,1) [3x3]  | Red, Yellow              |
          | (4,0) [4x2]  | Red, Blue, Green         |
          | (4,4) [6x2]  | Red, Blue, Green, Yellow |
          | (5,6) [5x2]  | Red, Blue, Green         |
          | (8,0) [2x1]  | Red, Green               |
          | (8,1) [2x1]  | Blue                     |
        And I have proposed the following block and colour dictionary as a solution to the Map Colouring problem
          | Block        | Colour |
          | (0,6) [5x2]  | Green  |
          | (0,8) [10x2] | Red    |
          | (1,0) [3x1]  | Blue   |
          | (1,1) [3x3]  | Red    |
          | (4,0) [4x2]  | Green  |
          | (4,4) [6x2]  | Yellow |
          | (5,6) [5x2]  | Blue   |
          | (8,0) [2x1]  | Red    |
          | (8,1) [2x1]  | Blue   |
        When I ask the Map Colouring problem to verify the correctness of the proposed solution
        Then the verification result should be successful

    @D/2
    Scenario: Verify an N-Queens problem solution
        Given I have created an N-Queens problem for N = 5
        And I have proposed the following squares as a solution to the N-Queens problem
          | Square |
          | (0,4)  |
          | (1,1)  |
          | (2,3)  |
          | (3,0)  |
          | (4,2)  |
        When I ask the N-Queens problem to verify the correctness of the proposed solution
        Then the verification result should be successful

    @E/2
    Scenario: Verify a Shikaku problem solution
        Given I have created a Shikaku problem from the following grid
        """
        05 __ __ __ __
        __ __ 08 __ __
        __ __ __ 04 __
        __ __ __ 06 02
        __ __ __ __ __
        """
        And I have proposed the following blocks as a solution to the Shikaku problem
          | Block       |
          | (0,0) [1x5] |
          | (1,0) [4x2] |
          | (1,2) [4x1] |
          | (1,3) [3x2] |
          | (4,3) [1x2] |
        When I ask the Shikaku problem to verify the correctness of the proposed solution
        Then the verification result should be successful

    @F/2
    Scenario: Verify a Sudoku problem solution
        Given I have created a Sudoku problem from the following grid
        """
        __ 02 __ __ __ 06 07 08 __
        __ 05 06 07 08 09 01 02 03
        __ 08 09 01 02 03 04 05 06
        08 09 01 02 03 04 05 06 07
        02 03 04 05 __ 07 08 09 01
        05 06 07 08 09 01 02 03 04
        06 07 08 09 01 02 03 04 05
        09 01 02 03 04 05 06 07 08
        __ 04 05 06 __ 08 09 01 __
        """
        And I have proposed the following filled squares as a solution to the Sudoku problem
          | Filled Square |
          | (0,0) [1]     |
          | (0,1) [4]     |
          | (0,2) [7]     |
          | (0,8) [3]     |
          | (2,0) [3]     |
          | (3,0) [4]     |
          | (4,0) [5]     |
          | (4,4) [6]     |
          | (4,8) [7]     |
          | (8,0) [9]     |
          | (8,8) [2]     |
        When I ask the Sudoku problem to verify the correctness of the proposed solution
        Then the verification result should be successful
