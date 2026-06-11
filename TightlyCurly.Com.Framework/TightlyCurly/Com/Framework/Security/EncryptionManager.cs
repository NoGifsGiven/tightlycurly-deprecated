using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TightlyCurly.Com.Framework.Security;

public static class EncryptionManager
{
    // PasswordDeriveBytes is obsolete but intentionally kept so that data encrypted by
    // the original .NET Framework implementation can still be decrypted. Aes.Create()
    // with CBC mode is equivalent to the RijndaelManaged configuration used before.
    public static string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
        byte[] keyBytes = passwordDeriveBytes.GetBytes(keySize / 8);
        using Aes aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        using ICryptoTransform transform = aes.CreateEncryptor(keyBytes, initVectorBytes);
        using MemoryStream memoryStream = new MemoryStream();
        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
        {
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
        }
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
        byte[] keyBytes = passwordDeriveBytes.GetBytes(keySize / 8);
        using Aes aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        using ICryptoTransform transform = aes.CreateDecryptor(keyBytes, initVectorBytes);
        using MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int count = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        return Encoding.UTF8.GetString(plainTextBytes, 0, count);
    }
}
