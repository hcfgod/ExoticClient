using ExoticClient.App;
using ExoticClient.Classes.Client.PacketSystem;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExoticClient.Classes.Client
{
    public class ClientHandler
    {
        private ExoticTcpClient _exoticTcpClient;

        private TcpClient _client;
        private NetworkStream _clientStream;

        public event Action<ClientHandler> ClientDisconnected;

        // This will store a reference to the task handling the client, as per your TcpServer design.
        public Task ClientTask { get; set; }

        private PacketHandler _packetHandler;

        public ClientHandler(ExoticTcpClient exoticTcpClient, TcpClient client, PacketHandler packetHandler)
        {
            _exoticTcpClient = exoticTcpClient;
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _clientStream = _client.GetStream();

            _packetHandler = packetHandler;
        }

        public async Task HandleClientAsync(CancellationToken token)
        {
            ArrayPool<byte> pool = ArrayPool<byte>.Shared;
            byte[] dataBuffer = pool.Rent(4096);

            // Send Server Client Public Key
            byte[] clientPublicKeyData = Encoding.UTF8.GetBytes(_exoticTcpClient.ClientKeyManager.GetPublicKey());
            Packet clientPublicKeyPacket = _packetHandler.CreateNewPacket(clientPublicKeyData, "Client Public Key Packet");
            await _packetHandler.SendPacketAsync(clientPublicKeyPacket, _clientStream);

            while (!token.IsCancellationRequested && _client.Connected)
            {
                if (_client.Client.Poll(0, SelectMode.SelectRead))
                {
                    byte[] buff = new byte[1];

                    if (_client.Client.Receive(buff, SocketFlags.Peek) == 0)
                    {
                        // Client disconnected
                        ChronicApplication.Instance.Logger.Information("(ClientHandler.cs) HandleClientAsync(): Client Disconnected");
                        break;
                    }
                }

                List<Packet> receivedPackets = await _packetHandler.ReceivePacketAsync(_clientStream, dataBuffer);

                if (receivedPackets != null)
                {
                    foreach (var receivedPacket in receivedPackets)
                    {
                        _packetHandler.ProcessPacket(receivedPacket, this);
                    }
                }
            }
        }

        public void DisconnectedClient()
        {
            _client.Close();
            _clientStream.Dispose();
            ClientDisconnected?.Invoke(this);
        }

        public TcpClient GetTcpClient()
        {
            return _client;
        }

        public NetworkStream GetNetworkStream()
        {
            return _clientStream;
        }

        public ExoticTcpClient ExoticTcpClient => _exoticTcpClient;
    }
}
