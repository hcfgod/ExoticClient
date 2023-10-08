using ExoticClient.App;
using ExoticClient.Classes.Utils;
using System.Text;
using System.Windows.Forms;

namespace ExoticClient.Classes.Client.PacketSystem.Packets
{
    public class LoginResponsePacket : IPacketHandler
    {
        private ChronicApplication _application;

        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            _application = ChronicApplication.Instance;

            string response = Encoding.UTF8.GetString(packet.Data);

            if(response == "Login Successful")
            {
                MessageBox.Show("Login Successful");

                PacketUtils.SendRequestPacketForUserDetails(clientHandler.PacketHandler, clientHandler.GetNetworkStream());

                _application.HideForm(_application.FormHandler.LoginForm);
                _application.ShowForm(_application.FormHandler.MainForm);
            }
            else if(response == "Login Failed")
            {
                MessageBox.Show("Login Failed");
            }
        }
    }
}
