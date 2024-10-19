using System.Drawing;

using Contorio.Core.Types;
using Contorio.Core.Managers;
using Contorio.Core.Interfaces;

namespace Contorio.Core
{
    public class Planet
    {
        private string _name;
        private int _size;
        private Dictionary<Point, BlockState> _blocks;
        private Dictionary<Point, GroundState> _ground;
        private Dictionary<string, int> _resources;
        private int _energy;

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

        public Dictionary<Point, BlockState> Blocks
        {
            get { return _blocks; }
            init { _blocks = value; }
        }

        public Dictionary<Point, GroundState> Ground
        {
            get { return _ground; }
            init { _ground = value; }
        }

        public Dictionary<string, int> Resources
        {
            get { return _resources; }
            set { _resources = value; }
        }

        public int Energy
        {
            get { return _energy; }
            set { _energy = value; }
        }

        public Planet()
        {
            _blocks = new Dictionary<Point, BlockState>();
            _ground = new Dictionary<Point, GroundState>();
            _resources = new Dictionary<string, int>();
            _name = GenerateName();
            _size = 33;

            for (int y = 0; y < 33; y++)
            {
                for (int x = 0; x < 33; x++)
                {
                    _ground[new Point(x, y)] = new GroundState("dirt");
                }
            }

            for (int y = 0; y < 30; y++)
            {
                for (int x = 0; x < 30; x++)
                {
                    if (new Random().Next(0, 20) == 9)
                    {
                        _ground[new Point(x, y)] = new GroundState("iron");
                    }
                    if (new Random().Next(0, 20) == 9)
                    {
                        _ground[new Point(x, y)] = new GroundState("copper");
                    }
                }
            }
        }

        public Planet(int size, Dictionary<string, int> oreChance, string dirt="dirt")
        {
            _blocks = new Dictionary<Point, BlockState>();
            _ground = new Dictionary<Point, GroundState>();
            _resources = new Dictionary<string, int>();
            _name = GenerateName();
            _size = size;

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    _ground[new Point(x, y)] = new GroundState(dirt);
                }
            }

            Random random = new Random();
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    foreach (var ore in oreChance.OrderBy(ore => ore.Value).Reverse())
                    {
                        if (random.Next(0, 100) < ore.Value)
                        {
                            _ground[new Point(x, y)] = new GroundState(ore.Key);
                        }
                    }
                }
            }
        }

        private string GenerateName()
        {
            List<string> prefixes = new List<string>
            { "Neo", "Exo", "Zeta", "Omega", "Alpha", "Nova", "Astro", "Cosmo" };

            List<string> roots = new List<string>
            { "terra", "tron", "prime", "mund", "stella", "orbis", "sphere", "globe" };

            List<string> suffixes = new List<string>
            { "X", "Prime", "Major", "Minor", "Alpha", "Beta", "Gamma", "Delta" };
            
            Random random = new Random();
            return prefixes[random.Next(prefixes.Count)] + roots[random.Next(roots.Count)] + "-" + suffixes[random.Next(suffixes.Count)];
        }

        public bool SetBlock(Point coord, Block block)
        {
            switch (block.Type)
            {
                case BlockType.DRONE_STATION:
                    _blocks[coord] = new BlockState(block.Name);
                    ConnectNearbyBlocks(coord, block);
                    break;
                case BlockType.ENERGY_POINT:
                    _blocks[coord] = new BlockState(block.Name);
                    ConnectNearbyBlocks(coord, block);
                    break;
                case BlockType.SOLAR_PANEL:
                    _blocks[coord] = new SolarPanelState(
                        block.Name,
                        SearchEnergyPoint(coord)
                    );
                    break;
                case BlockType.CRYPTOR:
                    _blocks[coord] = new CryptorState(
                        block.Name,
                        SearchEnergyPoint(coord)
                    );
                    break;
                case BlockType.DRILL:
                    _blocks[coord] = new DrillState(
                        block.Name,
                        SearchDroneStation(coord),
                        SearchEnergyPoint(coord)
                    );
                    break;
                case BlockType.FACTORY:
                    _blocks[coord] = new FactoryState(
                       block.Name,
                       SearchDroneStation(coord),
                       SearchEnergyPoint(coord)
                   );
                    break;
                case BlockType.TRANSFER_BEACON:
                    _blocks[coord] = new TransferBeaconState(
                        block.Name,
                        SearchDroneStation(coord),
                        SearchEnergyPoint(coord)
                    );
                    break;
                default:
                    return false;
            }
            return true;
        }

        public bool RemoveBlock(Point coord)
        {
            if (!_blocks.ContainsKey(coord))
            {
                return false;
            }

            ResourceManager resourceManager = ResourceManager.Instance;

            Block blockToRemove = resourceManager.Blocks[_blocks[coord].Name];

            _blocks.Remove(coord);

            int range;
            switch (blockToRemove.Type)
            {
                case BlockType.ENERGY_POINT:
                    range = ((EnergyPoint)blockToRemove).Range;
                    break;
                case BlockType.DRONE_STATION:
                    range = ((DroneStation)blockToRemove).Range;
                    break;
                default:
                    return true;
            }

            foreach (var blockState in _blocks
                .Where(pair => coord.DistanceTo(pair.Key) <= range))
            {
                if (blockToRemove.Type == BlockType.ENERGY_POINT)
                {
                    if (blockState.Value is IConnectToEnergyPoint iConnectToEnergyPoint)
                    {
                        iConnectToEnergyPoint.EnergyPoint = SearchEnergyPoint(blockState.Key);
                    }
                }
                else if(blockToRemove.Type == BlockType.DRONE_STATION)
                {
                    if (blockState.Value is IConnectToDroneStation iConnectToDroneStation)
                    {
                        iConnectToDroneStation.DroneStation = SearchDroneStation(blockState.Key);
                    }
                }
            }
            return true;
        }

        private Point? SearchDroneStation(Point startCoord)
        {
            ResourceManager resourceManager = ResourceManager.Instance;
            Point? closestStation = null;
            double closestDistance = double.MaxValue;
            foreach (var block in _blocks)
            {
                if (resourceManager.Blocks[block.Value.Name].Type == BlockType.DRONE_STATION)
                {
                    double distance = startCoord.DistanceTo(block.Key);
                    int range = ((DroneStation)resourceManager.Blocks[block.Value.Name]).Range;

                    if (distance < range && distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestStation = block.Key;
                    }
                }
            }
            return closestStation;
        }

        private Point? SearchEnergyPoint(Point startCoord)
        {
            ResourceManager resourceManager = ResourceManager.Instance;
            Point? closestStation = null;
            double closestDistance = double.MaxValue;
            foreach (var block in _blocks)
            {
                if (resourceManager.Blocks[block.Value.Name].Type == BlockType.ENERGY_POINT)
                {
                    double distance = startCoord.DistanceTo(block.Key);
                    int range = ((EnergyPoint)resourceManager.Blocks[block.Value.Name]).Range;

                    if (distance < range && distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestStation = block.Key;
                    }
                }
            }
            return closestStation;
        }

        private void ConnectNearbyBlocks(Point coord, Block toBlock)
        {
            ResourceManager resourceManager = ResourceManager.Instance;
            
            int range;
            switch (toBlock.Type)
            {
                case BlockType.ENERGY_POINT:
                    range = ((EnergyPoint)toBlock).Range;
                    break;
                case BlockType.DRONE_STATION:
                    range = ((DroneStation)toBlock).Range;
                    break;
                default:
                    return;
            }

            foreach (var blockState in _blocks
                .Where((pair) => coord.DistanceTo(pair.Key) <= range)
                .Select(pair => pair.Value))
            {
                if (toBlock.Type == BlockType.ENERGY_POINT)
                {
                    if (blockState is IConnectToEnergyPoint iConnectToEnergyPoint)
                    {
                        iConnectToEnergyPoint.EnergyPoint = coord;
                    }
                }
                else if (toBlock.Type == BlockType.DRONE_STATION)
                {
                    if (blockState is IConnectToDroneStation iConnectToDroneStation)
                    {
                        iConnectToDroneStation.DroneStation = coord;
                    }
                }
            }
        }
    }
}