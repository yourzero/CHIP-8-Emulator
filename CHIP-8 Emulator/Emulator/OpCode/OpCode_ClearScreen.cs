namespace CHIP_8_Emulator.Emulator
{
    class OpCode_ClearScreen : OpCodeBase
    {
        public override byte OperationNibble => 0;

        public override ExecutionResult Execute(ExecutionContext context)
        {
            
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"[OpCode: Clear Screen]";
        }
    }


}
