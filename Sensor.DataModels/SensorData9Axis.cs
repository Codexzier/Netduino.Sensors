using System;
using Microsoft.SPOT;

namespace Sensors.Contracts
{
    public class SensorData9Axis : SensorData6Axis
    {
        public SensorData9Axis(byte[] result)
            :base(result)
        {
            // TODO: Prüfen was mit Byte 14 war
            this.MagnometerX = (short)(result[15] << 0x08 | result[16]);
            this.MagnometerX = (short)(result[17] << 0x08 | result[18]);
            this.MagnometerX = (short)(result[19] << 0x08 | result[20]);
        }

        public int MagnometerX { get; private set; }
        public int MagnometerY { get; private set; }
        public int MagnometerZ { get; private set; }

        public override string ToString()
        {
            return base.ToString() + ", " +
                this.MagnometerX + ", " + 
                this.MagnometerY + ", " + 
                this.MagnometerZ + " ";
        }
    }
}
