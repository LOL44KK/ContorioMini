namespace CharEngine
{
    public class PixelCanvas
    {
        protected Pixel[,] _pixels;

        public Pixel[,] Pixels
        {
            get { return _pixels; }
            set { _pixels = value; }
        }

        public int Height => _pixels.GetLength(0);
        public int Width => _pixels.GetLength(1);

        public PixelCanvas(Pixel[,] pixels)
        {
            _pixels = pixels;
        }

        public PixelCanvas(int width, int height)
        {
            _pixels = new Pixel[height, width];
        }

        public void FillPixels(Pixel pixel)
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
