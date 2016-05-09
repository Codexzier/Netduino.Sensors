using System;
using Microsoft.SPOT;

namespace Sensors.MPU6050.Register.Enums
{
    /// <summary>
    /// Das 'R' steht für Range.
    /// Werte stehen jeweils für Grad pro Sekunde.
    /// </summary>
    public enum Select_FS_SEL
    {
        R0250 = 0,
        R0500 = 1,
        R1000 = 2,
        R2000 = 3
    }
}
