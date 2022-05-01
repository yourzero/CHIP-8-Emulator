namespace CHIP_8.Emulator.OpCode
{
    [OpCodeForInstruction(0xD)] // DXYN
    class OpCode_DisplayDraw : OpCodeBase
    {
        public OpCode_DisplayDraw(Instruction instruction) : base(instruction)
        {
        }

        public override ExecutionResult Execute(ExecutionContext context)
        {
            // get the x and y coordinates from VX and VY

            var x = context.Registers.Variables[this.X] % Screen.PIXELS_WIDTH; // modulo by the width allows the value to be "wrapped"
            var y = context.Registers.Variables[this.Y] % Screen.PIXELS_HEIGHT; // modulo by the height allows the value to be "wrapped"

            context.Registers.VF = 0;

            var startingMemoryLoc = context.Registers.I;

            var spriteBytes = context.Memory.Read(startingMemoryLoc, this.N);
            context.Screen.DrawBytesAsSprite(spriteBytes, x, y);


            // TODO - XOR/flip pixels
            // 0 is interpreted as transparent, 1 flips the pixel


            /*
             * This is the most involved instruction. It will draw an N pixels tall sprite from the memory location that the 
             * I index register is holding to the screen, 
             * at the horizontal X coordinate in VX
             * and the Y coordinate in VY. 
             * 
             * All the pixels that are “on” in the sprite will flip the pixels on the screen that it is drawn to. 
             * If any pixels on the screen were turned “off” by this, the VF flag register is set to 1. Otherwise, it’s set to 0.

                Sounds hard? Well, it is, a little.

                The first thing to do is to get the X and Y coordinates from VX and VY.

                A common mistake here is to use X and Y directly; don’t do that, fetch them from the registers.

                One area where people get confused is whether sprites should wrap if they go over the edge of the screen. The answer is yes and no.

                The starting position of the sprite will wrap. In other words, an X coordinate of 5 is the same as an X of 68 (since the screen is 64 pixels wide). Another way of saying it is that the coordinates are modulo (or binary AND) the size of the display (when counting from 0).

                However, the actual drawing of the sprite should not wrap. If a sprite is drawn near the edge of the screen, it should be clipped, and not wrap. The sprite should be partly drawn near the edge, and the other part should not reappear on the opposite side of the screen.

            */


            return new ExecutionResult();
        }

        public override string ToString()
        {
            return $"[OpCode: Display/Draw]";
        }
    }

}
