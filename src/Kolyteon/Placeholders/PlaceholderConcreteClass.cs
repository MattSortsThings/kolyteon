namespace Kolyteon.Placeholders;

public sealed class PlaceholderConcreteClass : PlaceholderAbstractClass
{
    public override PlaceholderRecord GetItem() => new();
}
