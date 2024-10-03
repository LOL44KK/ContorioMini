using System.Drawing;

namespace Contorio
{
    public class Planet
    {
        private string _name;
        private Dictionary<Point, BlockState> _blocks;
        private Dictionary<Point, GroundState> _ground;
        private Dictionary<string, int> _resources;
        private int _energy;

        public string Name
        {
            get { return _name; }
            init { _name = value; }
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

        public Planet(int size, Dictionary<string, int> oreChance)
        {
            _blocks = new Dictionary<Point, BlockState>();
            _ground = new Dictionary<Point, GroundState>();
            _resources = new Dictionary<string, int>();
            _name = GenerateName();

            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    _ground[new Point(x, y)] = new GroundState("dirt");
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
            if (block.Type == BlockType.DRONE_STATION)
            {
                _blocks[coord] = new BlockState(block.Name);
                ConnectNearbyBlocks(coord, BlockType.DRONE_STATION);
            }
            else if (block.Type == BlockType.ENERGY_POINT)
            {
                _blocks[coord] = new BlockState(block.Name);
                ConnectNearbyBlocks(coord, BlockType.ENERGY_POINT);
            }
            else if (block.Type == BlockType.DRILL)
            {
                _blocks[coord] = new DrillState(
                    block.Name,
                    SearchDroneStation(coord),
                    SearchEnergyPoint(coord)
                );
            }
            else if (block.Type == BlockType.SOLAR_PANEL)
            {
                _blocks[coord] = new SolarPanelState(
                    block.Name,
                    SearchEnergyPoint(coord)
                );
            }
            else if (block.Type == BlockType.FACTORY)
            {
                _blocks[coord] = new FactoryState(
                    block.Name,
                    SearchDroneStation(coord),
                    SearchEnergyPoint(coord)
                );
            }
            else if (block.Type == BlockType.CRYPTOR)
            {
                _blocks[coord] = new CryptorState(
                    block.Name,
                    SearchEnergyPoint(coord)
                );
            }
            return false;
        }

        public bool RemoveBlock(Point coord)
        {
            if (!_blocks.ContainsKey(coord))
            {
                return false;

            }

            BlockType type = ResourceManager.Instance.Blocks[_blocks[coord].Name].Type;

            _blocks.Remove(coord);

            foreach (var block in _blocks)
            {
                if (ResourceManager.Instance.Blocks[block.Value.Name].Type == BlockType.DRILL)
                {
                    DrillState drillState = (DrillState)block.Value;
                    if (type == BlockType.DRONE_STATION && drillState.DroneSation == coord)
                    {
                        drillState.DroneSation = SearchDroneStation(block.Key);
                    }
                    else if (type == BlockType.ENERGY_POINT && drillState.EnergyPoint == coord)
                    {
                        drillState.EnergyPoint = SearchEnergyPoint(block.Key);
                    }
                }
                else if (ResourceManager.Instance.Blocks[block.Value.Name].Type == BlockType.FACTORY)
                {
                    FactoryState factoryState = (FactoryState)block.Value;
                    if (type == BlockType.DRONE_STATION && factoryState.DroneSation == coord)
                    {
                        factoryState.DroneSation = SearchDroneStation(block.Key);
                    }
                    else if (type == BlockType.ENERGY_POINT && factoryState.EnergyPoint == coord)
                    {
                        factoryState.EnergyPoint = SearchEnergyPoint(block.Key);
                    }
                }
                else if (ResourceManager.Instance.Blocks[block.Value.Name].Type == BlockType.SOLAR_PANEL)
                {
                    SolarPanelState solarPanelState = (SolarPanelState)block.Value;
                    if (type == BlockType.ENERGY_POINT && solarPanelState.EnergyPoint == coord)
                    {
                        solarPanelState.EnergyPoint = SearchEnergyPoint(block.Key);
                    }
                }
                else if (ResourceManager.Instance.Blocks[block.Value.Name].Type == BlockType.CRYPTOR)
                {
                    CryptorState cryptorState = (CryptorState)block.Value;
                    if (type == BlockType.CRYPTOR && cryptorState.EnergyPoint == coord)
                    {
                        cryptorState.EnergyPoint = SearchEnergyPoint(block.Key);
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
                    double distance = CalculateDistance(startCoord, block.Key);
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
                    double distance = CalculateDistance(startCoord, block.Key);
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

        private void ConnectNearbyBlocks(Point coord, BlockType type)
        {
            ResourceManager resourceManager = ResourceManager.Instance;
            int range;

            if (type == BlockType.DRONE_STATION)
            {
                var station = (DroneStation)resourceManager.Blocks[_blocks[coord].Name];
                range = station.Range;
            }
            else if (type == BlockType.ENERGY_POINT)
            {
                var energyPoint = (EnergyPoint)resourceManager.Blocks[_blocks[coord].Name];
                range = energyPoint.Range;
            }
            else
            {
                return;
            }

            foreach (var block in _blocks)
            {
                double distance = CalculateDistance(coord, block.Key);
                if (distance <= range)
                {
                    BlockType blockType = resourceManager.Blocks[block.Value.Name].Type;

                    if (blockType == BlockType.DRILL)
                    {
                        DrillState drillState = (DrillState)block.Value;
                        if (type == BlockType.DRONE_STATION)
                        {
                            drillState.DroneSation = coord;
                        }
                        else if (type == BlockType.ENERGY_POINT)
                        {
                            drillState.EnergyPoint = coord;
                        }
                    }
                    else if (blockType == BlockType.FACTORY)
                    {
                        FactoryState factoryState = (FactoryState)block.Value;
                        if (type == BlockType.DRONE_STATION)
                        {
                            factoryState.DroneSation = coord;
                        }
                        else if (type == BlockType.ENERGY_POINT)
                        {
                            factoryState.EnergyPoint = coord;
                        }
                    }
                    else if (blockType == BlockType.SOLAR_PANEL)
                    {
                        SolarPanelState solarPanelState = (SolarPanelState)block.Value;
                        if (type == BlockType.ENERGY_POINT)
                        {
                            solarPanelState.EnergyPoint = coord;
                        }
                    }
                    else if (blockType == BlockType.CRYPTOR)
                    {
                        CryptorState cryptorState = (CryptorState)block.Value;
                        if (type == BlockType.ENERGY_POINT)
                        {
                            cryptorState.EnergyPoint = coord;
                        }
                    }
                }
            }
        }

        private static double CalculateDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}