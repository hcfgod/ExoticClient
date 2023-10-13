namespace ExoticClient.Classes.Client.PacketSystem.Packets
{
    internal class AesIVPacket : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            byte[] decryptedData = CryptoUtility.DecryptWithPrivateKey(packet.Data, clientHandler.ExoticTcpClient.ClientKeyManager.GetPrivateKey());

            CryptoUtility.SetAesIV(decryptedData);
        }
    }
}
