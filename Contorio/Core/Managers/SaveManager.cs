using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

using Contorio.Core.Types;
using Contorio.Utils;

namespace Contorio.Core.Managers
{
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

    public static class SaveManager
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            TypeInfoResolver = MyJsonContext.Default.Options.TypeInfoResolver,
            Converters = 
            {
                new PointDictionaryConverter<GroundState>(),
                new PointDictionaryConverter<BlockState>(),
            },
            IncludeFields = true,
        };

        public static void SaveWorld(string path, World world)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(world, _options));
        }

        public static World LoadWorld(string path)
        {
            World? world = JsonSerializer.Deserialize<World>(File.ReadAllText(path), _options);
            if (world == null)
            {
                throw new Exception("Куда залез?");
            }
            return world;
        }
    }
}
