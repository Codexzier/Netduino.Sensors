using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.MPU6050.Register.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    public class RegisterGyroscopeConfiguration : RegisterBase, IRegisterItem
    {
        public RegisterGyroscopeConfiguration()
        {
            this.XG_ST = false;
            this.YG_ST = false;
            this.ZG_ST = false;

            this.FS_SEL = Select_FS_SEL.R2000;
        }

        /// <summary>
        /// Ruft die Einstellung für den Selbsttest an der Z Achse ab oder legt diesen fest.
        /// TODO: Selftest genauer erläutern
        /// </summary>
        public bool XG_ST { get; set; }

        /// <summary>
        /// Ruft die Einstellung für den Selbsttest an der Y Achse ab oder legt diesen fest.
        /// TODO: Selftest genauer erläutern
        /// </summary>
        public bool YG_ST { get; set; }

        /// <summary>
        /// Ruft die Einstellung für den Selbsttest an der Z Achse ab oder legt diesen fest.
        /// TODO: Selftest genauer erläutern
        /// </summary>
        public bool ZG_ST { get; set; }

        /// <summary>
        /// Ruft die Auswahl für Grad pro Sekunde ab oder legt diese fest.
        /// Die Angeban stehen für +- xxxx °/s.
        /// </summary>
        public Select_FS_SEL FS_SEL { get; set; }

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

            this.SetBit(this.XG_ST, 7, ref ba[1]);
            this.SetBit(this.YG_ST, 6, ref ba[1]);
            this.SetBit(this.ZG_ST, 5, ref ba[1]);

            // FS_SEL
            this.SetBit(((byte)this.FS_SEL & (1 << 4)) != 0, 4, ref ba[1]);
            this.SetBit(((byte)this.FS_SEL & (1 << 3)) != 0, 3, ref ba[1]);

            return ba;
        }
    }
}
