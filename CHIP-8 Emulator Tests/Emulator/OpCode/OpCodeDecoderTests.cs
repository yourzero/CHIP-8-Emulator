using CHIP_8.Emulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
