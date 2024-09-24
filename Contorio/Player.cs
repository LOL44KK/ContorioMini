using System.Drawing;

namespace Contorio
{
    public struct Player
    {
        public Point Coord;
        public int Planet;

        public Player()
        {
            Coord = new Point(0, 0);
            Planet = 0;
        }
    }
}
