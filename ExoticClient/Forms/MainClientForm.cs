using ExoticClient.App;
using ExoticClient.App.UI;
using ExoticClient.Classes.Client;
using ExoticClient.Classes.Client.PacketSystem;
using System.Text;
using System.Windows.Forms;

namespace ExoticClient.Forms
{
    public partial class MainClientForm : CustomForm
    {
        private ChronicApplication _application;
        private ExoticTcpClient _tcpClient;

        public MainClientForm()
        {
            InitializeComponent();

            _application = ChronicApplication.Instance;
        }

        private async void MainClientForm_Load(object sender, System.EventArgs e)
        {
            await ChronicApplication.Instance.TcpClient.ConnectToServer();

            _tcpClient = _application.TcpClient;

            string testDataString = "Hey Keith You Did It!";
            byte[] testDataBytes = Encoding.UTF8.GetBytes(testDataString);

            Packet testPacket = ChronicApplication.Instance.TcpClient.PacketHandler.CreateNewPacket(testDataBytes, 1);
            await _tcpClient.PacketHandler.SendPacketAsync(testPacket, _tcpClient.ClientHandler.GetNetworkStream());
        }

        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            ChronicApplication.Instance.TcpClient.DisconnectFromServer();

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
