using BoDi;
using Mjt85.Kolyteon.Modelling;
using Mjt85.Kolyteon.NQueens;

namespace Mjt85.Kolyteon.FeatureTests.Hooks;

[Binding]
public sealed class NQueensHooks
{
    [BeforeFeature]
    [Scope(Feature = "N-Queens")]
    public static void RegisterBinaryCsp(IObjectContainer objectContainer)
    {
        NQueensBinaryCsp binaryCsp = new(5);
        objectContainer.RegisterInstanceAs<IModellingBinaryCsp<NQueensPuzzle, int, Queen>>(binaryCsp);
    }
}
