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
          | (8,1) [1x1]  | Red                      |
          | (9,1) [1x1]  | Yellow                   |
        And I have proposed the following block and colour dictionary as a solution to the Map Colouring problem
          | Block        | Colour |
          | (0,6) [5x2]  | Blue   |
          | (0,8) [10x2] | Red    |
          | (1,0) [3x1]  | Red    |
          | (1,1) [3x3]  | Yellow |
          | (4,0) [4x2]  | Blue   |
          | (4,4) [6x2]  | Red    |
          | (5,6) [5x2]  | Green  |
          | (8,0) [2x1]  | Green  |
          | (8,1) [1x1]  | Red    |
          | (9,1) [1x1]  | Yellow |
        When I ask the Map Colouring problem to verify the correctness of the proposed solution
        Then the verification result should be successful

    @D/2
    Scenario: Verify an N-Queens problem solution
        Given I have created an N-Queens problem for N = 8
        And I have proposed the following squares as a solution to the N-Queens problem
          | Square |
          | (0,6)  |
          | (1,4)  |
          | (2,2)  |
          | (3,0)  |
          | (4,5)  |
          | (5,7)  |
          | (6,1)  |
          | (7,3)  |
        When I ask the N-Queens problem to verify the correctness of the proposed solution
        Then the verification result should be successful

    @E/2
    Scenario: Verify a Shikaku problem solution
        Given I have created a Shikaku problem from the following grid
        """
        03 __ __ __ __ __ __ 07 __ __
        __ __ __ __ __ __ __ __ __ __
        __ __ __ __ __ __ __ __ __ __
        __ __ __ 14 __ __ __ __ __ __
        __ __ __ __ __ __ __ __ __ __
        05 __ 10 __ __ __ 21 __ __ __
        03 __ __ __ __ __ __ __ __ __
        __ __ 03 __ __ __ __ __ __ __
        __ __ __ __ __ __ __ 16 __ __
        __ __ __ __ __ __ __ __ __ 18
        """
        And I have proposed the following blocks as a solution to the Shikaku problem
          | Block       |
          | (0,0) [3x1] |
          | (0,1) [1x5] |
          | (0,6) [3x1] |
          | (0,7) [3x1] |
          | (0,8) [8x2] |
          | (1,1) [2x5] |
          | (3,0) [7x1] |
          | (3,1) [2x7] |
          | (5,1) [3x7] |
          | (8,1) [2x9] |
        When I ask the Shikaku problem to verify the correctness of the proposed solution
        Then the verification result should be successful

    @F/2
    Scenario: Verify a Sudoku problem solution
        Given I have created a Sudoku problem from the following grid
        """
        02 __ 04 05 06 __ 09 01 03
        01 05 09 03 04 02 06 __ 07
        08 03 __ __ __ __ 02 05 04
        03 02 01 06 __ __ __ __ 09
        09 __ 05 __ __ __ 01 __ 02
        06 04 07 02 01 09 05 __ 08
        __ __ 08 09 __ 06 03 04 05
        __ 06 02 04 07 __ __ 09 01
        04 __ 03 08 __ 01 07 02 06
        """
        And I have proposed the following filled squares as a solution to the Sudoku problem
          | Filled Square |
          | (0,6) [7]     |
          | (0,7) [5]     |
          | (1,0) [7]     |
          | (1,4) [8]     |
          | (1,6) [1]     |
          | (1,8) [9]     |
          | (2,2) [6]     |
          | (3,2) [1]     |
          | (3,4) [7]     |
          | (4,2) [9]     |
          | (4,3) [8]     |
          | (4,4) [3]     |
          | (4,6) [2]     |
          | (4,8) [5]     |
          | (5,0) [8]     |
          | (5,2) [7]     |
          | (5,3) [5]     |
          | (5,4) [4]     |
          | (5,7) [3]     |
          | (6,3) [4]     |
          | (6,7) [8]     |
          | (7,1) [8]     |
          | (7,3) [7]     |
          | (7,4) [6]     |
          | (7,5) [3]     |
        When I ask the Sudoku problem to verify the correctness of the proposed solution
        Then the verification result should be successful
