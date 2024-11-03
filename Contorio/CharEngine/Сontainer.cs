namespace Contorio.CharEngine
{
    public class Сontainer
    {
        List<Sprite> _sprites;
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

        public Сontainer(List<Sprite> sprites)
        {
            _sprites = sprites;
        }

        public Сontainer()
        {
            _sprites = new List<Sprite>();
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
    }
}
