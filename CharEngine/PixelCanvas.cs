namespace CharEngine
{
    public class PixelCanvas
    {
        private Pixel[,] _pixels;

        public Pixel[,] Pixels
        {
            get { return _pixels; }
            set { _pixels = value; }
        }

        public int Width => _pixels.GetLength(1);
        public int Height => _pixels.GetLength(0);

        /// <summary>
        /// Аналог this.Pixels[y, x]
        /// </summary>
        public Pixel this[int y, int x]
        {
            get { return _pixels[y, x]; }
            set { _pixels[y, x] = value; }
        }

        public PixelCanvas(Pixel[,] pixels)
        {
            _pixels = pixels;
        }

        public PixelCanvas(int width, int height)
        {
            _pixels = new Pixel[height, width];
            Fill(new Pixel(' ', ConsoleColor.Black));
        }

        /// <summary>
        /// Заполняет одним указанным пикселем.
        /// </summary>
        public void Fill(Pixel pixel)
        {
            for (int i = 0; i < _pixels.GetLength(0); i++)
            {
                for (int j = 0; j < _pixels.GetLength(1); j++)
                {
                    _pixels[i, j] = pixel;
                }
            }
        }
    }
}
