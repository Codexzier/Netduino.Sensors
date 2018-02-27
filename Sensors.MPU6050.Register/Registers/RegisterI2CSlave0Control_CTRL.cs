using System;
using Microsoft.SPOT;

namespace Sensors.MPU6050.Register.Registers
{
    /// <summary>
    /// Slave 0 Control CTRL
    /// </summary>
    public class RegisterI2CSlave0Control_CTRL : RegisterBase
    {
        public RegisterI2CSlave0Control_CTRL()
            : base()
        {
            this.I2C_SLV0_EN = false;
            this.I2C_SLV0_BYTE_SW = false;
            this.I2C_SLV0_REG_DIS = false;
            this.I2C_SLV0_GRP = false;
            this.I2C_SLV0_LENG = 8;
        }

        /// <summary>
        /// Ruft die Einstellung für lesen der Daten vom Slave Modul und speichert die daten zwischen oder legt diese fest.
        /// </summary>
        public bool I2C_SLV0_EN { get; set; }

        /// <summary>
        /// Ruft die Einstellung für den Wechsel der Bytes, wenn beide (low and high) gelesen werden oder legt diese fest.
        /// Hinweis: Der wechsel nach dem lesen des ersten Byte, 
        /// wenn zu der Konfiguration I2C_SL0_Reg[0] = 1 festgelgt ist oder
        /// Beispiel auf Seite 21 (Dokument: MPU 9250 Register Map )
        /// </summary>
        public bool I2C_SLV0_BYTE_SW { get; set; }

        /// <summary>
        /// Ruft die Einstellung für die Transaction die kein Register Wert enthählt, 
        /// sondern Daten liest oder schreibt.
        /// </summary>
        public bool I2C_SLV0_REG_DIS { get; set; }

        /// <summary>
        /// Ruft die Einstellung für Slave Register Adresse oder Gruppierte Register aber oder legt diese fest.
        /// false = Zeigt den SLAVE Register Adresse, true =  Sind Groupiert oder ungrade zeigen das ende der Gruppe an.
        /// </summary>
        public bool I2C_SLV0_GRP { get; set; }

        /// <summary>
        /// Ruft die Anzahl der Bytes, die gelesen werden können vom Slave Modul oder legt diese hier fest.
        /// </summary>
        public int I2C_SLV0_LENG { get; set; }

        public override byte[] GetRegisterSetup()
        {
            byte[] ba = new byte[2];

            // register byte
            ba[0] = 0x26;

            this.SetBit(this.I2C_SLV0_EN, 7, ref ba[1]);
            this.SetBit(this.I2C_SLV0_BYTE_SW, 6, ref ba[1]);
            this.SetBit(this.I2C_SLV0_REG_DIS, 5, ref ba[1]);
            this.SetBit(this.I2C_SLV0_GRP, 4, ref ba[1]);

            this.SetValueToBits(this.I2C_SLV0_LENG, 3, ref ba[1]);

            return ba;
        }
    }
}
