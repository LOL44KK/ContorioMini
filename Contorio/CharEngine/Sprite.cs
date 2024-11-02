using System.Drawing;

namespace Contorio.CharEngine
{
    public class Sprite
    {
        protected Pixel[,] _pixels;
        protected bool _visible;
        protected int _layer;
        protected Point _position;

        public Pixel[,] Pixels
        { 
            get { return _pixels; }
            set { _pixels = value; }
        }

        public int Width => _pixels.GetLength(1);

        public int Height => _pixels.GetLength(0);

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public int Layer
        {
            get { return _layer; }
            set { _layer = value; }
        }

        public Point Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Sprite(Pixel[,] pixels, int layer = 0, bool visible = true, Point? position = null)
        {
            _pixels = pixels;
            _layer = layer;
            _visible = visible;
            _position = position ?? new Point(0, 0);
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

        virtual public void Tick()
        {
            //Что-то
        }
    }
}
