# Kolyteon

![Kolyteon icon](https://raw.githubusercontent.com/MattSortsThings/kolyteon/main/assets/kolyteon_icon_250x250.png)

1. Model a logic problem as a binary constraint satisfaction problem (binary CSP).
2. Choose a backtracking search algorithm.
3. Watch the binary CSP get solved.

Included problem types: Futoshiki, Graph Colouring, Map Colouring, N-Queens, Shikaku and Sudoku.

## About Kolyteon

- **Kolyteon** is a .NET class library for:
  - Modelling logic problems as binary [constraint satisfaction problems](https://en.wikipedia.org/wiki/Constraint_satisfaction_problem) (binary CSPs), and
  - Solving binary CSPs using a range of well-established backtracking search algorithms, and
  - Measuring and observing a search algorithm's behaviour as it attempts to find a solution to a binary CSP.
- **Kolyteon** is a solo development project by Matt Tantony.
- **Kolyteon** is expansion of my Computer Science Postgraduate Diploma project work undertaken at Birkbeck, University of London.

## Key Features

### Binary CSP modelling

- A family of generic interfaces representing a binary CSP with a specific variable type and domain value type that models a specific problem type.
- An abstract base class that implements the above using a constraint graph structure.

### Binary CSP solving

- A silent generic binary CSP solver that synchronously solves a binary CSP with optional cancellation.
  - Use the silent solver when you just want to find a solution to the problem (if one exists).
  - The silent solver returns a data structure containing the solution and metrics on the search algorithm that was used and how many assigning/backtracking steps it needed.
- A verbose generic binary CSP solver that asynchronously solves a binary CSP with optional cancellation, issuing a progress notification after every step of the algorithm.
  - The verbose solver returns the same result as the silent solver, but it also sends notifications to the caller while it's running.
  - The verbose solver can operate in a 'slow-motion' setting by configuring a time delay between each step of the search algorithm.
  - Use the verbose solver when you want to do something with the solving step progress notifications, like rendering the solution as it's built in real time.

### Choose your own algorithm

- Both solvers are configurable at startup and runtime with the user's choice of backtracking search algorithm.
- A backtracking search algorithm is composed of:
  - A checking strategy, which determines how it checks the safety of the solution at each step, and
  - An ordering strategy, which determines the order in which it approaches the variables of the binary CSP.
- Every backtracking search algorithm is guaranteed to find a solution to a binary CSP if it exists, but the number of assigning/backtracking steps required will vary considerably between algorithms.
- The library currently includes 8 checking strategies and 4 ordering strategies, making a total of 32 possible search algorithms.

### Example problem types

- Immutable, serializable types for representing in code any valid instance of the following problem types: Futoshiki, Graph Colouring, Map Colouring, *N*-Queens, Shikaku and Sudoku.
- Problem-specific constraint graph derivative classes, each of which models any instance of its problem type as a generic binary CSP.
- Services for generating random, solvable instances of all the problem types except *N*-Queens.

## Current Version: 0.1.0

**Kolyteon** is currently in its *initial development version*, published to NuGet for experimentation and evaluation.

I expect to have version 1.0.0 ready (with full documentation ) by approximately 30 September 2024.

## A quick example

In this example, the [8-Queens problem](https://en.wikipedia.org/wiki/Eight_queens_puzzle) is modelled as a binary CSP in which the variables are the column indexes from 0 to 8, each column's domain is the set of 8 possible squares in which a queen might be placed, and the constraints state that no two queens can occupy capturing squares.

The binary CSP is synchronously solved using a search algorithm composed of the *Backjumping* (BJ) checking strategy and the *Maximum Tightness* (MT) ordering strategy.

Finally, the generic binary CSP solution is converted into an array of 8 squares and its correctness is verified from the original problem.

First, we represent the 8-Queens problem as an instance of the `NQueensProblem` record type:

```csharp
NQueensProblem problem = NQueensProblem.FromN(8);
```

Then, we model the `NQueensProblem` as an `IBinaryCsp<int, Square, NQueensProblem>` using the included `NQueensConstraintGraph` class:

```csharp
IBinaryCsp<int, Square, NQueensProblem> binaryCsp = NQueensConstraintGraph.ModellingProblem(problem);
```

Then, we create a `SilentBinaryCspSolver<int, Square>` instance, configured with the `Backjumping` checking strategy and the `MaxTightness` ordering strategy:

```csharp
SilentBinaryCspSolver<int, Square> solver = SilentBinaryCspSolver<int, Square>.Create()
                                                .WithCapacity(8)
                                                .AndCheckingStrategy(CheckingStrategy.Backjumping)
                                                .AndOrderingStrategy(OrderingStrategy.MaxTightness)
                                                .Build();
```

We run the silent solver on the binary CSP:

```csharp
SolvingResult<int, Square> result = solver.Solve(binaryCsp);
```

The result contains metrics for how many steps the search algorithm required, and a set of assignments for each `int` binary CSP variable and the `Square` assigned to it.

For an *N*-Queens problem, we're only interested in the squares, so we convert the assignments to an array of `Square` values using the built-in extension method:

```csharp
Square[] solution = result.Assignments.ToNQueensSolution();
```

Finally, we get the original `NQueensProblem` instance to confirm the correctness of the solution:

```csharp
bool correct = problem.VerifyCorrect(solution); // returns true
```

## Installation

Install the `Kolyteon` package in your project from NuGet using the command `dotnet add package Kolyteon`.

**Kolyteon** has no third-party dependencies and never will.

## Credits

The template backtracking search algorithm and measuring system at **Kolyteon**'s heart has been adapted from the paper 'Hybrid Algorithms for the Constraint Satisfaction Problem' (Patrick Prosser, 1993, *Computational Intelligence* 9:3) \[[link](https://cse.unl.edu/~choueiry/Documents/Hybrid-Prosser.pdf)\].

All code is my own apart from where labelled in the source code.

Many thanks to Dr Panos Charalampopoulos, my Computer Science project supervisor at Birkbeck, University of London.
