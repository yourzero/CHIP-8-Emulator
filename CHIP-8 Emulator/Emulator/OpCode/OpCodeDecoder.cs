namespace CHIP_8_Emulator.Emulator
{
    public static class OpCodeDecoder
    {
        //static Dictionary<byte, IOpCode> _opCodes; // indexed by the op code nibble
        static Dictionary<byte, Type> _opCodes;



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

            // IOpCode decodedOpCode = Decode(firstOpCode);
            //decodedOpCode.SetInstruction(instruction);

            if (!OpCodeTypes.ContainsKey(firstOpCode))
            {
                Console.WriteLine($"Opcode {firstOpCode.ToHex()} is not implemented.");
                //   throw new NotImplementedException($"Opcode {b.ToHex()} is not implemented.");
                return null;
            }

            var decodedOpCode = GetOpCodeInstance(OpCodeTypes[firstOpCode], instruction);


            Console.WriteLine($"Decoded instruction {instruction.Bytes.ToHex()}: Op = {firstOpCode.ToHex()} -- OpCode found: {decodedOpCode}");


            return decodedOpCode;
        }

        //private static IOpCode Decode(byte b)
        //{
        //    if (!OpCodes.ContainsKey(b))
        //    {
        //        Console.WriteLine($"Opcode {b.ToHex()} is not implemented.");
        //        //   throw new NotImplementedException($"Opcode {b.ToHex()} is not implemented.");
        //        return null;
        //    }


        //    return OpCodes[b];
        //}

        //private static IOpCode Decode(Instruction instruction)
        //{
        //    byte b
        //    if (!OpCodes.ContainsKey(b))
        //    {
        //        Console.WriteLine($"Opcode {b.ToHex()} is not implemented.");
        //        //   throw new NotImplementedException($"Opcode {b.ToHex()} is not implemented.");
        //        return null;
        //    }


        //    return OpCodes[b];
        //}


        static Dictionary<byte, Type> OpCodeTypes
        {
            get
            {
                if (_opCodes == null) _opCodes = LoadOpCodeTypes();
                return _opCodes;
            }
        }

        static IOpCode GetOpCodeInstance(Type opCodeType, Instruction instruction)
        {
            return (IOpCode)Activator.CreateInstance(opCodeType, instruction);
        }


        //static Dictionary<byte, IOpCode> OpCodes
        //{
        //    get
        //    {
        //        if (_opCodes == null) _opCodes = LoadOpCodes();
        //        return _opCodes;
        //    }
        //}

        //private static Dictionary<byte, IOpCode> LoadOpCodes()
        //{
        //    var opCodes = new Dictionary<byte, IOpCode>();

        //    foreach (var type in GetAllTypesThatImplementInterface<IOpCode>())
        //    {
        //        var handler = (IOpCode)Activator.CreateInstance(type);

        //        opCodes.Add(handler.OperationNibble, handler);
        //    }

        //    return opCodes;
        //}

        private static Dictionary<byte, Type> LoadOpCodeTypes()
        {
            var opCodes = new Dictionary<byte, Type>();

            foreach (var type in GetAllTypesThatImplementInterface<IOpCode>())
            {
                // instantiate the type to get its operation nibble
                var handler = (IOpCode)Activator.CreateInstance(type);

                opCodes.Add(handler.OperationNibble, type);
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


}
