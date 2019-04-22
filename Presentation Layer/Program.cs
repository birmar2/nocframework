using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
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

            Loginform loginForm = new Loginform();
            Application.Run(loginForm);

            if (loginForm.UserSuccessfullyAuthenticated)
            {
                MainForm mainForm = new MainForm();
                mainForm.Controls["loggedInAs"].Text = "Bejelentkezve, mint "+ loginForm.UserName;
                Application.Run(mainForm);
            }

        }
    }
}
