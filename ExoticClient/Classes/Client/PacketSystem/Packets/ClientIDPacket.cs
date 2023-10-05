using System.Text;
using System.Windows.Forms;

namespace ExoticClient.Classes.Client.PacketSystem.Packets
{
    public class ClientIDPacket : IPacketHandler
    {
        public void Handle(Packet packet)
        {
            string clientID = Encoding.UTF8.GetString(packet.Data);
            UserManager.Instance.CurrentUser.ClientID = clientID;
            MessageBox.Show(UserManager.Instance.CurrentUser.ClientID);
        }
    }
}
