using System.Drawing;
using System.Text.Json.Serialization;

using Contorio.Core.Interfaces;

namespace Contorio.Core.Types
{
    // Используется для хранение состояния блока
    [JsonDerivedType(typeof(DrillState), "DrillState")]
    [JsonDerivedType(typeof(FactoryState), "FactoryState")]
    [JsonDerivedType(typeof(CryptorState), "CryptorState")]
    [JsonDerivedType(typeof(SolarPanelState), "SolarPanelState")]
    [JsonDerivedType(typeof(TransferBeaconState), "TransferBeaconState")]
    [JsonDerivedType(typeof(EnergyGeneratorState), "EnergyGeneratorState")]
    public class BlockState
    {
        private string _name;

        public string Name 
        { 
            get { return _name; }
            init { _name = value; }
        }
           
        public BlockState(string name)
        {
            _name = name;
        }
    }

    public class DrillState : BlockState, IConnectToDroneStation, IConnectToEnergyPoint
    {
        private Point? _droneStation;
        private Point? _energyPoint;

        public Point? EnergyPoint
        {
            get { return _energyPoint; }
            set { _energyPoint = value; }
        }

        public Point? DroneStation
        {
            get { return _droneStation; }
            set { _droneStation = value; }
        }

        public DrillState(string name, Point? droneStation = null, Point? energyPoint = null) : base(name)
        {
            _droneStation = droneStation;
            _energyPoint = energyPoint;
        }
    }

    public class SolarPanelState : BlockState, IConnectToEnergyPoint
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

    public class FactoryState : BlockState, IConnectToDroneStation, IConnectToEnergyPoint
    {
        private Point? _droneStation;
        private Point? _energyPoint;

        public Point? DroneStation
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

    public class CryptorState : BlockState, IConnectToEnergyPoint
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

    public class TransferBeaconState : BlockState, IConnectToDroneStation, IConnectToEnergyPoint
    {
        private Point? _droneStation;
        private Point? _energyPoint;
        private int _planet;
        private string? _resource;
        private int _count;

        public Point? DroneStation
        {
            get { return _droneStation; }
            set { _droneStation = value; }
        }

        public Point? EnergyPoint
        {
            get { return _energyPoint; }
            set { _energyPoint = value; }
        }

        public int Planet
        {
            get { return _planet; }
            set { _planet = value; }
        }

        public string? Resource
        {
            get { return _resource;  }
            set { _resource = value; }
        }

        public int Count 
        { 
            get { return _count;}
            set { _count = value; }
        }

        public TransferBeaconState(string name, Point? droneStation = null, Point? energyPoint = null, int planet = -1, string? resource = null, int count = 0) : base(name)
        {
            _droneStation = droneStation;
            _energyPoint = energyPoint;
            _planet = planet;
            _resource = resource;
            _count = count;
        }
    }

    public class EnergyGeneratorState : BlockState, IConnectToEnergyPoint, IConnectToDroneStation
    {
        private Point? _droneStation;
        private Point? _energyPoint;

        public Point? DroneStation
        {
            get { return _droneStation; }
            set { _droneStation = value; }
        }

        public Point? EnergyPoint
        {
            get { return _energyPoint; }
            set { _energyPoint = value; }
        }

        public EnergyGeneratorState(string name, Point? droneStation = null, Point? energyPoint = null) : base(name)
        {
            _droneStation = droneStation;
            _energyPoint = energyPoint;
        }
    }
}
