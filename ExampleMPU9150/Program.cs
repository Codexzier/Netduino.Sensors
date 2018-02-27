using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.MPU6050;
using Sensors.Contracts.Enums;
using Sensors.Contracts;
using System.Threading;

namespace ExampleMPU9150
{
    public class Program
    {
        public static void Main()
        {
            IMPU sensor = MPUSensor.GetInstance(SensorType.With9Axis);

            while (true)
            {
                ISensorData result = sensor.GetMeasurements();
                Debug.Print(result.ToString());
                SensorData9Axis result9Axis = result as SensorData9Axis;
                if (result9Axis != null)
                {
                    Debug.Print("MagX:" + result9Axis.MagnometerX + ", MagY:" + result9Axis.MagnometerY + ", MagZ:" + result9Axis.MagnometerZ);
                }
                
                Thread.Sleep(100);
            }
        }
    }
}
