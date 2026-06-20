using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QCK.Crypto;

public class AesEncryption
{
    private const int KeySize = 256;
    private const int NonceSize = 12;
    private const int TagSize = 16;

    public static EncryptionResult Encrypt(byte[] plaintext, SharedKey key)
    {
        if (plaintext == null)
        {
            throw new ArgumentNullException(nameof(plaintext));
        }

        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (key.Key.Length != KeySize / 8)
        {
            throw new ArgumentException($"Key must be {KeySize} bits", nameof(key));
        }

        byte[] nonce = new byte[NonceSize];
        byte[] tag = new byte[TagSize];
        byte[] ciphertext = new byte[plaintext.Length];

        using (var aes = new AesGcm(key.Key, TagSize))
        {
            RandomNumberGenerator.Fill(nonce);
            aes.Encrypt(nonce, plaintext, ciphertext, tag);
        }

        return new EncryptionResult(ciphertext, nonce, tag);
    }

    public static byte[] Decrypt(EncryptionResult result, SharedKey key)
    {
        if (result == null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        if (key.Key.Length != KeySize / 8)
        {
            throw new ArgumentException($"Key must be {KeySize} bits", nameof(key));
        }

        byte[] plaintext = new byte[result.CipherText.Length];

        using (var aes = new AesGcm(key.Key, TagSize))
        {
            aes.Decrypt(result.Nonce, result.CipherText, result.Tag, plaintext);
        }

        return plaintext;
    }

    public static void EncryptFile(string inputPath, string outputPath, SharedKey key)
    {
        if (string.IsNullOrEmpty(inputPath))
        {
            throw new ArgumentException("Input path cannot be empty", nameof(inputPath));
        }

        if (string.IsNullOrEmpty(outputPath))
        {
            throw new ArgumentException("Output path cannot be empty", nameof(outputPath));
        }

        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException("Input file not found", inputPath);
        }

        byte[] plaintext = File.ReadAllBytes(inputPath);
        EncryptionResult result = Encrypt(plaintext, key);

        using (var output = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            output.Write(result.Nonce, 0, result.Nonce.Length);
            output.Write(result.Tag, 0, result.Tag.Length);
            output.Write(result.CipherText, 0, result.CipherText.Length);
        }
    }

    public static void DecryptFile(string inputPath, string outputPath, SharedKey key)
    {
        if (string.IsNullOrEmpty(inputPath))
        {
            throw new ArgumentException("Input path cannot be empty", nameof(inputPath));
        }

        if (string.IsNullOrEmpty(outputPath))
        {
            throw new ArgumentException("Output path cannot be empty", nameof(outputPath));
        }

        if (!File.Exists(inputPath))
        {
            throw new FileNotFoundException("Input file not found", inputPath);
        }

        byte[] fileData = File.ReadAllBytes(inputPath);

        if (fileData.Length < NonceSize + TagSize)
        {
            throw new InvalidOperationException("Invalid encrypted file format");
        }

        byte[] nonce = new byte[NonceSize];
        byte[] tag = new byte[TagSize];
        byte[] ciphertext = new byte[fileData.Length - NonceSize - TagSize];

        Buffer.BlockCopy(fileData, 0, nonce, 0, NonceSize);
        Buffer.BlockCopy(fileData, NonceSize, tag, 0, TagSize);
        Buffer.BlockCopy(fileData, NonceSize + TagSize, ciphertext, 0, ciphertext.Length);

        var result = new EncryptionResult(ciphertext, nonce, tag);
        byte[] plaintext = Decrypt(result, key);

        File.WriteAllBytes(outputPath, plaintext);
    }
}
