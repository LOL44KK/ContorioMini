using System.Drawing;
using System.Text.Json.Serialization;
using System.Text.Json;

using Contorio.Core;
using Contorio.Core.Types;

namespace Contorio
{
    public static class PointExtensions
    {
        public static double DistanceTo(this Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }

    public class PointDictionaryConverter<TValue> : JsonConverter<Dictionary<Point, TValue>>
    {
        public override Dictionary<Point, TValue> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dictionary = new Dictionary<Point, TValue>();

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                string? key = reader.GetString();
                string[] parts = key.Split(',');
                
                Point point = new Point(int.Parse(parts[0]), int.Parse(parts[1]));

                reader.Read();
                TValue? value = JsonSerializer.Deserialize<TValue>(ref reader, options);
                
                dictionary[point] = value;
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<Point, TValue> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var kvp in value)
            {
                string key = $"{kvp.Key.X},{kvp.Key.Y}";
                writer.WritePropertyName(key);
                JsonSerializer.Serialize(writer, kvp.Value, options);
            }
            writer.WriteEndObject();
        }
    }

    [JsonSerializable(typeof(Point))]
    [JsonSerializable(typeof(World))]
    [JsonSerializable(typeof(Planet))]
    [JsonSerializable(typeof(Player))]
    [JsonSerializable(typeof(Research))]
    [JsonSerializable(typeof(ResearchSystem))]
    [JsonSerializable(typeof(Dictionary<Point, BlockState>))]
    [JsonSerializable(typeof(Dictionary<Point, GroundState>))]
    public partial class MyJsonContext : JsonSerializerContext
    {

    }
}
