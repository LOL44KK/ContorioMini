namespace CharEngine
{
    public class Renderer
    {
        private Scene _scene;
        private int _screenWidth;
        private int _screenHeight;
        private Pixel[,] _lastFrame;

        public Scene Scene
        {
            get { return _scene; }
        }

        public int ScreenWidth
        {
            get { return _screenWidth; }
            set { _screenWidth = value; }
        }

        public int ScreenHeight
        {
            get { return _screenHeight; }
            set { _screenHeight = value; }
        }

        public Renderer(int screenWidth, int screenHeight)
        {
            _scene = new Scene();
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            Console.CursorVisible = false;
            Console.SetWindowSize(_screenWidth, _screenHeight);

            _lastFrame = new Pixel[screenHeight, screenWidth];
            for (int y = 0; y < screenHeight; y++)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    _lastFrame[y, x] = new Pixel(' ', ConsoleColor.Black);
                }
            }
        }

        public void SetScene(Scene scene)
        {
            _scene = scene;
        }

        public void Render()
        {
            var visibleSprites = _scene.Sprites.Where(s => s.Visible).OrderBy(s => s.Layer);

            Pixel[,] currentFrame = new Pixel[_screenHeight, _screenWidth];
            for (int y = 0; y < _screenHeight; y++)
            {
                for (int x = 0; x < _screenWidth; x++)
                {
                    currentFrame[y, x] = new Pixel(' ', ConsoleColor.Black);
                }
            }

            foreach (var sprite in visibleSprites)
            {
                RenderSprite(sprite, currentFrame);
            }

            UpdateConsole(currentFrame);

            _lastFrame = currentFrame;
        }

        private void RenderSprite(Sprite sprite, Pixel[,] frame)
        {
            int spriteStartX = sprite.Position.X;

            switch (sprite.Alignment)
            {
                case Alignment.Center:
                    spriteStartX -= sprite.Width / 2;
                    break;
                case Alignment.Right:
                    spriteStartX -= sprite.Width;
                    break;
            }

            for (int y = 0; y < sprite.Height; y++)
            {
                for (int x = 0; x < sprite.Width; x++)
                {
                    Pixel pixel = sprite.Pixels[y, x];
                    if (pixel.C != ' ')
                    {
                        int globalX = x + spriteStartX;
                        int globalY = y + sprite.Position.Y;

                        if (globalX >= 0 && globalX < _screenWidth && globalY >= 0 && globalY < _screenHeight)
                        {
                            frame[globalY, globalX] = pixel;
                        }
                    }
                }
            }
        }

        private void UpdateConsole(Pixel[,] frame)
        {
            int cursorPositionX = -1;
            int cursorPositionY = -1;
            ConsoleColor consoleColor = Console.ForegroundColor;
            for (int y = 0; y < _screenHeight; y++)
            {
                for (int x = 0; x < _screenWidth; x++)
                {
                    Pixel currentPixel = frame[y, x];
                    Pixel previousPixel = _lastFrame[y, x];
                    if (currentPixel.C != previousPixel.C || currentPixel.Color != previousPixel.Color)
                    {
                        if (cursorPositionY != y || cursorPositionX != x)
                        {
                            Console.SetCursorPosition(x, y);
                            cursorPositionX = x;
                            cursorPositionY = y;
                        }
                        if (consoleColor != currentPixel.Color)
                        {
                            Console.ForegroundColor = currentPixel.Color;
                            consoleColor = currentPixel.Color;
                        }
                        Console.Write(currentPixel.C);
                        cursorPositionX++;
                    }
                }
            }
        }
    }
}
