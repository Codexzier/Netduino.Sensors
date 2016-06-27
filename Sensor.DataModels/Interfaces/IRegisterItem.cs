
namespace Sensors.Contracts.Interfaces
{
    /// <summary>
    /// Satandard Schnittstelle zu einem Register Einstellung.
    /// </summary>
    public interface IRegisterItem
    {
        bool Enable { get; set; }

        /// <summary>
        /// Ruft das Register und Setup Byte ab.
        /// </summary>
        /// <returns>Gibt die Bytes zur�ck.</returns>
        byte[] GetRegisterSetup();
    }
}
