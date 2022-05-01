namespace CHIP_8.Emulator.OpCode
{
    [OpCodeForInstruction(0x6)] // 6XNN 
    class OpCode_SetRegister : OpCodeBase
    {
        public OpCode_SetRegister(Instruction instruction) : base(instruction)
        {
        }

        public override ExecutionResult Execute(ExecutionContext context)
        {
            context.Registers.Variables[this.X] = this.NN;

            return new ExecutionResult();
        }

        public override string ToString()
        {
            return $"[OpCode: Set Register vx]";
        }
    }

}
