namespace Kolyteon.Placeholders.Internals;

internal sealed class PlaceholderConcreteClass : PlaceholderAbstractClass
{
    public override PlaceholderRecord GetItem() => new();
}
