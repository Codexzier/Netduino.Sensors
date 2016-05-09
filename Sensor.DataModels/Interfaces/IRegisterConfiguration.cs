

namespace Sensors.Contracts.Interfaces
{
    /// <summary>
    /// Standard Register Configuration.
    /// Stellt Adresse, Takt Geschwindigkeit und zahlreiche Register Einstellungen zusammen.
    /// </summary>
    public interface IRegisterConfiguration
    {
        /// <summary>
        /// Ruft die Ziel Adresse des Moduls ab oder legt diese fest.
        /// </summary>
        byte Address { get; }

        /// <summary>
        /// Ruft die Taktgeschwindigkeit zu dem Modul oder legt diese fest.
        /// </summary>
        int ClockRate { get; }

        /// <summary>
        /// 
        /// </summary>
        IRegisterItem[] RegisterSequence { get; }

        /// <summary>
        /// Ruft die Einstellungen zu Power Management ab oder legt diese fest.
        /// Hier kann ein Reset ausgeführt, Sensor schlafen legen, 
        /// cycle aktiviert werden , Temperatur Sensor ausgeschaltet und 
        /// die Takt Quelle ausgewelt werden.
        /// </summary>
        IRegisterItem PowerManagement1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IRegisterItem Configuration { get; set; }

        IRegisterItem I2CMasterControl { get; set; }

        IRegisterItem GyroscopeConfiguration { get; set; }

        IRegisterItem AccelerometerConfiguration { get; set; }
    }
}
