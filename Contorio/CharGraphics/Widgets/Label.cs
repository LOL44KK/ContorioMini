using System.Drawing;

namespace Contorio.CharGraphics.Widgets
{
    public class Label : Sprite
    {
        private string _text;
        private ConsoleColor _textColor;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                UpdatePixels();
            }
        }

        public ConsoleColor TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                UpdatePixels();
            }
        }

        public Label(string text, ConsoleColor textColor, Point position, int layer = 0, bool visible = true)
            : base(CreatePixelsFromText(text, textColor), layer, visible, position)
        {
            _text = text;
            _textColor = textColor;
        }

        private void UpdatePixels()
        {
            Pixels = CreatePixelsFromText(_text, _textColor);
        }

        private static Pixel[,] CreatePixelsFromText(string text, ConsoleColor textColor)
        {
            var lines = text.Split(new[] { '\n' }, StringSplitOptions.None);
            int width = lines.Max(line => line.Length);
            int height = lines.Length;

            Pixel[,] pixels = new Pixel[height, width];
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x < lines[y].Length)
                    {
                        pixels[y, x] = new Pixel(lines[y][x], textColor);
                    }
                    else
                    {
                        pixels[y, x] = new Pixel(' ', ConsoleColor.Black);
                    }
                }
            }
            return pixels;
        }
    }
}
