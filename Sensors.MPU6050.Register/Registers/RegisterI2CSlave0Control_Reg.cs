using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// Slave 0 Contol Reg (Register 38 oder Hex Adresse 0x26)
    /// </summary>
    public class RegisterI2CSlave0Control_REG : RegisterBase
    {
        public RegisterI2CSlave0Control_REG()
            : base()
        {
            this.I2C_SLV0_REG = 0;
        }

        /// <summary>
        /// Ruft das 8 Bit Register ab für Slave 0 Register 
        /// nach/von mit dem der Datentranfer beginnt.
        /// Hinweis: WIrd nicht wie bei anderen Properties als einzelner Bit gestezt,
        /// sondern wird so wie die Adresse angegeben wurde, 
        /// hier auch in die Konfiguration ohne weitere bearbeitung übertragen.
        /// </summary>
        public byte I2C_SLV0_REG { get; set; }

        /// <summary>
        /// Ruft das Register und Setup Byte ab.
        /// </summary>
        /// <returns>Gibt die Bytes zurück.</returns>
        public override byte[] GetRegisterSetup()
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
