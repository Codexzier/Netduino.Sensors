using System;
using Microsoft.SPOT;

namespace Sensors.MPU6050.Register.Enums
{
    /// <summary>
    /// Das 'R' steht für Range.
    /// Werte stehen jeweils für g Beschleunigung.
    /// </summary>
    public enum Select_AFS_SEL
    {
        R02,
        R04,
        R08,
        R16
    }
}
