using Contorio.CharGraphics;

namespace Contorio.Core.Types
{
    public enum GroundType
    {
        BARRIER,
        DIR,
        ORE
    }

    // Используется для хранение характеристик пола
    public class Ground
    {
        private GroundType _type;
        private string _name;
        private Sprite _sprite;
        public GroundType Type => _type;
        public string Name => _name;
        public Sprite Sprite => _sprite;

        public Ground(GroundType type, string name, Sprite sprite)
        {
            _type = type;
            _name = name;
            _sprite = sprite;
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
}
