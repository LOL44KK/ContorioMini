using CharEngine;
using Contorio.Core.Interfaces;

namespace Contorio.Core.Types
{
    public enum BlockType
    {
        BLOCK,
        DRONE_STATION,
        DRILL,
        FACTORY,
        ENERGY_POINT,
        SOLAR_PANEL,
        CRYPTOR,
        TRANSFER_BEACON,
        ENERGY_GENERATOR
    }

    // Используется для хранение характеристик блока
    public class Block
    {
        private BlockType _type;
        private string _name;
        private PixelCanvas _pixelCanvas;
        private Research _research;
        private Dictionary<string, int> _cost;

        public BlockType Type => _type;
        public string Name => _name;
        public PixelCanvas PixelCanvas => _pixelCanvas;
        public Research Research => _research;

        public Dictionary<string, int> Cost => _cost;

        public Block(BlockType type, string name, PixelCanvas pixelCanvas, Research research, Dictionary<string, int> cost)
        {
            _type = type;
            _name = name;
            _pixelCanvas = pixelCanvas;
            _research = research;
            _cost = cost;
        }
    }

    public class DroneStation : Block, IRanged
    {
        private int _range;

        public int Range => _range;

        public DroneStation(string name, PixelCanvas pixelCanvas, Research research, Dictionary<string, int> cost, int range) : base(BlockType.DRONE_STATION, name, pixelCanvas, research, cost)
        {
            _range = range;
        }
    }

    public class EnergyPoint : Block, IRanged
    {
        private int _range;

        public int Range => _range;

        public EnergyPoint(string name, PixelCanvas pixelCanvas, Research research, Dictionary<string, int> cost, int range) : base(BlockType.ENERGY_POINT, name, pixelCanvas, research, cost)
        {
            _range = range;
        }
    }

    public class Drill : Block, IEnergyInput
    {
        private int _speed;
        int _energyInput;

        public int Speed => _speed;

        public int EnergyInput => _energyInput;

        public Drill(string name, PixelCanvas pixelCanvas, Research research, Dictionary<string, int> cost, int speed, int energyInput) : base(BlockType.DRILL, name, pixelCanvas, research, cost)
        {
            _speed = speed;
            _energyInput = energyInput;
        }
    }

    public class SolarPanel : Block
    {
        private int _energyOutput;

        public int EnergyOutput => _energyOutput;

        public SolarPanel(string name, PixelCanvas pixelCanvas, Research research, Dictionary<string, int> cost, int energyOutput) : base(BlockType.SOLAR_PANEL, name, pixelCanvas, research, cost)
        {
            _energyOutput = energyOutput;
        }
    }

    public class Factory : Block, IEnergyInput
    {
        private int _energyInput;
        private Recipe _recipe;

        public int EnergyInput => _energyInput;

        public Recipe Recipe => _recipe;

        public Factory(string name, PixelCanvas pixelCanvas, Research research, Dictionary<string, int> cost, int energyInput, Recipe recipe) : base(BlockType.FACTORY, name, pixelCanvas, research, cost)
        {
            _energyInput = energyInput;
            _recipe = recipe;
        }
    }

    public class Cryptor : Block, IEnergyInput
    {
        private int _energyInput;
        private Dictionary<string, int> _outputToken;

        public int EnergyInput => _energyInput;
        public IReadOnlyDictionary<string, int> OutputToken => _outputToken;

        public Cryptor(string name, PixelCanvas pixelCanvas, Research research, Dictionary<string, int> cost, int energyInput, Dictionary<string, int> outputToken) : base(BlockType.CRYPTOR, name, pixelCanvas, research, cost)
        {
            _energyInput = energyInput;
            _outputToken = outputToken;
        }
    }

    public class TransferBeacon : Block, IEnergyInput
    {
        private int _energyInput;
        private int _maxTransferableCount;

        public int EnergyInput => _energyInput;

        public int MaxTransferableCount => _maxTransferableCount;

        public TransferBeacon(string name, PixelCanvas pixelCanvas, Research research, Dictionary<string, int> cost, int energyInput, int maxTransferableCount) : base(BlockType.TRANSFER_BEACON, name, pixelCanvas, research, cost)
        {
            _energyInput = energyInput;
            _maxTransferableCount = maxTransferableCount;
        }
    }

    public class EnergyGenerator : Block
    {
        private int _energyOutput;
        private Recipe _recipe;

        public int EnergyOutput => _energyOutput;
        public Recipe Recipe => _recipe;

        public EnergyGenerator(string name, PixelCanvas pixelCanvas, Research research, Dictionary<string, int> cost, int energyOutput, Recipe recipe) : base(BlockType.ENERGY_GENERATOR, name, pixelCanvas, research, cost)
        {
            _energyOutput = energyOutput;
            _recipe = recipe;
        }
    }
}
