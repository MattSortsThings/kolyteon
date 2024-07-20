# Problem Definition: Common Elements

This document outlines value types common to multiple problem types in the *Kolyteon* library.

- [Problem Definition: Common Elements](#problem-definition-common-elements)
  - [`Square` readonly record struct](#square-readonly-record-struct)
  - [`NumberedSquare` readonly record struct](#numberedsquare-readonly-record-struct)
  - [`Dimensions` readonly record struct](#dimensions-readonly-record-struct)
  - [`Block` readonly record struct](#block-readonly-record-struct)
  - [`Colour` readonly record struct](#colour-readonly-record-struct)

## `Square` readonly record struct

- A `Square` instance represents a specific square on a 2-dimensional axis of squares, in which the upper-left square is (0,0).
- A `Square` is a (`Column`, `Row`) tuple, where:
  - `Column` is a non-negative integer denoting the zero-based column index of the square on the axis, and
  - `Row` is a non-negative integer denoting the zero-based row index of the square on the axis.
- Two `Square` instances are compared by `Column`, then by `Row`.
- The string representation of the square at column 1, row 0 is `"(1,0)"`.

## `NumberedSquare` readonly record struct

- A `NumberedSquare` instance represents a specific square that has been filled with a specific non-negative integer number.
- A `NumberedSquare` is a (`Square`, `Number`) tuple, where:
  - `Square` is a `Square` value, and
  - `Number` is a non-negative integer.
- Two `NumberedSquare` instances are compared by `Square`, then by `Number`.
- The string representation of the number 73 in the square at column 1, row 0 is `"(1,0) [73]"`.

## `Dimensions` readonly record struct

- A `Dimensions` instance represents the width and height in squares of a rectangular shape composed of squares.
- A `Dimensions` is a (`WidthInSquares`, `HeightInSquares`) tuple, where:
  - `WidthInSquares` is an integer &ge; 1, and
  - `HeightInSquares` is an integer &ge; 1.
- Two `Dimensions` instances are compared by `WidthInSquares`, then by `HeightInSquares`.
- The string representation of the dimensions 5 squares wide by 2 squares high is `"5x2"`.

## `Block` readonly record struct

- A `Block` instance represents a rectangular block of squares occupying a specific position on a 2-dimensional axis of squares, in which the upper-left square is (0,0).
- A `Block` is an (`OriginSquare`, `Dimensions`) tuple, where:
  - `OriginSquare` is a `Square` value denoting the square inside the block's upper-left corner, and,
  - `Dimensions` is a `Dimensions` value.
- Two `Block` instances are compared by `OriginSquare`, then by `Dimensions`.
- The string representation of the block 5 squares wide by 2 squares high, with its upper-left square at column 1, row 0, is `"(1,0) [5x2]"`.
- A `Block` can determine if it contains a `Square`.
- A `Block` can determine if it contains a `NumberedSquare`.
- A `Block` can determine if it contains another `Block`.
- A `Block` can determine if it overlaps another `Block`.
- A `Block` can determine if it is adjacent to another `Block`.

## `Colour` readonly record struct

- A "smart enum" of 16 possible `Colour` values will be defined.
- A `Colour` is a (`Number`, `Name`) tuple, where:
  - `Number` is a unique integer in the range [0, 15], and
  - `Name` is a unique string name.
- Two `Colour` instances are compared by `Number`.
- The string representation of a `Colour` value is its `Name`.
