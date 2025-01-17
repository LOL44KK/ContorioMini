using System.Diagnostics;

namespace CharEngine
{
    public class Engine
    {
        private Renderer _renderer;
        private Scene? _scene;
        private bool _running;
        
        private int _maxFPS;

        private double _ms;

        public Renderer Renderer 
        {
            get { return _renderer; }
        }

        public int MaxFPS
        {
            get { return _maxFPS; }
            set { _maxFPS = value; }
        }

        public int FPS
        {
            get { return (int)(1000 / _ms); }
        }

        public Engine(Renderer renderer, int maxFPS = 165)
        {
            _renderer = renderer;
            _running = false;

            _maxFPS = maxFPS;
        }

        public void SetScene(Scene scene)
        {
            if (_scene != null)
            {
                InputManager.Instance.InputHandlers -= _scene.RaiseInput;
            }

            _scene = scene;
            _renderer.SetScene(scene);
            InputManager.Instance.InputHandlers += scene.RaiseInput;
        }

        public void Run()
        {
            if (_scene == null)
            {
                throw new InvalidOperationException("Scene is not set.");
            }

            _running = true;
            MainLoop();
        }

        private void MainLoop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _scene?.RaiseEnable();

            while (_running)
            {
                stopwatch.Restart();


                _scene?.RaiseTick();

                InputManager.Instance.Tick();

                foreach (Sprite sprite in _scene.Sprites)
                {
                    sprite.Tick();
                }

                _renderer.Render();


                double elapsed = stopwatch.Elapsed.TotalMilliseconds;
                int delay = (int)((1000 / _maxFPS) - elapsed);
                if (delay > 0)
                {
                    Thread.Sleep(delay);
                }
                _ms = elapsed + delay;
            }
        }

        public void Quit()
        {
            _running = false;
        }
    }
}
