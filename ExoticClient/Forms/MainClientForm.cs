using ExoticClient.App;
using ExoticClient.App.UI;
using System.Windows.Forms;

namespace ExoticClient.Forms
{
    public partial class MainClientForm : CustomForm
    {
        public MainClientForm()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            ChronicApplication.Instance.Shutdown();
        }

        private void MinimizeButton_Click(object sender, System.EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
    }
}
