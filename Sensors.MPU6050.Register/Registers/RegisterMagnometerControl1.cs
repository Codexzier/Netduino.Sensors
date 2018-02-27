using System;
using Microsoft.SPOT;
using Sensors.Contracts.Enums;
using Sensors.MPU6050.Register.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// Stellt verschiedene Optionen zu Control 1 (CNTL1) zur Verfügung.
    /// </summary>
    public class RegisterMagnometerControl1 : RegisterBase
    {
        public RegisterMagnometerControl1()
        {
            this.RegisterSetup = RegisterItemUsing.ResgisterSetup;

            this.Mode = Select_Control1_Mode.PowerDown;
            this.Bit = Select_BitOutput.Output_14Bit;
        }

        public RegisterMagnometerControl1(Select_Control1_Mode mode, Select_BitOutput bitOutput = Select_BitOutput.Output_14Bit)
            : base()
        {
            this.Mode = mode;
            this.Bit = bitOutput;
        }

        /// <summary>
        /// Ruft die Operation ab oder legt diese fest.
        /// Ermöglicht den Magnometer in Ziel Modus zu schalten.
        /// Für das Initialisieren wird jedoch zunächst der Power Down Mode angewendet.
        /// </summary>
        public Select_Control1_Mode Mode { get; set; }

        /// <summary>
        /// Ruft das Ausgangsergebnis ab für 14 oder 16 Bit oder legt diese fest.
        /// </summary>
        public Select_BitOutput Bit { get; set; }


        public override byte[] GetRegisterSetup()
        {
            byte[] ba = new byte[2];

            // Register byte
            ba[0] = 0x0A;

            // Setup
            ba[1] = 0x00;

            this.SetMode(ref ba[0]);

            this.SetBit(true, 4, ref ba[1]);

            return ba;
        }

        /// <summary>
        /// Legt den Mode Operation in das Setup Byte fest.
        /// </summary>
        /// <param name="setup"></param>
        private void SetMode(ref byte setup)
        {
            switch (this.Mode)
            {
                case Select_Control1_Mode.PowerDown:
                    this.SetBit(false, 3, ref setup);
                    this.SetBit(false, 2, ref setup);
                    this.SetBit(false, 1, ref setup);
                    this.SetBit(false, 0, ref setup);
                    break;
                case Select_Control1_Mode.SingleMeasurement:
                    this.SetBit(false, 3, ref setup);
                    this.SetBit(false, 2, ref setup);
                    this.SetBit(false, 1, ref setup);
                    this.SetBit(true, 0, ref setup);
                    break;
                case Select_Control1_Mode.ContinousMeasurement1:
                    this.SetBit(false, 3, ref setup);
                    this.SetBit(false, 2, ref setup);
                    this.SetBit(true, 1, ref setup);
                    this.SetBit(false, 0, ref setup);
                    break;
                case Select_Control1_Mode.ContinousMeasurement2:
                    this.SetBit(false, 3, ref setup);
                    this.SetBit(true, 2, ref setup);
                    this.SetBit(true, 1, ref setup);
                    this.SetBit(false, 0, ref setup);
                    break;
                case Select_Control1_Mode.ExternalTriggerMeasurement:
                    this.SetBit(false, 3, ref setup);
                    this.SetBit(true, 2, ref setup);
                    this.SetBit(false, 1, ref setup);
                    this.SetBit(false, 0, ref setup);
                    break;
                case Select_Control1_Mode.SelfTest:
                    this.SetBit(true, 3, ref setup);
                    this.SetBit(false, 2, ref setup);
                    this.SetBit(false, 1, ref setup);
                    this.SetBit(false, 0, ref setup);
                    break;
                case Select_Control1_Mode.FuseRomAccess:
                    this.SetBit(true, 3, ref setup);
                    this.SetBit(true, 2, ref setup);
                    this.SetBit(true, 1, ref setup);
                    this.SetBit(true, 0, ref setup);
                    break;
                default:
                    throw new ArgumentException("this option is not available");
            }
        }
    }
}
