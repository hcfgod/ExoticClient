using System.Text;
using System.Windows.Forms;

namespace ExoticClient.Classes.Client.PacketSystem.Packets.RegistrationPacketHandlers
{
    public class UsernameAlreadyExistPacketHandler : IPacketHandler
    {
        public void Handle(Packet packet, ClientHandler clientHandler)
        {
            string message = Encoding.UTF8.GetString(packet.Data);
            MessageBox.Show(message, "Registration");
        }
    }
}
