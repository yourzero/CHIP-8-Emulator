﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP_8_Emulator.Emulator
{
    internal static class Font
    {
        public readonly static byte[][] FontBitmap = new byte[][]
        {
                new byte[] { 0xF0, 0x90, 0x90, 0x90, 0xF0 }, // 0
                new byte[] { 0x20, 0x60, 0x20, 0x20, 0x70 }, // 1
                new byte[] { 0xF0, 0x10, 0xF0, 0x80, 0xF0 }, // 2
                new byte[] { 0xF0, 0x10, 0xF0, 0x10, 0xF0 }, // 3
                new byte[] { 0x90, 0x90, 0xF0, 0x10, 0x10 }, // 4
                new byte[] { 0xF0, 0x80, 0xF0, 0x10, 0xF0 }, // 5
                new byte[] { 0xF0, 0x80, 0xF0, 0x90, 0xF0 }, // 6
                new byte[] { 0xF0, 0x10, 0x20, 0x40, 0x40 }, // 7
                new byte[] { 0xF0, 0x90, 0xF0, 0x90, 0xF0 }, // 8
                new byte[] { 0xF0, 0x90, 0xF0, 0x10, 0xF0 }, // 9
                new byte[] { 0xF0, 0x90, 0xF0, 0x90, 0x90 }, // A
                new byte[] { 0xE0, 0x90, 0xE0, 0x90, 0xE0 }, // B
                new byte[] { 0xF0, 0x80, 0x80, 0x80, 0xF0 }, // C
                new byte[] { 0xE0, 0x90, 0x90, 0x90, 0xE0 }, // D
                new byte[] { 0xF0, 0x80, 0xF0, 0x80, 0xF0 }, // E
                new byte[] { 0xF0, 0x80, 0xF0, 0x80, 0x80 }  // F
        };

    }
}
