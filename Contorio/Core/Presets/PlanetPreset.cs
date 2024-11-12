namespace Contorio.Core.Presets
{
    public enum PlanetType
    {
        CIRCLE,
        SQUARE
    }

    public class PlanetPreset
    {
        private string _name;
        private int _size;
        private string _dirt;
        private PlanetType _type;
        private List<OrePreset> _ores;

        public string Name
        {
            get { return _name; }
            init { _name = value; }
        }

        public int Size
        {
            get { return _size; }
            init { _size = value; }
        }

        public string Dirt
        {
            get { return _dirt; }
            init { _dirt = value; }
        }

        public PlanetType Type
        {
            get { return _type; }
            init { _type = value; }
        }

        public List<OrePreset> Ores
        {
            get { return _ores; }
            init { _ores = value; }
        }

        public PlanetPreset(string name, int size, string dirt, PlanetType type, List<OrePreset> ores)
        {
            _name = name;
            _size = size;
            _dirt = dirt;
            _type = type;
            _ores = ores;
        }
    }
}