using System.Drawing;

namespace Contorio
{
    public class BlockState
    {
        private string _name;

        public string Name => _name;
           
        public BlockState(string name)
        {
            _name = name;
        }
    }

    public class DrillState : BlockState
    {
        private Point? _droneStation;
        private Point? _energyPoint;

        public Point? DroneSation
        {
            get { return _droneStation; }
            set { _droneStation = value; }
        }

        public Point? EnergyPoint
        {
            get { return _energyPoint; }
            set { _energyPoint = value; }
        }

        public DrillState(string name, Point? droneStation = null, Point? energyPoint = null) : base(name)
        {
            _droneStation = droneStation;
            _energyPoint = energyPoint;
        }
    }

    public class SolarPanelState : BlockState
    {
        private Point? _energyPoint;

        public Point? EnergyPoint
        {
            get { return _energyPoint; }
            set { _energyPoint = value; }
        }

        public SolarPanelState(string name, Point? energyPoint = null) : base(name)
        {
            _energyPoint = energyPoint;
        }
    }

    public class FactoryState : BlockState
    {
        private Point? _droneStation;
        private Point? _energyPoint;

        public Point? DroneSation
        {
            get { return _droneStation; }
            set { _droneStation = value; }
        }

        public Point? EnergyPoint
        {
            get { return _energyPoint; }
            set { _energyPoint = value; }
        }

        public FactoryState(string name, Point? droneStation = null, Point? energyPoint = null) : base(name)
        {
            _droneStation = droneStation;
            _energyPoint = energyPoint;
        }
    }

    public class CryptorState : BlockState
    {
        private Point? _energyPoint;

        public Point? EnergyPoint
        {
            get { return _energyPoint; }
            set { _energyPoint = value; }
        }

        public CryptorState(string name, Point? energyPoint = null) : base(name)
        {
            _energyPoint = energyPoint;
        }
    }
}
