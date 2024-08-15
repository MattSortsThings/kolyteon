Feature: Verbose Problem Solving

Solve an instance of a problem type modelled as a binary CSP, reporting progress at every step.

    @D/4
    @I/1
    Scenario Outline: Solve an N-Queens problem with reporting
        Given I have created an N-Queens problem for N = 4
        And I have modelled the N-Queens problem as a binary CSP
        When I solve the N-Queens binary CSP using the verbose solver configured with the 'BT'+'NO' search algorithm
        And I ask the N-Queens problem to verify the correctness of the proposed solution
        Then the verification result should be successful
        And the solving progress reporter should have 16 total steps
        And the solving progress reporter should have 1 simplifying step
        And the solving progress reporter should have 11 assigning steps
        And the solving progress reporter should have 4 backtracking steps
        And the solving progress reports should be as follows

          | Total Steps | Search Level | Solving State | Squares                    |
          | 1           | 0            | Assigning     |                            |
          | 2           | 1            | Assigning     | (0,0)                      |
          | 3           | 2            | Assigning     | (0,0), (1,2)               |
          | 4           | 2            | Backtracking  | (0,0), (1,2)               |
          | 5           | 1            | Assigning     | (0,0)                      |
          | 6           | 2            | Assigning     | (0,0), (1,3)               |
          | 7           | 3            | Assigning     | (0,0), (1,3), (2,1)        |
          | 8           | 3            | Backtracking  | (0,0), (1,3), (2,1)        |
          | 9           | 2            | Assigning     | (0,0), (1,3)               |
          | 10          | 2            | Backtracking  | (0,0), (1,3)               |
          | 11          | 1            | Backtracking  | (0,0)                      |
          | 12          | 0            | Assigning     |                            |
          | 13          | 1            | Assigning     | (0,1)                      |
          | 14          | 2            | Assigning     | (0,1), (1,3)               |
          | 15          | 3            | Assigning     | (0,1), (1,3), (2,0)        |
          | 16          | 4            | Finished      | (0,1), (1,3), (2,0), (3,2) |
