using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP_8.Emulator.OpCodes
{
    [OpCodeForInstruction(0xA)]
    class OpCode_SetIndexRegister : OpCodeBase
    {
        public OpCode_SetIndexRegister(Instruction instruction) : base(instruction)
        {
        }

        public override ExecutionResult Execute(ExecutionContext context)
        {
            context.Registers.I = this.NNN;

            return new ExecutionResult();
        }

        public override string ToString()
        {
            return $"[OpCode: Set Index Register I]";
        }
    }

}
