using System;
using System.Windows.Forms;

using ExoticClient.App.UI;
using ExoticClient.Classes;
using ExoticClient.Classes.Client;
using Serilog;

namespace ExoticClient.App
{
    public class ChronicApplication
    {
        public static ChronicApplication Instance { get; private set; }

        private readonly FormHandler _formHandler;
        private readonly ILogger _logger;
        private readonly ExoticTcpClient _tcpClient;
        private readonly UserManager _userManager;

        public ChronicApplication()
        {
            if(Instance == null)
                Instance = this;

            _formHandler = new FormHandler();

            _tcpClient = new ExoticTcpClient("127.0.0.1", 9000); // Delete when done testing
            //_tcpClient = new ExoticTcpClient("69.145.134.102", 9000); // Uncomment when done testing and is not on the same network as the server

            _userManager = new UserManager();

            _logger = new LoggerConfiguration()
                        .WriteTo.File("D:/Coding/Projects/C#/ServerAndClient Projects/ExoticClient/ExoticClient-logs.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();

            _logger.Information($"(ChronicApplication.cs) - ChronicApplication(): App Started!");
        }

        public async void Initialize()
        {
            await _tcpClient.ConnectToServer();
            ShowForm(_formHandler.LoginForm);
        }

        public void ShowForm(Form form)
        {
            form.Show();
        }

        public void HideForm(Form form)
        {
            form.Hide();
        }

        public void CloseForm(Form form)
        {
            form.Close();
        }

        public void Shutdown()
        {
            Application.Exit();
            Environment.Exit(0);
        }

        public FormHandler FormHandler { get { return _formHandler; } }
        public ILogger Logger { get { return _logger;} }
        public ExoticTcpClient TcpClient { get { return _tcpClient; } }
    }
}
