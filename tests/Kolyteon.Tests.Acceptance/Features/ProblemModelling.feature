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
