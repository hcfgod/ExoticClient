using System.Text;
using System.Windows.Forms;

namespace ExoticClient.Classes.Client.PacketSystem.Packets
{
    public class AesKeyAndIvPacket : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            byte[] decryptedData = CryptoUtility.DecryptWithPrivateKey(packet.Data, clientHandler.ExoticTcpClient.ClientKeyManager.GetPrivateKey());

            string aesKeyAndIv = Encoding.UTF8.GetString(decryptedData);

            string[] splitAesKeyAndIV = aesKeyAndIv.Split(':');

            string aesKey = splitAesKeyAndIV[0].Trim();
            string aesIv = splitAesKeyAndIV[1].Trim();

            CryptoUtility.SetAesKey(aesKey);
            CryptoUtility.SetAesIV(aesIv);
        }
    }
}
