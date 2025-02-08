using System.Drawing;

using Contorio.Core.Types;
using Contorio.Core.Managers;

namespace Contorio.Core
{
    public class Player
    {
        private Point _coord;
        private int _planet;
        private Dictionary<string, int> _resources;
        private bool _godMode;
        private string? _selectedBlockToBuild;

        public Point Coord 
        { 
            get {  return _coord; }
            set { _coord = value; }
        }

        public int Planet 
        { 
            get { return _planet; }
            set { _planet = value; }
        }

        public Dictionary<string, int> Resources 
        {
            get {  return _resources; }
            set { _resources = value; }
        }

        public bool GodMode
        {
            get { return _godMode; }
            set { _godMode = value; }
        }

        public string? SelectedBlockToBuild 
        {
            get { return _selectedBlockToBuild; }
            set {  _selectedBlockToBuild = value; }
        }

        public Player()
        {
            _coord = new Point(0, 0);
            _planet = 0;
            _resources = new Dictionary<string, int>() { { "copper", 10 }, { "iron", 10 } };
            _godMode = false;
            _selectedBlockToBuild = null;
        }

        public void Move(int x, int y)
        {
            _coord.X += x;
            _coord.Y += y;
        }

        public bool BuildBlock(Point coord, Block block, Planet planet)
        {
            if (_godMode)
            {
                planet.SetBlock(coord, block);
                return true;
            }

            if (!planet.Ground.ContainsKey(coord))
            {
                return false;
            }

            foreach (var resource in block.Cost)
            {
                if (_resources.GetValueOrDefault(resource.Key, 0) < resource.Value)
                {
                    return false;
                }
            }
            
            DestroyBlock(coord, planet);

            foreach (var resource in block.Cost)
            {
                _resources[resource.Key] -= resource.Value;
            }
            planet.SetBlock(coord, block);
            return true;
        }

        public bool DestroyBlock(Point coord, Planet planet)
        {
            if (planet.Blocks.ContainsKey(coord))
            {
                if (_godMode)
                {
                    planet.RemoveBlock(coord);
                    return true;
                }

                Block block = ResourceManager.Instance.Blocks[planet.Blocks[coord].Name];
                foreach (var resource in block.Cost)
                {
                    _resources[resource.Key] = _resources.GetValueOrDefault(resource.Key, 0) + resource.Value;
                }
                planet.RemoveBlock(coord);
                return true;
            }
            return false;
        }
    }
}
