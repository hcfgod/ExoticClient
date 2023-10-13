using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace ExoticClient.Classes.Client.PacketSystem.Packets
{
    public class ServerPublicKeyPacket : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            string serverPublicKeyJson = Encoding.UTF8.GetString(packet.Data);
            RSAParameters serverPublicKey = JsonConvert.DeserializeObject<RSAParameters>(serverPublicKeyJson);

            clientHandler.ExoticTcpClient.ClientKeyManager.SetServerPublicKey(serverPublicKey);
        }
    }
}
