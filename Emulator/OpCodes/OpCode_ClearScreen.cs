namespace CHIP_8.Emulator.OpCodes
{
    [OpCodeForInstruction(0x0)]
    class OpCode_ClearScreen : OpCodeBase
    {
        public OpCode_ClearScreen(Instruction instruction) : base(instruction)
        {
        }

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
}
