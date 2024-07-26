Feature: Problem Representation

Represent any valid instance of a given problem type as an immutable, serializable data structure.

    @B/1
    Scenario: Represent a Graph Colouring problem
        Given I have created a Graph Colouring problem with the following nodes and edges
          | Node | Permitted Colours  | Adjacent Nodes |
          | x1   | Red, Blue, Green   | x2, x3, x4, x7 |
          | x2   | Blue, Green        | x1, x6         |
          | x3   | Red, Blue          | x1, x7         |
          | x4   | Red, Blue          | x1, x5, x7     |
          | x5   | Blue, Green        | x4, x6, x7     |
          | x6   | Red, Green, Yellow | x2, x5         |
          | x7   | Red, Blue          | x1, x3, x4, x5 |
        And I have serialized the Graph Colouring problem to JSON
        When I deserialize a Graph Colouring problem from the JSON
        Then the deserialized and original Graph Colouring problems should be equal

    @C/1
    Scenario: Represent a Map Colouring problem
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
        And I have serialized the Map Colouring problem to JSON
        When I deserialize a Map Colouring problem from the JSON
        Then the deserialized and original Map Colouring problems should be equal

    @D/1
    Scenario: Represent an N-Queens problem
        Given I have created an N-Queens problem for N = 8
        And I have serialized the N-Queens problem to JSON
        When I deserialize an N-Queens problem from the JSON
        Then the deserialized and original N-Queens problems should be equal

    @E/1
    Scenario: Represent a Shikaku problem
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
        And I have serialized the Shikaku problem to JSON
        When I deserialize a Shikaku problem from the JSON
        Then the deserialized and original Shikaku problems should be equal

    @F/1
    Scenario: Represent a Sudoku problem
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
        And I have serialized the Sudoku problem to JSON
        When I deserialize a Sudoku problem from the JSON
        Then the deserialized and original Sudoku problems should be equal
