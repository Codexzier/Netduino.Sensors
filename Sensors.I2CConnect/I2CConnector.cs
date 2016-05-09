using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Sensors.I2CConnect
{
    public class I2CConnector : I2CDevice
    {
        public I2CConnector() : base(null) { }

        /// <summary>
        /// Legt die Einstellungen für Adresse und Takt Geschwindigkeit fest.
        /// </summary>
        /// <param name="address">Adresse des Moduls.</param>
        /// <param name="clockRate">Verwendete Takt.</param>
        public void SetConfiguration(byte address, int clockRate)
        {
            this.Config = new Configuration(address, clockRate);
        }

        /// <summary>
        /// Senden des zu ansprechenden Registers und 
        /// dessen zu verwendende Einstellung.
        /// </summary>
        /// <param name="register"></param>
        /// <param name="setup"></param>
        /// <returns></returns>
        public bool Write(byte register, byte setup)
        {
            return Write(new byte[] { register, setup });
        }

        /// <summary>
        /// Sendet des Buffer Inhaltes
        /// </summary>
        /// <param name="buffer">Register Byte und Setup Byte.</param>
        /// <returns>True, wenn erfolgreich.</returns>
        public bool Write(byte[] buffer)
        {
            I2CDevice.I2CTransaction[] writeTransaction = new I2CDevice.I2CTransaction[]
            { 
                I2CDevice.CreateWriteTransaction(buffer) 
            };

            return this.Execute(writeTransaction, 1000) != 0;
        }

        /// <summary>
        /// Senden des anzusprechenden Register für den Lesevorgang und
        /// Ausgabe der Ergebnisse über den Buffer.
        /// </summary>
        /// <param name="register">Start Register Byte senden.</param>
        /// <param name="buffer">Wird mit dem Eregbnis beschrieben.</param>
        /// <returns>True, wenn erfolgreich.</returns>
        public bool ReadMeasurements(byte register, ref byte[] buffer)
        {
            I2CDevice.I2CTransaction[] readTransaction = new I2CDevice.I2CTransaction[]
            {     
                I2CDevice.CreateWriteTransaction(new byte[] { register}),
                I2CDevice.CreateReadTransaction(buffer)        
            };

            return this.Execute(readTransaction, 1000) == buffer.Length + 1;
        }
    }
}
