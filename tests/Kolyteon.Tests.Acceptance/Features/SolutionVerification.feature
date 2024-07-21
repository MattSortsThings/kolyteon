Feature: Solution Verification

Verify the correctness of any proposed solution to any valid instance of a given problem type.

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
