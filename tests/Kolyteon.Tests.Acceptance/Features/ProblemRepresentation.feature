Feature: Problem Representation

Represent any valid instance of a given problem type as an immutable, serializable data structure.

    @A/1
    Scenario: Represent a Futoshiki problem
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
        And I have serialized the Futoshiki problem to JSON
        When I deserialize a Futoshiki problem from the JSON
        Then the deserialized and original Futoshiki problems should be equal

    @B/1
    Scenario: Represent a Graph Colouring problem
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
          | (8,1) [2x1]  | Blue                     |
        And I have serialized the Map Colouring problem to JSON
        When I deserialize a Map Colouring problem from the JSON
        Then the deserialized and original Map Colouring problems should be equal

    @D/1
    Scenario: Represent an N-Queens problem
        Given I have created an N-Queens problem for N = 5
        And I have serialized the N-Queens problem to JSON
        When I deserialize an N-Queens problem from the JSON
        Then the deserialized and original N-Queens problems should be equal

    @E/1
    Scenario: Represent a Shikaku problem
        Given I have created a Shikaku problem from the following grid
        """
        05 __ __ __ __
        __ __ 08 __ __
        __ __ __ 04 __
        __ __ __ 06 02
        __ __ __ __ __
        """
        And I have serialized the Shikaku problem to JSON
        When I deserialize a Shikaku problem from the JSON
        Then the deserialized and original Shikaku problems should be equal

    @F/1
    Scenario: Represent a Sudoku problem
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
        And I have serialized the Sudoku problem to JSON
        When I deserialize a Sudoku problem from the JSON
        Then the deserialized and original Sudoku problems should be equal
