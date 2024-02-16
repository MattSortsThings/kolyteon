Feature: N-Queens
As a developer, I want to represent any N-Queens puzzle in code so that I can model and solve it as a binary CSP

    Scenario: Create a serializable puzzle
        Given I have created an N-Queens puzzle in which N = 8
        And I have serialized the N-Queens puzzle to JSON
        When I deserialize an N-Queens puzzle from the JSON
        Then the deserialized N-Queens puzzle should be the same as the original puzzle
