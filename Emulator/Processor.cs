using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP_8.Emulator
{
    /// <summary>
    /// The core of the emulator, runs the instructions in a cycle.
    /// </summary>
    public class Processor
    {
        private const int FONT_LENGTH_BYTES = 5;
        private const byte MEMORY_FONT_START_POS = 0x50;

        private readonly Program _program;
        private readonly Action updateDisplayFunc;
        private readonly Memory _memory;
        private readonly Screen _screen;

        private ExecutionContext ExecutionContext { get; set; }

        public Screen Screen { get => _screen; }

        public Processor(Program program, Action updateDisplayFunc)
        {
            this.updateDisplayFunc = updateDisplayFunc;

            _program = program;
            _memory = new Memory();
            _screen = new Screen();
        }

        private void UpdateScreen()
        {
            this.updateDisplayFunc();
        }

        /// <summary>
        /// Just a wrapper for the run loop
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Executing program...");

            
            Reset();

            // load into memory
            ExecutionContext.Memory.LoadProgram(ExecutionContext.Program);
            ResetProgramCounter();

            RunLoop();
        }

        private void Reset()
        {
            ExecutionContext = new ExecutionContext
            {
                Memory = _memory,
                Screen = _screen,
                Program = _program,
                Registers = new Registers() // create a new instance so the registers are initialized
                //ProgramCounter = Memory.ProgramStartPos
            };

            ResetProgramCounter();

            ExecutionContext.Screen.Initialize(true);

            LoadFonts();

            // update the screen once
            UpdateScreen();


            // TODO - just testing
            TESTPrintAllFontCharacters();
            UpdateScreen();
        }

        private void LoadFonts()
        {

            // load the font bitmap into memory, starting at 0x50 (a commonly-used starting place)
            for (int i = 0; i < Font.FontBitmap.Length; i++)
            {
                var font = Font.FontBitmap[i];

                ExecutionContext.Memory.Load(MEMORY_FONT_START_POS + i * FONT_LENGTH_BYTES, font);
            }
        }

        // TODO - remove this test
        public void TESTPrintAllFontCharacters()
        {
            //for (int i = 0; i < 16; i++)
            for (int i = 0; i < 16; i++)
            {
                var row = i / 4;
                var col = i % 4;

                var memoryPosForFont = MEMORY_FONT_START_POS + i * FONT_LENGTH_BYTES; // each font character is 5 bytes
                //var screenX = i * FONT_LENGTH_BYTES * 8;
                var screenX = 0;
                var fontBytes = ExecutionContext.Memory.Read(memoryPosForFont, FONT_LENGTH_BYTES);

                Screen.DrawBytesAsSprite(fontBytes, col * 8, row * FONT_LENGTH_BYTES + (i / 4));
            }
        }


        private void ResetProgramCounter()
        {
            ExecutionContext.ProgramCounter = Memory.ProgramStartPos;
        }

        private void RunLoop()
        {
            while (true)
            {
                RunOneCycle();
                // TODO - how to exit loop?


                // todo - just for testing
                //Thread.Sleep(1000);
                //Console.Write(">>");
                //Console.ReadLine();
            }
        }

        private void RunOneCycle()
        {
            Console.WriteLine("|----------- Running one cycle...");

            var instruction = Fetch();

            var opcode = Decode(instruction);

            Execute(opcode);

            // TODO - is this the right place for this?
            if (this.Screen.NeedsRefreshed())
            {
                this.UpdateScreen();
                this.Screen.MarkAsRefreshed();
            }

            // TODO - test
            //this.Screen.RandomizeTest();

            Console.WriteLine(" One cycle completed -----------|");
        }


        private Instruction Fetch()
        {
            var instruction = Instruction.Read(ExecutionContext.Memory, ExecutionContext.ProgramCounter);
            // PC increments twice, as it reads 2 bytes
            ExecutionContext.ProgramCounter+=2;

            return instruction;
        }


        private IOpCode Decode(Instruction instruction)
        {
            var opcode = OpCodeDecoder.Decode(instruction);
            return opcode;
        }


        private void Execute(IOpCode opcode)
        {
            if (opcode == null)
            {
                Console.WriteLine($"NOT Executing because opcode was not found.");
                return;
            }

            Console.WriteLine($"Executing opcode {opcode.OpCodeInstruction.ToHex()}...");

            //Console.Write(">>");
            //Console.ReadLine();

            var context = ExecutionContext;
            opcode.Execute(context);
        }
    }

}
