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
