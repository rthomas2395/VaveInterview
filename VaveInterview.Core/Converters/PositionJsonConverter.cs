using System.Text.Json;
using System.Text.Json.Serialization;
using VaveInterview.Core.Models.Records;

namespace VaveInterview.Core.Converters
{
    public class PositionJsonConverter : JsonConverter<Position>
    {
        public override Position? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString()!.Trim('(', ')');
            var parts = s.Split(',');

            var x = int.Parse(parts[0].Trim());
            var y = int.Parse(parts[1].Trim());

            return new Position(x, y);
        }

        public override void Write(Utf8JsonWriter writer, Position value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
