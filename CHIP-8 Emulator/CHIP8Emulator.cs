﻿using CHIP_8_Emulator.Emulator;

namespace CHIP_8_Emulator
{
    /// <summary>
    /// The starting point for the emulator. starts all of the parts, then hands off control to the processor.
    /// </summary>
    internal class CHIP8Emulator
    {
        readonly ScreenDisplay _screenForm = new ScreenDisplay();
        readonly Emulator.Program _program;


        public CHIP8Emulator(string programFilePath)
        {
            _program = new Emulator.Program(programFilePath);

        }

        public async Task StartAsync()
        {
            // start the screen display (form) 

            ThreadStart start = new ThreadStart(delegate { StartScreen(_screenForm); });
            var screenThread = new Thread(start);
            screenThread.Start();


            // start the eulator

            ThreadStart startEmu = new ThreadStart(delegate { StartEmulatorAsync(); });
            var emuThread = new Thread(startEmu);
            emuThread.Start();

        }

        private async Task StartEmulatorAsync()
        {
            await _program.LoadProgramAsync();
            _program.DebugOutputProgramData();

            Thread.Sleep(500);

            var executor = new Processor(_program, UpdateScreen);
            this._screenForm.Screen = executor.Screen;
            executor.Run();
        }

        public void UpdateScreen()
        {
            _screenForm.Invoke(() => this._screenForm.Refresh());
        }


        private void StartScreen(ScreenDisplay screen)
        {
            Application.Run(screen);
        }

    }
}
