using CHIP_8_Emulator.Emulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP_8_Emulator
{
    internal class CHIP8Emulator
    {
        readonly Screen _screen = new Screen();
        readonly Emulator.Program _program;
        
        
        public CHIP8Emulator(string programFilePath)
        {
            _program = new Emulator.Program(programFilePath);
        }




        public async Task StartAsync()
        {
            ThreadStart start = new ThreadStart(delegate { StartScreen(_screen); });
            var screenThread = new Thread(start);
            screenThread.Start();


            ThreadStart startEmu = new ThreadStart(delegate { StartEmulatorAsync(); });
            var emuThread = new Thread(startEmu);
            emuThread.Start();

            //_screen.Show();
            //_screen.Initialize();
            //_screen.Close();
            //Thread.Sleep(100);
            //_screen.Invoke(() => _screen.TestDraw());

            //while (true)
            //{
            //    _screen.RandomizeTest();
            //  //  Thread.Sleep(100);
            //}
            //_screen.TestDraw();
        }

        private async Task StartEmulatorAsync()
        {

            await _program.LoadProgramAsync();
            _program.DebugOutputProgramData();

            Thread.Sleep(500);

            var executor = new Executor(_program);
            executor.Run();

        }



        private void StartScreen(Screen screen)
        {
            //screen = new Screen();
            Application.Run(screen);
            //Application.Run();
            //screen.Show();

        }

        //private async Task LoadProgramAsync()
        //{
        //    var programFileBytes = await File.ReadAllBytesAsync(_programFilePath);
        //    _program = programFileBytes;

        //    // TODO - error check
        //}
    }
}
