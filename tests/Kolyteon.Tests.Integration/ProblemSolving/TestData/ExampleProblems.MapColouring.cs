using System.Collections;
using Kolyteon.Common;
using Kolyteon.MapColouring;

namespace Kolyteon.Tests.Integration.ProblemSolving.TestData;

public static partial class ExampleProblems
{
    public static class MapColouring
    {
        public sealed class Solvable : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Black, Colour.White)
                        .AddBlock(Block.Parse("(0,0) [5x5]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Black, Colour.White)
                        .AddBlock(Block.Parse("(0,0) [2x2]"))
                        .AddBlock(Block.Parse("(2,0) [2x2]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Black)
                        .AddBlock(Block.Parse("(0,0) [2x2]"))
                        .AddBlock(Block.Parse("(4,4) [2x2]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [7x10]"))
                        .AddBlock(Block.Parse("(7,0) [1x5]"))
                        .AddBlock(Block.Parse("(7,5) [1x5]"))
                        .AddBlock(Block.Parse("(8,0) [2x10]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [10x4]"))
                        .AddBlock(Block.Parse("(0,4) [10x1]"))
                        .AddBlock(Block.Parse("(0,5) [10x1]"))
                        .AddBlock(Block.Parse("(0,6) [10x1]"))
                        .AddBlock(Block.Parse("(0,7) [10x3]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [7x1]"))
                        .AddBlock(Block.Parse("(0,1) [7x9]"))
                        .AddBlock(Block.Parse("(7,0) [2x3]"))
                        .AddBlock(Block.Parse("(7,3) [2x7]"))
                        .AddBlock(Block.Parse("(9,0) [1x3]"))
                        .AddBlock(Block.Parse("(9,3) [1x7]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [6x1]"))
                        .AddBlock(Block.Parse("(0,1) [10x1]"))
                        .AddBlock(Block.Parse("(0,2) [5x1]"))
                        .AddBlock(Block.Parse("(0,3) [8x7]"))
                        .AddBlock(Block.Parse("(5,2) [5x1]"))
                        .AddBlock(Block.Parse("(6,0) [4x1]"))
                        .AddBlock(Block.Parse("(8,3) [2x7]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [8x1]"))
                        .AddBlock(Block.Parse("(0,1) [8x2]"))
                        .AddBlock(Block.Parse("(0,3) [8x3]"))
                        .AddBlock(Block.Parse("(0,6) [8x4]"))
                        .AddBlock(Block.Parse("(8,0) [2x6]"))
                        .AddBlock(Block.Parse("(8,6) [2x1]"))
                        .AddBlock(Block.Parse("(8,7) [2x2]"))
                        .AddBlock(Block.Parse("(8,9) [2x1]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [1x10]"))
                        .AddBlock(Block.Parse("(1,0) [1x6]"))
                        .AddBlock(Block.Parse("(1,6) [1x4]"))
                        .AddBlock(Block.Parse("(2,0) [1x1]"))
                        .AddBlock(Block.Parse("(2,1) [1x1]"))
                        .AddBlock(Block.Parse("(2,2) [1x8]"))
                        .AddBlock(Block.Parse("(3,0) [1x2]"))
                        .AddBlock(Block.Parse("(3,2) [1x8]"))
                        .AddBlock(Block.Parse("(4,0) [6x10]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [3x4]"))
                        .AddBlock(Block.Parse("(0,4) [3x1]"))
                        .AddBlock(Block.Parse("(0,5) [3x5]"))
                        .AddBlock(Block.Parse("(3,0) [3x5]"))
                        .AddBlock(Block.Parse("(3,5) [3x2]"))
                        .AddBlock(Block.Parse("(3,7) [3x3]"))
                        .AddBlock(Block.Parse("(6,0) [1x1]"))
                        .AddBlock(Block.Parse("(6,1) [1x5]"))
                        .AddBlock(Block.Parse("(6,6) [4x4]"))
                        .AddBlock(Block.Parse("(7,0) [3x6]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [2x1]"))
                        .AddBlock(Block.Parse("(0,1) [10x3]"))
                        .AddBlock(Block.Parse("(0,4) [4x5]"))
                        .AddBlock(Block.Parse("(0,9) [1x1]"))
                        .AddBlock(Block.Parse("(1,9) [1x1]"))
                        .AddBlock(Block.Parse("(2,0) [1x1]"))
                        .AddBlock(Block.Parse("(2,9) [8x1]"))
                        .AddBlock(Block.Parse("(3,0) [2x1]"))
                        .AddBlock(Block.Parse("(4,4) [6x5]"))
                        .AddBlock(Block.Parse("(5,0) [1x1]"))
                        .AddBlock(Block.Parse("(6,0) [4x1]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [1x5]"))
                        .AddBlock(Block.Parse("(0,5) [10x1]"))
                        .AddBlock(Block.Parse("(0,6) [3x4]"))
                        .AddBlock(Block.Parse("(1,0) [1x4]"))
                        .AddBlock(Block.Parse("(1,4) [1x1]"))
                        .AddBlock(Block.Parse("(2,0) [8x1]"))
                        .AddBlock(Block.Parse("(2,1) [8x4]"))
                        .AddBlock(Block.Parse("(3,6) [1x4]"))
                        .AddBlock(Block.Parse("(4,6) [2x3]"))
                        .AddBlock(Block.Parse("(4,9) [2x1]"))
                        .AddBlock(Block.Parse("(6,6) [1x4]"))
                        .AddBlock(Block.Parse("(7,6) [3x4]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [4x7]"))
                        .AddBlock(Block.Parse("(0,7) [7x1]"))
                        .AddBlock(Block.Parse("(0,8) [5x1]"))
                        .AddBlock(Block.Parse("(0,9) [1x1]"))
                        .AddBlock(Block.Parse("(1,9) [6x1]"))
                        .AddBlock(Block.Parse("(4,0) [6x7]"))
                        .AddBlock(Block.Parse("(5,8) [2x1]"))
                        .AddBlock(Block.Parse("(7,7) [1x1]"))
                        .AddBlock(Block.Parse("(7,8) [1x1]"))
                        .AddBlock(Block.Parse("(7,9) [1x1]"))
                        .AddBlock(Block.Parse("(8,7) [1x1]"))
                        .AddBlock(Block.Parse("(8,8) [2x2]"))
                        .AddBlock(Block.Parse("(9,7) [1x1]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [7x10]"))
                        .AddBlock(Block.Parse("(7,0) [1x1]"))
                        .AddBlock(Block.Parse("(7,1) [1x1]"))
                        .AddBlock(Block.Parse("(7,2) [1x1]"))
                        .AddBlock(Block.Parse("(7,3) [1x2]"))
                        .AddBlock(Block.Parse("(7,5) [1x3]"))
                        .AddBlock(Block.Parse("(7,8) [1x1]"))
                        .AddBlock(Block.Parse("(7,9) [1x1]"))
                        .AddBlock(Block.Parse("(8,0) [1x2]"))
                        .AddBlock(Block.Parse("(8,2) [1x1]"))
                        .AddBlock(Block.Parse("(8,3) [1x5]"))
                        .AddBlock(Block.Parse("(8,8) [1x1]"))
                        .AddBlock(Block.Parse("(8,9) [1x1]"))
                        .AddBlock(Block.Parse("(9,0) [1x10]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [1x1]"))
                        .AddBlock(Block.Parse("(0,1) [4x3]"))
                        .AddBlock(Block.Parse("(0,4) [2x6]"))
                        .AddBlock(Block.Parse("(1,0) [3x1]"))
                        .AddBlock(Block.Parse("(2,4) [4x4]"))
                        .AddBlock(Block.Parse("(2,8) [2x1]"))
                        .AddBlock(Block.Parse("(2,9) [2x1]"))
                        .AddBlock(Block.Parse("(4,0) [1x4]"))
                        .AddBlock(Block.Parse("(4,8) [2x2]"))
                        .AddBlock(Block.Parse("(5,0) [1x4]"))
                        .AddBlock(Block.Parse("(6,0) [4x4]"))
                        .AddBlock(Block.Parse("(6,4) [1x2]"))
                        .AddBlock(Block.Parse("(6,6) [1x4]"))
                        .AddBlock(Block.Parse("(7,4) [2x6]"))
                        .AddBlock(Block.Parse("(9,4) [1x6]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [1x1]"))
                        .AddBlock(Block.Parse("(0,1) [1x1]"))
                        .AddBlock(Block.Parse("(0,2) [6x1]"))
                        .AddBlock(Block.Parse("(0,3) [10x1]"))
                        .AddBlock(Block.Parse("(0,4) [2x6]"))
                        .AddBlock(Block.Parse("(1,0) [4x1]"))
                        .AddBlock(Block.Parse("(1,1) [1x1]"))
                        .AddBlock(Block.Parse("(2,1) [1x1]"))
                        .AddBlock(Block.Parse("(2,4) [8x6]"))
                        .AddBlock(Block.Parse("(3,1) [2x1]"))
                        .AddBlock(Block.Parse("(5,0) [3x1]"))
                        .AddBlock(Block.Parse("(5,1) [1x1]"))
                        .AddBlock(Block.Parse("(6,1) [4x1]"))
                        .AddBlock(Block.Parse("(6,2) [4x1]"))
                        .AddBlock(Block.Parse("(8,0) [1x1]"))
                        .AddBlock(Block.Parse("(9,0) [1x1]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [6x7]"))
                        .AddBlock(Block.Parse("(0,7) [1x2]"))
                        .AddBlock(Block.Parse("(0,9) [1x1]"))
                        .AddBlock(Block.Parse("(1,7) [1x1]"))
                        .AddBlock(Block.Parse("(1,8) [1x1]"))
                        .AddBlock(Block.Parse("(1,9) [2x1]"))
                        .AddBlock(Block.Parse("(2,7) [1x1]"))
                        .AddBlock(Block.Parse("(2,8) [3x1]"))
                        .AddBlock(Block.Parse("(3,7) [5x1]"))
                        .AddBlock(Block.Parse("(3,9) [2x1]"))
                        .AddBlock(Block.Parse("(5,8) [2x1]"))
                        .AddBlock(Block.Parse("(5,9) [5x1]"))
                        .AddBlock(Block.Parse("(6,0) [4x2]"))
                        .AddBlock(Block.Parse("(6,2) [4x5]"))
                        .AddBlock(Block.Parse("(7,8) [1x1]"))
                        .AddBlock(Block.Parse("(8,7) [2x1]"))
                        .AddBlock(Block.Parse("(8,8) [2x1]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [7x2]"))
                        .AddBlock(Block.Parse("(0,2) [7x7]"))
                        .AddBlock(Block.Parse("(0,9) [1x1]"))
                        .AddBlock(Block.Parse("(1,9) [7x1]"))
                        .AddBlock(Block.Parse("(7,0) [1x4]"))
                        .AddBlock(Block.Parse("(7,4) [1x1]"))
                        .AddBlock(Block.Parse("(7,5) [1x1]"))
                        .AddBlock(Block.Parse("(7,6) [1x3]"))
                        .AddBlock(Block.Parse("(8,0) [1x1]"))
                        .AddBlock(Block.Parse("(8,1) [1x6]"))
                        .AddBlock(Block.Parse("(8,7) [1x1]"))
                        .AddBlock(Block.Parse("(8,8) [1x1]"))
                        .AddBlock(Block.Parse("(8,9) [1x1]"))
                        .AddBlock(Block.Parse("(9,0) [1x2]"))
                        .AddBlock(Block.Parse("(9,2) [1x4]"))
                        .AddBlock(Block.Parse("(9,6) [1x2]"))
                        .AddBlock(Block.Parse("(9,8) [1x1]"))
                        .AddBlock(Block.Parse("(9,9) [1x1]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [5x1]"))
                        .AddBlock(Block.Parse("(0,1) [8x1]"))
                        .AddBlock(Block.Parse("(0,2) [1x1]"))
                        .AddBlock(Block.Parse("(0,3) [2x2]"))
                        .AddBlock(Block.Parse("(0,5) [10x1]"))
                        .AddBlock(Block.Parse("(0,6) [6x2]"))
                        .AddBlock(Block.Parse("(0,8) [10x2]"))
                        .AddBlock(Block.Parse("(1,2) [1x1]"))
                        .AddBlock(Block.Parse("(2,2) [1x1]"))
                        .AddBlock(Block.Parse("(2,3) [8x2]"))
                        .AddBlock(Block.Parse("(3,2) [1x1]"))
                        .AddBlock(Block.Parse("(4,2) [1x1]"))
                        .AddBlock(Block.Parse("(5,0) [5x1]"))
                        .AddBlock(Block.Parse("(5,2) [1x1]"))
                        .AddBlock(Block.Parse("(6,2) [1x1]"))
                        .AddBlock(Block.Parse("(6,6) [4x2]"))
                        .AddBlock(Block.Parse("(7,2) [3x1]"))
                        .AddBlock(Block.Parse("(8,1) [1x1]"))
                        .AddBlock(Block.Parse("(9,1) [1x1]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Green, Colour.Red, Colour.Yellow, Colour.Blue)
                        .AddBlock(Block.Parse("(0,0) [3x1]"))
                        .AddBlock(Block.Parse("(0,1) [2x3]"))
                        .AddBlock(Block.Parse("(0,4) [2x1]"))
                        .AddBlock(Block.Parse("(0,5) [6x5]"))
                        .AddBlock(Block.Parse("(2,1) [1x2]"))
                        .AddBlock(Block.Parse("(2,3) [1x1]"))
                        .AddBlock(Block.Parse("(2,4) [1x1]"))
                        .AddBlock(Block.Parse("(3,0) [1x1]"))
                        .AddBlock(Block.Parse("(3,1) [1x4]"))
                        .AddBlock(Block.Parse("(4,0) [1x1]"))
                        .AddBlock(Block.Parse("(4,1) [1x4]"))
                        .AddBlock(Block.Parse("(5,0) [1x1]"))
                        .AddBlock(Block.Parse("(5,1) [1x4]"))
                        .AddBlock(Block.Parse("(6,0) [1x10]"))
                        .AddBlock(Block.Parse("(7,0) [1x10]"))
                        .AddBlock(Block.Parse("(8,0) [1x10]"))
                        .AddBlock(Block.Parse("(9,0) [1x1]"))
                        .AddBlock(Block.Parse("(9,1) [1x1]"))
                        .AddBlock(Block.Parse("(9,2) [1x1]"))
                        .AddBlock(Block.Parse("(9,3) [1x7]"))
                        .Build()
                ];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public sealed class Unsolvable : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Black)
                        .AddBlock(Block.Parse("(0,0) [2x2]"))
                        .AddBlock(Block.Parse("(2,0) [2x2]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseBlockSpecificColours()
                        .AddBlockAndColours(Block.Parse("(0,0) [2x2]"), Colour.Black, Colour.White)
                        .AddBlockAndColours(Block.Parse("(2,0) [2x2]"), Colour.Black, Colour.White)
                        .AddBlockAndColours(Block.Parse("(0,2) [4x4]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Black, Colour.White)
                        .AddBlock(Block.Parse("(0,0) [2x2]"))
                        .AddBlock(Block.Parse("(2,0) [2x2]"))
                        .AddBlock(Block.Parse("(0,2) [4x4]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Black, Colour.White)
                        .AddBlock(Block.Parse("(0,0) [1x1]"))
                        .AddBlock(Block.Parse("(0,1) [1x1]"))
                        .AddBlock(Block.Parse("(0,2) [6x1]"))
                        .AddBlock(Block.Parse("(0,3) [10x1]"))
                        .AddBlock(Block.Parse("(0,4) [2x6]"))
                        .AddBlock(Block.Parse("(1,0) [4x1]"))
                        .AddBlock(Block.Parse("(1,1) [1x1]"))
                        .AddBlock(Block.Parse("(2,1) [1x1]"))
                        .AddBlock(Block.Parse("(2,4) [8x6]"))
                        .AddBlock(Block.Parse("(3,1) [2x1]"))
                        .AddBlock(Block.Parse("(5,0) [3x1]"))
                        .AddBlock(Block.Parse("(5,1) [1x1]"))
                        .AddBlock(Block.Parse("(6,1) [4x1]"))
                        .AddBlock(Block.Parse("(6,2) [4x1]"))
                        .AddBlock(Block.Parse("(8,0) [1x1]"))
                        .AddBlock(Block.Parse("(9,0) [1x1]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Black, Colour.White)
                        .AddBlock(Block.Parse("(0,0) [6x7]"))
                        .AddBlock(Block.Parse("(0,7) [1x2]"))
                        .AddBlock(Block.Parse("(0,9) [1x1]"))
                        .AddBlock(Block.Parse("(1,7) [1x1]"))
                        .AddBlock(Block.Parse("(1,8) [1x1]"))
                        .AddBlock(Block.Parse("(1,9) [2x1]"))
                        .AddBlock(Block.Parse("(2,7) [1x1]"))
                        .AddBlock(Block.Parse("(2,8) [3x1]"))
                        .AddBlock(Block.Parse("(3,7) [5x1]"))
                        .AddBlock(Block.Parse("(3,9) [2x1]"))
                        .AddBlock(Block.Parse("(5,8) [2x1]"))
                        .AddBlock(Block.Parse("(5,9) [5x1]"))
                        .AddBlock(Block.Parse("(6,0) [4x2]"))
                        .AddBlock(Block.Parse("(6,2) [4x5]"))
                        .AddBlock(Block.Parse("(7,8) [1x1]"))
                        .AddBlock(Block.Parse("(8,7) [2x1]"))
                        .AddBlock(Block.Parse("(8,8) [2x1]"))
                        .Build()
                ];

                yield return
                [
                    MapColouringProblem.Create()
                        .WithCanvasSize(Dimensions.FromWidthAndHeight(10, 10))
                        .UseGlobalColours(Colour.Black, Colour.White)
                        .AddBlock(Block.Parse("(0,0) [7x2]"))
                        .AddBlock(Block.Parse("(0,2) [7x7]"))
                        .AddBlock(Block.Parse("(0,9) [1x1]"))
                        .AddBlock(Block.Parse("(1,9) [7x1]"))
                        .AddBlock(Block.Parse("(7,0) [1x4]"))
                        .AddBlock(Block.Parse("(7,4) [1x1]"))
                        .AddBlock(Block.Parse("(7,5) [1x1]"))
                        .AddBlock(Block.Parse("(7,6) [1x3]"))
                        .AddBlock(Block.Parse("(8,0) [1x1]"))
                        .AddBlock(Block.Parse("(8,1) [1x6]"))
                        .AddBlock(Block.Parse("(8,7) [1x1]"))
                        .AddBlock(Block.Parse("(8,8) [1x1]"))
                        .AddBlock(Block.Parse("(8,9) [1x1]"))
                        .AddBlock(Block.Parse("(9,0) [1x2]"))
                        .AddBlock(Block.Parse("(9,2) [1x4]"))
                        .AddBlock(Block.Parse("(9,6) [1x2]"))
                        .AddBlock(Block.Parse("(9,8) [1x1]"))
                        .AddBlock(Block.Parse("(9,9) [1x1]"))
                        .Build()
                ];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
