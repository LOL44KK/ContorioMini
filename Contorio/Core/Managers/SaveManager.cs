using System.Text.Json;

namespace Contorio
{
    public class SaveManager
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
