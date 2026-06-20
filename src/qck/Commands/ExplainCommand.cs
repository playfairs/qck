using System;

namespace QCK.Commands;

public static class ExplainCommand
{
    public static int Execute(string[] args)
    {
        Console.WriteLine("BB84 Quantum Key Distribution Protocol");
        Console.WriteLine();
        Console.WriteLine("Overview:");
        Console.WriteLine("BB84 is a quantum key distribution protocol developed by Bennett and Brassard in 1984.");
        Console.WriteLine("It allows two parties (Alice and Bob) to generate a shared secret key using quantum mechanics.");
        Console.WriteLine();
        Console.WriteLine("Protocol Steps:");
        Console.WriteLine();
        Console.WriteLine("1. Alice generates random bits (0 or 1)");
        Console.WriteLine("2. Alice chooses random bases (0 = computational/Z, 1 = Hadamard/X)");
        Console.WriteLine("3. Alice encodes each bit in a qubit using the chosen basis");
        Console.WriteLine("4. Bob receives the qubits and chooses random measurement bases");
        Console.WriteLine("5. Bob measures each qubit in his chosen basis");
        Console.WriteLine("6. Alice and Bob publicly compare their bases (not the bits)");
        Console.WriteLine("7. They keep only the bits where bases matched (sifting)");
        Console.WriteLine("8. The matching bits form the shared key");
        Console.WriteLine();
        Console.WriteLine("Quantum Properties:");
        Console.WriteLine();
        Console.WriteLine("- Superposition: Qubits can be in multiple states simultaneously");
        Console.WriteLine("- Measurement: Measuring a qubit collapses its state");
        Console.WriteLine("- No-cloning: It's impossible to copy an unknown quantum state");
        Console.WriteLine("- Basis mismatch: Measuring in the wrong basis gives random results");
        Console.WriteLine();
        Console.WriteLine("Security:");
        Console.WriteLine();
        Console.WriteLine("An eavesdropper (Eve) cannot intercept the qubits without disturbing them.");
        Console.WriteLine("Any measurement by Eve introduces errors that Alice and Bob can detect");
        Console.WriteLine("by comparing a subset of their key bits.");
        Console.WriteLine();
        Console.WriteLine("QCK Implementation:");
        Console.WriteLine();
        Console.WriteLine("- Quantum operations use Microsoft Q#");
        Console.WriteLine("- Random bits generated via quantum superposition");
        Console.WriteLine("- Hadamard gate creates superposition");
        Console.WriteLine("- Measurement collapses to classical bit");
        Console.WriteLine("- Shared key used for AES-256-GCM file encryption");
        Console.WriteLine();
        Console.WriteLine("Usage:");
        Console.WriteLine("  qck exchange [bits]  Run BB84 key exchange");
        Console.WriteLine("  qck encrypt <file>   Encrypt a file with the shared key");
        Console.WriteLine("  qck decrypt <file>   Decrypt a file with the shared key");

        return 0;
    }
}
