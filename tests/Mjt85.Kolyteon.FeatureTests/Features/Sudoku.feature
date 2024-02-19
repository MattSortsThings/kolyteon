Feature: Sudoku
As a developer, I want to represent any Sudoku puzzle in code so that I can model and solve it as a binary CSP.

    Scenario: Create a serializable puzzle
        Given I have created a Sudoku puzzle from the following grid
        """
        _ 2 _ _ 7 _ 4 3 _
        _ 5 1 3 _ _ 2 _ 6
        _ 7 _ _ _ 6 _ _ 1
        6 _ _ _ _ _ 7 _ 9
        7 3 _ 5 _ _ _ 1 _
        _ _ 9 _ 6 4 _ _ 8
        5 6 8 _ 2 7 1 _ 3
        3 _ 2 _ 1 _ _ _ _
        _ _ 7 _ 8 3 5 2 _
        """
        And I have serialized the Sudoku puzzle to JSON
        When I deserialize a Sudoku puzzle from the JSON
        Then the deserialized Sudoku puzzle should be the same as the original puzzle
