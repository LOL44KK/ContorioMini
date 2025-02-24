﻿using CharEngine;
using Contorio.Core;
using Contorio.Core.Presets;
using Contorio.Core.Types;

namespace Contorio
{
    public class BaseMod : Mod
    {
        public BaseMod()
        {
            Name = "BaseMod";
            Description = "BaseMod";
            Author = "LOL4K";
            Version = "0.7.5";

            Blocks = new List<Block>();
            Grounds = new List<Ground>();
            PlanetPresets = new List<PlanetPreset>();

            //PlanetPreset
            PlanetPreset baseCIRCLEPlanetPreset = new PlanetPreset(
                name: "Base CIRCLE",
                size: 32,
                dirt: "dirt",
                type: PlanetType.CIRCLE,
                ores: new List<OrePreset>()
                {
                    new OrePreset("iron",   0.01,  8, 16),
                    new OrePreset("copper", 0.01,  8, 16),
                    new OrePreset("uran",   0.005, 4, 12),
                }
            );
            PlanetPresets.Add(baseCIRCLEPlanetPreset);

            PlanetPreset baseSQUAREPlanetPreset = new PlanetPreset(
                name: "Base SQUARE",
                size: 32,
                dirt: "dirt",
                type: PlanetType.SQUARE,
                ores: new List<OrePreset>()
                {
                    new OrePreset("iron",   0.01,  8, 16),
                    new OrePreset("copper", 0.01,  8, 16),
                    new OrePreset("uran",   0.005, 4, 12),
                }
            );
            PlanetPresets.Add(baseSQUAREPlanetPreset);

            // Ground
            Ground dirt = new Ground(
                type: GroundType.DIR,
                name: "dirt",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green) },
                    { new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green) },
                    { new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green), new Pixel('.', ConsoleColor.Green) }
                }));
            Grounds.Add(dirt);

            Ore ironOre = new Ore(
                name: "iron",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray) },
                    { new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray) },
                    { new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray), new Pixel('O', ConsoleColor.DarkGray) }
                }),
                product: "iron-ore"
                );
            Grounds.Add(ironOre);

            Ore copperOre = new Ore(
                name: "copper",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow) },
                    { new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow) },
                    { new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('O', ConsoleColor.DarkYellow) }
                }),
                product: "copper-ore"
            );
            Grounds.Add(copperOre);

            Ore uranOre = new Ore(
                name: "uran",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                        { new Pixel('O', ConsoleColor.Green), new Pixel('O', ConsoleColor.Green), new Pixel('O', ConsoleColor.Green), new Pixel('O', ConsoleColor.Green) },
                        { new Pixel('O', ConsoleColor.Green), new Pixel('O', ConsoleColor.Green), new Pixel('O', ConsoleColor.Green), new Pixel('O', ConsoleColor.Green) },
                        { new Pixel('O', ConsoleColor.Green), new Pixel('O', ConsoleColor.Green), new Pixel('O', ConsoleColor.Green), new Pixel('O', ConsoleColor.Green) }
                }),
                product: "uran-ore"
            );
            Grounds.Add(uranOre);



            // Block
            DroneStation droneStation = new DroneStation(
                name: "drone_station",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('%', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel('%', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel(' ', ConsoleColor.White), new Pixel('#', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel('%', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel('%', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) }
                }),
                research: new Research(
                    "drone_station",
                    "logic"
                    ),
                cost: new Dictionary<string, int>() { { "iron", 1 } },
                range: 4
                );
            Blocks.Add(droneStation);

            EnergyPoint energyPoint = new EnergyPoint(
                name: "energy_point",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel(' ', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel(' ', ConsoleColor.DarkYellow) },
                    { new Pixel('*', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow) },
                    { new Pixel(' ', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel('*', ConsoleColor.DarkYellow), new Pixel(' ', ConsoleColor.DarkYellow) }
                }),
                research: new Research(
                    "energy_point",
                    "energy"
                    ),
                cost: new Dictionary<string, int>() { { "iron", 1 } },
                range: 4
                );
            Blocks.Add(energyPoint);

            Drill drill = new Drill(
                name: "drill",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('|', ConsoleColor.White), new Pixel('-', ConsoleColor.White), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel('|', ConsoleColor.White), new Pixel('|', ConsoleColor.White), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel(' ', ConsoleColor.White), new Pixel('*', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) }
                }),
                research: new Research(
                    "drill",
                    "mining"
                    ),
                cost: new Dictionary<string, int>() { { "iron", 1 } },
                speed: 1,
                energyInput: 1
                );
            Blocks.Add(drill);

            Drill drill_MK2 = new Drill(
                name: "drill-MK2",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('|', ConsoleColor.White), new Pixel('-', ConsoleColor.White), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel('|', ConsoleColor.White), new Pixel('|', ConsoleColor.Magenta), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel(' ', ConsoleColor.White), new Pixel('*', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) }
                }),
                research: new Research(
                    "drill-MK2",
                    "mining",
                    "drill",
                    new Dictionary<string, int>() { { "RS", 10000 } }
                    ),
                cost: new Dictionary<string, int>() { { "iron", 1 } },
                speed: 3,
                energyInput: 2
                );
            Blocks.Add(drill_MK2);

            Drill drill_MK3 = new Drill(
                name: "drill-MK3",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('|', ConsoleColor.White), new Pixel('-', ConsoleColor.Magenta), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel('|', ConsoleColor.White), new Pixel('|', ConsoleColor.Magenta), new Pixel('|', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) },
                    { new Pixel(' ', ConsoleColor.White), new Pixel('*', ConsoleColor.White), new Pixel(' ', ConsoleColor.White), new Pixel(' ', ConsoleColor.White) }
                }),
                research: new Research(
                    "drill-MK3",
                    "mining",
                    "drill-MK2",
                    new Dictionary<string, int>() { { "RS", 10000000 } }
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } },
                speed: 8,
                energyInput: 5
            );
            Blocks.Add(drill_MK3);

            SolarPanel solarPanel = new SolarPanel(
                name: "solar_panel",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue) },
                    { new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue) },
                    { new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue) }
                }),
                cost: new Dictionary<string, int>() { { "iron", 1 }, { "copper", 2 } },
                research: new Research(
                    "solar_panel",
                    "energy"
                    ),
                energyOutput: 1
                );
            Blocks.Add(solarPanel);

            SolarPanel solarPanel_MK2 = new SolarPanel(
                name: "solar_panel-MK2",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.DarkBlue) },
                    { new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.Blue) },
                    { new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.Blue), new Pixel('#', ConsoleColor.DarkBlue) }
                }),
                cost: new Dictionary<string, int>() { { "iron", 2 }, { "copper", 5 } },
                research: new Research(
                    "solar_panel-MK2",
                    "energy",
                    "solar_panel",
                    new Dictionary<string, int>() { { "RS", 1000 } }
                    ),
                energyOutput: 2
                );
            Blocks.Add(solarPanel_MK2);

            SolarPanel solarPanel_MK3 = new SolarPanel(
                name: "solar_panel-MK3",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue) },
                    { new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue) },
                    { new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue), new Pixel('#', ConsoleColor.DarkBlue) }
                }),
                research: new Research(
                    "solar_panel-MK3",
                    "energy",
                    "solar_panel-MK2",
                    new Dictionary<string, int>() { { "RS", 100000 } }
                    ),
                cost: new Dictionary<string, int>() { { "iron", 5 }, { "copper", 10 } },
                energyOutput: 4
                );
            Blocks.Add(solarPanel_MK3);

            Factory smelterIron = new Factory(
                name: "smelter-iron",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray) },
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('$', ConsoleColor.DarkRed), new Pixel('O', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) },
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('$', ConsoleColor.DarkRed), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) }
                }),
                research: new Research(
                    "smelter-iron",
                    "factory"
                    ),
                cost: new Dictionary<string, int>() { { "iron", 1 } },
                energyInput: 1,
                recipe: new Recipe(
                    input: new Dictionary<string, int> { { "iron-ore", 1 } },
                    output: new Dictionary<string, (int, float)> { { "iron", (1, 1f) } }
                    )
                );
            Blocks.Add(smelterIron);

            Factory smelterCopper = new Factory(
                name: "smelter-copper",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel(' ', ConsoleColor.DarkGray) },
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('$', ConsoleColor.DarkRed), new Pixel('O', ConsoleColor.DarkYellow), new Pixel('#', ConsoleColor.DarkGray) },
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('$', ConsoleColor.DarkRed), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) }
                }),
                research: new Research(
                    "smelter-copper",
                    "factory"
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } },
                energyInput: 1,
                recipe: new Recipe(
                    input: new Dictionary<string, int> { { "copper-ore", 1 } },
                    output: new Dictionary<string, (int, float)> { { "copper", (1, 1f) } }
                )
            );
            Blocks.Add(smelterCopper);

            Cryptor cryptorRS = new Cryptor(
                name: "cryptor-RS",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('╔', ConsoleColor.DarkCyan), new Pixel('═', ConsoleColor.DarkCyan), new Pixel('═', ConsoleColor.DarkCyan), new Pixel('╗', ConsoleColor.DarkCyan) },
                    { new Pixel('║', ConsoleColor.DarkCyan), new Pixel('╬', ConsoleColor.DarkCyan), new Pixel('╬', ConsoleColor.DarkCyan), new Pixel('║', ConsoleColor.DarkCyan) },
                    { new Pixel('╚', ConsoleColor.DarkCyan), new Pixel('═', ConsoleColor.DarkCyan), new Pixel('═', ConsoleColor.DarkCyan), new Pixel('╝', ConsoleColor.DarkCyan) }
                }),
                research: new Research(
                    "cryptor-RS",
                    "cryptors"
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } },
                energyInput: 1,
                outputToken: new Dictionary<string, int>() { { "RS", 1 } }
            );
            Blocks.Add(cryptorRS);

            Cryptor cryptorPL = new Cryptor(
                name: "cryptor-PL",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('╔', ConsoleColor.DarkYellow), new Pixel('═', ConsoleColor.DarkYellow), new Pixel('═', ConsoleColor.DarkYellow), new Pixel('╗', ConsoleColor.DarkYellow) },
                    { new Pixel('║', ConsoleColor.DarkYellow), new Pixel('╬', ConsoleColor.DarkBlue), new Pixel('╬', ConsoleColor.DarkBlue), new Pixel('║', ConsoleColor.DarkYellow) },
                    { new Pixel('╚', ConsoleColor.DarkYellow), new Pixel('═', ConsoleColor.DarkYellow), new Pixel('═', ConsoleColor.DarkYellow), new Pixel('╝', ConsoleColor.DarkYellow) }
                }),
                research: new Research(
                    "cryptor-PL",
                    "cryptors",
                    null,
                    new Dictionary<string, int>() { { "RS", 1 } }
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } },
                energyInput: 1,
                outputToken: new Dictionary<string, int>() { { "PL", 1 } }
            );
            Blocks.Add(cryptorPL);

            TransferBeacon transferBeacon = new TransferBeacon(
                name: "transfer_beacon",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('╔', ConsoleColor.DarkMagenta), new Pixel('═', ConsoleColor.DarkMagenta), new Pixel('═', ConsoleColor.DarkMagenta), new Pixel('╗', ConsoleColor.DarkMagenta) },
                    { new Pixel('║', ConsoleColor.DarkMagenta), new Pixel(' ', ConsoleColor.DarkMagenta), new Pixel(' ', ConsoleColor.DarkMagenta), new Pixel('║', ConsoleColor.DarkMagenta) },
                    { new Pixel('╚', ConsoleColor.DarkMagenta), new Pixel('═', ConsoleColor.DarkMagenta), new Pixel('═', ConsoleColor.DarkMagenta), new Pixel('╝', ConsoleColor.DarkMagenta) }
                }),
                research: new Research(
                    "transfer_beacon",
                    "logic"
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 } },
                energyInput: 10,
                maxTransferableCount: 10
            );
            Blocks.Add(transferBeacon);

            EnergyGenerator uran_generator = new EnergyGenerator(
                name: "uran_generator",
                pixelCanvas: new PixelCanvas(new Pixel[3, 4]
                {
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) },
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('&', ConsoleColor.Green), new Pixel('&', ConsoleColor.Green), new Pixel('#', ConsoleColor.DarkGray) },
                    { new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray), new Pixel('#', ConsoleColor.DarkGray) }
                }),
                research: new Research(
                    "uran_generator",
                    "energy"
                ),
                cost: new Dictionary<string, int>() { { "iron", 1 }, { "copper", 2 } },
                energyOutput:3,
                recipe: new Recipe(
                    input: new Dictionary<string, int> { { "uran-ore", 1 } },
                    output: new Dictionary<string, (int, float)>()
                )
            );
            Blocks.Add(uran_generator);
        }
    }
}
