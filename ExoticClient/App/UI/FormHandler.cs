using ExoticClient.Forms;
using System.Windows.Forms;

namespace ExoticClient.App.UI
{
    public class FormHandler
    {
        public Form MainForm { get; }

        public FormHandler()
        {
            MainForm = new MainClientForm();
        }
    }
}
