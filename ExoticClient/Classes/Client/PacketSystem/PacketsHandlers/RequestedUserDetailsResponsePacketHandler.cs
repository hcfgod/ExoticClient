using Newtonsoft.Json;
using System.Text;
using System.Windows.Forms;

namespace ExoticClient.Classes.Client.PacketSystem.PacketsHandlers
{
    public class RequestedUserDetailsResponsePacketHandler : IPacketHandler
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
