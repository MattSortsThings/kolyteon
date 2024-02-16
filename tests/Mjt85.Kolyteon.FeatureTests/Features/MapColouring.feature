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
