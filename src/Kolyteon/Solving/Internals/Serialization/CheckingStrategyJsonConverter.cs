using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kolyteon.Solving.Internals.Serialization;

internal sealed class CheckingStrategyJsonConverter : JsonConverter<CheckingStrategy>
{
    public override CheckingStrategy? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        CheckingStrategy.FromCode(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, CheckingStrategy value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Code);
}
