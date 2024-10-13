using System.Diagnostics;
using System.Drawing;

namespace Contorio
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

            if (block.Type == BlockType.ENERGY_POINT || block.Type == BlockType.DRONE_STATION)
            {
                return;
            }

            //SolarPanel
            else if (block.Type == BlockType.SOLAR_PANEL && ((SolarPanelState)blockState).EnergyPoint != null)
            {
                planet.Energy += ((SolarPanel)block).EnergyOutput;
            }

            //Drill
            else if (block.Type == BlockType.DRILL && ((DrillState)blockState).DroneStation != null && ((DrillState)blockState).EnergyPoint != null)
            {
                GroundState? groundState;
                if (planet.Ground.TryGetValue(blockCoord, out groundState))
                {
                    Ground ground = resourceManager.Grounds[groundState.Name];
                    if (ground.Type == GroundType.ORE && planet.Energy >= ((Drill)block).EnergyInput)
                    {
                        planet.Resources[((Ore)ground).Product] = planet.Resources.GetValueOrDefault(((Ore)ground).Product, 0) + ((Drill)block).Speed;
                        planet.Energy -= ((Drill)block).EnergyInput;
                    }
                }
            }

            //Factory
            else if (block.Type == BlockType.FACTORY && ((FactoryState)blockState).DroneStation != null && ((FactoryState)blockState).EnergyPoint != null)
            {
                if (planet.Energy < ((Factory)block).EnergyInput)
                {
                    return;
                }
                Recipe recipe = ((Factory)block).Recipe;
                bool work = true;
                foreach (var input in recipe.Input)
                {
                    if (planet.Resources.GetValueOrDefault(input.Key, 0) < input.Value)
                    {
                        work = false;
                        break;
                    }
                }
                if (work)
                {
                    planet.Energy -= ((Factory)block).EnergyInput;
                    foreach (var input in recipe.Input)
                    {
                        planet.Resources[input.Key] = planet.Resources.GetValueOrDefault(input.Key, 0) - input.Value;
                    }
                    foreach (var output in recipe.Output)
                    {
                        planet.Resources[output.Key] = planet.Resources.GetValueOrDefault(output.Key, 0) + output.Value;
                    }
                }
            }

            //Cryptor
            else if (block.Type == BlockType.CRYPTOR && ((CryptorState)blockState).EnergyPoint != null)
            {
                if (planet.Energy < ((Cryptor)block).EnergyInput)
                {
                    return;
                }
                planet.Energy -= ((Cryptor)block).EnergyInput;
                foreach (var token in ((Cryptor)block).OutputToken)
                {
                    _world.Tokens[token.Key] = _world.Tokens.GetValueOrDefault(token.Key, 0) + token.Value;
                }
            }

            //TransferBeacon
            else if (block.Type == BlockType.TRANSFER_BEACON && ((TransferBeaconState)blockState).DroneStation != null && ((TransferBeaconState)blockState).EnergyPoint != null)
            {
                TransferBeaconState transferBeaconState = (TransferBeaconState)blockState;
                if (transferBeaconState.Count <= 0 || transferBeaconState.Planet == -1 || _world.Planets.Count <= transferBeaconState.Planet || transferBeaconState.Resource == null)
                {
                    return;
                }

                if (planet.Energy < ((TransferBeacon)block).EnergyInput)
                {
                    return;
                }
                if (planet.Resources.GetValueOrDefault(transferBeaconState.Resource, 0) >= transferBeaconState.Count)
                {
                    planet.Resources[transferBeaconState.Resource] -= transferBeaconState.Count;
                    _world.Planets[transferBeaconState.Planet].Resources[transferBeaconState.Resource] = _world.Planets[transferBeaconState.Planet].Resources[transferBeaconState.Resource] + transferBeaconState.Count;
                }
                planet.Energy -= ((TransferBeacon)block).EnergyInput;
            }
        }
    }
}
