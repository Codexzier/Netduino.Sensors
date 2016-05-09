using System;
using Microsoft.SPOT;

namespace Sensors.MPU6050.Register.Enums
{
    public enum Select_EXT_SYNC_SET
    {
        Input_disabled = 0,
        TEMP_OUT_L0 = 1,
        GYRO_XOUT_L0 = 2,
        GYRO_YOUT_L0 = 3,
        GYRO_ZOUT_L0 = 4,
        ACCEL_XOUT_L0 = 5,
        ACCEL_YOUT_L0 = 6,
        ACCEL_ZOUT_L0 = 7,
    }
}
