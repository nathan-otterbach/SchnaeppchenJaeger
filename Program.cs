using SchnaeppchenJaeger.Client;
using SchnaeppchenJaeger.Database;
using SchnaeppchenJaeger.Utility;

namespace SchnaeppchenJaeger
{
    public static class Program
    {
        public static Utils _utils = new Utils();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}