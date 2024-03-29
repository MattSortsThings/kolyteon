﻿Feature: Shikaku
As a developer, I want to represent any Shikaku puzzle in code so that I can model and solve it as a binary CSP.

    Scenario: Create a serializable puzzle
        Given I have created a Shikaku puzzle from the following grid
        """
        05 __ __ __ __
        __ 05 __ __ __
        __ __ 09 __ __
        __ __ __ __ 03
        03 __ __ __ __
        """
        And I have serialized the Shikaku puzzle to JSON
        When I deserialize a Shikaku puzzle from the JSON
        Then the deserialized Shikaku puzzle should be the same as the original puzzle

    Scenario: Confirm a proposed solution is valid
        Given I have created a Shikaku puzzle from the following grid
        """
        05 __ __ __ __
        __ 05 __ __ __
        __ __ 09 __ __
        __ __ __ __ 03
        03 __ __ __ __
        """
        And I have obtained the following list of rectangles as a proposed solution to the Shikaku puzzle
          | OriginColumn | OriginRow | WidthInCells | HeightInCells |
          | 0            | 0         | 5            | 1             |
          | 0            | 1         | 5            | 1             |
          | 0            | 2         | 1            | 3             |
          | 1            | 2         | 3            | 3             |
          | 4            | 2         | 1            | 3             |
        When I ask the Shikaku puzzle to validate the proposed solution
        Then the validation result should be successful

    Scenario: Model a puzzle as a binary CSP
        Given I have created a Shikaku puzzle from the following grid
        """
        __ __ 09 __ __
        __ __ __ __ 10
        __ __ __ __ __
        __ 04 02 __ __
        __ __ __ __ __
        """
        And I have modelled the Shikaku puzzle as a binary CSP
        When I request the binary CSP metrics for the Shikaku puzzle
        Then the binary CSP problem metrics should be as follows
          | Field               | Value    |
          | Variables           | 4        |
          | Constraints         | 5        |
          | ConstraintDensity   | 0.833333 |
          | ConstraintTightness | 0.5      |
        And the binary CSP variable domain size statistics should be as follows
          | Field          | Value |
          | MinimumValue   | 2     |
          | MeanValue      | 2.75  |
          | MaximumValue   | 4     |
          | DistinctValues | 3     |
        And the binary CSP variable degree statistics should be as follows
          | Field          | Value |
          | MinimumValue   | 2     |
          | MeanValue      | 2.5   |
          | MaximumValue   | 3     |
          | DistinctValues | 2     |
        And the binary CSP variable sum tightness statistics should be as follows
          | Field          | Value    |
          | MinimumValue   | 0.666667 |
          | MeanValue      | 1.270833 |
          | MaximumValue   | 1.833333 |
          | DistinctValues | 4        |

    Scenario: Solve a binary CSP modelling a solvable puzzle
        Given I have created a Shikaku puzzle from the following grid
        """
        05 __ __ __ __
        __ 05 __ __ __
        __ __ 09 __ __
        __ __ __ __ 03
        03 __ __ __ __
        """
        And I have modelled the Shikaku puzzle as a binary CSP
        And I have set the Shikaku binary CSP solver to use the '<Search>' search strategy
        And I have set the Shikaku binary CSP solver to use the '<Ordering>' ordering strategy
        When I run the Shikaku binary CSP solver on the binary CSP
        And I ask the Shikaku puzzle to validate the proposed solution
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
      | PartialLookingAhead         | None           |
      | PartialLookingAhead         | Brelaz         |
      | PartialLookingAhead         | MaxCardinality |
      | PartialLookingAhead         | MaxTightness   |
      | FullLookingAhead            | None           |
      | FullLookingAhead            | Brelaz         |
      | FullLookingAhead            | MaxCardinality |
      | FullLookingAhead            | MaxTightness   |
      | MaintainingArcConsistency   | None           |
      | MaintainingArcConsistency   | Brelaz         |
      | MaintainingArcConsistency   | MaxCardinality |
      | MaintainingArcConsistency   | MaxTightness   |
