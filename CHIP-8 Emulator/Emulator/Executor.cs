using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP_8_Emulator.Emulator
{
    internal class Executor
    {
        private readonly Program _program;
        private readonly Action updateScreenFunc;
        private readonly Memory _memory;
        private readonly Screen _screen;

        private int _programCounter = 0;


        public Action UpdateDisplay { get; set; }

        public Screen Screen { get => _screen; }

        public Executor(Program program, Action updateScreenFunc)
        {
            _program = program;
            this.updateScreenFunc = updateScreenFunc;
            _memory = new Memory();
            _screen = new Screen();
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

            // update the screen once
            this.updateScreenFunc();
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
            if(this.Screen.NeedsRefreshed())
            {
                this.updateScreenFunc();
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
            if(opcode == null)
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


    public static class Extensions
    {
        /// <summary>
        /// Extracts a nibble from a large number.
        /// </summary>
        /// <typeparam name="T">Any integer type.</typeparam>
        /// <param name="t">The value to extract nibble from.</param>
        /// <param name="nibblePos">The nibble to check,
        /// where 0 is the least significant nibble.</param>
        /// <returns>The extracted nibble.</returns>
        public static byte GetNibble<T>(this T t, int nibblePos)
         where T : struct, IConvertible
        {
            nibblePos *= 4;
            var value = t.ToInt64(CultureInfo.CurrentCulture);
            return (byte)((value >> nibblePos) & 0xF);
        }

        public static byte GetNibble(this short x)
        {
            byte nibble1 = (byte)(x & 0x0F);
            byte nibble2 = (byte)((x & 0xF0) >> 4);


            Console.WriteLine($"nibble1: {nibble1.ToHex()}, nibble2: {nibble2.ToHex()}");

            return nibble1;
        }

        public static byte GetNibble(this byte x)
        {
            byte nibble1 = (byte)(x & 0x0F);
            byte nibble2 = (byte)((x & 0xF0) >> 4);


            Console.WriteLine($"nibble1: {nibble1.ToHex()}, nibble2: {nibble2.ToHex()}");

            return nibble1;
        }

        public static (byte, byte) GetNibbles(this byte b)
        {
            // e.g.,
            //byte x = 0x12; //hexadecimal notation for decimal 18 or binary 0001 0010

            byte highNibble = (byte)(b >> 4 & 0xF); // = 0000 0001
            byte lowNibble = (byte)(b & 0xF); // = 0000 0010


//            Console.WriteLine($" -- GetNibbles: b = {b.ToHex()}, highNibble: {highNibble.ToHex()}, lowNibble: {lowNibble.ToHex()}");

            return (highNibble, lowNibble);
        }

        public static string ToHex(this byte[] bytes)
        {
            return Convert.ToHexString(bytes);
        }

        public static string ToHex(this byte b)
        {
            return Convert.ToHexString(new [] { b });
        }
    }
}
