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

        public Engine(Renderer renderer)
        {
            _renderer = renderer;
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
            if (_scene == null)
            {
                throw new InvalidOperationException("Scene is not set.");
            }
            MainLoop();
        }

        private void MainLoop()
        {
            while (true)
            {
                // Оброботать события ввода
                InputManager.Instance.Tick();

                // Вызовы методов Tick
                _scene.TickInvoke();

                // Вызовы методa Tick во всех спрайтах
                foreach (Sprite sprite in _scene.Sprites)
                {
                    sprite.Tick();
                }

                // Обновление изображения
                _renderer.Render();
            }
        }
    }
}
