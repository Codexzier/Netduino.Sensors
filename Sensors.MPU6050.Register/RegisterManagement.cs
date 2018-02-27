using System;
using Microsoft.SPOT;
using Sensors.MPU6050.Register.Enums;
using Sensors.Contracts.Interfaces;
using Sensors.MPU6050.Register.Registers;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050.Register
{
    /// <summary>
    /// Haupt Klasse zu verwaltung und festlegung der Einstellungen zu dem Sensor.
    /// Die Einstellungen werden übernommen und durch eine Initialisierungssequenz 
    /// an den Sensor gesendet, sobald eine Instanz für den Sensor erstellt wurde.
    /// </summary>
    public class RegisterManagement : IRegisterConfiguration
    {
        /// <summary>
        /// Array mit den Register Einstellungen.
        /// </summary>
        private IRegisterItem[] _registerItems;
        private RegisterManagementSetup registerManagementSetup;

        /// <summary>
        /// Standard Konstruktor
        /// Default Einstellungen:
        /// Acc=5Hz, Delay=19.0ms, Gyro=5Hz, Delay=18.6ms, Fs=1kHz, Fast Mode, Gyroscope = +-2000°/s, Beschleunigung = +-8g 
        /// </summary>
        /// <param name="registerConfigurationSetup"></param>
        /// <param name="address"></param>
        /// <param name="clockRate"></param>
        public RegisterManagement(RegisterManagementSetup registerConfigurationSetup, SensorType sensorType, byte address = 0x68, int clockRate = 400)
        {
            this.Address = address;
            this.ClockRate = clockRate;

            if(registerConfigurationSetup == RegisterManagementSetup.Default)
            {
                this.DefaultSetup(sensorType);

                if (sensorType == SensorType.With9Axis)
                {
                    this.MagnometerDefaultSetup();
                }
            }
        }

        /// <summary>
        /// Einfache Einstellung eines sechs Axen Sensors.
        /// </summary>
        private void DefaultSetup(SensorType sensorType)
        {
            this._registerItems = sensorType == SensorType.With6Axis ? new IRegisterItem[10]: new IRegisterItem[12];

            // Reset
            this._registerItems[0] = new RegisterPowerManagement1();

            // Power Management festlegen
            this._registerItems[1] = new RegisterPowerManagement1(false, false);

            // Configuration
            this._registerItems[2] = new RegisterConfiguration();

            // I²C Master Control
            this._registerItems[3] = new RegisterI2CMasterControl();

            // Gyroscope Configuration
            this._registerItems[4] = new RegisterGyroscopeConfiguration();

            // Acceleration Configuration
            this._registerItems[5] = new RegisterAccelerometerConfiguration();


            // Sample Divider (Aus)
            this._registerItems[6] = new RegisterSampleRateDivider();

            // Motion Detection Threshold (Aus)
            this._registerItems[7] = new RegisterMotionDetectionThreshold();

            // FIFO Enable (aus)
            this._registerItems[8] = new RegisterFIFOEnable();
        }

        private void MagnometerDefaultSetup()
        {
            // I²C Setup umstellen
            this._registerItems[9] = new AddressChangeToMagnometer();

            // Control Power Down
            this._registerItems[10] = new RegisterMagnometerControl1();

            // Self Test Mode
            this._registerItems[11] = new RegisterMagnometerControl1(Select_Control1_Mode.SelfTest);

            // Nochmal Power Down
            this._registerItems[12] = new RegisterMagnometerControl1();

            // Adresse wieder zurück stellen
            this._registerItems[13] = new AddressChangeToBase();
        }

        private void SetupSlaveControl()
        {
            // TODO: Muss ich nochmal aufarbeiten

            // Master Control
            // 0x40 = Stellt den MULT_MST_EN
            this._registerItems[14] = new RegisterI2CMasterControl() { MULT_MST_EN = true };

            // I²C Slave Control
            // I2C SLV0_EN = an, I2C_SLV_LEN[3;0] = ???
            this._registerItems[15] = new RegisterI2CSlave0Control_ADDR() { I2C_SLV0_ADDR = 0x8C };
            
            // Wo soll von Slave 0 gelesen werden
            this._registerItems[16] = new RegisterI2CSlave0Control_REG() { I2C_SLV0_REG = 0x02 };

            // Offset
            this._registerItems[17] = new RegisterI2CSlave0Control_CTRL() { I2C_SLV0_EN = true, I2C_SLV0_LENG = 8 };

            // Adresse für Slave 1 fest legen.
            this._registerItems[18] = new RegisterI2CSlave1Control_ADDR();


            // und den rest..
            //{ MPU9150Register.I2C_SLV1_REG, 0x0a }), "Set where reading at slave starts");
            //{ MPU9150Register.I2C_SLV1_CTRL, 0x81 }), "Set Enable at set length to 1");
            //{ 0x64, 0x01 }), "Set override register");
            //{ 0x67, 0x03 }), "Set delay rate");
            //{ 0x01, 0x80 }), "Set Unkown");
            
            //{ 0x34, 0x04 }), "Set I2C slv4 delay");
            //{ 0x64, 0x00 }), "Set override register");
            //{ 0x6a, 0x00 }), "Set clear usr setting");
            //{ 0x64, 0x01 }), "Set override register");
            //{ 0x6a, 0x20 }), "Set enable master I2C mode");
            //{ 0x34, 0x13 }), "Set disable slv4");
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
        /// Einstllungen für Reset, Sleep, Cycle, Temperatur Sensor und Taktquelle.
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
        /// Ruft die Einstellungen für die I²C Master Control ab oder legt diese fest.
        /// Einstellung der Taktgeschwindigkeit und erweiterte Unterbrechungsroutinen.
        /// </summary>
        public IRegisterItem I2CMasterControl
        {
            get { return this._registerItems[3]; }
            set { this._registerItems[3] = value; }
        }

        /// <summary>
        /// Ruft die Einstellungen für die Gyroskopesensoren ab oder legt diesen fest.
        /// Ermöglicht den Selftest der einzelnen Achsen und die Auflösung die Drehgeschwindigkeit einzustellen.
        /// </summary>
        public IRegisterItem GyroscopeConfiguration
        {
            get { return this._registerItems[4]; }
            set { this._registerItems[4] = value; }
        }

        /// <summary>
        /// Ruft die Einstellungen für den Beschleunigungssensor ab oder legt diesen fest.
        /// Ermöglicht den Selftest der einzelnen Achsen und die Auflösung der Beschleunigung einzustellen.
        /// </summary>
        public IRegisterItem AccelerometerConfiguration
        {
            get { return this._registerItems[5]; }
            set { this._registerItems[5] = value; }
        }

        /// <summary>
        /// Ruft die Einstellungen für den Sample rate divider ab oder legt diesen fest.
        /// TODO: Geneuer Erläutern.
        /// </summary>
        public IRegisterItem SampleRateDivider
        {
            get { return this._registerItems[6]; }
            set { this._registerItems[6] = value; }
        }

        /// <summary>
        /// Ruft die Einstellungen für die Bewegungserkennung ab oder legt diese fest.
        /// Damit läst sich festlegen, bei welcher Beschleunigung reagiert werden soll. 
        /// Dazu sind weitere Einstellungen über Register 56 (Interrupt Enable) und 58 (Interrupt Status) erforderlich.
        /// </summary>
        public IRegisterItem MotionDetectionThreshold
        {
            get { return this._registerItems[7]; }
            set { this._registerItems[7] = value; }
        }

        /// <summary>
        /// Ruft die Einstellungen für den FIFO Puffer ab oder legt diese fest.
        /// Damit können Sensormessungen in den Puffer zwischengespeichert werden.
        /// </summary>
        public IRegisterItem FIFO_ENABLE
        {
            get { return this._registerItems[8]; }
            set { this._registerItems[8] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IRegisterItem I2C_Slave0Control
        {
            get { return this._registerItems[9]; }
            set { this._registerItems[9] = value; }
        }
    }
}
