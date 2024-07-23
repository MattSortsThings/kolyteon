Feature: Problem Representation

Represent any valid instance of a given problem type as an immutable, serializable data structure.

    @C/1
    Scenario: Represent a Map Colouring problem
        Given I have created a Map Colouring problem with a 10x10 canvas and the following blocks
          | Block        | Permitted Colours     |
          | (0,6) [5x2]  | Red,Blue,Green        |
          | (0,8) [10x2] | Red                   |
          | (1,0) [3x1]  | Red,Blue,Green        |
          | (1,1) [3x3]  | Red,Yellow            |
          | (4,0) [4x2]  | Red,Blue,Green        |
          | (4,4) [6x2]  | Red,Blue,Green,Yellow |
          | (5,6) [5x2]  | Red,Blue,Green        |
          | (8,0) [2x1]  | Red,Green             |
          | (8,1) [1x1]  | Red                   |
          | (9,1) [1x1]  | Yellow                |
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
