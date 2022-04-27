using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP_8_Emulator.Emulator.OpCode
{
    [OpCodeForInstruction(0xA)]
    class OpCode_SetIndexRegister : OpCodeBase
    {
        public OpCode_SetIndexRegister(Instruction instruction) : base(instruction)
        {
        }

        //    public override byte OperationNibble => 1;

        public override ExecutionResult Execute(ExecutionContext context)
        {
            // 
            throw new NotImplementedException();

            //return new ExecutionResult();
        }

        public override string ToString()
        {
            return $"[OpCode: Set Index Register I]";
        }
    }


    [OpCodeForInstruction(0x6)]
    class OpCode_SetRegister : OpCodeBase
    {
        public OpCode_SetRegister(Instruction instruction) : base(instruction)
        {
        }

        //    public override byte OperationNibble => 1;

        public override ExecutionResult Execute(ExecutionContext context)
        {
            // 
            throw new NotImplementedException();

            //return new ExecutionResult();
        }

        public override string ToString()
        {
            return $"[OpCode: Set Register vx]";
        }
    }


}
