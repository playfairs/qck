using System;
using QCK.Commands;

namespace QCK;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("QCK - Quantum Cryptography Toolkit");
            Console.WriteLine("Usage: qck <command> [options]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("  random [bits]    Generate quantum random bits");
            Console.WriteLine("  exchange [bits]  Run BB84 key exchange");
            Console.WriteLine("  encrypt <file>  Encrypt a file");
            Console.WriteLine("  decrypt <file>  Decrypt a file");
            Console.WriteLine("  explain          Explain BB84 protocol");
            return 1;
        }

        string command = args[0].ToLower();

        try
        {
            switch (command)
            {
                case "random":
                    return RandomCommand.Execute(args);
                case "exchange":
                    return ExchangeCommand.Execute(args);
                case "encrypt":
                    return EncryptCommand.Execute(args);
                case "decrypt":
                    return DecryptCommand.Execute(args);
                case "explain":
                    return ExplainCommand.Execute(args);
                default:
                    Console.WriteLine($"Unknown command: {command}");
                    return 1;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }
}
