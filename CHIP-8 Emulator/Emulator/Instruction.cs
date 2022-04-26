namespace CHIP_8_Emulator.Emulator
{
    public class Instruction
    {
        private readonly byte[] _data = new byte[2]; // an instruction is 2 bytes
        private readonly short _instruction;

        public short OpCode => _instruction;
        public byte[] Bytes => _data;

        public int FullInstruction
        {
            get => (_data[1] << 8 | _data[1]);
        }

        private Instruction(byte b1, byte b2)
        {
            _data[0] = b1;
            _data[1] = b2;

            // combine the 2 bytes into a single 16-bit op code
            _instruction = (short)(b1 << 8 | b2);
        }

        internal static Instruction Read(Memory memory, int programCounter)
        {
            return new Instruction(
                memory[programCounter],
                memory[programCounter + 1]
            );
        }

        internal byte GetInstructionOpCode()
        {
            //return OpCode.GetNibble(0);
            //return OpCode.GetNibble();
            //return _data[0].GetNibble();
            return _data[0].GetNibbles().Item1;
        }
    }
}
