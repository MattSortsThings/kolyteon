# Kolyteon

**Kolyteon is a .NET class library for modelling common problem types as generic binary CSPs (constraint satisfaction problems) and solving them using a choice of 32 different algorithms, with performance metrics and optional progress reporting.**

## Key features

- An abstract generic binary CSP class that can model any instance of a specific problem type, extensible using the Template Method pattern.
- Comprehensive class/struct type systems and problem-specific binary CSP derivative classes for representing and modelling the following problem types:
    - Map Colouring
    - *N*-Queens
    - Shikaku
    - Sudoku
- A synchronous generic binary CSP solver class that can be configured with any of 32 different solving algorithms.
- An aynchronous generic binary CSP solver class, configurable like the synchronous solver, with user-defined progress reporting after each algorithm step.
- Interfaces for the binary CSP class and the solver classes so that they can be injected as dependencies.

## Background

This library originated as project work I undertook as part of my Postgraduate Diploma in Computer Science from Birkbeck, University of London. All coding work is my own, but the underlying algorithms are well-established in CSP literature.
