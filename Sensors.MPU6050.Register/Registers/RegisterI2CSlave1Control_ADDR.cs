using System;
using Microsoft.SPOT;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// I²C Slave 1 Control (Register 40, HEX 28)
    /// </summary>
    public class RegisterI2CSlave1Control_ADDR : RegisterBase
    {
        /// <summary>
        /// Legt die Default Werte fest.
        /// RNW = false, ID 1 = 12
        /// </summary>
        public RegisterI2CSlave1Control_ADDR()
            : base()
        {
            this.I2C_SLV1_RNW = false;
            this.I2C_ID_1 = 12;
        }

        /// <summary>
        /// Ruf den Transfer zustand ab, ob dieser lesend oder schreiben ist.
        /// false = Schreiben, true = lesen
        /// </summary>
        public bool I2C_SLV1_RNW { get; set; }

        /// <summary>
        /// Ruft die Adresse des I²C Slave 2 ab oder legt diese fest.
        /// Default Wert ist 12.
        /// </summary>
        public int I2C_ID_1 { get; set; }

        public override byte[] GetRegisterSetup()
        {
            byte[] ba = new byte[2];

            // Adresse
            ba[0] = 0x28;

            // setup
            ba[1] = 0x00;

            this.SetBit(this.I2C_SLV1_RNW, 7, ref ba[1]);

            this.SetValueToBits(this.I2C_ID_1, 6, ref ba[1]);

            return ba;
        }
    }
}
