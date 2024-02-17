Feature: Shikaku
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
