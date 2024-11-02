namespace Contorio.CharEngine
{
    public class Scene
    {
        public delegate void TickDelegate();
        public delegate void InputDelegate(ConsoleKey key);

        private List<Sprite> _sprites = new List<Sprite>();

        public List<Sprite> Sprites
        { 
            get { return _sprites; }
            set { _sprites = value; }
        }

        public TickDelegate? Ticks;
        public InputDelegate? Inputs;

        public Scene(List<Sprite> sprites)
        {
            _sprites = sprites;
        }

        public Scene()
        {
            _sprites = new List<Sprite>();
        }

        public void AddSprite(Sprite sprite)
        {
            _sprites.Add(sprite);
        }

        public void RemoveSprite(Sprite sprite)
        {
            _sprites.Remove(sprite);
        }

        public void TickInvoke()
        {
            Ticks?.Invoke();
        }

        public void InputInvoke(ConsoleKey key)
        {
            Inputs?.Invoke(key);
        }
    }
}
