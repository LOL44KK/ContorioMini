using System.Drawing;

namespace CharEngine
{
    public enum Alignment
    {
        Left,
        Center,
        Right
    }

    public class Sprite
    {
        protected Pixel[,] _pixels;
        protected bool _visible;
        protected int _layer;
        protected Point _position;
        protected Alignment _alignment; 

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

        public Alignment Alignment
        {
            get { return _alignment; }
            set { _alignment = value; }
        }

        public Sprite(Pixel[,] pixels, Point? position = null, int layer = 0, bool visible = true, Alignment alignment = Alignment.Left)
        {
            _pixels = pixels;
            _position = position ?? new Point(0, 0);
            _layer = layer;
            _visible = visible;
            _alignment = alignment;
        }

        /// <summary>
        /// Заполняет спрайт одним указанным пикселем.
        /// </summary>
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

        /// <summary>
        /// Выполняется каждый кадр (тик) игрового цикла.
        /// </summary>
        virtual public void Tick()
        {
            // Реализация обновления спрайта
        }
    }
}
