Feature: Solution Verification

Verify the correctness of any proposed solution to any valid instance of a given problem type.

    @B/2
    Scenario: Verify a Graph Colouring problem solution
        Given I have created a Graph Colouring problem with the following nodes and edges
          | Node | Permitted Colours  | Adjacent Nodes |
          | x1   | Red, Blue, Green   | x2, x3, x4, x7 |
          | x2   | Blue, Green        | x1, x6         |
          | x3   | Red, Blue          | x1, x7         |
          | x4   | Red, Blue          | x1, x5, x7     |
          | x5   | Blue, Green        | x4, x6, x7     |
          | x6   | Red, Green, Yellow | x2, x5         |
          | x7   | Red, Blue          | x1, x3, x4, x5 |
        And I have proposed the following node and colour dictionary as a solution to the Graph Colouring problem
          | Node | Colour |
          | x1   | Green  |
          | x2   | Blue   |
          | x3   | Red    |
          | x4   | Red    |
          | x5   | Green  |
          | x6   | Yellow |
          | x7   | Blue   |
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
