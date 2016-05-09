using System;
using Microsoft.SPOT;
using Sensors.MPU6050.Register.Enums;
using Sensors.Contracts.Interfaces;
using Sensors.MPU6050.Register.Registers;

namespace Sensors.MPU6050.Register
{
    /// <summary>
    /// Haupt Klasse zu verwaltung und festlegung der Einstellungen zu dem Sensor.
    /// Die Einstellungen werden �bernommen und durch eine Initialisierungssequenz 
    /// an den Sensor gesendet, sobald eine Instanz f�r den Sensor erstellt wurde.
    /// </summary>
    public class RegisterManagement : IRegisterConfiguration
    {
        /// <summary>
        /// Array mit den Register Einstellungen.
        /// </summary>
        private IRegisterItem[] _registerItems;

        /// <summary>
        /// Standard Konstruktor
        /// Default Einstellungen:
        /// Acc=5Hz, Delay=19.0ms, Gyro=5Hz, Delay=18.6ms, Fs=1kHz, Fast Mode, Gyroscope = +-2000�/s, Beschleunigung = +-8g 
        /// </summary>
        /// <param name="registerConfigurationSetup"></param>
        /// <param name="address"></param>
        /// <param name="clockRate"></param>
        public RegisterManagement(RegisterManagementSetup registerConfigurationSetup, byte address = 0x68, int clockRate = 400)
        {
            this.Address = address;
            this.ClockRate = clockRate;

            if(registerConfigurationSetup == RegisterManagementSetup.Default)
            {
                this._registerItems = new IRegisterItem[10];

                // Reset
                this._registerItems[0] = new RegisterPowerManagement1();

                // Power Management festlegen
                this._registerItems[1] = new RegisterPowerManagement1(false, false);

                // Configuration
                this._registerItems[2] = new RegisterConfiguration();

                // I�C Master Control
                this._registerItems[3] = new RegisterI2CMasterControl();

                // Gyroscope Configuration
                this._registerItems[4] = new RegisterGyroscopeConfiguration();

                // Acceleration Configuration
                this._registerItems[5] = new RegisterAccelerometerConfiguration();
            }
        }

        /// <summary>
        /// Ruft die Ziel Adresse des Modul ab.
        /// </summary>
        public byte Address { get; private set; }

        /// <summary>
        /// Ruft die Taktgeschwindigkeit ab.
        /// </summary>
        public int ClockRate { get; private set; }

        /// <summary>
        /// Ruft das Array mit den zu verwendenden Register Einstellungen ab.
        /// </summary>
        public IRegisterItem[] RegisterSequence
        {
            get { return this._registerItems; }
        }

        /// <summary>
        /// Ruft die Einstellungen des Power Management (PWR_MGMT_1) ab oer legt diese fest.
        /// Einstllungen f�r Reset, Sleep, Cycle, Temperatur Sensor und Taktquelle.
        /// </summary>
        public IRegisterItem PowerManagement1
        {
            get { return this._registerItems[1]; }
            set { this._registerItems[1] = value; }
        }

        /// <summary>
        /// Ruft die Einstellungen zu Configuration (CONFIG) ab oder legt diese fest.
        /// Einstellung des Tiefpassfilters und des Ext_sync.
        /// </summary>
        public IRegisterItem Configuration
        {
            get { return this._registerItems[2]; }
            set { this._registerItems[2] = value; }
        }

        /// <summary>
        /// Ruft die Einstellungen f�r die I�C Master Control ab oder legt diese fest.
        /// Einstellung der Taktgeschwindigkeit und erweiterte Unterbrechungsroutinen.
        /// </summary>
        public IRegisterItem I2CMasterControl
        {
            get { return this._registerItems[3]; }
            set { this._registerItems[3] = value; }
        }

        public IRegisterItem GyroscopeConfiguration
        {
            get { return this._registerItems[4]; }
            set { this._registerItems[4] = value; }
        }

        public IRegisterItem AccelerometerConfiguration
        {
            get { return this._registerItems[5]; }
            set { this._registerItems[5] = value; }
        }
    }
}
