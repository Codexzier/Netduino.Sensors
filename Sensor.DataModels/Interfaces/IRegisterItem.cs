
namespace Sensors.Contracts.Interfaces
{
    /// <summary>
    /// Satandard Schnittstelle zu einem Register Einstellung.
    /// </summary>
    public interface IRegisterItem
    {
        /// <summary>
        /// Ruft das Register und Setup Byte ab.
        /// </summary>
        /// <returns>Gibt die Bytes zurück.</returns>
        byte[] GetRegisterSetup();
    }
}
