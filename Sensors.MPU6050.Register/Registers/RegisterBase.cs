using System;
using Microsoft.SPOT;
using Sensors.Contracts.Interfaces;
using Sensors.Contracts.Enums;

namespace Sensors.MPU6050.Register
{
    public abstract class RegisterBase : IRegisterItem
    {
        public RegisterBase(RegisterItemUsing setup = RegisterItemUsing.ResgisterSetup) { }

        /// <summary>
        /// Ruft die Verwendung des Registers ab oder legt diese fest.
        /// Wird zum Beispiel verwendet, eine Adress Änderung vor zunehmen, wenn mehrere Sensoren initialisiert werden sollen.
        /// </summary>
        public RegisterItemUsing RegisterSetup { get; set; }
        
        /// <summary>
        /// Legt das Bit im Byte fest.
        /// </summary>
        /// <param name="bitOn">True wenn das Bit gestzt werden soll.</param>
        /// <param name="bit">Legt die Bitstelle fest.</param>
        /// <param name="setup">Das zu verarbeitende byte.</param>
        protected void SetBit(bool bitOn, byte bit, ref byte setup)
        {
            if (bitOn)
            {
                setup |= (byte)(0x01 << bit);
            }
        }

        /// <summary>
        /// Wandelt den Wert in das bestehende Setup Byte ab.
        /// </summary>
        /// <param name="value">Einzustellende Wert das in das byte geschrieben weden soll.</param>
        /// <param name="bit">Beginnde Bit Stelle, von der runter gerechnet werden soll.</param>
        /// <param name="setup">Setup Byte, dass hierfür beschrieben werden soll.</param>
        protected void SetValueToBits(int value, int bit, ref byte setup)
        {
            int d = 8;
            for (int i = bit; i > 0; i--)
            {
                if (value >= d)
                {
                    this.SetBit(true, (byte)(i - 1), ref setup);
                    value -= d;
                }

                d = d / 2;
            }
        }

        /// <summary>
        /// Abruf der für das Register vorgenommenen Einstellungen.
        /// </summary>
        /// <returns></returns>
        public abstract byte[] GetRegisterSetup();
    }
}
