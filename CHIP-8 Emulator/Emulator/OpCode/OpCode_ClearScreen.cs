namespace CHIP_8_Emulator.Emulator
{
    class OpCode_ClearScreen : OpCodeBase
    {
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


}
