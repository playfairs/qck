using System;
using System.Text;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using QCK.Crypto;

namespace QCK.Commands;

public static class ExchangeCommand
{
    public static int Execute(string[] args)
    {
        int bitCount = 256;

        if (args.Length > 1)
        {
            if (!int.TryParse(args[1], out bitCount) || bitCount <= 0)
            {
                Console.WriteLine("Error: bit count must be a positive integer");
                return 1;
            }
        }

        using var simulator = new QuantumSimulator();
        var (aliceBits, aliceBases, bobBases, bobBits, sharedKey) = 
            QCK.Quantum.GenerateSharedKey.Run(simulator, bitCount).Result;

        Console.WriteLine("BB84 Quantum Key Exchange");
        Console.WriteLine($"Total bits: {bitCount}");
        Console.WriteLine();

        Console.WriteLine("Alice bases:");
        Console.WriteLine(BasesToString(aliceBases));
        Console.WriteLine();

        Console.WriteLine("Alice bits:");
        Console.WriteLine(BitsToString(aliceBits));
        Console.WriteLine();

        Console.WriteLine("Bob bases:");
        Console.WriteLine(BasesToString(bobBases));
        Console.WriteLine();

        Console.WriteLine("Bob bits:");
        Console.WriteLine(BitsToString(bobBits));
        Console.WriteLine();

        Console.WriteLine("Shared key:");
        Console.WriteLine(BitsToString(sharedKey));
        Console.WriteLine();

        Console.WriteLine($"Shared key length: {sharedKey.Length} bits");

        if (sharedKey.Length >= 32)
        {
            byte[] keyBytes = new byte[32];
            for (int i = 0; i < 32; i++)
            {
                keyBytes[i] = sharedKey[i] == Microsoft.Quantum.Primitive.Result.One ? (byte)1 : (byte)0;
            }
            var sharedKeyObj = new SharedKey(keyBytes);
            KeyManager.SaveKey(sharedKeyObj);
            Console.WriteLine("Key saved to .qck_key");
        }

        return 0;
    }

    private static string BasesToString(int[] bases)
    {
        StringBuilder sb = new StringBuilder();
        foreach (int basis in bases)
        {
            sb.Append(basis == 0 ? '+' : '×');
        }
        return sb.ToString();
    }

    private static string BitsToString(Microsoft.Quantum.Primitive.Result[] bits)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var bit in bits)
        {
            sb.Append(bit == Microsoft.Quantum.Primitive.Result.One ? '1' : '0');
        }
        return sb.ToString();
    }
}
