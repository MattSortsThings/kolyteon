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
