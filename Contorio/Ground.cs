using Contorio.CharGraphics;

namespace Contorio
{

    public enum GroundType
    {
        BARRIER,
        DIR,
        ORE
    }

    public class Ground
    {
        private GroundType _type;
        private string _name;
        private Sprite _sprite;
        private int _tileId;

        public GroundType Type => _type;
        public string Name => _name;
        public Sprite Sprite => _sprite;

        public int TileId
        {
            get { return _tileId; }
            set { _tileId = value; }
        }

        public Ground(GroundType type, string name, Sprite sprite)
        {
            _type = type;
            _name = name;
            _sprite = sprite;
            _tileId = -1;
        }
    }

    public class Ore : Ground
    {
        private string _product;

        public string Product => _product;

        public Ore(string name, Sprite sprite, string product) : base(GroundType.ORE, name, sprite)
        {
            _product = product;
        }
    }

    public class GroundState
    {
        private string _name;

        public string Name => _name;

        public GroundState(string name)
        {
            _name = name;
        }
    }
}
