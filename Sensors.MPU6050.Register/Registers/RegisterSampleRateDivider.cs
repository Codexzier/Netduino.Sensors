using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// Die Sensor Eregbnisse (FIFO, DMP, Motion detection) basierend auf die Sample Rate.
    /// Register 25, Sample Rate Divider (SMPRT_DIV)
    /// </summary>
    public class RegisterSampleRateDivider : RegisterBase
    {
        /// <summary>
        /// Legt die Standard Einstellung des Divider fest. 
        /// SMPLRT_DIV = 0 (Off)
        /// </summary>
        public RegisterSampleRateDivider()
            : base()
        {
            this.SMPLRT_DIV = 0;
        }

        public byte _SMPLRT_DIV = 0;

        /// <summary>
        /// Ruft den Divider ab für die Gyroscope Ausgabe oder legt diese fest.
        /// Hier kann ein Wert von 0 bis 255 festgelegt werden.
        /// </summary>
        public byte SMPLRT_DIV {
            get { return this._SMPLRT_DIV; }
            set 
            { 
                this._SMPLRT_DIV = value;
                this.RegisterSetup = this._SMPLRT_DIV != 0 ? RegisterItemUsing.ResgisterSetup : RegisterItemUsing.Disabled;
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
            ba[0] = 0x19;

            // Setup
            ba[1] = SMPLRT_DIV;

            return ba;
        }
    }
}
