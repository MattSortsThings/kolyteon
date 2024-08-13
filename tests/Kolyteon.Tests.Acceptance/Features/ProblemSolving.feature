Feature: Problem Solving

Solve an instance of a problem type modelled as a binary CSP.

    @A/5
    Scenario Outline: Solve a Futoshiki problem
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
        And I have modelled the Futoshiki problem as a binary CSP
        When I solve the Futoshiki binary CSP using the '<Checking>'+'<Ordering>' search algorithm
        And I ask the Futoshiki problem to verify the correctness of the proposed solution
        Then the verification result should be successful

        Examples:
          | Checking | Ordering |
          | BT       | NO       |
          | BT       | BZ       |

    @B/5
    Scenario Outline: Solve a Graph Colouring problem
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
        And I have modelled the Graph Colouring problem as a binary CSP
        When I solve the Graph Colouring binary CSP using the '<Checking>'+'<Ordering>' search algorithm
        And I ask the Graph Colouring problem to verify the correctness of the proposed solution
        Then the verification result should be successful

        Examples:
          | Checking | Ordering |
          | BT       | NO       |
          | BT       | BZ       |

    @C/5
    Scenario Outline: Solve a Map Colouring problem
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
        And I have modelled the Map Colouring problem as a binary CSP
        When I solve the Map Colouring binary CSP using the '<Checking>'+'<Ordering>' search algorithm
        And I ask the Map Colouring problem to verify the correctness of the proposed solution
        Then the verification result should be successful

        Examples:
          | Checking | Ordering |
          | BT       | NO       |
          | BT       | BZ       |

    @D/4
    Scenario Outline: Solve an N-Queens problem
        Given I have created an N-Queens problem for N = 5
        And I have modelled the N-Queens problem as a binary CSP
        When I solve the N-Queens binary CSP using the '<Checking>'+'<Ordering>' search algorithm
        And I ask the N-Queens problem to verify the correctness of the proposed solution
        Then the verification result should be successful

        Examples:
          | Checking | Ordering |
          | BT       | NO       |
          | BT       | BZ       |

    @E/5
    Scenario Outline: Solve a Shikaku problem
        Given I have created a Shikaku problem from the following grid
        """
        05 __ __ __ __
        __ __ 08 __ __
        __ __ __ 04 __
        __ __ __ 06 02
        __ __ __ __ __
        """
        And I have modelled the Shikaku problem as a binary CSP
        When I solve the Shikaku binary CSP using the '<Checking>'+'<Ordering>' search algorithm
        And I ask the Shikaku problem to verify the correctness of the proposed solution
        Then the verification result should be successful

        Examples:
          | Checking | Ordering |
          | BT       | NO       |
          | BT       | BZ       |

    @F/5
    Scenario Outline: Solve a Sudoku problem
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
        And I have modelled the Sudoku problem as a binary CSP
        When I solve the Sudoku binary CSP using the '<Checking>'+'<Ordering>' search algorithm
        And I ask the Sudoku problem to verify the correctness of the proposed solution
        Then the verification result should be successful

        Examples:
          | Checking | Ordering |
          | BT       | NO       |
          | BT       | BZ       |
