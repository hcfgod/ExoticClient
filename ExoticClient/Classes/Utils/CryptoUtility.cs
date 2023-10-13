using System;
using System.IO;
using System.Security.Cryptography;

public static class CryptoUtility
{
    private static byte[] _aesKey; // 32 bytes for AES-256
    private static byte[] _aesIV; // 16 bytes for AES-256

    public static byte[] AesKey => _aesKey;
    public static byte[] AesIV => _aesIV;

    public static byte[] Encrypt(byte[] data)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = _aesKey;
            aesAlg.IV = _aesIV;

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
            aesAlg.Key = _aesKey;
            aesAlg.IV = _aesIV;

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

    // RSA encryption using the server's public key
    public static byte[] EncryptWithPublicKey(byte[] data, RSAParameters publicKey)
    {
        using (RSA rsa = new RSACng())
        {
            rsa.ImportParameters(publicKey);
            return rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
        }
    }
    // RSA decryption using the server's private key
    public static byte[] DecryptWithPrivateKey(byte[] encryptedData, RSAParameters privateKey)
    {
        using (RSA rsa = new RSACng())
        {
            rsa.ImportParameters(privateKey);
            return rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
        }
    }

    public static void SetAesKey(byte[] aesKey)
    {
        _aesKey = aesKey;
    }

    public static void SetAesIV(byte[] aesIV)
    {
        _aesIV = aesIV;
    }
}