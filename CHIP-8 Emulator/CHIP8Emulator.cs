using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP_8_Emulator
{
    internal class CHIP8Emulator
    {
        Screen _screen = new Screen();
        public void Start()
        {




            ThreadStart start = new ThreadStart(delegate { StartScreen(_screen); });
            var screenThread = new Thread(start);
            screenThread.Start();

            //_screen.Show();
            //_screen.Initialize();
            //_screen.Close();
            Thread.Sleep(100);
            //_screen.Invoke(() => _screen.TestDraw());

            while (true)
            {
                _screen.RandomizeTest();
                Thread.Sleep(100);
            }
            //_screen.TestDraw();
        }

        private void StartScreen(Screen screen)
        {
            //screen = new Screen();
            Application.Run(screen);
            //Application.Run();
            //screen.Show();

        }
    }
}
