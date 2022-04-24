using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHIP_8_Emulator.Emulator
{
    public class Screen
    {
        public const int PIXELS_WIDTH = 64;
        public const int PIXELS_HEIGHT = 32;

        public const bool PIXEL_OFF = false;
        public const bool PIXEL_ON = true;

        //private bool[,] _pixels;

        public bool[,] Pixels { get; private set; }

        /// <summary>
        /// Indicates that the screen data has been updated and the screen must be refreshed
        /// </summary>
        private bool _needsRefreshed { get; set; }

        private void InitializePixels()
        {
            Pixels = new bool[Emulator.Screen.PIXELS_WIDTH, Emulator.Screen.PIXELS_HEIGHT];

            for (int i = 0; i < Emulator.Screen.PIXELS_WIDTH; i++)
            {
                for (int j = 0; j < Emulator.Screen.PIXELS_HEIGHT; j++)
                {
                    Pixels[i, j] = Emulator.Screen.PIXEL_OFF;
                }
            }

            _needsRefreshed = true;
        }


        public void Initialize()
        {
            InitializePixels();
        }

        public void RandomizeTest()
        {
            Random rnd = new Random();
            for (int x = 0; x < Emulator.Screen.PIXELS_WIDTH; x++)
            {
                for (int y = 0; y < Emulator.Screen.PIXELS_HEIGHT; y++)
                {
                    var v = rnd.Next(2);
                    Pixels[x, y] = (v == 1);

                }
            }

            this._needsRefreshed = true;
        }

        public bool NeedsRefreshed()
        {
            return _needsRefreshed;
        }

        public void MarkAsRefreshed()
        {
            _needsRefreshed = false;
        }

    }
}
