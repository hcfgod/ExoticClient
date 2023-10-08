using ExoticClient.Forms;
using System.Windows.Forms;

namespace ExoticClient.App.UI
{
    public class FormHandler
    {
        public Form LoginForm { get; }
        public Form RegisterForm { get; }
        public Form MainForm { get; }

        public FormHandler()
        {
            LoginForm = new LoginForm();
            RegisterForm = new RegisterForm();
            MainForm = new MainClientForm();
        }
    }
}
