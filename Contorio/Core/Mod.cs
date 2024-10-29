using Contorio.Core.Types;

namespace Contorio.Core
{
    public class Mod
    {
        private string _name;
        private string _description;
        private string _version;
        private string _author;

        private List<Block> _blocks;
        private List<Ground> _grounds;
        private List<PlanetPreset> _planetPresets;

        public string Name
        {
            get { return _name; }
            init { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            init { _description = value; }
        }

        public string Version
        {
            get { return _version; }
            init { _version = value; }
        }

        public string Author
        {
            get { return _author; }
            init { _author = value; }
        }

        public List<Block> Blocks
        {
            get { return _blocks; }
            init { _blocks = value; }
        }

        public List<Ground> Grounds
        {
            get { return _grounds; }
            init { _grounds = value; }
        }

        public List<PlanetPreset> PlanetPresets
        {
            get { return _planetPresets; }
            init { _planetPresets = value; }
        }

        public Mod()
        {
            _name = "None";
            _description = "None";
            _version = "None";
            _author = "None";
            _blocks = new List<Block>();
            _grounds = new List<Ground>();
            _planetPresets = new List<PlanetPreset>();
        }
    }
}
