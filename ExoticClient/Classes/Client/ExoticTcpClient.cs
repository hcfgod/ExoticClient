using Serilog;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System;
using ExoticClient.Classes.Client.PacketSystem;

namespace ExoticClient.Classes.Client
{
    public class ExoticTcpClient
    {
        private string _host;
        private int _port;

        private TcpClient _tcpClient;
        private NetworkStream _clientNetworkStream;

        private bool _isClientConnectedToServer;

        private ClientHandler _clientHandler;

        private CancellationTokenSource _cancellationToken;

        private PacketHandler _packetHandler;

        public ExoticTcpClient(string host, int port)
        {
            _host = host;
            _port = port;
            _tcpClient = new TcpClient();

            _cancellationToken = new CancellationTokenSource();

            _packetHandler = new PacketHandler();
        }

        public async Task ConnectToServer()
        {
            try
            {
                await _tcpClient.ConnectAsync(_host, _port).ConfigureAwait(false);

                _clientNetworkStream = _tcpClient.GetStream();

                _clientHandler = new ClientHandler(_tcpClient, _packetHandler);

                Task clientTask = _clientHandler.HandleClientAsync(_cancellationToken.Token);
                _isClientConnectedToServer = true;
            }
            catch (Exception ex)
            {
                Log.Error($"(NetworkClient) ConnectToServer: {ex.Message}");
            }
        }

        public void DisconnectFromServer()
        {
            try
            {
                _clientNetworkStream?.Close();
                _tcpClient?.Close();
                _clientNetworkStream?.Dispose();
                _tcpClient?.Dispose();

                _isClientConnectedToServer = false;

                _cancellationToken.Cancel();
            }
            catch (Exception ex)
            {
                Log.Error($"(NetworkClient) DisconnectFromServer(): {ex.Message}");
            }
        }

        public void Dispose()
        {
            _clientNetworkStream?.Close();
            _tcpClient?.Close();
            _clientNetworkStream?.Dispose();
            _tcpClient?.Dispose();
        }

        public ClientHandler ClientHandler { get { return _clientHandler; } }

        public bool IsClientConnectedToServer { get { return _isClientConnectedToServer; } }
    }
}
