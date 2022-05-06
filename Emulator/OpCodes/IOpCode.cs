namespace CHIP_8.Emulator.OpCodes
{
    /// <summary>
    /// Stores all of the information about an op code, including the possible combinations of its bytes/nibbles
    /// </summary>
    public interface IOpCode
    {
        byte OpCodeInstruction { get;}
        byte N { get;}
        byte NN { get;}
        ushort NNN { get;}
        byte X { get;}
        byte Y { get;}

        ExecutionResult Execute(ExecutionContext context);
    }

}
