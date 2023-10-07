namespace ExoticClient.Classes.Client.PacketSystem
{
    public interface IPacketHandler
    {
        void Handle(Packet packet, ClientHandler clientHandler);
    }
}
