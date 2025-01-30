using CharEngine;

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
        private PixelCanvas _pixelCanvas;
        public GroundType Type => _type;
        public string Name => _name;
        public PixelCanvas PixelCanvas => _pixelCanvas;

        public Ground(GroundType type, string name, PixelCanvas pixelCanvas)
        {
            _type = type;
            _name = name;
            _pixelCanvas = pixelCanvas;
        }
    }

    public class Ore : Ground
    {
        private string _product;

        public string Product => _product;

        public Ore(string name, PixelCanvas pixelCanvas, string product) : base(GroundType.ORE, name, pixelCanvas)
        {
            _product = product;
        }
    }
}
