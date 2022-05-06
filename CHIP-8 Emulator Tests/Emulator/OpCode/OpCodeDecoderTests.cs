using CHIP_8.Emulator;
using CHIP_8.Emulator.OpCodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CHIP_8_Emulator_Tests.Emulator.OpCode
{
    [TestClass]
    public class OpCodeDecoderTests
    {
        [TestMethod]
        public void Decode_Test()
        {
            // Arrange
            byte testOpCode = 0x12;
            var instruction = new Mock<Instruction>();
            instruction.Setup(x => x.GetInstructionOpCode()).Returns(testOpCode);

            // Act
            var decoded = OpCodeDecoder.Decode(instruction.Object);

            // Assert
            Assert.AreEqual(testOpCode, decoded.OpCodeInstruction);
        }

        // TODO - a lot more!


        private class Scope
        {

        }
    }
}
