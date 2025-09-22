using System;
using System.Windows.Forms;

namespace SPCPharmacyManagement
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set application-wide exception handling
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Show splash screen or login form
            try
            {
                LoginForm loginForm = new LoginForm();
                Application.Run(loginForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application startup error: {ex.Message}", "Startup Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"An unexpected error occurred: {e.Exception.Message}", "Application Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"A critical error occurred: {((Exception)e.ExceptionObject).Message}", "Critical Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
