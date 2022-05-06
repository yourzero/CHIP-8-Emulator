# Introduction

This is the emulator itself. Each component of the "CPU"/emulator is contained in a separate class (e.g., Memory (RAM), Registers, Screen). The "Processor" class contains the run loop. Each possible op code is implemented in a separate class.

### Organization

*Extensions*: Any C# Extensions go here.
*OpCodes*: The implementations of all of the CHIP-8 op codes. See OpCodeDecoder.cs for more information on how each op code should be implemented. All opcodes should imlement OpCodeBase.