using System;
using Microsoft.SPOT;
using Sensors.MPU6050.Register.Enums;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    public class AddressChangeToMagnometer : RegisterBase
    {
        public AddressChangeToMagnometer()
            : base(RegisterItemUsing.AddressSetup)
        {
            this.ClockSpeed = Select_I2C_Common_Clock.ClockSpeed_400;
        }

        /// <summary>
        /// Ruft die einzustellende Takt für die I²C Verbindung ab oder legt diese fest.
        /// </summary>
        public Select_I2C_Common_Clock ClockSpeed { get; set; }

        public override byte[] GetRegisterSetup()
        {
            byte[] ba = new byte[2];

            // Adresse des Magnometers
            ba[0] = 0x0C;
            ba[1] = (byte)(this.ClockSpeed == Select_I2C_Common_Clock.ClockSpeed_100 ? 0x00 : 0x01);

            return ba;
        }
    }
}
