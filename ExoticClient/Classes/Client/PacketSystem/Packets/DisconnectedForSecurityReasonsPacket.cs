using ExoticClient.App;
using System.Windows.Forms;

namespace ExoticClient.Classes.Client.PacketSystem.Packets
{
    public class DisconnectedForSecurityReasonsPacket : IPacketHandler
    {
        public void Handle(Packet packet)
        {
            MessageBox.Show("Your application will now close for security reasons.");
            ChronicApplication.Instance.TcpClient.DisconnectFromServer();
            ChronicApplication.Instance.Shutdown();
        }
    }
}
