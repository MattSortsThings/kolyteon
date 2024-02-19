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

    Scenario: Confirm a proposed solution is valid
        Given I have created a Sudoku puzzle from the following grid
        """
        _ 9 8 1 5 2 6 3 _
        6 _ 3 8 4 7 2 _ 9
        2 7 _ 3 6 9 _ 8 5
        1 3 4 _ 7 _ 8 9 2
        8 2 9 4 _ 1 7 5 6
        7 6 5 _ 2 _ 1 4 3
        5 8 _ 6 9 4 _ 2 1
        9 _ 2 7 1 3 5 _ 8
        _ 1 6 2 8 5 9 7 _
        """
        And I have obtained the following list of filled cells as a proposed solution to the Sudoku puzzle
          | Column | Row | Number |
          | 0      | 0   | 4      |
          | 0      | 8   | 3      |
          | 1      | 1   | 5      |
          | 1      | 7   | 4      |
          | 2      | 2   | 1      |
          | 2      | 6   | 7      |
          | 3      | 3   | 5      |
          | 3      | 5   | 9      |
          | 4      | 4   | 3      |
          | 5      | 3   | 6      |
          | 5      | 5   | 8      |
          | 6      | 2   | 4      |
          | 6      | 6   | 3      |
          | 7      | 1   | 1      |
          | 7      | 7   | 6      |
          | 8      | 0   | 7      |
          | 8      | 8   | 4      |
        When I ask the Sudoku puzzle to validate the proposed solution
        Then the validation result should be successful
