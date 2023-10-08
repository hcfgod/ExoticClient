using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class CryptoUtility
{
    private static string _aesKey; // 32 bytes for AES-256
    private static string _aesIV; // 16 bytes for AES

    public static byte[] Encrypt(byte[] data)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(_aesKey);
            aesAlg.IV = Encoding.UTF8.GetBytes(_aesIV);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(data, 0, data.Length);
                }

                return msEncrypt.ToArray();
            }
        }
    }

    public static byte[] Decrypt(byte[] encryptedData)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(_aesKey);
            aesAlg.IV = Encoding.UTF8.GetBytes(_aesIV);

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    byte[] decryptedData = new byte[encryptedData.Length];
                    int bytesRead = csDecrypt.Read(decryptedData, 0, decryptedData.Length);

                    Array.Resize(ref decryptedData, bytesRead);
                    return decryptedData;
                }
            }
        }
    }

    // RSA encryption using the client's public key
    public static byte[] EncryptWithPublicKey(string data, string publicKey)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.FromXmlString(publicKey);
            return rsa.Encrypt(Encoding.UTF8.GetBytes(data), RSAEncryptionPadding.Pkcs1);
        }
    }

    // RSA decryption using the server's private key
    public static byte[] DecryptWithPrivateKey(byte[] encryptedData, string privateKey)
    {
        using (RSA rsa = RSA.Create())
        {
            rsa.FromXmlString(privateKey);
            return rsa.Decrypt(encryptedData, RSAEncryptionPadding.Pkcs1);
        }
    }

    public static void SetAesKey(string aesKey)
    {
        _aesKey = aesKey;
    }

    public static void SetAesIV(string aesIV)
    {
        _aesIV = aesIV;
    }
}