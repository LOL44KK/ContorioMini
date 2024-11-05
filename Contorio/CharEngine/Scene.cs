namespace Contorio.CharEngine
{
    public delegate void TickDelegate();
    public delegate void EnableDelegate();

    public class Scene
    {
        private List<Sprite> _sprites;
        private bool _visible;
        private bool _enable;

        public event TickDelegate? OnTick;
        public event InputDelegate? OnInput;
        public event EnableDelegate? OnEnable;

        public List<Sprite> Sprites
        { 
            get { return _sprites; }
            init { _sprites = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                foreach (Sprite sprite in _sprites)
                {
                    sprite.Visible = _visible;
                }
            }
        }

        public bool Enable
        {
            get { return _enable; }
            set
            {
                _enable = value;

                Visible = value;
                if (_enable)
                {
                    RaiseEnable();
                }
            }
        }

        public Scene(List<Sprite> sprites, bool enable, bool visible)
        {
            _sprites = sprites;
            _visible = enable;
            _enable = visible;

            Enable = enable;
            Visible = visible;

            OnTick += Tick;
            OnInput += Input;
            OnEnable += Ready;
        }

        public Scene(bool enable = true, bool visible = true) : this(new List<Sprite>(), enable, visible) { }


        public void AddSprite(Sprite sprite)
        {
            _sprites.Add(sprite);
        }

        public void RemoveSprite(Sprite sprite)
        {
            _sprites.Remove(sprite);
        }

        public void RaiseTick()
        {
            if (_enable)
            {
                OnTick?.Invoke();
            }
        }

        public void RaiseInput(ConsoleKey key)
        {
            if (_enable)
            {
                OnInput?.Invoke(key);
            }
        }

        public void RaiseEnable() 
        { 
            OnEnable?.Invoke();
        }

        public void IncludeScene(Scene scene)
        {
            foreach (var sprite in scene.Sprites)
            {
                _sprites.Add(sprite);
            }

            OnTick += scene.RaiseTick;
            OnInput += scene.RaiseInput;
            OnEnable += scene.RaiseEnable;
        }

        public void ExcludeScene(Scene scene)
        {
            foreach (var sprite in scene.Sprites)
            {
                _sprites.Remove(sprite);
            }

            OnTick -= scene.RaiseTick;
            OnInput -= scene.RaiseInput;
            OnEnable -= scene.RaiseEnable;
        }

        public virtual void Tick()
        {
            // Что-то
        }

        // Вызывается при запуске движка
        // Также при изменении поля Enable на true
        public virtual void Ready()
        {
            // Что-то
        }

        public virtual void Input(ConsoleKey key)
        {
            // Что-то
        }
    }
}
