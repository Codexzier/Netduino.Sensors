using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// Stellt den Register ein für die Bewegungserkennung ein. 
    /// Hiermit lässt sich der Schwellwert Einstellen.
    /// </summary>
    public class RegisterMotionDetectionThreshold : RegisterBase
    {
        /// <summary>
        /// Standard Einstellung für MOT_THR = 0 (OFF)
        /// </summary>
        public RegisterMotionDetectionThreshold()
            : base()
        {

            this.MOT_THR = 0;
        }

        /// <summary>
        /// Register Einstellung für den Schwellwert der Bewegungserkennung.
        /// </summary>
        /// <param name="threshold"></param>
        public RegisterMotionDetectionThreshold(byte threshold)
        {
            this.RegisterSetup = RegisterItemUsing.ResgisterSetup;

            this.MOT_THR = threshold;
        }

        public byte _MOT_THR = 0;

        /// <summary>
        /// Ruft den Schwellwert ab oder legt diesen fest.
        /// Siehe die Spezifikation für mg / LSB.
        /// </summary>
        public byte MOT_THR {
            get { return this._MOT_THR; }
            set
            {
                this._MOT_THR = value;
                this.RegisterSetup = this._MOT_THR != 0 ? RegisterItemUsing.ResgisterSetup : RegisterItemUsing.Disabled;
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
            ba[0] = 0x1f;

            // Setup
            ba[1] = this.MOT_THR;

            return ba;
        }
    }
}
