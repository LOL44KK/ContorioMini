﻿using CharEngine;
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
        private Sprite _sprite;
        private Research _research;
        private Dictionary<string, int> _cost;

        public BlockType Type => _type;
        public string Name => _name;
        public Sprite Sprite => _sprite;
        public Research Research => _research;

        public Dictionary<string, int> Cost => _cost;

        public Block(BlockType type, string name, Sprite sprite, Research research, Dictionary<string, int> cost)
        {
            _type = type;
            _name = name;
            _sprite = sprite;
            _research = research;
            _cost = cost;
        }
    }

    public class DroneStation : Block
    {
        private int _range;

        public int Range => _range;

        public DroneStation(string name, Sprite sprite, Research research, Dictionary<string, int> cost, int range) : base(BlockType.DRONE_STATION, name, sprite, research, cost)
        {
            _range = range;
        }
    }

    public class EnergyPoint : Block
    {
        private int _range;

        public int Range => _range;

        public EnergyPoint(string name, Sprite sprite, Research research, Dictionary<string, int> cost, int range) : base(BlockType.ENERGY_POINT, name, sprite, research, cost)
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

        public Drill(string name, Sprite sprite, Research research, Dictionary<string, int> cost, int speed, int energyInput) : base(BlockType.DRILL, name, sprite, research, cost)
        {
            _speed = speed;
            _energyInput = energyInput;
        }
    }

    public class SolarPanel : Block
    {
        private int _energyOutput;

        public int EnergyOutput => _energyOutput;

        public SolarPanel(string name, Sprite sprite, Research research, Dictionary<string, int> cost, int energyOutput) : base(BlockType.SOLAR_PANEL, name, sprite, research, cost)
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

        public Factory(string name, Sprite sprite, Research research, Dictionary<string, int> cost, int energyInput, Recipe recipe) : base(BlockType.FACTORY, name, sprite, research, cost)
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

        public Cryptor(string name, Sprite sprite, Research research, Dictionary<string, int> cost, int energyInput, Dictionary<string, int> outputToken) : base(BlockType.CRYPTOR, name, sprite, research, cost)
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

        public TransferBeacon(string name, Sprite sprite, Research research, Dictionary<string, int> cost, int energyInput, int maxTransferableCount) : base(BlockType.TRANSFER_BEACON, name, sprite, research, cost) 
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

        public EnergyGenerator(string name, Sprite sprite, Research research, Dictionary<string, int> cost, int energyOutput, Recipe recipe) : base(BlockType.ENERGY_GENERATOR, name, sprite, research, cost)
        {
            _energyOutput = energyOutput;
            _recipe = recipe;
        }
    }
}