using System.Drawing;

using Contorio.Utils;
using Contorio.Core.Types;
using Contorio.Core.Managers;
using Contorio.Core.Factories;
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

        public Planet(PlanetPreset preset)
        {
            _blocks = new Dictionary<Point, BlockState>();
            _ground = new Dictionary<Point, GroundState>();
            _resources = new Dictionary<string, int>();
            _name = GeneratePlanetName.GenerateName();
            _size = preset.Size;

            GenerateLandscape(preset);
        }

        public Planet() : this(ResourceManager.Instance.PlanetPresets[0]) { }

        private void GenerateLandscape(PlanetPreset preset)
        {
            // Генериация пола
            switch (preset.Type)
            {
                case PlanetType.SQUARE:
                    for (int y = 0; y < preset.Size; y++)
                    {
                        for (int x = 0; x < preset.Size; x++)
                        {
                            _ground[new Point(x, y)] = new GroundState(preset.Dirt);
                        }
                    }
                    break;
                case PlanetType.CIRCLE:
                    int newSize = preset.Size + ((preset.Size + 1) % 2);
                    int radius = preset.Size / 2;
                    
                    Point center = new Point(radius, radius);

                    for (int y = 0; y < newSize; y++)
                    {
                        for (int x = 0; x < newSize; x++)
                        {
                            Point point = new Point(x, y);
                            int dx = x - center.X;
                            int dy = y - center.Y;
                            if (dx * dx + dy * dy <= radius * radius)
                            {
                                _ground[point] = new GroundState(preset.Dirt);
                            }
                        }
                    }
                    _size = newSize;
                    break;
            }

            // Генерация руд
            Random random = new Random();
            foreach (var groundState in _ground)
            {
                foreach (var ore in preset.Ores.OrderBy(ore => ore.Chance).Reverse())
                {
                    if (random.NextDouble() < ore.Chance)
                    {
                        Point[] cluster = ClusterGenerator.GenerateCluster(ore.MinClusterSize, ore.MaxClusterSize);
                        foreach (Point p in cluster)
                        {
                            if (_ground.ContainsKey(new Point(p.X + groundState.Key.X, p.Y + groundState.Key.Y)))
                            {
                                _ground[new Point(p.X + groundState.Key.X, p.Y + groundState.Key.Y)] = new GroundState(ore.Name);
                            }
                        }
                    }
                }
            }
        }

        public bool SetBlock(Point coord, Block block)
        {
            _blocks[coord] = BlockStateFactory.CreateBlockState(block.Type, block.Name);

            if (block.Type == BlockType.ENERGY_POINT || block.Type == BlockType.DRONE_STATION)
            {
                ConnectNearbyBlocks(coord, block);
            }
            else
            {
                if (_blocks[coord] is IConnectToEnergyPoint iConnectToEnergyPoint)
                {
                    iConnectToEnergyPoint.EnergyPoint = SearchEnergyPoint(coord);
                }
                if (_blocks[coord] is IConnectToDroneStation iConnectToDroneStation)
                {
                    iConnectToDroneStation.DroneStation = SearchDroneStation(coord);
                }
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
                else if (blockToRemove.Type == BlockType.DRONE_STATION)
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