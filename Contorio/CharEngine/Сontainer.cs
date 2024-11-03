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

        public Сontainer(bool visible = false) : this(new List<Sprite>(), visible) { }

        public Сontainer(List<Sprite> sprites, bool visible = false)
        {
            _sprites = sprites;
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
    }
}
