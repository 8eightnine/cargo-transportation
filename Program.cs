using cargo_transportation.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cargo_transportation
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AuthForm authForm = new AuthForm();
            try
            {
                Application.Run(authForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }

            if (authForm.isAuthorized == true)
            {
                User user = authForm.user;
                Application.Run( new MainForm(user));
            }
        }
    }
}
