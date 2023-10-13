namespace ExoticClient.Classes.Client.PacketSystem.PacketsHandlers
{
    internal class AesIVPacketHandler : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            byte[] decryptedData = CryptoUtility.RsaDecrypt(packet.Data, clientHandler.ExoticTcpClient.ClientKeyManager.GetPrivateKey());

            CryptoUtility.SetAesIV(decryptedData);
        }
    }
}
