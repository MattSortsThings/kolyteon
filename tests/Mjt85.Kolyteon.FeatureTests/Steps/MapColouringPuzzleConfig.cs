using Mjt85.Kolyteon.MapColouring;

namespace Mjt85.Kolyteon.FeatureTests.Steps;

public sealed record MapColouringPuzzleConfig
{
    public PresetMap PresetMap { get; set; } = null!;

    public Colour[] GlobalColours { get; set; } = null!;
}
