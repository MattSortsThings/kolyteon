namespace Kolyteon.Placeholders;

public readonly record struct PlaceholderStruct
{
    public PlaceholderStruct(PlaceholderEnum value)
    {
        Value = value;
    }

    public PlaceholderEnum Value { get; }
}
