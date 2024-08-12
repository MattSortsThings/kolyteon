using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kolyteon.Solving.Internals.Serialization;

internal sealed class OrderingStrategyJsonConverter : JsonConverter<OrderingStrategy>
{
    public override OrderingStrategy? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        OrderingStrategy.FromCode(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, OrderingStrategy value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Code);
}
