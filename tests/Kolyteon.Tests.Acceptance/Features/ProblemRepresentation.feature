Feature: Problem Representation

Represent any valid instance of a given problem type as an immutable, serializable data structure.

    @D/1
    Scenario: Represent an N-Queens problem
        Given I have created an N-Queens problem for N = 8
        And I have serialized the N-Queens problem to JSON
        When I deserialize an N-Queens problem from the JSON
        Then the deserialized and original N-Queens problem should be equal
