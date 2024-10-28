namespace Contorio.Core.Types
{
    public class PlanetPreset
    {
        private int _size;
        private string _dirt;
        private List<(string Name, double Chance)> _ores;

        public int Size => _size;
        public string Dirt => _dirt;
        public List<(string Name, double Chance)> Ores => _ores;

        public PlanetPreset(int size, string dirt, List<(string Name, double Chance)> ores)
        {
            _size = size;
            _dirt = dirt;
            _ores = ores;
        }
    }
}
