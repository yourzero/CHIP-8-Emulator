namespace CHIP_8.Emulator.OpCodes
{
    [OpCodeForInstruction(0x7)] // 7XNN 
    class OpCode_AddValueToRegister : OpCodeBase
    {
        public OpCode_AddValueToRegister(Instruction instruction) : base(instruction)
        {
        }

        public override ExecutionResult Execute(ExecutionContext context)
        {
            // TODO - do we need to check for overflow?

            context.Registers.Variables[this.X] += this.NN;

            return new ExecutionResult();
        }

        public override string ToString()
        {
            return $"[OpCode: Add Value To Register vx]";
        }
    }

}
