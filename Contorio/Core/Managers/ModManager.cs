using Contorio.Core.Presets;
using Contorio.Core.Types;

namespace Contorio.Core.Managers
{
    // Разработка заморожена на неопределенное время
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
            List<PlanetPreset> planetPresets = new List<PlanetPreset>();
            List<Research> research = new List<Research>();
            foreach (Mod mod in _mods)
            {
                foreach (Block block in mod.Blocks)
                {
                    blocks.Add(block);
                    research.Add(block.Research);
                }
                foreach (Ground ground in mod.Grounds)
                {
                    grounds.Add(ground);
                }
                foreach (PlanetPreset preset in mod.PlanetPresets)
                {
                    planetPresets.Add(preset);
                }
            }
            ResourceManager.Instance.Initialize(blocks, grounds, planetPresets, research);
        }
    }
}
