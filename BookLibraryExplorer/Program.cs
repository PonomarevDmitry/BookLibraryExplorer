using System;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;

namespace BookLibraryExplorer
{
    static class Program
    {
        private static Mutex mutex = new Mutex(false, "Panama.Progs.BookLibraryExplorer");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!mutex.WaitOne(TimeSpan.FromSeconds(1), false))
            {
                return;
            }

            try
            {
                ProgramConfiguraton.LoadXmlConfig();

                Application.CurrentInputLanguage = InputLanguage.FromCulture(new CultureInfo("ru-RU"));

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormBookLibrary());

                ProgramConfiguraton.SaveXmlConfig();
            }
            finally { mutex.ReleaseMutex(); }
        }
    }
}
