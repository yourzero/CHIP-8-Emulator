namespace CHIP_8_Emulator.Emulator
{
    /// <summary>
    /// Represents the registers of the CHIP-8
    /// </summary>
    public class Registers
    {
        private const int NUMBER_OF_VARIABLE_REGISTERS = 16;

        /* 
        * One 16-bit index register called “I” which is used to point at locations in memory
        * 16 8-bit (one byte) general-purpose variable registers numbered 0 through F hexadecimal,
        *      i.e. 0 through 15 in decimal, called V0 through VF
        */

        public ushort I { get; set; }
        public byte[] Variables { get; set; }

        // VF is a special register
        public byte VF
        {
            get { return Variables[0xF]; }
            set { Variables[0xF] = value; }
        }

        public Registers()
        {
            I = default(ushort);
            Variables = new byte[NUMBER_OF_VARIABLE_REGISTERS];
            for (int i = 0; i < NUMBER_OF_VARIABLE_REGISTERS; i++) Variables[i] = default(byte);
        }
    }
}
