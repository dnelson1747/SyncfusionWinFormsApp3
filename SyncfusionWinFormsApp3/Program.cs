using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SyncfusionWinFormsApp3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTU5OTc2OUAzMjMxMmUzMTJlMzMzNUFsKzJaMDJ4SHFuUGYwaW1paWdkeEdjTURWR2F3N3FXbEo4YnRnalZidDQ9");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
