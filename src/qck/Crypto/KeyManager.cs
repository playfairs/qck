using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

namespace QCK.Crypto;

public class KeyManager
{
    private const string KeyFileName = ".qck_key";
    private const int KeySizeBytes = 32;

    public static SharedKey GenerateKey()
    {
        byte[] key = new byte[KeySizeBytes];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(key);
        }
        return new SharedKey(key);
    }

    public static SharedKey GenerateQuantumKey(int bitCount)
    {
        using var simulator = new QuantumSimulator();
        var results = QCK.Quantum.RandomBits.Run(simulator, bitCount).Result;
        
        byte[] key = new byte[KeySizeBytes];
        for (int i = 0; i < KeySizeBytes && i < results.Length; i++)
        {
            key[i] = results[i] == Microsoft.Quantum.Primitive.Result.One ? (byte)1 : (byte)0;
        }
        
        return new SharedKey(key);
    }

    public static void SaveKey(SharedKey key, string? directory = null)
    {
        string path = directory != null 
            ? Path.Combine(directory, KeyFileName) 
            : KeyFileName;
        
        File.WriteAllBytes(path, key.Key);
    }

    public static SharedKey LoadKey(string? directory = null)
    {
        string path = directory != null 
            ? Path.Combine(directory, KeyFileName) 
            : KeyFileName;
        
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Key file not found", path);
        }
        
        byte[] key = File.ReadAllBytes(path);
        return new SharedKey(key);
    }

    public static bool KeyExists(string? directory = null)
    {
        string path = directory != null 
            ? Path.Combine(directory, KeyFileName) 
            : KeyFileName;
        
        return File.Exists(path);
    }

    public static void DeleteKey(string? directory = null)
    {
        string path = directory != null 
            ? Path.Combine(directory, KeyFileName) 
            : KeyFileName;
        
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
