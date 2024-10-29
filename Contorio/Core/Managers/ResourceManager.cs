using Contorio.CharGraphics;

using Contorio.Core.Types;

namespace Contorio.Core.Managers
{
    //Singleton
    public class ResourceManager
    {
        private static ResourceManager _instance;
        public static ResourceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ResourceManager();
                }
                return _instance;
            }
        }

        private Dictionary<string, Block> _blocks;
        private Dictionary<string, Ground> _grounds;
        private List<PlanetPreset> _planetPresets;
        
        private TileSet _tileSet;
        private Dictionary<string, int> _tileIds;

        public Dictionary<string, Block> Blocks { get { return _blocks; } }
        public Dictionary<string, Ground> Grounds { get { return _grounds; } }
        public List<PlanetPreset> PlanetPresets { get { return _planetPresets; } }

        public TileSet TileSet { get { return _tileSet; } }
        public IReadOnlyDictionary<string, int> TileIds 
        { 
            get { return _tileIds; } 
        }

        private ResourceManager()
        {
            _blocks = new Dictionary<string, Block>();
            _grounds = new Dictionary<string, Ground>();
            _planetPresets = new List<PlanetPreset>();

            _tileSet = new TileSet(4, 3);
            _tileIds = new Dictionary<string, int>();
        }

        public void Initialize(List<Block> blocks, List<Ground> grounds, List<PlanetPreset> planetPresets)
        {
            _blocks.Clear();
            _grounds.Clear();
            _planetPresets.Clear();
            foreach (Block block in blocks)
            {
                _blocks.Add(block.Name, block);
            }
            foreach (Ground ground in grounds)
            {
                _grounds.Add(ground.Name, ground);
            }
            foreach (PlanetPreset preset in planetPresets)
            {
                _planetPresets.Add(preset);
            }
            InitializeTileSet();
        }

        private void InitializeTileSet()
        {
            _tileSet = new TileSet(4, 3);
            _tileIds.Clear();

            foreach (Ground ground in _grounds.Values)
            {
                _tileSet.AddTile(ground.Sprite);
                _tileIds[ground.Name] = _tileSet.Tiles.Count - 1;
            }
            foreach (Block block in _blocks.Values)
            {
                _tileSet.AddTile(block.Sprite);
                _tileIds[block.Name] = _tileSet.Tiles.Count - 1;
            }
        }
    }
}
