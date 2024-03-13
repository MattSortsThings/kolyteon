# N-Queens

## Introduction

An *N*-Queens puzzle comprises an *N*x*N* square chess board and *N* queens, for a fixed value *N* &ge; 1. To solve the puzzle, one must place every queen in a different square on the chess board so that no two queens can capture each other.

The *N*-Queens puzzle is solvable for every value of *N* except 2 and 3.

## Types

All *N*-Queens code is located in the `Mjt85.Kolyteon.NQueens` namespace. The public types are:

| Name               |          Type          | Role                                                       |
|:-------------------|:----------------------:|:-----------------------------------------------------------|
| `NQueensPuzzle`    |         record         | represents an *N*-Queens puzzle                            |
| `Queen`            | readonly record struct | represents a queen occupying a specific chess board square |
| `NQueensBinaryCsp` |         class          | can model any *N*-Queens puzzle as a binary CSP            |

`NQueensPuzzle` and `Queen` instances are immutable and serializable.

A solution to an *N*-Queens puzzle is represented by an `IReadOnlyList<Queen>`.

## How to use

This section walks through an example of creating an *N*-Queens puzzle, modelling it as a binary CSP, running the solver, then extracting the solution from the variable/domain value assignments.

###  Create a puzzle

Create an `NQueensPuzzle` instance using the static `FromN(int)` factory method:

```C#
NQueensPuzzle puzzle = NQueensPuzzle.FromN(8);
```

This method throws an exception if you attempt to create an invalid puzzle, i.e. by passing in an *N* value less than 1.

### Model a puzzle as a binary CSP

The `NQueensBinaryCsp` class is the problem-specific derivative of the abstract `BinaryCsp<P,V,D>` class. Create an instance or inject it as a service. Then invoke its `Model(NQueensPuzzle)` method, passing in the puzzle instance:

```C#
NQueensPuzzle puzzle = NQueensPuzzle.FromN(8);

var binaryCsp = new NQueensBinaryCsp(8);
binaryCsp.Model(puzzle);
```

The `NQueensBinaryCsp` class extends `BinaryCsp<P,V,D>` as follows:
- `P` is `NQueensPuzzle`,
- `V` is `int`,
- `D` is `Queen`.

It therefore implements the generic interfaces:
- `ISolvableBinaryCsp<int,Queen>`,
- `IModellingBinaryCsp<NQueensPuzzle,int,Queen>`,
- `ITestableBinaryCsp<NQueensPuzzle,int,Queen>`.

### Solve the binary CSP

Create an instance of the `BinaryCspSolver<V,D>` class, or inject it as a service. It must be parametrized the same as the binary CSP, i.e. `<int,Queen>`. Then invoke its `Solve` method, passing in the binary CSP instance:

```C#
NQueensPuzzle puzzle = NQueensPuzzle.FromN(8);

var binaryCsp = new NQueensBinaryCsp(8);
binaryCsp.Model(puzzle);

var solver = BinaryCspSolver<int,Queen>.Create()
                .WithInitialCapacity(8)
                .AndInitialSearchStrategy(Search.ForwardChecking)
                .AndInitialOrderingStrategy(Ordering.None)
                .Build();

Result<int,Queen> result = solver.Solve(binaryCsp);
```

### Extract the solution

Convert the `Assignments` property of the `Result<int,Queen>` object to a list of `Queen` instances:

```C#
NQueensPuzzle puzzle = NQueensPuzzle.FromN(8);

var binaryCsp = new NQueensBinaryCsp(8);
binaryCsp.Model(puzzle);

var solver = BinaryCspSolver<int,Queen>.Create()
                .WithInitialCapacity(8)
                .AndInitialSearchStrategy(Search.ForwardChecking)
                .AndInitialOrderingStrategy(Ordering.None)
                .Build();

Result<int,Queen> result = solver.Solve(binaryCsp);

var solution = result.Assignments.ToPuzzleSolution();
```
