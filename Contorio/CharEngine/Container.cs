namespace Contorio.CharEngine
{
    public class Container 
    {
        List<Sprite> _sprites;
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

        public Container(bool enable = false, bool visible = false) : this(new List<Sprite>(), enable, visible) { }

        public Container(List<Sprite> sprites, bool enable = false, bool visible = false)
        {
            _sprites = sprites;
            _enable = enable;
            _visible = visible;

            OnTick += Tick;
            OnInput += Input;
            OnEnable += Ready;
        }

        public void AddSprite(Sprite sprite)
        {
            _sprites.Add(sprite);
            sprite.Visible = _visible;
        }

        public void RemoveSprite(Sprite sprite)
        {
            _sprites.Remove(sprite);
        }

        public void RaiseInput(ConsoleKey key)
        {
            if (_enable)
            {
                Input(key);
            }
        }

        public void RaiseTick()
        {
            if (_enable)
            {
                Tick();
            }
        }

        public void RaiseEnable()
        {
            OnEnable?.Invoke();
        }

        public virtual void Tick()
        {
            // Что-то
        }

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
