namespace ExoticClient.Classes.Client.PacketSystem.Packets
{
    public class AesKeyPacket : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            byte[] decryptedData = CryptoUtility.DecryptWithPrivateKey(packet.Data, clientHandler.ExoticTcpClient.ClientKeyManager.GetPrivateKey());

            CryptoUtility.SetAesKey(decryptedData);
        }
    }
}
