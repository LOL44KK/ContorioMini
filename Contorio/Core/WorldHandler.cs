using System.Diagnostics;
using System.Drawing;

using Contorio.Core.Types;
using Contorio.Core.Managers;
using Contorio.Core.Interfaces;

namespace Contorio.Core
{
    public class WorldHandler
    {
        private World _world;
        private Stopwatch _stopwatch;
        private double _timeAccumulator;

        public WorldHandler(World world)
        {
            _world = world;
            _stopwatch = new Stopwatch();
            _timeAccumulator = 0;
        }

        public void Tick()
        {
            if (_timeAccumulator > 1000)
            {
                Handle();
                _timeAccumulator = 0;
            }
            _timeAccumulator += _stopwatch.Elapsed.TotalMilliseconds;
            _stopwatch.Restart();
        }

        public void Handle()
        {
            foreach (Planet planet in _world.Planets)
            {
                foreach (var pair in planet.Blocks)
                {
                    BlockHandler(planet, pair.Value, pair.Key);
                }
            }
        }

        private void BlockHandler(Planet planet, BlockState blockState, Point blockCoord)
        {
            ResourceManager resourceManager = ResourceManager.Instance;
            Block block = resourceManager.Blocks[blockState.Name];

            // Проверка на не обрабатываемые типы
            if (
                block.Type == BlockType.ENERGY_POINT ||
                block.Type == BlockType.DRONE_STATION
                )
            {
                return;
            }

            // Проверка на базовые подключения
            if (blockState is IConnectToDroneStation iConnectToDroneStation)
            {
                if (iConnectToDroneStation.DroneStation == null)
                {
                    return;
                }
            }
            if (blockState is IConnectToEnergyPoint iConnectToEnergyPoint)
            {
                if (iConnectToEnergyPoint.EnergyPoint == null)
                {
                    return;
                }
            }

            // Проверка на потребление электричества
            IEnergyInput? energyInput = null;
            if (block is IEnergyInput iEnergyInput)
            {
                if (planet.Energy < iEnergyInput.EnergyInput)
                {
                    return;
                }
                energyInput = iEnergyInput;
            }

            // Надо переписать с использованием паттерна Strategy 
            // Или как вариант в классе Block добавить метод Process
            // Который буедт отвечать за обработку блока

            // Оброботка Блока
            Recipe recipe;
            Random random;
            switch (block.Type)
            {
                case BlockType.SOLAR_PANEL:
                    planet.Energy += ((SolarPanel)block).EnergyOutput;
                    break;
                
                case BlockType.DRILL:
                    GroundState? groundState;
                    if (planet.Ground.TryGetValue(blockCoord, out groundState))
                    {
                        Ground ground = resourceManager.Grounds[groundState.Name];
                        if (ground.Type == GroundType.ORE)
                        {
                            planet.Resources[((Ore)ground).Product] = planet.Resources.GetValueOrDefault(((Ore)ground).Product, 0) + ((Drill)block).Speed;
                        }
                    }
                    break;
                
                case BlockType.FACTORY:
                    recipe = ((Factory)block).Recipe;
                    foreach (var input in recipe.Input)
                    {
                        if (planet.Resources.GetValueOrDefault(input.Key, 0) < input.Value)
                        {
                            return;
                        }
                    }

                    foreach (var input in recipe.Input)
                    {
                        planet.Resources[input.Key] = planet.Resources.GetValueOrDefault(input.Key, 0) - input.Value;
                    }
                    random = new Random();
                    foreach (var output in recipe.Output)
                    {
                        if (random.NextDouble() < output.Value.Chance)
                        {
                            planet.Resources[output.Key] = planet.Resources.GetValueOrDefault(output.Key, 0) + output.Value.Quantity;
                        }
                    }
                    break;
                
                case BlockType.CRYPTOR:
                    foreach (var token in ((Cryptor)block).OutputToken)
                    {
                        _world.Tokens[token.Key] = _world.Tokens.GetValueOrDefault(token.Key, 0) + token.Value;
                    }
                    break;
                
                case BlockType.TRANSFER_BEACON:
                    TransferBeaconState transferBeaconState = (TransferBeaconState)blockState;
                    if (transferBeaconState.Count <= 0 || 
                        transferBeaconState.Planet == -1 || 
                        transferBeaconState.Planet >= _world.Planets.Count || 
                        transferBeaconState.Resource == null)
                    {
                        return;
                    }
                    if (planet.Resources.GetValueOrDefault(transferBeaconState.Resource, 0) >= transferBeaconState.Count)
                    {
                        planet.Resources[transferBeaconState.Resource] -= transferBeaconState.Count;
                        _world.Planets[transferBeaconState.Planet].Resources[transferBeaconState.Resource] = _world.Planets[transferBeaconState.Planet].Resources.GetValueOrDefault(transferBeaconState.Resource, 0) + transferBeaconState.Count;
                    }
                    break;

                case BlockType.ENERGY_GENERATOR:
                    recipe = ((EnergyGenerator)block).Recipe;
                    foreach (var input in recipe.Input)
                    {
                        if (planet.Resources.GetValueOrDefault(input.Key, 0) < input.Value)
                        {
                            return;
                        }
                    }

                    foreach (var input in recipe.Input)
                    {
                        planet.Resources[input.Key] = planet.Resources.GetValueOrDefault(input.Key, 0) - input.Value;
                    }
                    random = new Random();
                    foreach (var output in recipe.Output)
                    {
                        if (random.NextDouble() < output.Value.Chance)
                        {
                            planet.Resources[output.Key] = planet.Resources.GetValueOrDefault(output.Key, 0) + output.Value.Quantity;
                        }
                    }
                    planet.Energy += ((EnergyGenerator)block).EnergyOutput;
                    break;
            }

            // Потребление энергии после обработки
            if (energyInput != null)
            {
                planet.Energy -= energyInput.EnergyInput;
            }
        }
    }
}
