using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.MPU6050.Register.Enums;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// Register 36 - I²C Master Control
    /// </summary>
    public class RegisterI2CMasterControl : RegisterBase
    {
        /// <summary>
        /// Festlegen der Standard Einstellungen.
        /// Legt die Takt Geschwindigkeit auf 400kHz fest.
        /// </summary>
        public RegisterI2CMasterControl() : base()
        {
            this.MULT_MST_EN = false;
            this.WAIT_FOR_ES = false;
            this.SLV_3_FIFO_EN = false;
            this.I2C_MST_P_NSR = false;
            this.I2C_MST_CLK = Select_I2C_MST_CLK.ClockSpeed_400kHz_Div20;
        }

        /// <summary>
        /// Ruft die Multi-Master ab.
        /// TODO: Bessere Erläuterung notwendig.
        /// Ermöglicht Funktion für Multi Master Fähigkeit.
        /// </summary>
        public bool MULT_MST_EN { get; set; }

        /// <summary>
        /// Ruft die Únerbrechungsverzögerung für Vollständige Daten ab oder legt diese fest.
        /// Wenn dieser eingestellt ist, wird auf die externe Sensordaten gewartet. (EXT_SENS_DATA).
        /// </summary>
        public bool WAIT_FOR_ES { get; set; }

        /// <summary>
        /// Ruft die möglichkeit über Slave 3 den FIFO zuschreiben ab oder legt diesen fest.
        /// TODO: Probieren und Erläutern.
        /// </summary>
        public bool SLV_3_FIFO_EN { get; set; }

        /// <summary>
        /// Ruft die Übergangssteuerung von Slave zu Slave ab oder legt diese fest.
        /// TODO: Probieren und genauer erläutern.
        /// Mit False wird zwischen dem lesen neu gestartet.
        /// Mit true haltet der Vorgang und geht dann zu dem nächsten.
        /// </summary>
        public bool I2C_MST_P_NSR { get; set; }

        /// <summary>
        /// Ruft die Takt Geschwindigkeit für den I²C Master ab oder legt diese fest.
        /// </summary>
        public Select_I2C_MST_CLK I2C_MST_CLK { get; set; }

        /// <summary>
        /// Ruft das Register und Setup Byte ab.
        /// </summary>
        /// <returns>Gibt die Bytes zurück.</returns>
        public override byte[] GetRegisterSetup()
        {
            byte[] ba = new byte[2];

            // Register Byte
            // I²C Master Control
            ba[0] = 0x24;

            // Setup
            ba[1] = 0x00;

            // Multi 
            this.SetBit(this.MULT_MST_EN, 7, ref ba[1]);
            this.SetBit(this.WAIT_FOR_ES, 6, ref ba[1]);
            this.SetBit(this.SLV_3_FIFO_EN, 5, ref ba[1]);
            this.SetBit(this.I2C_MST_P_NSR, 4, ref ba[1]);

            // I²C Master Clock und Devider einstellen.
            this.SetBit(((byte)this.I2C_MST_CLK & (1 << 3)) != 0, 3, ref ba[1]);
            this.SetBit(((byte)this.I2C_MST_CLK & (1 << 2)) != 0, 2, ref ba[1]);
            this.SetBit(((byte)this.I2C_MST_CLK & (1 << 1)) != 0, 1, ref ba[1]);
            this.SetBit(((byte)this.I2C_MST_CLK & (1 << 0)) != 0, 0, ref ba[1]);

            return ba;
        }
    }
}
