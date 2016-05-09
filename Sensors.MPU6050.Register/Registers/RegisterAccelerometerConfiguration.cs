using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.MPU6050.Register.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    public class RegisterAccelerometerConfiguration : RegisterBase, IRegisterItem
    {
        public RegisterAccelerometerConfiguration()
        {
            this.XA_ST = false;
            this.YA_ST = false;
            this.ZA_ST = false;

            this.AFS_SEL = Select_AFS_SEL.R08;
        }

        /// <summary>
        /// Ruft die Einstellung für den Selbsttest an der X Achse ab oder legt diesen fest.
        /// TODO: Selftest genauer erläutern
        /// </summary>
        public bool XA_ST { get; set; }

        /// <summary>
        /// Ruft die Einstellung für den Selbsttest an der Y Achse ab oder legt diesen fest.
        /// TODO: Selftest genauer erläutern
        /// </summary>
        public bool YA_ST { get; set; }

        /// <summary>
        /// Ruft die Einstellung für den Selbsttest an der Z Achse ab oder legt diesen fest.
        /// TODO: Selftest genauer erläutern
        /// </summary>
        public bool ZA_ST { get; set; }

        /// <summary>
        /// Ruft die Auswahl für die Messung der Beschleunigung ab oder legt diese fest.
        /// Die Angaben sind jeweils in G für Beschleunigung.
        /// </summary>
        public Select_AFS_SEL AFS_SEL { get; set; }

        /// <summary>
        /// Ruft das Register und Setup Byte ab.
        /// </summary>
        /// <returns>Gibt die Bytes zurück.</returns>
        public byte[] GetRegisterSetup()
        {
            byte[] ba = new byte[2];

            // Register Byte
            ba[0] = 0x1b;

            // Setup
            ba[1] = 0x00;

            this.SetBit(this.XA_ST, 7, ref ba[1]);
            this.SetBit(this.YA_ST, 6, ref ba[1]);
            this.SetBit(this.ZA_ST, 5, ref ba[1]);

            // AFS_SEL
            this.SetBit(((byte)this.AFS_SEL & (1 << 4)) != 0, 4, ref ba[1]);
            this.SetBit(((byte)this.AFS_SEL & (1 << 3)) != 0, 3, ref ba[1]);

            return ba;
        }
    }
}
