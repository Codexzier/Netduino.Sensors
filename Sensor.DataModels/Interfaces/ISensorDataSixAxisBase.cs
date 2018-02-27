using System;
using Microsoft.SPOT;

namespace Sensors.Contracts.Interfaces
{
    public interface ISensorData
    {
        int AccelerationX { get; }
        int AccelerationY { get; }
        int AccelerationZ { get; }

        int GyroscopeX { get; }
        int GyroscopeY { get; }
        int GyroscopeZ { get; }

        int Temperatur { get; }
    }
}
