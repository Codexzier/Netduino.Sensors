using System;
using Microsoft.SPOT;
using Sensors.MPU6050;
using Sensors.Contracts.Interfaces;
using Sensors.Contracts.Enums;
using Sensors.Contracts;
using System.Threading;

namespace ExampleMPU6050
{
    public class Program
    {
        public static void Main()
        {
            IMPU sensor = MPUSensor.GetInstance(SensorType.With6Axis);

            while (true)
            {
                ISensorData result = sensor.GetMeasurements();
                Debug.Print(result.ToString());

                Thread.Sleep(100);
            }
        }
    }
}
