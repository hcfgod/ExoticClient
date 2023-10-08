using System.Text;

namespace ExoticClient.Classes.Client.PacketSystem.Packets
{
    public class ServerPublicKeyPacket : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            string serverPublicKey = Encoding.UTF8.GetString(packet.Data);
            clientHandler.ExoticTcpClient.ClientKeyManager.SetServerPublicKey(serverPublicKey);
        }
    }
}
