using System;
using QCK.Crypto;

namespace QCK.Commands;

public static class EncryptCommand
{
    public static int Execute(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Error: file path required");
            Console.WriteLine("Usage: qck encrypt <file>");
            return 1;
        }

        string inputPath = args[1];

        if (!System.IO.File.Exists(inputPath))
        {
            Console.WriteLine($"Error: file not found: {inputPath}");
            return 1;
        }

        SharedKey key;

        if (KeyManager.KeyExists())
        {
            try
            {
                key = KeyManager.LoadKey();
                Console.WriteLine("Using existing key from .qck_key");
            }
            catch
            {
                Console.WriteLine("Error: failed to load existing key");
                return 1;
            }
        }
        else
        {
            try
            {
                key = KeyManager.GenerateKey();
                KeyManager.SaveKey(key);
                Console.WriteLine("Generated new key and saved to .qck_key");
            }
            catch
            {
                Console.WriteLine("Error: failed to generate key");
                return 1;
            }
        }

        string outputPath = inputPath + ".enc";

        try
        {
            AesEncryption.EncryptFile(inputPath, outputPath, key);
            Console.WriteLine($"Encrypted file saved to: {outputPath}");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }
}
