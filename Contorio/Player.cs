using System.Drawing;

namespace Contorio
{
    public class Player
    {
        private Point _coord;
        private int _planet;
        private Dictionary<string, int> _resources;

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

        public Player()
        {
            _coord = new Point(0, 0);
            _planet = 0;
            _resources = new Dictionary<string, int>();
        }

        public void Move(int x, int y)
        {
            _coord.X += x;
            _coord.Y += y;
        }
    }
}
