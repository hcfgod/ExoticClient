using System.Security.Cryptography;

namespace ExoticClient.Classes.Client.Security
{
    public class KeyManager
    {
        private string _publicKey;
        private string _privateKey;

        private string _serverPublicKey;

        // Initialize RSA for key pair generation
        private RSA _rsa = RSA.Create();

        public KeyManager()
        {
            GenerateKeyPair();
        }

        // Generate RSA public and private keys
        private void GenerateKeyPair()
        {
            _publicKey = _rsa.ToXmlString(false);  // false to get the public key
            _privateKey = _rsa.ToXmlString(true);  // true to get the private key
        }

        // Get the server's public key
        public string GetPublicKey()
        {
            return _publicKey;
        }

        public string GetPrivateKey()
        {
            return _privateKey;
        }

        public string GetServerPublicKey()
        {
            return _serverPublicKey;
        }

        public void SetServerPublicKey(string serverPublicKey)
        {
            _serverPublicKey = serverPublicKey;
        }
    }
}
