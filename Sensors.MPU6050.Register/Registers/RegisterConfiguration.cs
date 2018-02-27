using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.MPU6050.Register.Enums;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// Register 26 - Configuration (CONFIG)
    /// Stellt die Einstellungen für den Tief-Pass Filter und Externe Syncronsierung fest.
    /// </summary>
    public class RegisterConfiguration : RegisterBase
    {
        /// <summary>
        /// Standard Einstellung
        /// EXT_SYNC_SET = Input Disabled, 
        /// Tief-Pass einstellen 
        /// Setting => Acc=5Hz, Delay=19.0ms, 
        /// Gyro=5Hz, Delay=18.6ms, Fs=1kHz
        /// </summary>
        public RegisterConfiguration()
            : base()
        {
            this.EXT_SYNC = Select_EXT_SYNC_SET.Input_disabled;
            this.DLPF = Select_DLPF_CFG.Acc_Hz260_d000_Gyr_Hz256_d0098_Fs8kHz;
        }

        /// <summary>
        /// TODO: Hintergrund erläutern und testen.
        /// Verwendet eine Messung aus der zu verwendenden Sensor Achse,
        /// um ein Bit zu senden am EXT_SYNC_SET.
        /// </summary>
        public Select_EXT_SYNC_SET EXT_SYNC { get; set; }

        /// <summary>
        /// Ruft die Tief-Pass Filter ab oder legt dieses fest.
        /// Filter hochfrequente Bewegungen aus. Zum Beispiel 
        /// können damit Vibrationen von Motoren herausgefiltert werden.
        /// </summary>
        public Select_DLPF_CFG DLPF { get; set; }

        /// <summary>
        /// Ruft das Register und Setup Byte ab.
        /// </summary>
        /// <returns>Gibt die Bytes zurück.</returns>
        public override byte[] GetRegisterSetup()
        {
            byte[] ba = new byte[2];
            
            // Register byte
            ba[0] = 0x1a;

            // Setup
            ba[1] = 0x00;

            // EXT_SYNC_SET
            this.SetBit(((byte)this.EXT_SYNC & (1 << 5)) != 0, 5, ref ba[1]);
            this.SetBit(((byte)this.EXT_SYNC & (1 << 4)) != 0, 4, ref ba[1]);
            this.SetBit(((byte)this.EXT_SYNC & (1 << 3)) != 0, 3, ref ba[1]);

            // DLPF
            this.SetBit(((byte)this.DLPF & (1 << 2)) != 0, 2, ref ba[1]);
            this.SetBit(((byte)this.DLPF & (1 << 1)) != 0, 1, ref ba[1]);
            this.SetBit(((byte)this.DLPF & (1 << 0)) != 0, 0, ref ba[1]);

            return ba;
        }
    }
}
