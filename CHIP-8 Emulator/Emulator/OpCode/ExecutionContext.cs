namespace CHIP_8_Emulator.Emulator
{
    public class ExecutionContext
    {
        public Program Program { get; set; }
        public Memory Memory { get; set; }
        public Screen Screen { get; set; }
        public Registers Registers { get; set; }

        public int ProgramCounter { get; set; }
    }


}
