namespace CharEngine.Containers
{
    public class Container
    {
        private List<Sprite> _sprites;
        private bool _visible;

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

        public Container(List<Sprite> sprites, bool visible)
        {
            _sprites = sprites;
            _visible = visible;
        }

        public Container(bool visible = true) : this(new List<Sprite>(), visible) { }

        public void AddSprite(Sprite sprite)
        {
            _sprites.Add(sprite);
        }

        public void RemoveSprite(Sprite sprite)
        {
            _sprites.Remove(sprite);
        }
    }
}
