# Software Requirements

This document outlines the software requirements for the *Kolyteon* library (hereafter 'the library').

- [Software Requirements](#software-requirements)
  - [Functional Requirements](#functional-requirements)
    - [A - Futoshiki](#a---futoshiki)
      - [A/1 - Problem Representation](#a1---problem-representation)
      - [A/2 - Solution Verification](#a2---solution-verification)
      - [A/3 - Problem Modelling](#a3---problem-modelling)
      - [A/4 - Problem Generation](#a4---problem-generation)
      - [A/5 - Problem Solving](#a5---problem-solving)
    - [B - Graph Colouring](#b---graph-colouring)
      - [B/1 - Problem Representation](#b1---problem-representation)
      - [B/2 - Solution Verification](#b2---solution-verification)
      - [B/3 - Binary CSP modelling](#b3---binary-csp-modelling)
      - [B/4 - Problem generation](#b4---problem-generation)
      - [B/5 - Problem Solving](#b5---problem-solving)
    - [C - Map Colouring](#c---map-colouring)
      - [C/1 - Problem Representation](#c1---problem-representation)
      - [C/2 - Solution Verification](#c2---solution-verification)
      - [C/3 - Binary CSP modelling](#c3---binary-csp-modelling)
      - [C/4 - Problem generation](#c4---problem-generation)
      - [C/5 - Problem Solving](#c5---problem-solving)
    - [D - *N*-Queens](#d---n-queens)
      - [D/1 - Problem Representation](#d1---problem-representation)
      - [D/2 - Solution Verification](#d2---solution-verification)
      - [D/3 - Binary CSP modelling](#d3---binary-csp-modelling)
      - [D/4 - Problem Solving](#d4---problem-solving)
    - [E - Shikaku](#e---shikaku)
      - [E/1 - Problem Representation](#e1---problem-representation)
      - [E/2 - Solution Verification](#e2---solution-verification)
      - [E/3 - Problem Modelling](#e3---problem-modelling)
      - [E/4 - Problem Generation](#e4---problem-generation)
      - [E/5 - Problem Solving](#e5---problem-solving)
    - [F - Sudoku](#f---sudoku)
      - [F/1 - Problem Representation](#f1---problem-representation)
      - [F/2 - Solution Verification](#f2---solution-verification)
      - [F/3 - Problem Modelling](#f3---problem-modelling)
      - [F/4 - Problem Generation](#f4---problem-generation)
      - [F/5 - Problem Solving](#f5---problem-solving)
    - [G - Binary CSP Modelling](#g---binary-csp-modelling)
      - [G/1 - ReadOnly Binary CSP API](#g1---readonly-binary-csp-api)
      - [G/2 - Modelling Binary CSP API](#g2---modelling-binary-csp-api)
    - [H - Binary CSP Solving](#h---binary-csp-solving)
      - [H/1 - Solving](#h1---solving)
      - [H/2 - Solver Configuration](#h2---solver-configuration)
    - [I - Verbose Binary CSP Solving](#i---verbose-binary-csp-solving)
      - [I/1 - Solving](#i1---solving)
      - [I/2 - Solver Configuration](#i2---solver-configuration)
      - [I/3 - Reporting](#i3---reporting)
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

#### A/3 - Problem Modelling

| Code  | Summary                                                                                                                                   |
|:------|:------------------------------------------------------------------------------------------------------------------------------------------|
| A/3/1 | The library will allow the user to model any valid Futoshiki problem as a generic binary CSP.                                             |
| A/3/2 | The user will be able to set the capacity of the binary CSP object at initialization and at runtime by passing in a single integer value. |
| A/3/3 | The user will be able to initialize a generic binary CSP that is already modelling a specified problem.                                   |

#### A/4 - Problem Generation

| Code  | Summary                                                                                                                                                                                                                                    |
|:------|:-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| A/4/1 | The library will generate a random, valid, solvable Futoshiki problem when the user specifies the problem grid side length (from the range \[4, \9]) and the number of empty squares (greater than 0 and less than the problem grid area). |
| A/4/2 | The user will be able to set the random number generation seed for the problem generation algorithm.                                                                                                                                       |

#### A/5 - Problem Solving

| Code  | Summary                                                                                                                                  |
|:------|:-----------------------------------------------------------------------------------------------------------------------------------------|
| A/5/1 | The user will be able to solve a binary CSP modelling a Futoshiki problem using their choice of checking strategy and ordering strategy. |

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
| B/3/3 | The user will be able to initialize a generic binary CSP that is already modelling a specified problem.                                                   |

#### B/4 - Problem generation

| Code  | Summary                                                                                                                                                                                                                |
|:------|:-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| B/4/1 | The library will generate a random, valid, solvable Graph Colouring problem when the user specifies the number of nodes (from the range \[1, 50\]) and the global set of permitted colours (at least 4 unique values). |
| B/4/2 | The user will be able to set the random number generation seed for the problem generation algorithm.                                                                                                                   |

#### B/5 - Problem Solving

| Code  | Summary                                                                                                                                        |
|:------|:-----------------------------------------------------------------------------------------------------------------------------------------------|
| B/5/1 | The user will be able to solve a binary CSP modelling a Graph Colouring problem using their choice of checking strategy and ordering strategy. |

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
| C/3/3 | The user will be able to initialize a generic binary CSP that is already modelling a specified problem.                                                 |

#### C/4 - Problem generation

| Code  | Summary                                                                                                                                                                                                               |
|:------|:----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| C/4/1 | The library will generate a random, valid, solvable Map Colouring problem when the user specifies the number of blocks (from the range \[1, 50\]) and the global set of permitted colours (at least 4 unique values). |
| C/4/2 | The user will be able to set the random number generation seed for the problem generation algorithm.                                                                                                                  |

#### C/5 - Problem Solving

| Code  | Summary                                                                                                                                      |
|:------|:---------------------------------------------------------------------------------------------------------------------------------------------|
| C/5/1 | The user will be able to solve a binary CSP modelling a Map Colouring problem using their choice of checking strategy and ordering strategy. |

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
| D/3/3 | The user will be able to initialize a generic binary CSP that is already modelling a specified problem.                                              |

#### D/4 - Problem Solving

| Code  | Summary                                                                                                                                    |
|:------|:-------------------------------------------------------------------------------------------------------------------------------------------|
| D/4/1 | The user will be able to solve a binary CSP modelling an *N*-Queens problem using their choice of checking strategy and ordering strategy. |

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

#### E/3 - Problem Modelling

| Code  | Summary                                                                                                                                   |
|:------|:------------------------------------------------------------------------------------------------------------------------------------------|
| E/3/1 | The library will allow the user to model any valid Shikaku problem as a generic binary CSP.                                               |
| E/3/2 | The user will be able to set the capacity of the binary CSP object at initialization and at runtime by passing in a single integer value. |
| E/3/3 | The user will be able to initialize a generic binary CSP that is already modelling a specified problem.                                   |

#### E/4 - Problem Generation

| Code  | Summary                                                                                                                                                                                                                                                  |
|:------|:---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| E/4/1 | The library will generate a random, valid, solvable Shikaku problem when the user specifies the problem grid side length (from the range \[5, 20\]) and the number of hints (greater than 0 and less than or equal to 2 x the problem grid side length). |
| E/4/2 | The user will be able to set the random number generation seed for the problem generation algorithm.                                                                                                                                                     |

#### E/5 - Problem Solving

| Code  | Summary                                                                                                                                |
|:------|:---------------------------------------------------------------------------------------------------------------------------------------|
| E/5/1 | The user will be able to solve a binary CSP modelling a Shikaku problem using their choice of checking strategy and ordering strategy. |

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

#### F/3 - Problem Modelling

| Code  | Summary                                                                                                                                   |
|:------|:------------------------------------------------------------------------------------------------------------------------------------------|
| F/3/1 | The library will allow the user to model any valid Sudoku problem as a generic binary CSP.                                                |
| F/3/2 | The user will be able to set the capacity of the binary CSP object at initialization and at runtime by passing in a single integer value. |
| F/3/3 | The user will be able to initialize a generic binary CSP that is already modelling a specified problem.                                   |

#### F/4 - Problem Generation

| Code  | Summary                                                                                                                                            |
|:------|:---------------------------------------------------------------------------------------------------------------------------------------------------|
| F/4/1 | The library will generate a random, valid, solvable Sudoku problem when the user specifies the number of fixed numbers (from the range \[1, 80\]). |
| F/4/2 | The user will be able to set the random number generation seed for the problem generation algorithm.                                               |

#### F/5 - Problem Solving

| Code  | Summary                                                                                                                               |
|:------|:--------------------------------------------------------------------------------------------------------------------------------------|
| F/5/1 | The user will be able to solve a binary CSP modelling a Sudoku problem using their choice of checking strategy and ordering strategy. |

### G - Binary CSP Modelling

#### G/1 - ReadOnly Binary CSP API

| Code  | Summary                                                                                                                             |
|:-----:|:------------------------------------------------------------------------------------------------------------------------------------|
| G/1/2 | The library will expose an API of read-only properties and methods allowing a binary CSP object, modelling a problem, to be solved. |

#### G/2 - Modelling Binary CSP API

| Code  | Summary                                                                                                                        |
|:-----:|:-------------------------------------------------------------------------------------------------------------------------------|
| G/2/1 | The library will expose an API of properties and methods allowing a binary CSP object to model any instance of a problem type. |

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

### I - Verbose Binary CSP Solving

#### I/1 - Solving

| Code  | Summary                                                                                                                                                                                                                                           |
|:------|:--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| I/1/1 | Given a binary CSP modelling a problem, the verbose binary CSP solver returns a data structure containing the solution that was found (if any), the algorithm used, and the number of simplifying, assigning and backtracking steps.              |
| I/1/2 | Given a binary CSP modelling a solvable problem, the verbose binary CSP solver finds a valid solution to the problem, when configured with every possible algorithm.                                                                              |
| I/1/3 | Given a binary CSP modelling an unsolvable problem, the verbose binary CSP solver finds that there is no solution, when configured with every possible algorithm.                                                                                 |
| I/1/4 | Given a sequence of binary CSPs modelling solvable and unsolvable problems, the verbose binary CSP solver finds a valid solution or no solution (as appropriate) for each binary CSP sequentially, when configured with every possible algorithm. |
| I/1/5 | The user will be able to cancel a solving operation using a cancellation token, causing an exception to be thrown.                                                                                                                                |

#### I/2 - Solver Configuration

| Code  | Summary                                                                                                                                                                                                       |
|:------|:--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| I/2/1 | The user will be able to set the verbose binary CSP solver's checking strategy at initialization and at runtime (but *not* when a solving operation is in progress) by passing in a single enumeration value. |
| I/2/2 | The user will be able to set the verbose binary CSP solver's ordering strategy at initialization and at runtime (but *not* when a solving operation is in progress) by passing in a single enumeration value. |
| I/2/3 | The user will be able to set the verbose binary CSP solver's capacity at initialization and at runtime (but *not* when a solving operation is in progress) by passing in a single integer value.              |
| I/2/4 | The user will be able to set the verbose binary CSP solver's step delay in milliseconds at initialization and at runtime, by passing in a single integer value.                                               |

#### I/3 - Reporting

| Code  | Summary                                                                                                                                 |
|:------|:----------------------------------------------------------------------------------------------------------------------------------------|
| I/3/1 | The observable binary CSP solver will publish a progress report to a specified progress reporter after each step of a solving operation |

## Non-Functional Requirements

1. The library will depend on no other libraries except the native C# SDK.
2. The modelling binary CSP base class will be abstract and extensible, with abstract and virtual methods to be extended by the user to create new problem-specific derivatives.
3. The binary CSP solver and verbose binary CSP solver will be closed, with all implementation details hidden from the user.
