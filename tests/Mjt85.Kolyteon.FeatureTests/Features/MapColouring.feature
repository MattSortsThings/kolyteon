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
