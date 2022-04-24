namespace CHIP_8_Emulator.Emulator
{


    public static class OpCodeDecoder
    {
        static Dictionary<byte, IOpCode> _opCodes; // indexed by the op code nibble



        internal static IOpCode Decode(Instruction instruction)
        {
            /*
             *  00E0 (clear screen)
                1NNN (jump)
                6XNN (set register VX)
                7XNN (add value to register VX)
                ANNN (set index register I)
                DXYN (display/draw)
            */

            //switch(instruction.)

            var firstOpCode = instruction.GetInstructionOpCode();
            
            Console.WriteLine($"Decoding instruction {instruction.Bytes.ToHex()}: Op = {firstOpCode.ToHex()}...");
            
            IOpCode decodedOpCode = Decode(firstOpCode);

            Console.WriteLine($"Decoded instruction {instruction.Bytes.ToHex()}: Op = {firstOpCode.ToHex()} -- OpCode found: {decodedOpCode}");





            return decodedOpCode;
        }

        private static IOpCode Decode(byte b)
        {
            if(!OpCodes.ContainsKey(b))
            {
                throw new NotImplementedException($"Opcode {b.ToHex()} is not implemented.");
            }

            return OpCodes[b];
        }

            

        static Dictionary<byte, IOpCode> OpCodes
        {
            get
            {
                if (_opCodes == null) _opCodes = LoadOpCodes();
                return _opCodes;
            }
        }

        private static Dictionary<byte, IOpCode> LoadOpCodes()
        {
            var opCodes = new Dictionary<byte, IOpCode>();

            foreach (var type in GetAllTypesThatImplementInterface<IOpCode>())
            {
                var handler = (IOpCode)Activator.CreateInstance(type);

                opCodes.Add(handler.OperationNibble, handler);
            }

            return opCodes;
        }

        private static IEnumerable<Type> GetAllTypesThatImplementInterface<T>()
        {
            return System.Reflection.Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract);
        }
    }

    public interface IOpCode
    {
        byte Instruction { get; set; }
        byte N { get; set; }
        byte NN { get; set; }
        byte NNN { get; set; }
        byte OperationNibble { get; }
        byte X { get; set; }
        byte Y { get; set; }
    }

    public abstract class OpCode : IOpCode
    {
        public abstract byte OperationNibble { get; }

        private byte _instruction;
        private byte _x; // The second nibble.Used to look up one of the 16 registers(VX) from V0 through VF.
        private byte _y; // The third nibble.Also used to look up one of the 16 registers(VY) from V0 through VF.
        private byte _n; // The fourth nibble.A 4-bit number.
        private byte _nn; // The second byte (third and fourth nibbles). An 8-bit immediate number.
        private byte _nnn; // The second, third and fourth nibbles.A 12-bit immediate memory address.

        public byte Instruction { get => _instruction; set => _instruction = value; }
        public byte X { get => _x; set => _x = value; }
        public byte Y { get => _y; set => _y = value; }
        public byte N { get => _n; set => _n = value; }
        public byte NN { get => _nn; set => _nn = value; }
        public byte NNN { get => _nnn; set => _nnn = value; }

        /*
         * X: The second nibble. Used to look up one of the 16 registers (VX) from V0 through VF.
        Y: The third nibble. Also used to look up one of the 16 registers (VY) from V0 through VF.
        N: The fourth nibble. A 4-bit number.
        NN: The second byte (third and fourth nibbles). An 8-bit immediate number.
        NNN: The second, third and fourth nibbles. A 12-bit immediate memory address.
        */




    }


    class OpCode_ClearScreen : OpCode
    {
        public override byte OperationNibble => 0;

        public override string ToString()
        {
            return $"[OpCode: Clear Screen]";
        }
    }


}
