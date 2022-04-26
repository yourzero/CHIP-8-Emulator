namespace CHIP_8_Emulator.Emulator
{
    class OpCode_ClearScreen : OpCodeBase
    {
        public OpCode_ClearScreen(Instruction instruction) : base(instruction)
        {
        }

        public override byte OperationNibble => 0;


        public override ExecutionResult Execute(ExecutionContext context)
        {
            // clear the screen
            context.Screen.Initialize(false);


            return new ExecutionResult();
        }

        public override string ToString()
        {
            return $"[OpCode: Clear Screen]";
        }
    }



    class OpCode_Jump : OpCodeBase
    {
        public OpCode_Jump(Instruction instruction) : base(instruction)
        {
        }

        public override byte OperationNibble => 1;

        public override ExecutionResult Execute(ExecutionContext context)
        {
            context.ProgramCounter = this.NNN;

            return new ExecutionResult();
        }

        public override string ToString()
        {
            return $"[OpCode: Clear Screen]";
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class OpCodeForInstructionAttribute : Attribute
    {
        public byte InstructionNibble { get; set; }
    }
}
