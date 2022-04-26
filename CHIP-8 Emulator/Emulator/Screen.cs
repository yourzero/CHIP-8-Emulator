using System;
using System.Collections;
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
            InitializeInner(Emulator.Screen.PIXEL_OFF);
        }

        private void InitializePixelsWhite()
        {
            InitializeInner(Emulator.Screen.PIXEL_ON);
        }

        private void InitializeInner(bool pixelValue)
        {
            Pixels = new bool[Emulator.Screen.PIXELS_WIDTH, Emulator.Screen.PIXELS_HEIGHT];

            for (int i = 0; i < Emulator.Screen.PIXELS_WIDTH; i++)
            {
                for (int j = 0; j < Emulator.Screen.PIXELS_HEIGHT; j++)
                {
                    Pixels[i, j] = pixelValue;
                }
            }

            _needsRefreshed = true;
        }

        public void Initialize(bool isFirstTime)
        {
            //if (isFirstTime) InitializePixelsWhite();

            //else
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

        internal void DrawBytesAsSprite(byte[] bytes, int x, int y)
        {
            // TODO - add Sprite class

            // each individual byte is a separate row (y)
            for(int bytesArrayIndex=0; bytesArrayIndex<bytes.Length; bytesArrayIndex++)
            {
                var thisByte = bytes[bytesArrayIndex];


                var bits = new LittleEndianBitArray(new[] { thisByte } );


                var bitsLog = new StringBuilder();
                for(int i=0; i<bits.Bits.Length; i++) bitsLog.Append(bits.Bits[i] ? "1" : "0");
                Console.WriteLine($"DrawBytesAsSprite: {thisByte.ToHex()} = {bitsLog}");


                // TODO - bounds check(s)

                for (int bitIndex =0; bitIndex < bits.Bits.Length; bitIndex++)
                {
                    var drawAtX = x + bitIndex;
                    var drawAtY = y + bytesArrayIndex;

                    //if (drawAtX < Pixels.GetUpperBound(0) && drawAtY < Pixels.GetUpperBound(1))
                    //{
                        Pixels[drawAtX, drawAtY] = bits.Bits[bitIndex];
                    //}
                }


            }


            //BitArray bytesAsBits = new BitArray(bytes);

            //// TODO - bounds check(s)

            //for(int i=0; i<bytesAsBits.Length; i++)
            //{
            //    var drawAtAx = x + i;
            //    var drawAtY = y+
            //    Pixels[drawAtAx, y] = bytesAsBits[i];
            //}

//            bytesAsBits.CopyTo(Pixels[x, y], bytes.Length);

            //for(int i = 0; i<bytes.Length; i++)

            this._needsRefreshed = true;
        }
    }

    /// <summary>
    /// A BitArray (wrapper) that stores the bits in the little endian (original BitArray puts least significant bits first)
    /// </summary>
    public class LittleEndianBitArray
    {
        public BitArray Bits { get;  }

        public LittleEndianBitArray(byte[] bytes)
        {
            var originalBitArray = new BitArray(bytes);

            // this isn't the most efficient, but it doesn't really need to be
            var arrOriginalBitArray = new bool[originalBitArray.Length];
            originalBitArray.CopyTo(arrOriginalBitArray, 0);
            var arrReversedBitarray = arrOriginalBitArray.Reverse().ToArray();
           Bits = new BitArray(arrReversedBitarray);
        }

    }
}
