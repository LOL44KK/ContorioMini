using System.Drawing;

namespace Contorio.Core.Interfaces
{
    // Для BlockState
    public interface IConnectToDroneStation
    {
        public Point? DroneStation { get; set; }
    }
}
