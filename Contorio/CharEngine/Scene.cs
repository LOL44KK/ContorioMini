namespace Contorio.CharEngine
{
    public delegate void TickDelegate();

    public class Scene
    {
        private List<Sprite> _sprites = new List<Sprite>();

        public event TickDelegate? OnTick;
        public event InputDelegate? OnInput;

        public List<Sprite> Sprites
        { 
            get { return _sprites; }
            init { _sprites = value; }
        }

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

        public void IncludeСontainer(Container container)
        {
            foreach (var sprite in container.Sprites)
            {
                _sprites.Add(sprite);
            }

            OnTick += container.Tick;
            OnInput += container.Input;
        }

        public void ExcludeContainer(Container container)
        {
            foreach (var sprite in container.Sprites)
            {
                _sprites.Remove(sprite);
            }

            OnTick -= container.Tick;
            OnInput -= container.Input;
        }
    }
}
