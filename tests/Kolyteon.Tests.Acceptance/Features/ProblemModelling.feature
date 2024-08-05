Feature: Problem Modelling

Model any valid instance of a given problem type as a generic binary CSP.

    @A/3
    Scenario: Model a Futoshiki problem
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
        When I model the Futoshiki problem as a binary CSP
        Then the Futoshiki binary CSP should have 10 variables
        And the Futoshiki binary CSP should have 8 constraints
        And the Futoshiki binary CSP should have a constraint density of 0.177778
        And the Futoshiki binary CSP should have a harmonic mean tightness of 0.4

    @B/3
    Scenario: Model a Graph Colouring problem
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
        When I model the Graph Colouring problem as a binary CSP
        Then the Graph Colouring binary CSP should have 8 variables
        And the Graph Colouring binary CSP should have 10 constraints
        And the Graph Colouring binary CSP should have a constraint density of 0.357143
        And the Graph Colouring binary CSP should have a harmonic mean tightness of 0.277777

    @C/3
    Scenario: Model a Map Colouring problem
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
        When I model the Map Colouring problem as a binary CSP
        Then the Map Colouring binary CSP should have 9 variables
        And the Map Colouring binary CSP should have 10 constraints
        And the Map Colouring binary CSP should have a constraint density of 0.277778
        And the Map Colouring binary CSP should have a harmonic mean constraint tightness of 0.263158

    @D/3
    Scenario: Model an N-Queens problem
        Given I have created an N-Queens problem for N = 5
        When I model the N-Queens problem as a binary CSP
        Then the N-Queens binary CSP should have 5 variables
        And the N-Queens binary CSP should have 10 constraints
        And the N-Queens binary CSP should have a constraint density of 1.0
        And the N-Queens binary CSP should have a harmonic mean constraint tightness of 0.423057

    @E/3
    Scenario: Model a Shikaku problem
        Given I have created a Shikaku problem from the following grid
        """
        05 __ __ __ __
        __ __ 08 __ __
        __ __ __ 04 __
        __ __ __ 06 02
        __ __ __ __ __
        """
        When I model the Shikaku problem as a binary CSP
        Then the Shikaku binary CSP should have 5 variables
        And the Shikaku binary CSP should have 5 constraints
        And the Shikaku binary CSP should have a constraint density of 0.5
        And the Shikaku binary CSP should have a harmonic mean of 0.326797
