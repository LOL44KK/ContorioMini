using Contorio.CharGraphics;

namespace Contorio
{
    //Singleton
    public class ResourceManager
    {
        private static ResourceManager _instance;
        public static ResourceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ResourceManager();
                }
                return _instance;
            }
        }

        private Dictionary<string, Block> _blocks;
        private Dictionary<string, Ground> _grounds;
        
        private TileSet _tileSet;
        private Dictionary<string, int> _tileIds;

        public Dictionary<string, Block> Blocks { get { return _blocks; } }
        public Dictionary<string, Ground> Grounds { get { return _grounds; } }
        
        public TileSet TileSet { get { return _tileSet; } }
        public IReadOnlyDictionary<string, int> TileIds 
        { 
            get { return _tileIds; } 
        }

        private ResourceManager()
        {
            _blocks = new Dictionary<string, Block>();
            _grounds = new Dictionary<string, Ground>();
            _tileSet = new TileSet(4, 3);
            _tileIds = new Dictionary<string, int>();

            Ground dirt = new Ground(
                type: GroundType.DIR,
                name: "dirt",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green) },
                    { new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green) },
                    { new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green) }
                }));
            _grounds.Add(dirt.Name, dirt);

            Ore ironOre = new Ore(
                name: "iron",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray) },
                    { new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray) },
                    { new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray) }
                }),
                product: "iron-ore"
                );
            _grounds.Add(ironOre.Name, ironOre);

            Ore copperOre = new Ore(
                name: "copper",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow) },
                    { new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow) },
                    { new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow) }
                }),
                product: "copper-ore"
            );
            _grounds.Add(copperOre.Name, copperOre);

            DroneStation droneStation = new DroneStation(
                name: "drone_station",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel('%', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel('%', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel(' ', ConsoleColor.White), new Pixel('#', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel('%', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel('%', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) }
                }),
                range: 4,
                research: new Research(
                    "drone_station",
                    "logic",
                    null,
                    new Dictionary<string, int>()
                ),
                cost: new Dictionary<string, int>() { { "iron", 1} }
                );
            _blocks.Add(droneStation.Name, droneStation);

            EnergyPoint energyPoint = new EnergyPoint(
                name: "energy_point",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel(' ', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel(' ', ConsoleColor.DarkYellow) },
                    { new Pixel('*', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow) },
                    { new Pixel(' ', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel(' ', ConsoleColor.DarkYellow) }
                }),
                range: 4,
                research: new Research(
                    "energy_point",
                    "energy",
                    null,
                    new Dictionary<string, int>()
                ),
                cost: new Dictionary<string, int>() { { "copper", 5 } }
                );
            _blocks.Add(energyPoint.Name, energyPoint);

            Drill drill = new Drill(
                name: "drill",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel('|', ConsoleColor.White), new Pixel('-', ConsoleColor.White), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel('|', ConsoleColor.White), new Pixel('|', ConsoleColor.White), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel(' ', ConsoleColor.White), new Pixel('*', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) }
                }),
                speed: 1,
                energyInput: 1,
                research: new Research(
                    "drill",
                    "mining",
                    null,
                    new Dictionary<string, int>()
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } }
                );
            _blocks.Add(drill.Name, drill);

            Drill drill_MK2 = new Drill(
                name: "drill-MK2",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel('|', ConsoleColor.White), new Pixel('-', ConsoleColor.White), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel('|', ConsoleColor.White), new Pixel('|', ConsoleColor.Magenta), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel(' ', ConsoleColor.White), new Pixel('*', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) }
                }),
                speed: 3,
                energyInput: 2,
                research: new Research(
                    "drill-MK2",
                    "mining",
                    "drill",
                    new Dictionary<string, int>() { {"RS", 10000} }
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } }
                );
            _blocks.Add(drill_MK2.Name, drill_MK2);

            Drill drill_MK3 = new Drill(
                name: "drill-MK3",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel('|', ConsoleColor.White), new Pixel('-', ConsoleColor.Magenta), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel('|', ConsoleColor.White), new Pixel('|', ConsoleColor.Magenta), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel(' ', ConsoleColor.White), new Pixel('*', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) }
                }),
                speed: 8,
                energyInput: 5,
                research: new Research(
                    "drill-MK3",
                    "mining",
                    "drill-MK2",
                    new Dictionary<string, int>() { { "RS", 10000000 } }
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } }
            );
            _blocks.Add(drill_MK3.Name, drill_MK3);

            SolarPanel solarPanel = new SolarPanel(
                name: "solar_panel",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue) },
                    { new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue) },
                    { new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue) }
                }),
                energyOutput: 1,
                research: new Research(
                    "solar_panel",
                    "energy",
                    null,
                    new Dictionary<string, int>()
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 }, { "copper", 2 } }
                );
            _blocks.Add(solarPanel.Name, solarPanel);

            SolarPanel solarPanel_MK2 = new SolarPanel(
                name: "solar_panel-MK2",
                sprite: new Sprite(new Pixel[3, 4]
                {
                                { new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.DarkBlue) },
                                { new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.Blue) },
                                { new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.DarkBlue) }
                }),
                energyOutput: 2,
                research: new Research(
                    "solar_panel-MK2",
                    "energy",
                    "solar_panel",
                    new Dictionary<string, int>() { { "RS", 1000 } }
                ),
                cost: new Dictionary<string, int>() { { "iron", 2 }, { "copper", 5 } }
                );
            _blocks.Add(solarPanel_MK2.Name, solarPanel_MK2);
            
            SolarPanel solarPanel_MK3 = new SolarPanel(
                name: "solar_panel-MK3",
                sprite: new Sprite(new Pixel[3, 4]
                {
                                            { new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue) },
                                            { new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue) },
                                            { new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue) }
                }),
                energyOutput: 4,
                research: new Research(
                    "solar_panel-MK3",
                    "energy",
                    "solar_panel-MK2",
                    new Dictionary<string, int>() { { "RS", 100000 } }
                ),
                cost: new Dictionary<string, int>() { { "iron", 5 }, { "copper", 10 } }
                );
            _blocks.Add(solarPanel_MK3.Name, solarPanel_MK3);

            Factory smelterIron = new Factory(
                name: "smelter-iron",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray) },
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('$', ConsoleColor.DarkRed), new Pixel('O', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) },
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('$', ConsoleColor.DarkRed), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) }
                }),
                energyInput: 1,
                recipe: new Recipe(
                    input: new Dictionary<string, int> { { "iron-ore", 1 } },
                    output: new Dictionary<string, int> { { "iron", 1 } }
                    ),
                research: new Research(
                    "smelter-iron",
                    "factory",
                    null,
                    new Dictionary<string, int>()
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } }
                );
            _blocks.Add(smelterIron.Name, smelterIron);

            Factory smelterCopper = new Factory(
                name: "smelter-copper",
                sprite: new Sprite(new Pixel[3, 4]
                {
                                { new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray) },
                                { new Pixel('#', ConsoleColor.DarkGray), new Pixel('$', ConsoleColor.DarkRed), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('#', ConsoleColor.DarkGray) },
                                { new Pixel('#', ConsoleColor.DarkGray), new Pixel('$', ConsoleColor.DarkRed), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) }
                }),
                energyInput: 1,
                recipe: new Recipe(
                    input: new Dictionary<string, int> { { "copper-ore", 1 } },
                    output: new Dictionary<string, int> { { "copper", 1 } }
                ),
                research: new Research(
                    "smelter-copper",
                    "factory",
                    null,
                    new Dictionary<string, int>()
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } }
            );
            _blocks.Add(smelterCopper.Name, smelterCopper);

            Cryptor cryptorRS = new Cryptor(
                name: "cryptor-RS",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel('╔', ConsoleColor.DarkCyan), new Pixel('═', ConsoleColor.DarkCyan), new Pixel('═', ConsoleColor.DarkCyan), new Pixel('╗', ConsoleColor.DarkCyan) },
                    { new Pixel('║', ConsoleColor.DarkCyan), new Pixel('╬', ConsoleColor.DarkCyan), new Pixel('╬', ConsoleColor.DarkCyan), new Pixel('║', ConsoleColor.DarkCyan) },
                    { new Pixel('╚', ConsoleColor.DarkCyan), new Pixel('═', ConsoleColor.DarkCyan), new Pixel('═', ConsoleColor.DarkCyan), new Pixel('╝', ConsoleColor.DarkCyan) }
                }),
                energyInput: 1,
                outputToken: new Dictionary<string, int>() { { "RS", 1 } },
                research: new Research(
                    "cryptor-RS",
                    "cryptors",
                    null,
                    new Dictionary<string, int>()
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } }
            );
            _blocks.Add(cryptorRS.Name, cryptorRS);

            Cryptor cryptorPL = new Cryptor(
                name: "cryptor-PL",
                sprite: new Sprite(new Pixel[3, 4]
                {
                                { new Pixel('╔', ConsoleColor.DarkYellow), new Pixel('═', ConsoleColor.DarkYellow), new Pixel('═', ConsoleColor.DarkYellow), new Pixel('╗', ConsoleColor.DarkYellow) },
                                { new Pixel('║', ConsoleColor.DarkYellow), new Pixel('╬', ConsoleColor.DarkBlue), new Pixel('╬', ConsoleColor.DarkBlue), new Pixel('║', ConsoleColor.DarkYellow) },
                                { new Pixel('╚', ConsoleColor.DarkYellow), new Pixel('═', ConsoleColor.DarkYellow), new Pixel('═', ConsoleColor.DarkYellow), new Pixel('╝', ConsoleColor.DarkYellow) }
                }),
                energyInput: 1,
                outputToken: new Dictionary<string, int>() { { "PL", 1 } },
                research: new Research(
                    "cryptor-PL",
                    "cryptors",
                    null,
                    new Dictionary<string, int>() { { "RS", 1} }
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } }
            );
            _blocks.Add(cryptorPL.Name, cryptorPL);

            TransferBeacon transferBeacon = new TransferBeacon(
                name: "transfer_beacon",
                sprite: new Sprite(new Pixel[3, 4]
                {
                    { new Pixel('╔', ConsoleColor.DarkMagenta), new Pixel('═', ConsoleColor.DarkMagenta), new Pixel('═', ConsoleColor.DarkMagenta), new Pixel('╗', ConsoleColor.DarkMagenta) },
                    { new Pixel('║', ConsoleColor.DarkMagenta), new Pixel(' ', ConsoleColor.DarkMagenta), new Pixel(' ', ConsoleColor.DarkMagenta), new Pixel('║', ConsoleColor.DarkMagenta) },
                    { new Pixel('╚', ConsoleColor.DarkMagenta), new Pixel('═', ConsoleColor.DarkMagenta), new Pixel('═', ConsoleColor.DarkMagenta), new Pixel('╝', ConsoleColor.DarkMagenta) }
                }),
                research: new Research(
                    "transfer_beacon",
                    "logic",
                    null,
                    new Dictionary<string, int>()
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } },
                energyInput: 1,
                maxTransferableCount: 10
            );
            _blocks.Add(transferBeacon.Name, transferBeacon);

            InitializeTileSet();
        }

        private void InitializeTileSet()
        {
            _tileSet = new TileSet(4, 3);
            _tileIds.Clear();

            foreach (Ground ground in _grounds.Values)
            {
                _tileSet.AddTile(ground.Sprite);
                _tileIds[ground.Name] = _tileSet.Tiles.Count - 1;
            }
            foreach (Block block in _blocks.Values)
            {
                _tileSet.AddTile(block.Sprite);
                _tileIds[block.Name] = _tileSet.Tiles.Count - 1;
            }
        }
    }
}
