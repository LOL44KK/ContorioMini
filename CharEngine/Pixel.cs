namespace CharEngine
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

        public static readonly Pixel Empty = new Pixel(' ', ConsoleColor.Black);
    }
}
