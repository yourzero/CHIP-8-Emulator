using CHIP_8.Emulator.Extensions;

namespace CHIP_8.Emulator.OpCodes
{
    /// <summary>
    /// The base class for all op codes. Provides access to the different parts (or combinations of parts) of the entire op code.
    /// </summary>
    public abstract class OpCodeBase : IOpCode
    {
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
        public ushort NNN { get; private set; }

        public OpCodeBase(Instruction instruction)
        {
            this.Instruction = instruction;

            // decode the possible operands
            (this.OpCodeInstruction, this.X) = instruction.Bytes[0].GetNibbles();
            (this.Y, this.N) = instruction.Bytes[1].GetNibbles();
            this.NN = instruction.Bytes[1];
            this.NNN = (ushort)(instruction.FullInstruction & 0x0FFF);
        }

        /*
         * X: The second nibble. Used to look up one of the 16 registers (VX) from V0 through VF.
            Y: The third nibble. Also used to look up one of the 16 registers (VY) from V0 through VF.
            N: The fourth nibble. A 4-bit number.
            NN: The second byte (third and fourth nibbles). An 8-bit immediate number.
            NNN: The second, third and fourth nibbles. A 12-bit immediate memory address.
        */

        public abstract ExecutionResult Execute(ExecutionContext context);
    }

}
