using System;
using System.Windows.Forms;

using ExoticClient.App.UI;
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

        public ChronicApplication()
        {
            if(Instance == null)
                Instance = this;

            _formHandler = new FormHandler();

            _tcpClient = new ExoticTcpClient("127.0.0.1", 24000);

            _logger = new LoggerConfiguration()
                        .WriteTo.File("ExoticClient-logs.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();
        }

        public void Initialize()
        {
            ShowForm(_formHandler.MainForm);
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
