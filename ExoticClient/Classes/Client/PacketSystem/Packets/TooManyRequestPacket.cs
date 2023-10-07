using ExoticClient.App;
using System.Text;
using System.Windows.Forms;

namespace ExoticClient.Classes.Client.PacketSystem.Packets
{
    public class TooManyRequestPacket : IPacketHandler
    {
        public async void Handle(Packet packet, ClientHandler clientHandler)
        {
            string data = Encoding.UTF8.GetString(packet.Data);
            MessageBox.Show(data, "Too Many Request");

            Packet testPacket = ChronicApplication.Instance.TcpClient.PacketHandler.CreateNewPacket(packet.Data, "Test Packet");
            await ChronicApplication.Instance.TcpClient.PacketHandler.SendPacketAsync(testPacket, ChronicApplication.Instance.TcpClient.ClientHandler.GetNetworkStream());
        }
    }
}
