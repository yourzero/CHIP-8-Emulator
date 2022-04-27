namespace CHIP_8_Emulator.Emulator
{

    public interface IOpCode
    {
        byte OpCodeInstruction { get;}
        byte N { get;}
        byte NN { get;}
        int NNN { get;}
        //byte OperationNibble { get; }
        byte X { get;}
        byte Y { get;}

        //void SetInstruction(Instruction instruction);

        ExecutionResult Execute(ExecutionContext context);
    }

    public abstract class OpCodeBase : IOpCode
    {
        ///// <summary>
        ///// The operation this OpCode instance handles.
        ///// </summary>
        //public abstract byte OperationNibble { get; }

        protected Instruction Instruction { get; set; }

        /// <summary>
        /// The nibble that identifies the op code
        /// </summary>
        public byte OpCodeInstruction { get; private set; }

        /// <summary>
        /// The second nibble. Used to look up one of the 16 registers(VX) from V0 through VF.
        /// </summary>
        public byte X { get; private set; }

        /// <summary>
        /// The third nibble .Also used to look up one of the 16 registers(VY) from V0 through VF.
        /// </summary>
        public byte Y { get; private set; }

        /// <summary>
        // The fourth nibble. A 4-bit number.
        /// </summary>
        public byte N { get; private set; }

        /// <summary>
        // The second byte (third and fourth nibbles). An 8-bit immediate number.
        /// </summary>
        public byte NN { get; private set; }

        /// <summary>
        // The second, third and fourth nibbles. A 12-bit immediate memory address.
        /// </summary>
        public int NNN { get; private set; }

        public OpCodeBase(Instruction instruction)
        {
            this.Instruction = instruction;

            // TODO - move this to a derived class
            this.NNN = instruction.FullInstruction & 0x0FFF;
        }

        //void SetInstruction(Instruction instruction)
        //{
        //    this.NNN = instruction.FullInstruction & 0x0FFF;
        //}

        //private int DeriveNNN(byte[] instructionBytes)
        //{

        //}

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
        public Program Program { get; set; }
        public Memory Memory { get; set; }
        public Screen Screen { get; set; }
        public int ProgramCounter { get; set; }
    }

    public class ExecutionResult
    {
    }


}
