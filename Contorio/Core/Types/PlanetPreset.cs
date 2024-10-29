namespace Contorio.Core.Types
{
    public class PlanetPreset
    {
        private string _name; 
        private int _size;
        private string _dirt;
        private List<(string Name, double Chance)> _ores;

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

        public List<(string Name, double Chance)> Ores
        {
            get { return _ores; }
            init { _ores = value; }
        }

        public PlanetPreset(string name, int size, string dirt, List<(string Name, double Chance)> ores)
        {
            _name = name;
            _size = size;
            _dirt = dirt;
            _ores = ores;
        }
    }
}