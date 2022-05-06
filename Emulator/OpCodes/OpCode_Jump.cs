namespace CHIP_8.Emulator.OpCodes
{
    [OpCodeForInstruction(0x1)]
    class OpCode_Jump : OpCodeBase
    {
        public OpCode_Jump(Instruction instruction) : base(instruction)
        {
        }

    //    public override byte OperationNibble => 1;

        public override ExecutionResult Execute(ExecutionContext context)
        {
            context.ProgramCounter = this.NNN;

            return new ExecutionResult();
        }

        public override string ToString()
        {
            return $"[OpCode: Jump]";
        }
    }
}
