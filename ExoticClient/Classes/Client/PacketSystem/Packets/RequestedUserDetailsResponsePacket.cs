using Newtonsoft.Json;
using System.Text;

namespace ExoticClient.Classes.Client.PacketSystem.Packets
{
    public class RequestedUserDetailsResponsePacket : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            string jsonString = Encoding.UTF8.GetString(packet.Data);

            // Deserialize the JSON string to UserAuthDetails object
            UserDetails userDetails = JsonConvert.DeserializeObject<UserDetails>(jsonString);

            UserManager.Instance.SetUserDetails(userDetails);
        }
    }
}
