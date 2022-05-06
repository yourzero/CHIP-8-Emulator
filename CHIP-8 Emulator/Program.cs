namespace CHIP_8
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // TODO - eventually allow choosing or passing in the ROM path.

            const string PROGRAM_PATH = @"D:\CHIP-8 Emulator\ROMs\IBM Logo.ch8";

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var emulator = new CHIP8Emulator(PROGRAM_PATH);
            emulator.StartAsync();
        }

    }
}