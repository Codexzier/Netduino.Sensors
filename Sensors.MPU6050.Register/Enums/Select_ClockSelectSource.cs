
namespace Sensors.MPU6050.Register.Enums
{
    public enum Select_ClockSelectSource
    {
        Internal8MHz = 0,
        PLLwithAxisX = 1,
        PLLwithAxisY = 2,
        PLLwithAxisZ = 3,
        PLLwithExternal32_768kHz = 4,
        PLLwithExternal19_2MHz = 5,
        Reserved = 6,
        StopsTheClockAndKeepsTheTimingGeneratorInReset = 7
    }
}
