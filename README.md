# Kolyteon

![Kolyteon icon](https://raw.githubusercontent.com/MattSortsThings/kolyteon/main/assets/kolyteon_icon_250x250.png)

1. Generate a logic problem.
2. Model it as a binary constraint satisfaction problem (CSP).
3. Choose a backtracking search algorithm.
4. Watch the CSP get solved.

## Kolyteon is

- a .NET class library for modelling and solving problems as binary constraint satisfaction problems (binary CSPs).
- a tool for observing and measuring the performance of established backtracking search algorithms.
- an expansion of project work I undertook for my Postgraduate Diploma in Computer Science at Birkbeck, University of London.

## Key features

### Binary CSP modelling

- `IReadOnlyBinaryCsp<TVariable, TDomainValue>` interface represents a generic binary CSP with the given variable and domain value types.
- `IBinaryCsp<TVariable, TDomainValue, TProblem>` interface extends the above to model any instance of the given problem type.
- `ConstraintGraph<TVariable, TDomainValue, TProblem>` abstract base class implements the above, with template methods for implementing a problem-specific concrete derivative.

### Silent binary CSP solving

- `ISilentBinaryCspSolver<TVariable, TDomainValue>` interface silently and synchronously solves an `IReadOnlyBinaryCsp<TVariable, TDomainValue, TProblem>` with optional cancellation, using its configured search algorithm.
- `SilentBinaryCspSolver<TVariable, TDomainValue>` class implements the above.

### Verbose binary CSP solving

- `IVerboseBinaryCspSolver<TVariable, TDomainValue>` interface asynchronously solves an `IReadOnlyBinaryCsp<TVariable, TDomainValue, TProblem>` with optional cancellation, using its configured search algorithm and sending a progress notification to the client after every step.
- `VerboseBinaryCspSolver<TVariable, TDomainValue>` class implements the above.

### Choose your own algorithm

- The silent and verbose solvers are configurable at instantiation and at runtime with any backtracking search algorithm composed of a checking strategy and an ordering strategy.
- Eight checking strategies and four ordering strategies are included, for a total of 36 possible search algorithms.

### Six different problem types

- A complete set of immutable, serializable types for representing the following problems in code:
  - Futoshiki
  - Graph Colouring
  - Map Colouring
  - *N*-Queens
  - Shikaku
  - Sudoku
- A problem-specific constraint graph class for each problem type, so that it can be modelled as a binary CSP.
- Problem generators for creating random, solvable problems.

## Installation

Install the `Kolyteon` package from NuGet, using the command `dotnet add package Kolyteon`.

**Kolyteon** has no dependencies outside the native .NET SDK.

## A quick example

In this example, the 8-Queens chess problem is modelled as a binary CSP then solved using the silent (synchronous) binary CSP solver configured with the **FC+MT** search algorithm.

```csharp
// 1. Create N-Queens problem object
NQueensProblem problem = NQueensProblem.FromN(8);

// 2. Model N-Queens problem as binary CSP
var binaryCsp = NQueensConstraintGraph.ModellingProblem(problem);

// 3. Get silent solver with FC+MT algorithm
var solver = SilentBinaryCspSolver<int, Square>.Create()
                .WithCapacity(8)
                .AndCheckingStrategy(CheckingStrategy.ForwardChecking)
                .AndOrderingStrategy(OrderingStrategy.MaxTightness)
                .Build();

// 4. Solve binary CSP
var result = solver.Solve(binaryCsp);

// 5. Print the solution
foreach (var a in result.Assignments)
{
    Console.WriteLine(a);
}
```
