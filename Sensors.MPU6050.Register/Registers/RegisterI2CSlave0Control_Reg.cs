using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;

namespace Sensors.MPU6050.Register.Registers
{
    public class RegisterI2CSlave0Control_Reg : RegisterBase, IRegisterItem
    {
        /// <summary>
        /// Einstellungen zu dem Register werden verwendet, wenn Enable True gesetzt ist.
        /// </summary>
        public bool Enable { get; set; }

        public RegisterI2CSlave0Control_Reg()
        {
            this.Enable = false;

            this.I2C_SLV0_REG = 0;
        }

        /// <summary>
        /// Ruft das 8 Bit Register ab für Slave 0 Register 
        /// nach/von mit dem der Datentranfer beginnt.
        /// </summary>
        public byte I2C_SLV0_REG { get; set; }

        /// <summary>
        /// Ruft das Register und Setup Byte ab.
        /// </summary>
        /// <returns>Gibt die Bytes zurück.</returns>
        public byte[] GetRegisterSetup()
        {
            byte[] ba = new byte[2];

            // Register Byte
            ba[0] = 0x26;

            // Setup
            ba[1] = 0x00;

            // Register
            ba[1] = this.I2C_SLV0_REG;

            return ba;
        }
    }
}
