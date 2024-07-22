Feature: Problem Representation

Represent any valid instance of a given problem type as an immutable, serializable data structure.

    @D/1
    Scenario: Represent an N-Queens problem
        Given I have created an N-Queens problem for N = 8
        And I have serialized the N-Queens problem to JSON
        When I deserialize an N-Queens problem from the JSON
        Then the deserialized and original N-Queens problems should be equal

    @E/1
    Scenario: Represent a Shikaku problem
        Given I have created a Shikaku problem from the following grid
        """
        03 __ __ __ __ __ __ 07 __ __
        __ __ __ __ __ __ __ __ __ __
        __ __ __ __ __ __ __ __ __ __
        __ __ __ 14 __ __ __ __ __ __
        __ __ __ __ __ __ __ __ __ __
        05 __ 10 __ __ __ 21 __ __ __
        03 __ __ __ __ __ __ __ __ __
        __ __ 03 __ __ __ __ __ __ __
        __ __ __ __ __ __ __ 16 __ __
        __ __ __ __ __ __ __ __ __ 18
        """
        And I have serialized the Shikaku problem to JSON
        When I deserialize a Shikaku problem from the JSON
        Then the deserialized and original Shikaku problems should be equal
