using System;
using Microsoft.SPOT;

namespace Sensors.MPU6050.Register.Enums
{
    public enum Select_DLPF_CFG
    {
        Acc_Hz260_d000_Gyr_Hz256_d0098_Fs8kHz = 0,
        Acc_Hz183_d020_Gyr_Hz188_d0190_Fs1kHz = 1,
        Acc_Hz094_d030_Gyr_Hz098_d0280_Fs1kHz = 2,
        Acc_Hz044_d049_Gyr_Hz042_d0480_Fs1kHz = 3,
        Acc_Hz021_d085_Gyr_Hz020_d0830_Fs1kHz = 4,
        Acc_Hz010_d138_Gyr_Hz010_d1340_Fs1kHz = 5,
        Acc_Hz005_d190_Gyr_Hz005_d1860_Fs1kHz = 6,,
        Acc_Reserved_Fs8kHz = 7
    }
}
