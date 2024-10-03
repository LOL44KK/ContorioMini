using System.Text.Json;

namespace Contorio
{
    public class SaveManager
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            TypeInfoResolver = MyJsonContext.Default.Options.TypeInfoResolver,
        };

        public static void SaveWorld(string path, World world)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(world, _options));
        }

        public static World? LoadWorld(string path)
        {
            return JsonSerializer.Deserialize<World>(File.ReadAllText(path), _options);
        }
    }
}
