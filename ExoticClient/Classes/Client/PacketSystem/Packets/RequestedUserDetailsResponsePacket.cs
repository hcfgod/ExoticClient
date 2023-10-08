using ExoticClient.Classes.Client.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExoticClient.Classes.Client.PacketSystem.Packets
{
    public class RequestedUserDetailsResponsePacket : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            string jsonString = Encoding.UTF8.GetString(packet.Data);

            // Deserialize the JSON string to UserAuthDetails object
            UserDetails userDetails = JsonConvert.DeserializeObject<UserDetails>(jsonString);

            MessageBox.Show(userDetails.ClientID);
            MessageBox.Show(userDetails.UserID);
            MessageBox.Show(userDetails.Username);
            MessageBox.Show(userDetails.Email);

            UserManager.Instance.SetUserDetails(userDetails);
        }
    }
}
