namespace CHIP_8.Emulator.OpCodes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OpCodeForInstructionAttribute : Attribute
    {
        public byte InstructionNibble { get; private set; }

        public OpCodeForInstructionAttribute(byte instructionNibble)
        {
            InstructionNibble = instructionNibble;
        }
    }
}
