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

    Scenario: Model a puzzle as a binary CSP
        Given I have created an N-Queens puzzle in which N = 5
        And I have modelled the N-Queens puzzle as a binary CSP
        When I request the binary CSP metrics for the N-Queens puzzle
        Then the binary CSP problem metrics should be as follows
          | Field               | Value |
          | Variables           | 5     |
          | Constraints         | 10    |
          | ConstraintDensity   | 1     |
          | ConstraintTightness | 0.44  |
        And the binary CSP variable domain size statistics should be as follows
          | Field          | Value |
          | MinimumValue   | 5     |
          | MeanValue      | 5     |
          | MaximumValue   | 5     |
          | DistinctValues | 1     |
        And the binary CSP variable degree statistics should be as follows
          | Field          | Value |
          | MinimumValue   | 4     |
          | MeanValue      | 4     |
          | MaximumValue   | 4     |
          | DistinctValues | 1     |
        And the binary CSP variable sum tightness statistics should be as follows
          | Field          | Value |
          | MinimumValue   | 1.6   |
          | MeanValue      | 1.76  |
          | MaximumValue   | 1.92  |
          | DistinctValues | 3     |

    Scenario: Solve a binary CSP modelling a solvable puzzle
        Given I have created an N-Queens puzzle in which N = 8
        And I have modelled the N-Queens puzzle as a binary CSP
        And I have set the N-Queens binary CSP solver to use the '<Search>' search strategy
        And I have set the N-Queens binary CSP solver to use the '<Ordering>' ordering strategy
        When I run the N-Queens binary CSP solver on the binary CSP
        And I ask the N-Queens puzzle to validate the proposed solution
        Then the validation result should be successful

    Examples:
      | Search                      | Ordering       |
      | Backtracking                | None           |
      | Backtracking                | Brelaz         |
      | Backtracking                | MaxCardinality |
      | Backtracking                | MaxTightness   |
      | Backjumping                 | None           |
      | Backjumping                 | Brelaz         |
      | Backjumping                 | MaxCardinality |
      | Backjumping                 | MaxTightness   |
      | GraphBasedBackjumping       | None           |
      | GraphBasedBackjumping       | Brelaz         |
      | GraphBasedBackjumping       | MaxCardinality |
      | GraphBasedBackjumping       | MaxTightness   |
      | ConflictDirectedBackjumping | None           |
      | ConflictDirectedBackjumping | Brelaz         |
      | ConflictDirectedBackjumping | MaxCardinality |
      | ConflictDirectedBackjumping | MaxTightness   |
      | ForwardChecking             | None           |
      | ForwardChecking             | Brelaz         |
      | ForwardChecking             | MaxCardinality |
      | ForwardChecking             | MaxTightness   |
