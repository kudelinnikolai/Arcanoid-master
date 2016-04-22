#region using

using System;
using System.Windows.Forms;

#endregion

namespace Arcanoid
{
    /// <summary>
    ///     The program.
    /// </summary>
    public static class Program
    {
        #region Private Methods

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        #endregion
    }
}