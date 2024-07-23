using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kolyteon.Common.Internals;

internal sealed class ColourJsonConverter : JsonConverter<Colour>
{
    public override Colour Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        Colour.FromName(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, Colour value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Name);
}
