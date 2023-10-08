using ExoticClient.App;
using ExoticClient.App.UI;
using ExoticClient.Classes;
using ExoticClient.Classes.Client;
using ExoticClient.Classes.Client.Authentication;
using ExoticClient.Classes.Client.PacketSystem;
using ExoticClient.Classes.Utils;
using Newtonsoft.Json;
using System.Text;

namespace ExoticClient.Forms
{
    public partial class LoginForm : CustomForm
    {
        private ChronicApplication _applicaton;
        private ExoticTcpClient _tcpClient;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, System.EventArgs e)
        {
            _applicaton = ChronicApplication.Instance;
            _tcpClient = _applicaton.TcpClient;
        }

        private async void LoginButton_Click(object sender, System.EventArgs e)
        {
            // if client is not connected do not try to send packet or handle it a different way
            string hashedPassword = PasswordHelper.HashPassword(PasswordTextbox.Text);

            UserAuthDetails userAuthDetails = new UserAuthDetails()
            {
                UserID = "",
                Username = UsernameTextbox.Text,
                PasswordHash = hashedPassword,
                PasswordSalt = ""
            };

            UserManager.Instance.SetUsername(userAuthDetails.Username);

            string userAuthDetailsJsonString = JsonConvert.SerializeObject(userAuthDetails);
            byte[] userAuthDetailsData = Encoding.UTF8.GetBytes(userAuthDetailsJsonString);

            Packet userAuthDetailsPacket = _tcpClient.PacketHandler.CreateNewPacket(userAuthDetailsData, "User Login Packet", true);
            await _tcpClient.PacketHandler.SendPacketAsync(userAuthDetailsPacket, _tcpClient.ClientHandler.GetNetworkStream());
        }

        private void SignupButton_Click(object sender, System.EventArgs e)
        {
            _applicaton.HideForm(_applicaton.FormHandler.LoginForm);
            _applicaton.ShowForm(_applicaton.FormHandler.RegisterForm);
        }

        private void ExitButton_Click(object sender, System.EventArgs e)
        {
            _tcpClient.DisconnectFromServer();
            ChronicApplication.Instance.Shutdown();
        }

        private void MinimizeButton_Click(object sender, System.EventArgs e)
        {
            WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }
    }
}
