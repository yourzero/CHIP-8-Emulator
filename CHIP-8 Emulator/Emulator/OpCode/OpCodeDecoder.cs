namespace CHIP_8_Emulator.Emulator
{
    /// <summary>
    /// A factory class that decodes an instruction and determines/generates the op code to be used to represent and execute the instruction
    /// </summary>
    public static class OpCodeDecoder
    {
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

            var firstOpCode = instruction.GetInstructionOpCode();

            Console.WriteLine($"Decoding instruction {instruction.Bytes.ToHex()}: Op = {firstOpCode.ToHex()}...");

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

        /// <summary>
        /// Loads all of the op code classes, and indexes them by the primary op code (the first nibble in the instruction).
        /// // TODO - some op codes/instructions don't use just the first nibble, so this needs to be handled
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static Dictionary<byte, Type> LoadOpCodeTypes()
        {
            var opCodes = new Dictionary<byte, Type>();

            foreach (var type in GetAllTypesThatImplementInterface<IOpCode>())
            {
                // Each op code class has an attribute that represents the main op code (first nibble)
                var instructionAttribute = type.GetCustomAttributes(typeof(OpCodeForInstructionAttribute), false).FirstOrDefault() as OpCodeForInstructionAttribute;

                if (instructionAttribute != null) opCodes.Add(instructionAttribute.InstructionNibble, type);
                else throw new NotImplementedException($"Unable to find teh OpCodeForInstructionAttribute for class {type}.");
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
