using CHIP_8.Emulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHIP_8
{
    /// <summary>
    /// Represents the end result of the screen - e.g., the CRT or monitor
    /// </summary>
    public partial class ScreenDisplay : Form
    {
        private const int SCREEN_PIXEL_MULTIPLIER = 20;
        private const int ScreenWidth = Emulator.Screen.PIXELS_WIDTH * SCREEN_PIXEL_MULTIPLIER;
        private const int ScreenHeight = Emulator.Screen.PIXELS_HEIGHT * SCREEN_PIXEL_MULTIPLIER;

        public Emulator.Screen Screen { get; set; }
        
        public ScreenDisplay()
        {
            //this.Size = new System.Drawing.Size(ScreenWidth, ScreenHeight);
            this.ClientSize = new System.Drawing.Size(ScreenWidth+1, ScreenHeight+1);
            
            InitializeComponent();

            SetStyle(
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.UserPaint |
                 ControlStyles.AllPaintingInWmPaint, true);
        }

        public override void Refresh()
        {
            Console.WriteLine("-- UPDATING SCREEN --");
            base.Refresh();
        }

        protected override void InitLayout()
        {
            this.Width = ScreenWidth;
            this.Height = ScreenHeight;

            base.InitLayout();

        }

        public void Initialize()
        {
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // TODO - this might need to be in OnPaintBackground
            DrawMainScreen(e.Graphics);

            DrawGraphics(e.Graphics);

            // TODO - make this a debug option
            DrawGridOverlay(e.Graphics);
        }

        private void DrawGridOverlay(Graphics graphics)
        {
            var redPen = new Pen(Color.FromArgb(20, 20, 20));
            for(int y = 0; y <= Emulator.Screen.PIXELS_HEIGHT; y++)
            {
                int verticalPos = y * SCREEN_PIXEL_MULTIPLIER;
                graphics.DrawLine(redPen, 0, verticalPos, ScreenWidth - 1, verticalPos);
            }
            for (int x = 0; x <= Emulator.Screen.PIXELS_WIDTH; x++)
            {
                int horizontalPos = x * SCREEN_PIXEL_MULTIPLIER;
                graphics.DrawLine(redPen, horizontalPos, 0, horizontalPos, ScreenHeight-1);
            }

            var blue = new SolidBrush(Color.FromArgb(20, 0, 255, 0));
            for(byte x = 0; x < Emulator.Screen.PIXELS_WIDTH; x++)
            {
                graphics.DrawString(x.ToHex(), DefaultFont, blue, x * SCREEN_PIXEL_MULTIPLIER, 0);
            }

            for (byte y = 1; y < Emulator.Screen.PIXELS_HEIGHT; y++)
            {
                graphics.DrawString(y.ToHex(), DefaultFont, blue, 0, y * SCREEN_PIXEL_MULTIPLIER);
            }
        }

        private void DrawGraphics(Graphics graphics)
        {
            if (Screen == null) return;

            // TODO - eventually remove tons of this logging code

            Console.WriteLine("");


            Console.Write("|   || ");
            for (int x = 0; x < Emulator.Screen.PIXELS_WIDTH; x++) Console.Write("=");
            Console.Write(" ||");
            Console.WriteLine("");



            Console.Write("|   || ");
            //for (int x = 0; x < Emulator.Screen.PIXELS_WIDTH; x++) Console.Write("=");
            for (int x = 0; x < Emulator.Screen.PIXELS_WIDTH; x++) Console.Write(x % 10);
            Console.Write(" ||");
            Console.WriteLine("");



            Console.Write("|   || ");
            for (int x = 0; x < Emulator.Screen.PIXELS_WIDTH; x++) Console.Write("-");
            Console.Write(" ||");
            Console.WriteLine("");


            var pixelOffBrush = new SolidBrush(Color.Black);
            var pixelOnBrush = new SolidBrush(Color.White);

            for (int y = 0; y < Emulator.Screen.PIXELS_HEIGHT; y++)
            {
                var rowNumber = y;
                var strRowNumber = (rowNumber < 10) ? $"0{rowNumber}" : rowNumber.ToString();
                Console.Write($"| {strRowNumber}|| ");

                for (int x = 0; x < Emulator.Screen.PIXELS_WIDTH; x++)
                {
                    var pixel = Screen.Pixels[x, y];
                    var pixelBinary = pixel ? 1 : 0;

                    Console.Write($"{pixelBinary}");

                    // draw each pixel as a rectangle since it's being upscaled (10x)


                    var pixelRectangle = new Rectangle(x * SCREEN_PIXEL_MULTIPLIER, y * SCREEN_PIXEL_MULTIPLIER, SCREEN_PIXEL_MULTIPLIER, SCREEN_PIXEL_MULTIPLIER);
                    var brushToUse = pixel ? pixelOnBrush : pixelOffBrush;
                    graphics.FillRectangle(brushToUse, pixelRectangle);
                }


                Console.Write(" ||");
                Console.WriteLine("");
            }

            Console.Write("|   || ");
            for (int x = 0; x < Emulator.Screen.PIXELS_WIDTH; x++) Console.Write("=");
            Console.Write(" ||");
            Console.WriteLine("");
        }

        /// <summary>
        /// Draw the base / background of the screen
        /// </summary>
        /// <param name="graphics"></param>
        private void DrawMainScreen(Graphics graphics)
        {
            var consoleBrush = new SolidBrush(Color.Black);
            graphics.FillRectangle(consoleBrush, GetConsoleRectangle());
        }

        private static Rectangle GetConsoleRectangle()
        {
            return new Rectangle(0, 0, ScreenWidth, ScreenHeight);
        }


    }
}
