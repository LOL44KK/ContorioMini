namespace CharEngine
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

        /// <summary>
        /// Выполняется каждый кадр (тик) игрового цикла, когда сцена активна.
        /// </summary>
        public virtual void Tick()
        {
            // Реализация обновления сцены
        }

        /// <summary>
        /// Вызывается при инициализации сцены в двух случаях:
        /// 1. Когда сцена впервые добавляется в игровой движок
        /// 2. Когда свойство Enable меняется с false на true
        /// </summary>
        public virtual void Ready()
        {
            // Реализация инициализации сцены
        }

        /// <summary>
        /// Обрабатывает ввод с клавиатуры, когда сцена активна.
        /// Вызывается для каждой нажатой клавиши.
        public virtual void Input(ConsoleKey key)
        {
            // Реализация обработки ввода
        }
    }
}
