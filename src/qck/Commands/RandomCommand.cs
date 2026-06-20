using System;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;

namespace QCK.Commands;

public static class RandomCommand
{
    public static int Execute(string[] args)
    {
        int bitCount = 32;

        if (args.Length > 1)
        {
            if (!int.TryParse(args[1], out bitCount) || bitCount <= 0)
            {
                Console.WriteLine("Error: bit count must be a positive integer");
                return 1;
            }
        }

        using var simulator = new QuantumSimulator();
        var results = QCK.Quantum.RandomBits.Run(simulator, bitCount).Result;

        StringBuilder sb = new StringBuilder();
        foreach (var result in results)
        {
            sb.Append(result == Microsoft.Quantum.Primitive.Result.One ? '1' : '0');
        }

        Console.WriteLine(sb.ToString());
        return 0;
    }
}
