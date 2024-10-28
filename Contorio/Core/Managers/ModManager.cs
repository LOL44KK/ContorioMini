using Contorio.Core.Types;

namespace Contorio.Core.Managers
{
    // Добавить в Mod PlanetPreset
    // При генерациий мира сделать выбор присета планеты
    public class ModManager
    {
        private static ModManager _instance;
        public static ModManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ModManager();
                }
                return _instance;
            }
        }

        private List<Mod> _mods;

        private ModManager()
        {
            _mods = new List<Mod>();
        }

        public void AddMode(Mod mod)
        {
            _mods.Add(mod);
        }

        public void InitializeResources()
        {
            List<Block> blocks = new List<Block>();
            List<Ground> grounds = new List<Ground>();
            foreach (Mod mod in _mods)
            {
                foreach (Block block in mod.Blocks)
                {
                    blocks.Add(block);
                }
                foreach (Ground ground in mod.Grounds)
                {
                    grounds.Add(ground);
                }
            }
            ResourceManager.Instance.Initialize(blocks, grounds);
        }
    }
}
