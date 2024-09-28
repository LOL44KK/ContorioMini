using System.Drawing;

namespace Contorio
{
    public struct Player
    {
        public Point Coord;
        public int Planet;
        public Dictionary<string, int> Resources;

        public Player()
        {
            Coord = new Point(0, 0);
            Planet = 0;
            Resources = new Dictionary<string, int>();
        }
    }
}
