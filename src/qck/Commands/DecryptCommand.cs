using System;
using QCK.Crypto;

namespace QCK.Commands;

public static class DecryptCommand
{
    public static int Execute(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Error: file path required");
            Console.WriteLine("Usage: qck decrypt <file>");
            return 1;
        }

        string inputPath = args[1];

        if (!System.IO.File.Exists(inputPath))
        {
            Console.WriteLine($"Error: file not found: {inputPath}");
            return 1;
        }

        if (!KeyManager.KeyExists())
        {
            Console.WriteLine("Error: no key found. Run 'qck exchange' to generate a shared key");
            return 1;
        }

        SharedKey key;

        try
        {
            key = KeyManager.LoadKey();
        }
        catch
        {
            Console.WriteLine("Error: failed to load key");
            return 1;
        }

        string outputPath = inputPath.EndsWith(".enc") 
            ? inputPath.Substring(0, inputPath.Length - 4) 
            : inputPath + ".dec";

        try
        {
            AesEncryption.DecryptFile(inputPath, outputPath, key);
            Console.WriteLine($"Decrypted file saved to: {outputPath}");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }
}
