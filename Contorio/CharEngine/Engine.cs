namespace Contorio.CharEngine
{
    public class Engine
    {
        private Renderer _renderer;
        private Scene? _scene;

        public Renderer Renderer 
        {
            get { return _renderer; }
        }

        public Engine()
        {
            _renderer = new Renderer(120, 30);
        }

        public void SetScene(Scene scene)
        {
            if (_scene != null)
            {
                InputManager.Instance.RemoveInputHandler(_scene.InputInvoke);
            }

            _scene = scene;
            _renderer.SetScene(scene);
            InputManager.Instance.AddInputHandler(scene.InputInvoke);
        }

        public void Run()
        {
            if (_scene != null)
            {
                MainLoop();
            }
            else
            {
                throw new Exception("Not Scene");
            }
        }

        private void MainLoop()
        {
            while (true)
            {
                InputManager.Instance.Tick();

                foreach (Sprite sprite in _scene.Sprites)
                {
                    sprite.Tick();
                }

                _scene.TickInvoke();

                _renderer.Render();
            }
        }
    }
}
