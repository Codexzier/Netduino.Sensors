using Sensors.Contracts.Enums;
using Sensors.MPU6050.Register.Enums;
using System;
using System.Text;

namespace Sensors.MPU6050.Register
{
    public class AddressChangeToBase : RegisterBase
    {
        public AddressChangeToBase()
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

            // Adresse des MPU Sensors
            ba[0] = 0x68;
            ba[1] = (byte)(this.ClockSpeed == Select_I2C_Common_Clock.ClockSpeed_100 ? 0x00 : 0x01);

            return ba;
        }
    }
}
