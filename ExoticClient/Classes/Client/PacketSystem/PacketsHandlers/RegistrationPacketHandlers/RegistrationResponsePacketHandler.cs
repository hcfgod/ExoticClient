using ExoticClient.Classes.Utils;
using System.Text;
using System.Windows.Forms;

namespace ExoticClient.Classes.Client.PacketSystem.Packets.RegistrationPacketHandlers
{
    public class RegistrationResponsePacketHandler : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            string message = Encoding.UTF8.GetString(packet.Data);
            
            if(message == "Registration Successful")
            {
                PacketUtils.SendRequestPacketForUserDetails(clientHandler.PacketHandler, clientHandler.GetNetworkStream());

                MessageBox.Show($"Welcome {UserManager.Instance.GetUserDetails().Username}, Registration was a success!");
            }
            else if(message == "Registration Failed")
            {
                MessageBox.Show($"Registration has failed, Please contact a administrator.");
            }
        }
    }
}
