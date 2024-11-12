namespace Contorio.Core.Presets
{
    public class OrePreset
    {
        private string _name;
        private double _chance;
        private int _minClusterSize;
        private int _maxClusterSize;

        public string Name
        {
            get { return _name; }
            init { _name = value; }
        }

        public double Chance
        {
            get { return _chance; }
            init { _chance = value; }
        }

        public int MinClusterSize
        {
            get { return _minClusterSize; }
            init { _minClusterSize = value; }
        }

        public int MaxClusterSize
        {
            get { return _maxClusterSize; }
            init { _maxClusterSize = value; }
        }

        public OrePreset(string name, double chance, int minClusterSize, int maxClusterSize)
        {
            _name = name;
            _chance = chance;
            _minClusterSize = minClusterSize;
            _maxClusterSize = maxClusterSize;
        }
    }
}
