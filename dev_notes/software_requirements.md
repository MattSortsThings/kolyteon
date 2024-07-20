# Software Requirements

This document outlines the software requirements for the *Kolyteon* library (hereafter 'the library').

- [Software Requirements](#software-requirements)
  - [Functional Requirements](#functional-requirements)
    - [A - Futoshiki](#a---futoshiki)
      - [A/1 - Problem Representation](#a1---problem-representation)
      - [A/2 - Solution Verification](#a2---solution-verification)
      - [A/3 - Binary CSP Modelling](#a3---binary-csp-modelling)
      - [A/4 - Problem Generation](#a4---problem-generation)
    - [B - Graph Colouring](#b---graph-colouring)
      - [B/1 - Problem Representation](#b1---problem-representation)
      - [B/2 - Solution Verification](#b2---solution-verification)
      - [B/3 - Binary CSP modelling](#b3---binary-csp-modelling)
      - [B/4 - Problem generation](#b4---problem-generation)
    - [C - Map Colouring](#c---map-colouring)
      - [C/1 - Problem Representation](#c1---problem-representation)
      - [C/2 - Solution Verification](#c2---solution-verification)
      - [C/3 - Binary CSP modelling](#c3---binary-csp-modelling)
      - [C/4 - Problem generation](#c4---problem-generation)
    - [D - *N*-Queens](#d---n-queens)
      - [D/1 - Problem Representation](#d1---problem-representation)
      - [D/2 - Solution Verification](#d2---solution-verification)
      - [D/3 - Binary CSP modelling](#d3---binary-csp-modelling)
    - [E - Shikaku](#e---shikaku)
      - [E/1 - Problem Representation](#e1---problem-representation)
      - [E/2 - Solution Verification](#e2---solution-verification)
      - [E/3 - Binary CSP Modelling](#e3---binary-csp-modelling)
      - [E/4 - Problem Generation](#e4---problem-generation)
    - [F - Sudoku](#f---sudoku)
      - [F/1 - Problem Representation](#f1---problem-representation)
      - [F/2 - Solution Verification](#f2---solution-verification)
      - [F/3 - Binary CSP Modelling](#f3---binary-csp-modelling)
      - [F/4 - Problem Generation](#f4---problem-generation)
    - [G - Binary CSP Modelling](#g---binary-csp-modelling)
      - [G/1 - Measurable Binary CSP API](#g1---measurable-binary-csp-api)
      - [G/2 - Solvable Binary CSP API](#g2---solvable-binary-csp-api)
      - [G/3 - Modelling Binary CSP API](#g3---modelling-binary-csp-api)
    - [H - Binary CSP Solving](#h---binary-csp-solving)
      - [H/1 - Solving](#h1---solving)
      - [H/2 - Solver Configuration](#h2---solver-configuration)
    - [I - Observable Binary CSP Solving](#i---observable-binary-csp-solving)
      - [I/1 - Solving](#i1---solving)
      - [I/2 - Solver Configuration](#i2---solver-configuration)
      - [I/3 - Observability](#i3---observability)
  - [Non-Functional Requirements](#non-functional-requirements)

## Functional Requirements

### A - Futoshiki

#### A/1 - Problem Representation

| Code  | Summary                                                                                                 |
|:------|:--------------------------------------------------------------------------------------------------------|
| A/1/1 | The library will allow the user to represent any valid Futoshiki problem and proposed solution in code. |
| A/1/2 | It will be impossible to instantiate an invalid problem or any invalid problem element.                 |
| A/1/3 | The problem, solution, and all problem elements will be JSON-serializable.                              |

#### A/2 - Solution Verification

| Code  | Summary                                                                            |
|:------|:-----------------------------------------------------------------------------------|
| A/2/1 | A Futoshiki problem will confirm that a correct proposed solution is correct.      |
| A/2/2 | A Futoshiki problem will confirm that an incorrect proposed solution is incorrect. |

#### A/3 - Binary CSP Modelling

| Code  | Summary                                                                                                                                   |
|:------|:------------------------------------------------------------------------------------------------------------------------------------------|
| A/3/1 | The library will allow the user to model any valid Futoshiki problem as a generic binary CSP.                                             |
| A/3/2 | The user will be able to set the capacity of the binary CSP object at initialization and at runtime by passing in a single integer value. |

#### A/4 - Problem Generation

| Code  | Summary                                                                                                                                                                                         |
|:------|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| A/4/1 | The library will generate a random, valid, solvable Futoshiki problem when the user specifies the value of *N* (from the range [4, 9]) and the number of filled cells (from the range [1, 10]). |
| A/4/2 | The user will be able to set the random number generation seed for the problem generation algorithm.                                                                                            |

### B - Graph Colouring

#### B/1 - Problem Representation

| Code  | Summary                                                                                                       |
|:------|:--------------------------------------------------------------------------------------------------------------|
| B/1/1 | The library will allow the user to represent any valid Graph Colouring problem and proposed solution in code. |
| B/1/2 | It will be impossible to instantiate an invalid problem or any invalid problem element.                       |
| B/1/3 | The problem, solution, and all problem elements will be JSON-serializable.                                    |

#### B/2 - Solution Verification

| Code  | Summary                                                                                  |
|:------|:-----------------------------------------------------------------------------------------|
| B/2/1 | A Graph Colouring problem will confirm that a correct proposed solution is correct.      |
| B/2/2 | A Graph Colouring problem will confirm that an incorrect proposed solution is incorrect. |

#### B/3 - Binary CSP modelling

| Code  | Summary                                                                                                                                                   |
|:------|:----------------------------------------------------------------------------------------------------------------------------------------------------------|
| B/3/1 | The library will allow the user to model any valid Graph Colouring problem as a generic binary CSP, so that it can be solved using the binary CSP solver. |
| B/3/2 | The user will be able to set the capacity of the binary CSP object at initialization and at runtime by passing in a single integer value.                 |

#### B/4 - Problem generation

| Code  | Summary                                                                                                                                                                                                                |
|:------|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| B/4/1 | The library will generate a random, valid, solvable Graph Colouring problem when the user specifies the number of nodes (from the range [1, 100]) and the global number of permitted colours (from the range [4, 16]). |
| B/4/2 | The user will be able to set the random number generation seed for the problem generation algorithm.                                                                                                                   |

### C - Map Colouring

#### C/1 - Problem Representation

| Code  | Summary                                                                                                     |
|:------|:------------------------------------------------------------------------------------------------------------|
| C/1/1 | The library will allow the user to represent any valid Map Colouring problem and proposed solution in code. |
| C/1/2 | It will be impossible to instantiate an invalid problem or any invalid problem element.                     |
| C/1/3 | The problem, solution, and all problem elements will be JSON-serializable.                                  |

#### C/2 - Solution Verification

| Code  | Summary                                                                                |
|:------|:---------------------------------------------------------------------------------------|
| C/2/1 | A Map Colouring problem will confirm that a correct proposed solution is correct.      |
| C/2/2 | A Map Colouring problem will confirm that an incorrect proposed solution is incorrect. |

#### C/3 - Binary CSP modelling

| Code  | Summary                                                                                                                                                 |
|:------|:--------------------------------------------------------------------------------------------------------------------------------------------------------|
| C/3/1 | The library will allow the user to model any valid Map Colouring problem as a generic binary CSP, so that it can be solved using the binary CSP solver. |
| C/3/2 | The user will be able to set the capacity of the binary CSP object at initialization and at runtime by passing in a single integer value.               |

#### C/4 - Problem generation

| Code  | Summary                                                                                                                                                                                                               |
|:------|:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| C/4/1 | The library will generate a random, valid, solvable Map Colouring problem when the user specifies the number of blocks (from the range [1, 100]) and the global number of permitted colours (from the range [4, 16]). |
| C/4/2 | The user will be able to set the random number generation seed for the problem generation algorithm.                                                                                                                  |

### D - *N*-Queens

#### D/1 - Problem Representation

| Code  | Summary                                                                                                  |
|:------|:---------------------------------------------------------------------------------------------------------|
| D/1/1 | The library will allow the user to represent any valid *N*-Queens problem and proposed solution in code. |
| D/1/2 | It will be impossible to instantiate an invalid problem or any invalid problem element.                  |
| D/1/3 | The problem, solution, and all problem elements will be JSON-serializable.                               |

#### D/2 - Solution Verification

| Code  | Summary                                                                             |
|:------|:------------------------------------------------------------------------------------|
| D/2/1 | A *N*-Queens problem will confirm that a correct proposed solution is correct.      |
| D/2/2 | A *N*-Queens problem will confirm that an incorrect proposed solution is incorrect. |

#### D/3 - Binary CSP modelling

| Code  | Summary                                                                                                                                              |
|:------|:-----------------------------------------------------------------------------------------------------------------------------------------------------|
| D/3/1 | The library will allow the user to model any valid *N*-Queens problem as a generic binary CSP, so that it can be solved using the binary CSP solver. |
| D/3/2 | The user will be able to set the capacity of the binary CSP object at initialization and at runtime by passing in a single integer value.            |

### E - Shikaku

#### E/1 - Problem Representation

| Code  | Summary                                                                                               |
|:------|:------------------------------------------------------------------------------------------------------|
| E/1/1 | The library will allow the user to represent any valid Shikaku problem and proposed solution in code. |
| E/1/2 | It will be impossible to instantiate an invalid problem or any invalid problem element.               |
| E/1/3 | The problem, solution, and all problem elements will be JSON-serializable.                            |

#### E/2 - Solution Verification

| Code  | Summary                                                                          |
|:------|:---------------------------------------------------------------------------------|
| E/2/1 | A Shikaku problem will confirm that a correct proposed solution is correct.      |
| E/2/2 | A Shikaku problem will confirm that an incorrect proposed solution is incorrect. |

#### E/3 - Binary CSP Modelling

| Code  | Summary                                                                                                                                   |
|:------|:------------------------------------------------------------------------------------------------------------------------------------------|
| E/3/1 | The library will allow the user to model any valid Shikaku problem as a generic binary CSP.                                               |
| E/3/2 | The user will be able to set the capacity of the binary CSP object at initialization and at runtime by passing in a single integer value. |

#### E/4 - Problem Generation

| Code  | Summary                                                                                                                                   |
|:------|:------------------------------------------------------------------------------------------------------------------------------------------|
| E/4/1 | The library will generate a random, valid, solvable Shikaku problem when the user specifies the number of hints (from the range [1, 50]). |
| E/4/2 | The user will be able to set the random number generation seed for the problem generation algorithm.                                      |

### F - Sudoku

#### F/1 - Problem Representation

| Code  | Summary                                                                                              |
|:------|:-----------------------------------------------------------------------------------------------------|
| F/1/1 | The library will allow the user to represent any valid Sudoku problem and proposed solution in code. |
| F/1/2 | It will be impossible to instantiate an invalid problem or any invalid problem element.              |
| F/1/3 | The problem, solution, and all problem elements will be JSON-serializable.                           |

#### F/2 - Solution Verification

| Code  | Summary                                                                         |
|:------|:--------------------------------------------------------------------------------|
| F/2/1 | A Sudoku problem will confirm that a correct proposed solution is correct.      |
| F/2/2 | A Sudoku problem will confirm that an incorrect proposed solution is incorrect. |

#### F/3 - Binary CSP Modelling

| Code  | Summary                                                                                                                                   |
|:------|:------------------------------------------------------------------------------------------------------------------------------------------|
| F/3/1 | The library will allow the user to model any valid Sudoku problem as a generic binary CSP.                                                |
| F/3/2 | The user will be able to set the capacity of the binary CSP object at initialization and at runtime by passing in a single integer value. |

#### F/4 - Problem Generation

| Code  | Summary                                                                                                                                          |
|:------|:-------------------------------------------------------------------------------------------------------------------------------------------------|
| F/4/1 | The library will generate a random, valid, solvable Sudoku problem when the user specifies the number of fixed numbers (from the range [1, 80]). |
| F/4/2 | The user will be able to set the random number generation seed for the problem generation algorithm.                                             |

### G - Binary CSP Modelling

#### G/1 - Measurable Binary CSP API

| Code  | Summary                                                                                                                                    |
|:-----:|:-------------------------------------------------------------------------------------------------------------------------------------------|
| G/1/1 | The library will expose an API of read-only properties and methods allowing the size and complexity of a binary CSP object to be measured. |

#### G/2 - Solvable Binary CSP API

| Code  | Summary                                                                                                                             |
|:-----:|:------------------------------------------------------------------------------------------------------------------------------------|
| G/2/1 | The library will expose an API of read-only properties and methods allowing a binary CSP object, modelling a problem, to be solved. |

#### G/3 - Modelling Binary CSP API

| Code  | Summary                                                                                                                        |
|:-----:|:-------------------------------------------------------------------------------------------------------------------------------|
| G/3/1 | The library will expose an API of properties and methods allowing a binary CSP object to model multiple problems sequentially. |

### H - Binary CSP Solving

#### H/1 - Solving

| Code  | Summary                                                                                                                                                                                                                                   |
|:------|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| H/1/1 | Given a binary CSP modelling a problem, the binary CSP solver returns a data structure containing the solution that was found (if any), the algorithm used, and the number of simplifying, assigning and backtracking steps.              |
| H/1/2 | Given a binary CSP modelling a solvable problem, the binary CSP solver finds a valid solution to the problem, when configured with every possible algorithm.                                                                              |
| H/1/3 | Given a binary CSP modelling an unsolvable problem, the binary CSP solver finds that there is no solution, when configured with every possible algorithm.                                                                                 |
| H/1/4 | Given a sequence of binary CSPs modelling solvable and unsolvable problems, the binary CSP solver finds a valid solution or no solution (as appropriate) for each binary CSP sequentially, when configured with every possible algorithm. |
| H/1/5 | The user will be able to cancel a solving operation using a cancellation token, causing an exception to be thrown.                                                                                                                        |

#### H/2 - Solver Configuration

| Code  | Summary                                                                                                                                                                                               |
|:------|:------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| H/2/1 | The user will be able to set the binary CSP solver's checking strategy at initialization and at runtime (but *not* when a solving operation is in progress) by passing in a single enumeration value. |
| H/2/2 | The user will be able to set the binary CSP solver's ordering strategy at initialization and at runtime (but *not* when a solving operation is in progress) by passing in a single enumeration value. |
| H/2/3 | The user will be able to set the binary CSP solver's capacity at initialization and at runtime (but *not* when a solving operation is in progress) by passing in a single integer value.              |

### I - Observable Binary CSP Solving

#### I/1 - Solving

| Code  | Summary                                                                                                                                                                                                                                              |
|:------|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| I/1/1 | Given a binary CSP modelling a problem, the observable binary CSP solver returns a data structure containing the solution that was found (if any), the algorithm used, and the number of simplifying, assigning and backtracking steps.              |
| I/1/2 | Given a binary CSP modelling a solvable problem, the observable binary CSP solver finds a valid solution to the problem, when configured with every possible algorithm.                                                                              |
| I/1/3 | Given a binary CSP modelling an unsolvable problem, the observable binary CSP solver finds that there is no solution, when configured with every possible algorithm.                                                                                 |
| I/1/4 | Given a sequence of binary CSPs modelling solvable and unsolvable problems, the observable binary CSP solver finds a valid solution or no solution (as appropriate) for each binary CSP sequentially, when configured with every possible algorithm. |
| I/1/5 | The user will be able to cancel a solving operation using a cancellation token, causing an exception to be thrown.                                                                                                                                   |

#### I/2 - Solver Configuration

| Code  | Summary                                                                                                                                                                                                          |
|:------|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| I/2/1 | The user will be able to set the observable binary CSP solver's checking strategy at initialization and at runtime (but *not* when a solving operation is in progress) by passing in a single enumeration value. |
| I/2/2 | The user will be able to set the observable binary CSP solver's ordering strategy at initialization and at runtime (but *not* when a solving operation is in progress) by passing in a single enumeration value. |
| I/2/3 | The user will be able to set the observable binary CSP solver's capacity at initialization and at runtime (but *not* when a solving operation is in progress) by passing in a single integer value.              |
| I/2/4 | The user will be able to set the observable binary CSP solver's step delay in milliseconds at initialization and at runtime, by passing in a single integer value.                                               |

#### I/3 - Observability

| Code  | Summary                                                                                                                                                                                                                                                |
|:------|:-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| I/3/1 | The observable binary CSP will expose a thread-safe solving operation API of read-only properties describing the search operation, including: the solver state, the step counts, the search level, the solution in progress, and the present variable. |
| I/3/2 | The observable binary CSP will implement the 'pull-model observer pattern', providing a mechanism for observers to subscribe to the solving operation API.                                                                                             |
| I/3/3 | The observable binary CSP will notify its observers after setup, after teardown, and after every search step.                                                                                                                                          |
| I/3/4 | The observable binary CSP will notify its observers after completion of the solving operation, but it will be up to the observers to unsubscribe themselves.                                                                                           |

## Non-Functional Requirements

1. The library will depend on no other libraries except the native C# SDK.
2. The modelling binary CSP base class will be abstract and extensible, with abstract and virtual methods to be extended by the user to create new problem-specific derivatives.
3. The binary CSP solver and observable binary CSP solver will be closed, with all implementation details hidden from the user.
