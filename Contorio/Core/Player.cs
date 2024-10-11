using Contorio.CharGraphics.Widgets;
using System.Drawing;
using System.Numerics;
using System.Resources;

namespace Contorio
{
    public class Player
    {
        private Point _coord;
        private int _planet;
        private Dictionary<string, int> _resources;
        private bool _godMode;

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

        public Player()
        {
            _coord = new Point(0, 0);
            _planet = 0;
            _resources = new Dictionary<string, int>() { { "copper", 10 }, { "iron", 10 } };
            _godMode = false;
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

        public void DestroyBlock(Point coord, Planet planet)
        {
            if (planet.Blocks.ContainsKey(coord))
            {
                Block block = ResourceManager.Instance.Blocks[planet.Blocks[coord].Name];
                foreach (var resource in block.Cost)
                {
                    _resources[resource.Key] = _resources.GetValueOrDefault(resource.Key, 0) + resource.Value;
                }
                planet.RemoveBlock(coord);
            }
        }
    }
}
