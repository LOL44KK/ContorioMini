using Contorio.Core.Types;

namespace Contorio.Core.Factories
{
    public class BlockStateFactory
    {
        public static BlockState CreateBlockState(BlockType type, string name)
        {
            switch (type)
            {
                case BlockType.BLOCK:
                    return new BlockState(name);
                case BlockType.DRONE_STATION:
                    return new BlockState(name);
                case BlockType.DRILL:
                    return new DrillState(name);
                case BlockType.FACTORY:
                    return new FactoryState(name);
                case BlockType.ENERGY_POINT:
                    return new BlockState(name);
                case BlockType.SOLAR_PANEL:
                    return new SolarPanelState(name);
                case BlockType.CRYPTOR:
                    return new CryptorState(name);
                case BlockType.TRANSFER_BEACON:
                    return new TransferBeaconState(name);
                case BlockType.ENERGY_GENERATOR:
                    return new EnergyGeneratorState(name);
                default:
                    throw new ArgumentException($"Unknown block type: {type}");
            }
        }
    }
}
