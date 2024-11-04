namespace Contorio.CharEngine
{
    public class Container 
    {
        List<Sprite> _sprites;
        private bool _visible;
        private bool _enable;

        public event TickDelegate? OnTick;
        public event InputDelegate? OnInput;

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
                Visible = value;
                _enable = value;
            }
        }

        public Container(bool enable = false, bool visible = false) : this(new List<Sprite>(), enable, visible) { }

        public Container(List<Sprite> sprites, bool enable = false, bool visible = false)
        {
            _sprites = sprites;
            _enable = enable;
            _visible = visible;
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
    }
}
