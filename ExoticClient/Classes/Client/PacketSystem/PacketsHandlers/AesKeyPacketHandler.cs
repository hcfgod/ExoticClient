namespace ExoticClient.Classes.Client.PacketSystem.PacketsHandlers
{
    public class AesKeyPacketHandler : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            byte[] decryptedData = CryptoUtility.RsaDecrypt(packet.Data, clientHandler.ExoticTcpClient.ClientKeyManager.GetPrivateKey());

            CryptoUtility.SetAesKey(decryptedData);
        }
    }
}
