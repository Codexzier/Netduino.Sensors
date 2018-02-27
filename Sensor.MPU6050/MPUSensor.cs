using System;
using Microsoft.SPOT;
using Sensors.I2CConnect;
using Sensors.MPU6050.Register;
using Sensors.MPU6050.Register.Enums;
using Sensors.Contracts.Interfaces;
using Sensors.Contracts;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050
{
    /// <summary>
    /// Mit der MPU6050 Klasse kann eine Verbindung zu dem Sensor hergestellt werden und
    /// dessen Messungen ausgelesen werden.
    /// </summary>
    public class MPUSensor : IMPU
    {
        private static IMPU _mpu;
        private I2CConnector _I2CConnector;
        private SensorType _sensorType;

        /// <summary>
        /// Standard Konstruktor.
        /// </summary>
        /// <param name="i2cConnector">Verbindungs Instanz.</param>
        private MPUSensor(SensorType sensorType, I2CConnector i2cConnector)
        {
            this._sensorType = sensorType;
            this._I2CConnector = i2cConnector;
        }

        /// <summary>
        /// Ruft eine Instance von MPU6050.
        /// </summary>
        /// <param name="init">Initialisiert den Sensor mit Standard Einstellungen.</param>
        /// <returns>Gibt die Instanz von MPU6050 zurück.</returns>
        public static IMPU GetInstance(SensorType sensorType, bool init = true, IRegisterConfiguration regConfig = null)
        {
            if(_mpu == null)
            {
                _mpu = new MPUSensor(sensorType, new I2CConnector());
                
                if (init)
                {
                    _mpu.Init(regConfig);
                }
            }

            return _mpu;
        }

        /// <summary>
        /// Ruft eine Instance von MPU6050.
        /// Geeigent für die Nutzung mehrer I2C Module.
        /// </summary>
        /// <param name="i2c">Benötigt die Reference Instanz für die I2C Verbindung.</param>
        /// <param name="init">Initialisiert den Sensor mit Standard Einstellungen.</param>
        /// <returns>Gibt die Instanz von MPU6050 zurück.</returns>
        public static IMPU GetInstance(SensorType sensorType, I2CConnector i2c, bool init = true, IRegisterConfiguration regConfig = null)
        {
            if(_mpu == null)
            {
                _mpu = new MPUSensor(sensorType, i2c);
                
                if (init)
                {
                    _mpu.Init(regConfig);
                }
            }

            return _mpu;
        }

        /// <summary>
        /// Initialisieren des Sensors mit den Festgelegten Einstellungen.
        /// </summary>
        /// <param name="regConfig">Zusammenstellung der festgelegten Einstellungen.</param>
        public void Init(IRegisterConfiguration regConfig)
        {
            if(regConfig == null)
            {
               regConfig = new RegisterManagement(RegisterManagementSetup.Default, this._sensorType);
            }

            // Konfiguration des Master Einheit
            // Einstellung für Beschleunigung, 
            // Gyroskop, und Temperatur Sensor
            this._I2CConnector.SetConfiguration(regConfig.Address, regConfig.ClockRate);

            foreach (IRegisterItem item in regConfig.RegisterSequence)
            {
                if (item == null) continue;

                if (item.RegisterSetup == RegisterItemUsing.ResgisterSetup)
                {
                    this._I2CConnector.Write(item.GetRegisterSetup());
                }

                if(item.RegisterSetup == RegisterItemUsing.AddressSetup)
                {
                    byte[] setup = item.GetRegisterSetup();
                    this._I2CConnector.SetConfiguration(setup[0], this.MapClockRate(setup[1]));
                }
            }
        }

        /// <summary>
        /// Ruft den Sensor ab und gibt das Ergebnis zurück.
        /// </summary>
        /// <returns>Gibt das Ergebnis zurück.</returns>
        public ISensorData GetMeasurements()
        {
            byte[] result = this._sensorType == SensorType.With6Axis ? new byte[14] : new byte[21];

            // 0x3B ist die Anfangsadresse der abzurufenden Eregbnisse.
            if (this._I2CConnector.ReadMeasurements(0x3B, ref result))
            {
                return new SensorData6Axis(result);
            }

            return new ErrorSensorResult("Sensor konnte nicht gelesen werden.");
        }

        /// <summary>
        /// Ruft aus dem Einstellungs Byte, die einzustellende Takt ein.
        /// </summary>
        /// <param name="setupClock"></param>
        /// <returns></returns>
        private int MapClockRate(byte setupClock)
        {
            switch (setupClock)
            {
                case (0x00):
                    return 100;
                case (0x01):
                    return 400;
                default:
                    return 100;
            }
        }
    }
}
