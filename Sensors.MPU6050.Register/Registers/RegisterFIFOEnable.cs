using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// Stellt die Konfiguration zusammen für FIFO Enable (FIFO_EN).
    /// Mit diesem Register lässt sich festlegen weleche Sensormessungen in den FIFO Puffer geladen werden.
    /// </summary>
    public class RegisterFIFOEnable : RegisterBase
    {
        /// <summary>
        /// Standard Konstruktor.
        /// Register ist initial nicht enabled;
        /// </summary>
        public RegisterFIFOEnable()
            : base()
        {
            this.TEMP_FIFO_EN = false;

            this.XG_FIFO_EN = false;
            this.YG_FIFO_EN = false; 
            this.ZG_FIFO_EN = false; 

            this.ACCEL_FIFO_EN = false;

            this.SLV2_FIFO_EN = false;
            this.SLV1_FIFO_EN = false;
            this.SLV0_FIFO_EN = false;
        }

        /// <summary>
        /// Ruft den Enable FIFO Puffer für die Temperatur Ergebnisse oder legt diese fest.
        /// Hinweis Abruf register ist 65 und 66.
        /// </summary>
        public bool TEMP_FIFO_EN { get; set; }

        /// <summary>
        /// Ruft den Enable FIFO Puffer für die Gyroskop Achse X Ergebnisse oder legt diese fest.
        /// Hinweis Abruf register ist 67 und 68.
        /// </summary>
        public bool XG_FIFO_EN { get; set; }

        /// <summary>
        /// Ruft den Enable FIFO Puffer für die Gyroskop Achse Y Ergebnisse oder legt diese fest.
        /// Hinweis Abruf register ist 69 und 70.
        /// </summary>
        public bool YG_FIFO_EN { get; set; }

        /// <summary>
        /// Ruft den Enable FIFO Puffer für die Gyroskop Achse Z Ergebnisse oder legt diese fest.
        /// Hinweis Abruf register ist 71 und 72.
        /// </summary>
        public bool ZG_FIFO_EN { get; set; }

        /// <summary>
        /// Ruft den Enable FIFO Puffer für die Beschlunigungsachsen Ergebnisse oder legt diese fest. 
        /// Hinweis Abruf register ist 59 bis 64.
        /// </summary>
        public bool ACCEL_FIFO_EN { get; set; }

        /// <summary>
        /// Ruft den Enable FIFO Puffer für die Externen Ergebnisse (EXT_SENS_DATA) oder legt diese fest. 
        /// Hinweis Abruf register ist 73 bis 96.
        /// </summary>
        public bool SLV2_FIFO_EN { get; set; }

        /// <summary>
        /// Ruft den Enable FIFO Puffer für die Externen Ergebnisse (EXT_SENS_DATA) oder legt diese fest. 
        /// Hinweis Abruf register ist 73 bis 96.
        /// </summary>
        public bool SLV1_FIFO_EN { get; set; }

        /// <summary>
        /// Ruft den Enable FIFO Puffer für die Externen Ergebnisse (EXT_SENS_DATA) oder legt diese fest. 
        /// Hinweis Abruf register ist 73 bis 96.
        /// </summary>
        public bool SLV0_FIFO_EN { get; set; }

        /// <summary>
        /// Ruft das Register und Setup Byte ab.
        /// </summary>
        /// <returns>Gibt die Bytes zurück.</returns>
        public override byte[] GetRegisterSetup()
        {
            byte[] ba = new byte[2];

            // Register Byte
            ba[0] = 0x23;

            // Setup
            ba[1] = 0x00;

            // Temperatur
            this.SetBit(this.TEMP_FIFO_EN, 7, ref ba[1]);

            // Gyroskope
            this.SetBit(this.XG_FIFO_EN, 6, ref ba[1]);
            this.SetBit(this.YG_FIFO_EN, 5, ref ba[1]);
            this.SetBit(this.ZG_FIFO_EN, 4, ref ba[1]);

            // Beschleunigungsachen
            this.SetBit(this.ACCEL_FIFO_EN, 3, ref ba[1]);

            // Externe Sensor Ergebnisse
            this.SetBit(this.SLV2_FIFO_EN, 2, ref ba[1]);
            this.SetBit(this.SLV1_FIFO_EN, 1, ref ba[1]);
            this.SetBit(this.SLV0_FIFO_EN, 0, ref ba[1]);

            return ba;
        }
    }
}
