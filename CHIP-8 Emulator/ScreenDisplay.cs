using CHIP_8_Emulator.Emulator;
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

namespace CHIP_8_Emulator
{
    public partial class ScreenDisplay : Form
    {
        private const int SCREEN_PIXEL_MULTIPLIER = 20;
        private const int ScreenWidth = Emulator.Screen.PIXELS_WIDTH * SCREEN_PIXEL_MULTIPLIER;
        private const int ScreenHeight = Emulator.Screen.PIXELS_HEIGHT * SCREEN_PIXEL_MULTIPLIER;

        public Emulator.Screen Screen { get; set; }

        //public ScreenDisplay(Emulator.Screen screen) : this()
        //{
        //    Screen = screen;

        //    //// wire the emulator's screen update event
        //    //screen.UpdateDisplay = this.Refresh;
        //}

        public ScreenDisplay()
        {
            //this.Size = new System.Drawing.Size(ScreenWidth, ScreenHeight);
            this.ClientSize = new System.Drawing.Size(ScreenWidth+1, ScreenHeight+1);
            
            InitializeComponent();

            SetStyle(
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.UserPaint |
                 ControlStyles.AllPaintingInWmPaint, true);

            //InitializePixels();
        }

        public override void Refresh()
        {
            Console.WriteLine("-- UPDATING SCREEN --");
            base.Refresh();
        }

        //public void Refresh()
        //{
        //    this.Invalidate();
        //}





        protected override void InitLayout()
        {
            this.Width = ScreenWidth;
            this.Height = ScreenHeight;

            base.InitLayout();

        }



        public void Initialize()
        {
            ////this.Width = ScreenWidth;
            ////this.Height = ScreenHeight;

            //InitLayout();

            //var graphics = this.CreateGraphics();
            //var blackBrush = new SolidBrush(Color.Black);
            //var blackPen = new Pen(blackBrush, 12.0F);
            //graphics.DrawRectangle(blackPen, new Rectangle(0, 0, ScreenWidth, ScreenHeight));

            //this.Invalidate();
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
                //if (x % 2 == 0) continue;
                graphics.DrawString(x.ToHex(), DefaultFont, blue, x * SCREEN_PIXEL_MULTIPLIER, 0);
            }

            for (byte y = 1; y < Emulator.Screen.PIXELS_HEIGHT; y++)
            {
                //if (x % 2 == 0) continue;
                graphics.DrawString(y.ToHex(), DefaultFont, blue, 0, y * SCREEN_PIXEL_MULTIPLIER);
            }
        }

        private void DrawGraphics(Graphics graphics)
        {
            if (Screen == null) return;


            //// TODO - read memory and draw
            //if(_testDraw)
            //{
            //    var pen = new Pen(Color.Red, 5.0F);
            //    graphics.DrawLine(pen, new Point(5, 5), new Point(100, 100));
            //}
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

            //for (int x=0; x<PIXELS_WIDTH; x++)
            //{

            //    Console.Write("| ");

            //    for (int y=0; y<PIXELS_HEIGHT; y++)
            //    {
            //        var pixel = _pixels[x, y];
            //        var pixelBinary = pixel ? 1 : 0;

            //        Console.Write($"{pixelBinary}");
            //    }

            //    Console.Write(" |");
            //    Console.WriteLine("");

            //}

            Console.Write("|   || ");
            for (int x = 0; x < Emulator.Screen.PIXELS_WIDTH; x++) Console.Write("=");
            Console.Write(" ||");
            Console.WriteLine("");


        }

        private void DrawMainScreen(Graphics graphics)
        {
            //var graphics = this.CreateGraphics();
            //var blackBrush = new SolidBrush(Color.Black);
            //var blackPen = new Pen(blackBrush, 12.0F);

            //const int OuterOffset = 10; // offset from the main window borders
            //const float OutlineWidth = 5.0F;

            //var outlinePen = new Pen(Color.White, OutlineWidth);
            //graphics.DrawRectangle(outlinePen, new Rectangle(OuterOffset, OuterOffset, ScreenWidth, ScreenHeight));

            ////var blackPen = new Pen(Color.Black, 12.0F);
            //graphics.DrawRectangle(blackPen, new Rectangle(0, 0, ScreenWidth, ScreenHeight));

            var consoleBrush = new SolidBrush(Color.Black);
            graphics.FillRectangle(consoleBrush, GetConsoleRectangle());
            //            graphics.DrawRectangle(blackPen, GetConsoleRectangle());


        }

        private static Rectangle GetConsoleRectangle()
        {
            return new Rectangle(0, 0, ScreenWidth, ScreenHeight);
        }


    }
}
