namespace CHIP_8
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            const string PROGRAM_PATH = @"D:\CHIP-8 Emulator\ROMs\IBM Logo.ch8";

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());


            var emulator = new CHIP8Emulator(PROGRAM_PATH);
            await emulator.StartAsync();

            //while(true) { }
        }





    }
}