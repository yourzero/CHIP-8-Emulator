namespace CHIP_8_Emulator.Emulator
{
    /// <summary>
    /// Represents the 4096-byte memory
    /// </summary>
    public class Memory
    {
        /* 
            0x000-0x1FF - Chip 8 interpreter (contains font set in emu)
            0x050-0x0A0 - Used for the built in 4x5 pixel font set (0-F)
            0x200-0xFFF - Program ROM and work RAM
        */
        private readonly byte[] _memory;

        const int PROGRAM_START_POS = 0x200;

        public static int ProgramStartPos => PROGRAM_START_POS;

        public Memory()
        {
            _memory = new byte[4096];
            for (int i = 0; i < _memory.Length; i++) _memory[i] = 0;
        }

        public byte this[int i]
        {
            get { return _memory[i]; }
        }

        public byte[] Read(int startPos, int length)
        {
            if (startPos >= _memory.Length || startPos + length >= _memory.Length)
                throw new InvalidOperationException($"Attempt to read memory out of range.");

            byte[] readBytes = new byte[length];
            Array.Copy(_memory, startPos, readBytes, 0, length);
            return readBytes;
        }

        public void LoadProgram(Program program)
        {
            for (int i = 0; i < program.ProgramData.Length; i++)
            {
                var memoryPosition = PROGRAM_START_POS + i;
                _memory[memoryPosition] = program.ProgramData[i];
            }
        }

        internal void Load(int memoryStart, byte[] font)
        {
            font.CopyTo(_memory, memoryStart);
        }
    }
}
