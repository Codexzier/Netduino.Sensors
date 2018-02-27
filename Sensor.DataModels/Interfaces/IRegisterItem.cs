
using Sensors.Contracts.Enums;
namespace Sensors.Contracts.Interfaces
{
    /// <summary>
    /// Satandard Schnittstelle zu einem Register Einstellung.
    /// </summary>
    public interface IRegisterItem
    {
        /// <summary>
        /// Ruft die Verwendung des Registers ab oder legt diese fest.
        /// Wird zum Beispiel verwendet, eine Adress �nderung vor zunehmen, wenn mehrere Sensoren initialisiert werden sollen.
        /// </summary>
        RegisterItemUsing RegisterSetup { get; set; }

        /// <summary>
        /// Ruft das Register und Setup Byte ab.
        /// </summary>
        /// <returns>Gibt die Bytes zur�ck.</returns>
        byte[] GetRegisterSetup();
    }
}
