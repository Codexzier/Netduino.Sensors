using System;
using Microsoft.SPOT;

namespace Sensors.MPU6050.Register.Enums
{
    public enum Select_Control1_Mode
    {
        PowerDown,
        SingleMeasurement,
        ContinousMeasurement1,
        ContinousMeasurement2,
        ExternalTriggerMeasurement,
        SelfTest,
        FuseRomAccess
    }
}
