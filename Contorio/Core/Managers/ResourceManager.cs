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
        
        private TileSet _tileSet;
        private Dictionary<string, int> _tileIds;

        public Dictionary<string, Block> Blocks { get { return _blocks; } }
        public Dictionary<string, Ground> Grounds { get { return _grounds; } }
        
        public TileSet TileSet { get { return _tileSet; } }
        public IReadOnlyDictionary<string, int> TileIds 
        { 
            get { return _tileIds; } 
        }

        private ResourceManager()
        {
            _blocks = new Dictionary<string, Block>();
            _grounds = new Dictionary<string, Ground>();
            _tileSet = new TileSet(4, 3);
            _tileIds = new Dictionary<string, int>();
        }

        public void Initialize(List<Block> blocks, List<Ground> grounds)
        {
            _blocks.Clear();
            _grounds.Clear();
            foreach (Block block in blocks)
            {
                _blocks.Add(block.Name, block);
            }
            foreach (Ground ground in grounds)
            {
                _grounds.Add(ground.Name, ground);
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
