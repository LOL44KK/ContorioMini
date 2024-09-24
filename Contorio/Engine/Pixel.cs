using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contorio.Engine
{
    public struct Pixel
    {
        public char C { get; set; }
        public ConsoleColor Color { get; set; }

        public Pixel(char c, ConsoleColor color)
        {
            C = c;
            Color = color;
        }
    }
}
