namespace Contorio.CharEngine
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
