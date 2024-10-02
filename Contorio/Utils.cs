﻿using System.Drawing;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Contorio
{
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

                // Читаем ключ как строку и преобразуем в Point
                string key = reader.GetString();
                string[] parts = key.Split(',');
                Point point = new Point(int.Parse(parts[0]), int.Parse(parts[1]));

                // Читаем значение
                reader.Read();
                TValue value = JsonSerializer.Deserialize<TValue>(ref reader, options);

                dictionary[point] = value;
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<Point, TValue> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (var kvp in value)
            {
                // Преобразуем Point в строку "x,y"
                string key = $"{kvp.Key.X},{kvp.Key.Y}";
                writer.WritePropertyName(key);

                JsonSerializer.Serialize(writer, kvp.Value, options);
            }

            writer.WriteEndObject();
        }
    }

    [JsonSerializable(typeof(World))]
    [JsonSerializable(typeof(Planet))]
    [JsonSerializable(typeof(Dictionary<Point, BlockState>))]
    [JsonSerializable(typeof(Dictionary<Point, GroundState>))]
    public partial class MyJsonContext : JsonSerializerContext
    {

    }
}
