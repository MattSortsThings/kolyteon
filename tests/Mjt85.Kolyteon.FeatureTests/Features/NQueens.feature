Feature: N-Queens
As a developer, I want to represent any N-Queens puzzle in code so that I can model and solve it as a binary CSP

    Scenario: Create a serializable puzzle
        Given I have created an N-Queens puzzle in which N = 8
        And I have serialized the N-Queens puzzle to JSON
        When I deserialize an N-Queens puzzle from the JSON
        Then the deserialized N-Queens puzzle should be the same as the original puzzle

    Scenario: Confirm a proposed solution is valid
        Given I have created an N-Queens puzzle in which N = 8
        And I have obtained the following list of queens as a proposed solution to the N-Queens puzzle
          | Column | Row |
          | 0      | 6   |
          | 1      | 4   |
          | 2      | 2   |
          | 3      | 0   |
          | 4      | 5   |
          | 5      | 7   |
          | 6      | 1   |
          | 7      | 3   |
        When I ask the N-Queens puzzle to validate the proposed solution
        Then the validation result should be successful
