using System.Diagnostics;

namespace Contorio.Engine
{
    public class Engine
    {
        private Scene _scene;
        private int _screenWidth;
        private int _screenHeight;
        private Pixel[,] _previousFrame;

        private double _ms;

        public Scene Scene 
        { 
            get { return _scene; }
        }

        public int FPS { get { return (int)(1000 / _ms); } }

        public Engine(int screenWidth, int screenHeight)
        {
            _scene = new Scene();
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            Console.CursorVisible = false;

            _previousFrame = new Pixel[screenHeight, screenWidth];
            for (int y = 0; y < screenHeight; y++)
            {
                for (int x = 0; x < screenWidth; x++)
                {
                    _previousFrame[y, x] = new Pixel(' ', ConsoleColor.Black);
                }
            }
        }

        public void SetScene(Scene scene)
        {
            _scene = scene;
        }

        //???
        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();

            while (true)
            {
                stopwatch.Restart();

                Render();

                _ms = stopwatch.Elapsed.TotalMilliseconds;
            }
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
                for (int y = 0; y < sprite.Height; y++)
                {
                    for (int x = 0; x < sprite.Width; x++)
                    {
                        Pixel pixel = sprite.Pixels[y, x];
                        if (pixel.C != ' ')
                        {
                            int globalX = x + sprite.Position.X;
                            int globalY = y + sprite.Position.Y;

                            if (globalX >= 0 && globalX < _screenWidth && globalY >= 0 && globalY < _screenHeight)
                            {
                                currentFrame[globalY, globalX] = pixel;
                            }
                        }
                    }
                }
            }

            for (int y = 0; y < _screenHeight; y++)
            {
                for (int x = 0; x < _screenWidth; x++)
                {
                    Pixel currentPixel = currentFrame[y, x];
                    Pixel previousPixel = _previousFrame[y, x];
                    if (currentPixel.C != previousPixel.C || currentPixel.Color != previousPixel.Color)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = currentPixel.Color;
                        Console.Write(currentPixel.C);
                    }
                }
            }
            _previousFrame = currentFrame;
        }
    }
}
