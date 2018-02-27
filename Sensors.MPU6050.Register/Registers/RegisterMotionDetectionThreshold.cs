using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// Stellt den Register ein f�r die Bewegungserkennung ein. 
    /// Hiermit l�sst sich der Schwellwert Einstellen.
    /// </summary>
    public class RegisterMotionDetectionThreshold : RegisterBase
    {
        /// <summary>
        /// Standard Einstellung f�r MOT_THR = 0 (OFF)
        /// </summary>
        public RegisterMotionDetectionThreshold()
            : base()
        {

            this.MOT_THR = 0;
        }

        /// <summary>
        /// Register Einstellung f�r den Schwellwert der Bewegungserkennung.
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
        /// Siehe die Spezifikation f�r mg / LSB.
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
        /// <returns>Gibt die Bytes zur�ck.</returns>
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
