using System;
using Microsoft.SPOT;
using Sensors.I2CConnect;
using Sensors.MPU6050.Register;
using Sensors.MPU6050.Register.Enums;
using Sensors.Contracts.Interfaces;
using Sensors.Contracts;

namespace Sensors.MPU6050
{
    /// <summary>
    /// Mit der MPU6050 Klasse kann eine Verbindung zu dem Sensor hergestellt werden und
    /// dessen Messungen ausgelesen werden.
    /// </summary>
    public class MPU6050 : IMPU6050
    {
        private I2CConnector _I2CConnector;
        private static MPU6050 _mpu6050;

        /// <summary>
        /// Standard Konstruktor.
        /// </summary>
        /// <param name="i2cConnector">Verbindungs Instanz.</param>
        private MPU6050(I2CConnector i2cConnector)
        {
            this._I2CConnector = i2cConnector;
        }

        /// <summary>
        /// Ruft eine Instance von MPU6050.
        /// </summary>
        /// <param name="init">Initialisiert den Sensor mit Standard Einstellungen.</param>
        /// <returns>Gibt die Instanz von MPU6050 zurück.</returns>
        public static MPU6050 GetInstance(bool init = true, IRegisterConfiguration regConfig = null)
        {
            if(_mpu6050 == null)
            {
                _mpu6050 = new MPU6050(new I2CConnector());
                
                if (init)
                {
                    _mpu6050.Init(regConfig);
                }
            }

            return _mpu6050;
        }

        /// <summary>
        /// Ruft eine Instance von MPU6050.
        /// Geeigent für die Nutzung mehrer I2C Module.
        /// </summary>
        /// <param name="i2c">Benötigt die Reference Instanz für die I2C Verbindung.</param>
        /// <param name="init">Initialisiert den Sensor mit Standard Einstellungen.</param>
        /// <returns>Gibt die Instanz von MPU6050 zurück.</returns>
        public static MPU6050 GetInstance(I2CConnector i2c, bool init = true, IRegisterConfiguration regConfig = null)
        {
            if(_mpu6050 == null)
            {
                _mpu6050 = new MPU6050(i2c);
                
                if (init)
                {
                    _mpu6050.Init(regConfig);
                }
            }

            return _mpu6050;
        }

        /// <summary>
        /// Initialisieren des Sensors mit den Festgelegten Einstellungen.
        /// </summary>
        /// <param name="regConfig">Zusammenstellung der festgelegten Einstellungen.</param>
        private void Init(IRegisterConfiguration regConfig)
        {
            if(regConfig == null)
            {
               regConfig = new RegisterManagement(RegisterManagementSetup.Default);
            }

            // Konfiguration des Master Einheit
            // Einstellung für Beschleunigung, 
            // Gyroskop, und Temperatur Sensor
            this._I2CConnector.SetConfiguration(regConfig.Address, regConfig.ClockRate);

            foreach (var item in regConfig.RegisterSequence)
            {
                if (item != null && item.Enable)
                {
                    this._I2CConnector.Write(item.GetRegisterSetup());
                }
            }
        }

        /// <summary>
        /// Ruft den Sensor ab und gibt das Ergebnis zurück.
        /// </summary>
        /// <returns>Gibt das Ergebnis zurück.</returns>
        public ISensorDataSixAxisBase GetMeasurements()
        {
            byte[] result = new byte[14];
            if (this._I2CConnector.ReadMeasurements(0x3B, ref result))
            {
                return new SensorDataSixAxis(result);
            }

            return null;
        }
    }
}
