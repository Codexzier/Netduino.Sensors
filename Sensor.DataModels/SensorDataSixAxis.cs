using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;

namespace Sensors.Contracts
{
    public class SensorDataSixAxis : ISensorDataSixAxisBase
    {
        public SensorDataSixAxis(byte[] result)
        {
            AccelerationX = (short)(result[0] << 0x08 | result[1]);
            AccelerationY = (short)(result[2] << 0x08 | result[3]);
            AccelerationZ = (short)(result[4] << 0x08 | result[5]);
            Temperatur = (short)(result[6] << 0x08 | result[7]);
            GyroscopeX = (short)(result[8] << 0x08 | result[9]);
            GyroscopeY = (short)(result[10] << 0x08 | result[11]);
            GyroscopeZ = (short)(result[12] << 0x08 | result[13]);
        }
        public int AccelerationX { get; private set; }

        public int AccelerationY { get; private set; }

        public int AccelerationZ { get; private set; }

        public int GyroscopeX { get; private set; }

        public int GyroscopeY { get; private set; }

        public int GyroscopeZ { get; private set; }

        public int Temperatur { get; private set; }

        public override string ToString()
        {
            return "AccX: " + this.AccelerationX + ", " +
                "AccY: " + this.AccelerationY + ", " +
                "AccZ: " + this.AccelerationZ + ", " + 
                "GyrX: " + this.GyroscopeX + ", " +
                "GyrY: " + this.GyroscopeY + ", " + 
                "GyrZ: " + this.GyroscopeZ + ", ";
        }


    }
}
