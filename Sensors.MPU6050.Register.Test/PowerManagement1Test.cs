using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Sensors.MPU6050.Register.Test
{
    [TestClass]
    public class PowerManagement1Test
    {
        [TestMethod]
        public void TestMethod1()
        {
            byte b = 0x00;

            b |= (byte)(0x01 << 7);
            b |= (byte)(0x01 << 6);
            b |= (byte)(0x01 << 5);

            b |= 0x01 << 3;

            b |= 0x01 << 2;
            b |= 0x01 << 1;
            b |= 0x01 << 0;

            Debug.Print(b.ToString());
        }

        [TestMethod]
        public void TestModulo()
        {
            byte result = 0x00;

            MySelect sel = MySelect.Quad;

            bool b2 = ((byte)sel & (1 << 2)) != 0;
            bool b1 = ((byte)sel & (1 << 1)) != 0;
            bool b0 = ((byte)sel & (1 << 0)) != 0;

            SetBit(b2, 2, ref result);
            SetBit(b1, 1, ref result);
            SetBit(b0, 0, ref result);

            Debug.Print(result.ToString());
        }

        private void SetBit(bool bitOn, byte bit, ref byte setup)
        {
            if (bitOn)
            {
                setup |= (byte)(0x01 << bit);
            }
        }
    }

    public enum MySelect
    {
        One = 1,
        Secound = 2,
        Tribel = 3,
        Quad = 4,
        Penta = 5,
        Six = 6,
        Seven = 7
    }
}
