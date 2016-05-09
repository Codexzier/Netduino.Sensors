using System;
using Microsoft.SPOT;
using Sensors.MPU6050;
using Sensors.Contracts.Interfaces;

namespace ExampleMPU6050
{
    public class Program
    {
        public static void Main()
        {
            IMPU6050 sensor = MPU6050.GetInstance();

            while(true)
            {
                ISensorDataSixAxisBase result = sensor.GetMeasurements();

                Debug.Print(result.ToString());
            }
        }
    }
}
