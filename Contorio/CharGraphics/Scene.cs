namespace Contorio.CharGraphics
{
    public class Scene
    {
        private List<Sprite> _sprites = new List<Sprite>();

        public List<Sprite> Sprites
        { 
            get { return _sprites; }
            set { _sprites = value; }
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
    }
}
