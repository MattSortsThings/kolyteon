Feature: Map Colouring
As a developer, I want to represent any Map Colouring puzzle in code so that I can model and solve it as a binary CSP.

    Scenario: Create a serializable puzzle
        Given I have created a Map Colouring puzzle as follows
          | Field         | Value                     |
          | PresetMap     | Rwanda                    |
          | GlobalColours | Black,Cyan,Magenta,Yellow |
        And I have serialized the Map Colouring puzzle to JSON
        When I deserialize a Map Colouring puzzle from the JSON
        Then the deserialized Map Colouring puzzle should be the same as the original puzzle

    Scenario: Confirm a proposed solution is valid
        Given I have created a Map Colouring puzzle as follows
          | Field         | Value          |
          | PresetMap     | Australia      |
          | GlobalColours | Red,Blue,Green |
        And I have obtained the following region/colour dictionary as a proposed solution to the Map Colouring puzzle
          | Region | Colour |
          | WA     | Red    |
          | NT     | Green  |
          | SA     | Blue   |
          | Q      | Red    |
          | NSW    | Green  |
          | V      | Red    |
          | T      | Green  |
        When I ask the Map Colouring puzzle to validate the proposed solution
        Then the validation result should be successful

    Scenario: Model a puzzle as a binary CSP
        Given I have created a Map Colouring puzzle as follows
          | Field         | Value          |
          | PresetMap     | Australia      |
          | GlobalColours | Red,Blue,Green |
        And I have modelled the Map Colouring puzzle as a binary CSP
        When I request the binary CSP metrics for the Map Colouring puzzle
        Then the binary CSP problem metrics should be as follows
          | Field               | Value    |
          | Variables           | 7        |
          | Constraints         | 9        |
          | ConstraintDensity   | 0.428571 |
          | ConstraintTightness | 0.333333 |
        And the binary CSP variable domain size statistics should be as follows
          | Field          | Value |
          | MinimumValue   | 3     |
          | MeanValue      | 3     |
          | MaximumValue   | 3     |
          | DistinctValues | 1     |
        And the binary CSP variable degree statistics should be as follows
          | Field          | Value    |
          | MinimumValue   | 0        |
          | MeanValue      | 2.571429 |
          | MaximumValue   | 5        |
          | DistinctValues | 4        |
        And the binary CSP variable sum tightness statistics should be as follows
          | Field          | Value    |
          | MinimumValue   | 0        |
          | MeanValue      | 0.857143 |
          | MaximumValue   | 1.666667 |
          | DistinctValues | 4        |

    Scenario: Solve a binary CSP modelling a solvable puzzle
        Given I have created a Map Colouring puzzle as follows
          | Field         | Value          |
          | PresetMap     | Australia      |
          | GlobalColours | Red,Blue,Green |
        And I have modelled the Map Colouring puzzle as a binary CSP
        And I have set the Map Colouring binary CSP solver to use the '<Search>' search strategy
        And I have set the Map Colouring binary CSP solver to use the '<Ordering>' ordering strategy
        When I run the Map Colouring binary CSP solver on the binary CSP
        And I ask the Map Colouring puzzle to validate the proposed solution
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
