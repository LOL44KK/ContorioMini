using Contorio.Engine;

namespace Contorio
{
    public enum BlockType
    {
        BLOCK,
        DRONE_STATION,
        DRILL,
        FACTORY,
        ENERGY_POINT,
        SOLAR_PANEL,
        CRYPTOR
    }

    public class Block
    {
        private BlockType _type;
        private string _name;
        private Sprite _sprite;
        private Research _research;

        public BlockType Type => _type;
        public string Name => _name;
        public Sprite Sprite => _sprite;
        public Research Research => _research;

        public Block(BlockType type, string name, Sprite sprite, Research research)
        {
            _type = type;
            _name = name;
            _sprite = sprite;
            _research = research;
        }
    }

    public struct Recipe
    {
        private Dictionary<string, int> _input;
        private Dictionary<string, int> _output;

        public Dictionary<string, int> Input => _input;
        public Dictionary<string, int> Output => _output;

        public Recipe(Dictionary<string, int> input, Dictionary<string, int> output)
        {
            _input = input;
            _output = output;
        }
    }

    public class DroneStation : Block
    {
        private int _range;

        public int Range => _range;

        public DroneStation(string name, Sprite sprite, Research research, int range) : base(BlockType.DRONE_STATION, name, sprite, research)
        {
            _range = range;
        }
    }

    public class EnergyPoint : Block
    {
        private int _range;

        public int Range => _range;

        public EnergyPoint(string name, Sprite sprite, Research research, int range) : base(BlockType.ENERGY_POINT, name, sprite, research)
        {
            _range = range;
        }
    }

    public class Drill : Block
    {
        private int _speed;
        int _energyInput;

        public int Speed => _speed;

        public int EnergyInput => _energyInput;

        public Drill(string name, Sprite sprite, Research research, int speed, int energyInput) : base(BlockType.DRILL, name, sprite, research)
        {
            _speed = speed;
            _energyInput = energyInput;
        }
    }

    public class SolarPanel : Block
    {
        private int _energyOutput;

        public int EnergyOutput => _energyOutput;

        public SolarPanel(string name, Sprite sprite, Research research, int energyOutput) : base(BlockType.SOLAR_PANEL, name, sprite, research)
        {
            _energyOutput = energyOutput;
        }
    }

    public class Factory : Block
    {
        private int _energyInput;
        private Recipe _recipe;

        public int EnergyInput => _energyInput;

        public Recipe Recipe => _recipe;

        public Factory(string name, Sprite sprite, Research research, int energyInput, Recipe recipe) : base(BlockType.FACTORY, name, sprite, research)
        {
            _energyInput = energyInput;
            _recipe = recipe;
        }
    }
    public class Cryptor : Block
    {
        private int _energyInput;
        private Dictionary<string, int> _outputToken;

        public int EnergyInput => _energyInput;
        public IReadOnlyDictionary<string, int> OutputToken => _outputToken;

        public Cryptor(string name, Sprite sprite, Research research, int energyInput, Dictionary<string, int> outputToken) : base(BlockType.CRYPTOR, name, sprite, research)
        {
            _energyInput = energyInput;
            _outputToken = outputToken;
        }
    }
}