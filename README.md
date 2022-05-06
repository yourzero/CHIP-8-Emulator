# Introduction

This is a first attempt at a CHIP-8 Emulator.
It shall be able to read CHIP-8 program files (ROMs) and execute them.


# Project Organization

### CHIP-8 Emulator
This project is the starting point for the application. It is responsible for displaying the output screen (an emulator monitor screen), and executing the emulator.

### Emulator
This project contains the emulator code itself.

### CHIP-8 Emulator Tests
This is the unit test project.


# References

This emulator is based on CHIP-8 specifications:
- https://multigesture.net/articles/how-to-write-an-emulator-chip-8-interpreter/
- https://tobiasvl.github.io/blog/write-a-chip-8-emulator/#specifications


# Status

It is not yet complete: many opcodes must be implemented, and unit tests should be added.
The initial testing ROM, "IBM Logo.ch8", is not quite working yet - it requires a couple more opcodes to function.



