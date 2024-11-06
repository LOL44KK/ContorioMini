namespace Contorio.CharEngine
{
    public class Engine
    {
        private Renderer _renderer;
        private Scene? _scene;
        private bool _online;

        public Renderer Renderer 
        {
            get { return _renderer; }
        }

        public Engine(Renderer renderer)
        {
            _renderer = renderer;
            _online = true;
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
                // Вызовы методов Tick
                _scene.RaiseTick();

                // Оброботать события ввода
                InputManager.Instance.Tick();

                // Вызовы методa Tick во всех спрайтах
                foreach (Sprite sprite in _scene.Sprites)
                {
                    sprite.Tick();
                }

                // Обновление изображения
                _renderer.Render();
            }
        }

        public void Quit()
        {
            _online = false;
        }
    }
}
