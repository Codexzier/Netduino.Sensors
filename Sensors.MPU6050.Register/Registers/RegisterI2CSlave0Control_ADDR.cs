using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// Slae 0 Control Addr (Register 37 oder Hex Adresse 0x25)
    /// </summary>
    public class RegisterI2CSlave0Control_ADDR : RegisterBase
    {
        /// <summary>
        /// Standard Konstruktor.
        /// Inital nicht Enabled.
        /// </summary>
        public RegisterI2CSlave0Control_ADDR()
            : base()
        {
            this.I2C_SLV0_RW = false;
            this.I2C_SLV0_ADDR = 0;
        }

        /// <summary>
        /// Ruft die für den Daten transfer als Lesen Operation ab oder legt diese fest.
        /// </summary>
        public bool I2C_SLV0_RW { get; set; }

        private byte _I2C_SLV0_ADDR;

        /// <summary>
        /// Ruft die Ziel Adresse des Moduls im Slave ab oder legt diese fest.
        /// Adresse darf von '0' bis '127' sein.
        /// </summary>
        public byte I2C_SLV0_ADDR {
            get 
            { 
                return this._I2C_SLV0_ADDR; 
            }
            set
            {
                if(value > 127)
                {
                    throw new ArgumentException("The address can not be over 127.");
                }

                this._I2C_SLV0_ADDR = value;
            }
        }

        /// <summary>
        /// Ruft das Register und Setup Byte ab.
        /// </summary>
        /// <returns>Gibt die Bytes zurück.</returns>
        public override byte[] GetRegisterSetup()
        {
            byte[] ba = new byte[2];

            // Register Byte
            ba[0] = 0x25;

            // Setup
            // Adresse
            ba[1] = this._I2C_SLV0_ADDR;

            // Read operation
            this.SetBit(this.I2C_SLV0_RW, 7, ref ba[1]);

            return ba;
        }
    }
}
