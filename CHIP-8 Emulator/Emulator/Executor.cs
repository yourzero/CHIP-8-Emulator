using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP_8_Emulator.Emulator
{
    internal class Executor
    {
        private const int FONT_LENGTH_BYTES = 5;
        private const byte MEMORY_FONT_START_POS = 0x50;


        private readonly Program _program;
        private readonly Action updateDisplayFunc;
        private readonly Memory _memory;
        private readonly Screen _screen;

        private int _programCounter = 0;


        //public Action UpdateDisplay { get; set; }

        public Screen Screen { get => _screen; }

        public Executor(Program program, Action updateDisplayFunc)
        {
            _program = program;
            this.updateDisplayFunc = updateDisplayFunc;
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

            // load into memory
            _memory.LoadProgram(_program);

            Reset();

            RunLoop();
        }

        private void Reset()
        {
            ResetProgramCounter();
            _screen.Initialize(true);

            LoadFonts();


            // update the screen once
            UpdateScreen();


            // TODO - just testing
            TESTPrintAllFontCharacters();
            UpdateScreen();

            Console.ReadLine();
        }

        private void LoadFonts()
        {

            // load the font bitmap into memory, starting at 0x50 (a commonly-used starting place)
            for(int i= 0; i < Font.FontBitmap.Length; i++)
            {
                var font = Font.FontBitmap[i];

                _memory.Load(MEMORY_FONT_START_POS + i*FONT_LENGTH_BYTES, font);
            }
        }

        // TODO - remove test
        public void TESTPrintAllFontCharacters()
        {
            //for (int i = 0; i < 16; i++)
            for (int i = 0; i < 16; i++)
            {
                var row = i / 4;
                var col = i % 4;

                var memoryPosForFont = MEMORY_FONT_START_POS +   i * FONT_LENGTH_BYTES; // each font character is 5 bytes
                //var screenX = i * FONT_LENGTH_BYTES * 8;
                var screenX = 0;
                var fontBytes = _memory.Read(memoryPosForFont, FONT_LENGTH_BYTES);
                
                Screen.DrawBytesAsSprite(fontBytes, col * 8, row* FONT_LENGTH_BYTES + (i/4));
            }


            //int memLocation = 0;

        }


        private void ResetProgramCounter()
        {
            _programCounter = Memory.ProgramStartPos;
        }

        private void RunLoop()
        {
            while (true)
            {
                RunOneCycle();
                // TODO - how to exit loop?


                // todo - just for testing
                Thread.Sleep(1000);
            }
        }

        private void RunOneCycle()
        {
            Console.WriteLine("Running one cycle...");

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
        }


        private Instruction Fetch()
        {
            var instruction = Instruction.Read(_memory, _programCounter);
            // PC increments twice, as it reads 2 bytes
            _programCounter++;
            _programCounter++;

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

            Console.WriteLine($"Executing opcode {opcode.OperationNibble.ToHex()}...");
            var context = ExecutionContext;
            opcode.Execute(context);
        }

        // TODO - we could probably not instatiate this every time
        private ExecutionContext ExecutionContext => new ExecutionContext
        {
            Memory = this._memory,
            Screen = this._screen
        };


    }



}
