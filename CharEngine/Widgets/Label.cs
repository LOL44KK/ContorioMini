﻿using System.Drawing;

namespace CharEngine.Widgets
{
    public enum TextAlignment
    {
        Left,
        Center,
        Right
    }

    public class Label : Sprite
    {
        private string _text;
        private ConsoleColor _textColor;
        private TextAlignment _textAlignment;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                Pixels = CreatePixelsFromText(_text, _textColor, _textAlignment);
            }
        }

        public ConsoleColor TextColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                Pixels = CreatePixelsFromText(_text, _textColor, _textAlignment);
            }
        }

        public TextAlignment TextAlignment
        {
            get { return _textAlignment; }
            set
            {
                _textAlignment = value;
                Pixels = CreatePixelsFromText(_text, _textColor, _textAlignment);
            }
        }

        public Label(
            string text,
            ConsoleColor textColor,
            Point position,
            int layer = 0,
            bool visible = true,
            Alignment alignment = Alignment.Left,
            TextAlignment textAlignment = TextAlignment.Left
        )
            : base(
                  CreatePixelsFromText(text, textColor, textAlignment),
                  position,
                  layer,
                  visible,
                  alignment
            )
        {
            _text = text;
            _textColor = textColor;
            _textAlignment = textAlignment;
        }

        private static Pixel[,] CreatePixelsFromText(string text, ConsoleColor textColor, TextAlignment alignment)
        {
            var lines = text.Split(['\n'], StringSplitOptions.None);
            int width = lines.Max(line => line.Length);
            int height = lines.Length;

            Pixel[,] pixels = new Pixel[height, width];

            for (int y = 0; y < height; y++)
            {
                int lineLength = lines[y].Length;
                int offsetX = 0;

                switch (alignment)
                {
                    case TextAlignment.Center:
                        offsetX = (width - lineLength) / 2;
                        break;
                    case TextAlignment.Right:
                        offsetX = width - lineLength;
                        break;
                }

                for (int x = 0; x < width; x++)
                {
                    pixels[y, x] = new Pixel(
                        x >= offsetX && x < offsetX + lineLength ? lines[y][x - offsetX] : ' ',
                        textColor
                    );
                }
            }
            return pixels;
        }
    }
}
