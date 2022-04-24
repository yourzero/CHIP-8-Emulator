namespace CHIP_8_Emulator.Emulator
{

    public interface IOpCode
    {
        byte Instruction { get; set; }
        byte N { get; set; }
        byte NN { get; set; }
        byte NNN { get; set; }
        byte OperationNibble { get; }
        byte X { get; set; }
        byte Y { get; set; }

        ExecutionResult Execute(ExecutionContext context);
    }

    public abstract class OpCodeBase : IOpCode
    {
        public abstract byte OperationNibble { get; }

        private byte _instruction;
        private byte _x; // The second nibble.Used to look up one of the 16 registers(VX) from V0 through VF.
        private byte _y; // The third nibble.Also used to look up one of the 16 registers(VY) from V0 through VF.
        private byte _n; // The fourth nibble.A 4-bit number.
        private byte _nn; // The second byte (third and fourth nibbles). An 8-bit immediate number.
        private byte _nnn; // The second, third and fourth nibbles.A 12-bit immediate memory address.

        public byte Instruction { get => _instruction; set => _instruction = value; }
        public byte X { get => _x; set => _x = value; }
        public byte Y { get => _y; set => _y = value; }
        public byte N { get => _n; set => _n = value; }
        public byte NN { get => _nn; set => _nn = value; }
        public byte NNN { get => _nnn; set => _nnn = value; }

        /*
         * X: The second nibble. Used to look up one of the 16 registers (VX) from V0 through VF.
        Y: The third nibble. Also used to look up one of the 16 registers (VY) from V0 through VF.
        N: The fourth nibble. A 4-bit number.
        NN: The second byte (third and fourth nibbles). An 8-bit immediate number.
        NNN: The second, third and fourth nibbles. A 12-bit immediate memory address.
        */

        public abstract ExecutionResult Execute(ExecutionContext context);


    }

    public class ExecutionContext
    {
        public Memory Memory { get; set; }
    }

    public class ExecutionResult
    {
    }


}
