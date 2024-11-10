using System.Diagnostics;

namespace Contorio.CharEngine
{
    public class Engine
    {
        private Renderer _renderer;
        private Scene? _scene;
        private bool _online;
        
        private int _maxFPS;

        public Renderer Renderer 
        {
            get { return _renderer; }
        }

        public int MaxFPS
        {
            get { return _maxFPS; }
            set { _maxFPS = value; }
        }

        public Engine(Renderer renderer, int maxFPS = 165)
        {
            _renderer = renderer;
            _online = false;

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

            _online = true;
            MainLoop();
        }

        private void MainLoop()
        {
            _scene.RaiseEnable();

            while (_online)
            {
                _scene.RaiseTick();

                InputManager.Instance.Tick();

                foreach (Sprite sprite in _scene.Sprites)
                {
                    sprite.Tick();
                }

                _renderer.Render();
            }
        }

        public void Quit()
        {
            _online = false;
        }
    }
}
