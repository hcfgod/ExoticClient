using ExoticClient.Classes.Client.PacketSystem;
using System.Net.Sockets;
using System.Text;

namespace ExoticClient.Classes.Utils
{
    public static class PacketUtils
    {
        public static async void SendRequestPacketForUserDetails(PacketHandler packetHandler, NetworkStream networkStream)
        {
            string data = $"{UserManager.Instance.GetUserDetails().Username}: needs there information";
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            Packet userDetailsRequestPacket = packetHandler.CreateNewPacket(dataBytes, "User Details Request");
            await packetHandler.SendPacketAsync(userDetailsRequestPacket, networkStream);
        }
    }
}
