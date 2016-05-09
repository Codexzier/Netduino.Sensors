
namespace Sensors.Contracts.Interfaces
{
    /// <summary>
    /// Standard Schnittstelle für den MPU6050 Sensor.
    /// </summary>
    public interface IMPU6050
    {
        ISensorDataSixAxisBase GetMeasurements();
    }
}
