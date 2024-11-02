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
            init { _sprites = value; }
        }

        public event TickDelegate? OnTick;
        public event InputDelegate? OnInput;

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

        public void RaiseTick()
        {
            OnTick?.Invoke();
        }

        public void RaiseInput(ConsoleKey key)
        {
            OnInput?.Invoke(key);
        }
    }
}
