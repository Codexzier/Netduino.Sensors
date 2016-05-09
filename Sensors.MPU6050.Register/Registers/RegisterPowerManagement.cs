using System;
using System.Text;
using Sensors.MPU6050.Register.Enums;
using Sensors.Contracts.Interfaces;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// Stellt die Konfiguration zusammen für den Power Management (PWR_MGMT_1) zusammen. 
    /// </summary>
    public class RegisterPowerManagement1 : RegisterBase, IRegisterItem
    {
        /// <summary>
        /// Standard Konstruktor.
        /// Device Reset ist true.
        /// Verwendung des Internen Taktquelle.
        /// </summary>
        public RegisterPowerManagement1()
        {
            this.Device_Reset = true;
            this.Sleep = false;
            this.Cycle = false;
            this.EnableTemperature = true;
            this.ClockSelect = Select_ClockSelectSource.Internal8MHz;
        }

        /// <summary>
        /// Legt die Einstellungen für das Power Management fest.
        /// </summary>
        /// <param name="deviceReset">Alle Register werden zurück gesetzt.</param>
        /// <param name="sleep">Legt den Sensor Schlafen</param>
        /// <param name="cycle"></param>
        /// <param name="enableTemperature"></param>
        /// <param name="clockSelect"></param>
        public RegisterPowerManagement1(bool deviceReset, 
            bool sleep, 
            bool cycle = false, 
            bool enableTemperature = true, 
            Select_ClockSelectSource clockSelect = Select_ClockSelectSource.Internal8MHz)
        {
            this.Device_Reset = deviceReset;
            this.Sleep = sleep;
            this.Cycle = cycle;
            this.EnableTemperature = enableTemperature;
            this.ClockSelect = clockSelect;
        }

        /// <summary>
        /// Ruft den Device Reset ab oder legt diesen fest.
        /// Ist der Device Reset auf true gesetzt, 
        /// werden alle internen Register auf die Standard Werte gesetzt.
        /// </summary>
        public bool Device_Reset { get; set; }

        /// <summary>
        /// Ruft den Schlaf Modus ab oder legt diesen fest.
        /// Ist dieser auf true, wird nach der Ausführung, der Sensor schlafen gelegt.
        /// </summary>
        public bool Sleep { get; set; }

        /// <summary>
        /// Ruft die Cycle ab oder legt diese fest.
        /// Ist Sleep auf true und Cylcle ebenfalls auf true, 
        /// kann der Sensor zwischen Schlafen und Aufwachen, 
        /// eine einzelne Messung von Aktiven Sensoren abrufen.
        /// Hierzu muss die Einstellung LP_WAKE_CTRL festgelegt werden.
        /// </summary>
        public bool Cycle { get; set; }

        /// <summary>
        /// Ruft den Schalter für Temperatur Sensor gesetzt oder legt diesen fest.
        /// Wenn true gesetzt wurde, so ist dann der Sensor für die Temperatur eingeschaltet.
        /// Mit Abschalten des Sensors, verkürzt sich die Zeit die für das Einlesen benötigt wird.
        /// (Im Orignal wird eigentlich Disable wenn True. Ich fand eine Negiertes Ja irrführend.)
        /// </summary>
        public bool EnableTemperature { get; set; }

        /// <summary>
        /// Ruft die Auswahl der Takt Quelle ab oder legt diese fest.
        /// TODO: Definition eintragen.
        /// </summary>
        public Select_ClockSelectSource ClockSelect { get; set; }

        /// <summary>
        /// Ruft das Register und Setup Byte ab.
        /// </summary>
        /// <returns>Gibt die Bytes zurück.</returns>
        public byte[] GetRegisterSetup()
        {
            byte[] ba = new byte[2];

            // Register Byte
            ba[0] = 0x6b;

            // Setup
            ba[1] = 0x00;

            this.SetBit(this.Device_Reset, 7, ref ba[1]);
            this.SetBit(this.Sleep, 6, ref ba[1]);
            this.SetBit(this.Cycle, 5, ref ba[1]);
            
            // Eigentlich Disable, wenn true. Daher invertieren
            this.SetBit(!this.EnableTemperature, 3, ref ba[1]);

            // Clock Selekt
            this.SetBit(((byte)this.ClockSelect & (1 << 2)) != 0, 2, ref ba[1]);
            this.SetBit(((byte)this.ClockSelect & (1 << 1)) != 0, 1, ref ba[1]);
            this.SetBit(((byte)this.ClockSelect & (1 << 0)) != 0, 0, ref ba[1]);

            return ba;
        }

        
    }
}
