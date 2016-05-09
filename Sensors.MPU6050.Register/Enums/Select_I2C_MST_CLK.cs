using System;
using Microsoft.SPOT;

namespace Sensors.MPU6050.Register.Enums
{
    public enum Select_I2C_MST_CLK
    {
        ClockSpeed_348kHz_Div23 = 0,
        ClockSpeed_333kHz_Div24 = 1,
        ClockSpeed_320kHz_Div25 = 2,
        ClockSpeed_308kHz_Div26 = 3,
        ClockSpeed_296kHz_Div27 = 4,
        ClockSpeed_286kHz_Div28 = 5,
        ClockSpeed_276kHz_Div29 = 6,
        ClockSpeed_267kHz_Div30 = 7,
        ClockSpeed_258kHz_Div31 = 8,
        ClockSpeed_500kHz_Div16 = 9,
        ClockSpeed_471kHz_Div17 = 10,
        ClockSpeed_444kHz_Div18 = 11,
        ClockSpeed_421kHz_Div19 = 12,
        ClockSpeed_400kHz_Div20 = 13,
        ClockSpeed_381kHz_Div21 = 14,
        ClockSpeed_364kHz_Div22 = 15,
    }
}
