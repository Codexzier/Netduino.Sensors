
using Sensors.Contracts.Enums;
namespace Sensors.Contracts.Interfaces
{
    /// <summary>
    /// Standard Schnittstelle f�r den MPU6050 Sensor.
    /// </summary>
    public interface IMPU
    {
        ISensorData GetMeasurements();

        void Init(IRegisterConfiguration regConfig);
    }
}
