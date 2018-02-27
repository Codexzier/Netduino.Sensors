using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;

namespace Sensors.Contracts
{
    public class ErrorSensorResult : ISensorData
    {
        private string _message;

        public ErrorSensorResult(string message)
        {
            this._message = message;
        }

        public int AccelerationX { get { return -1; } }
        public int AccelerationY { get { return -1; } }
        public int AccelerationZ { get { return -1; } }
        public int GyroscopeX { get { return -1; } }
        public int GyroscopeY { get { return -1; } }
        public int GyroscopeZ { get { return -1; } }
        public int Temperatur { get { return -1; } }

        public override string ToString()
        {
            return "Error: " + this._message;
        }
    }
}
