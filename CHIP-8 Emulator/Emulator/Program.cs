using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP_8_Emulator.Emulator
{
    internal class Program
    {
        private byte[] _program;
        private readonly string _programFilePath;

        public Program(string programFilePath)
        {
            _programFilePath = programFilePath;
        }

        public byte[] ProgramData { get => _program; set => _program = value; }

        public async Task LoadProgramAsync()
        {

            Console.WriteLine($"Loading program from {_programFilePath}...");


            var programFileBytes = await File.ReadAllBytesAsync(_programFilePath);
            _program = programFileBytes;

            // TODO - error check

            Console.WriteLine($"Loaded program.");

        }

        public void DebugOutputProgramData()
        {
            // TODO - only debugging
            long posInProgram = 0;
            while (posInProgram < _program.Length)
            {
                for (int i = 0; i < 16 && posInProgram + i < _program.Length; i++)
                {
                    var b = _program[posInProgram + i];
                    Console.Write($"{Convert.ToHexString(new[] { b })} ");
                }
                Console.WriteLine();

                posInProgram += 16;
            }
        }

    }
}
