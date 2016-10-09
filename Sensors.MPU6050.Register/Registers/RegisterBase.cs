using System;
using Microsoft.SPOT;

namespace Sensors.MPU6050.Register
{
    public abstract class RegisterBase
    {
        /// <summary>
        /// Legt das Bit im Byte fest.
        /// </summary>
        /// <param name="bitOn">True wenn das Bit gestzt werden soll.</param>
        /// <param name="bit">Legt die Bitstelle fest.</param>
        /// <param name="setup">Das zu verarbeitende byte.</param>
        protected void SetBit(bool bitOn, byte bit, ref byte setup)
        {
            if (bitOn)
            {
                setup |= (byte)(0x01 << bit);
            }
        }
    }
}
