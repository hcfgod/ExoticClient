using ExoticClient.App;
using System.Windows.Forms;

namespace ExoticClient.Classes.Client.PacketSystem.PacketsHandlers
{
    public class DisconnectedForSecurityReasonsPacketHandler : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            MessageBox.Show("Your application will now close for security reasons.");
            ChronicApplication.Instance.TcpClient.DisconnectFromServer();
            ChronicApplication.Instance.Shutdown();
        }
    }
}
