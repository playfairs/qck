using System;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using Xunit;

namespace QCK.Tests.Quantum;

public class RandomTests
{
    [Fact]
    public async void RandomBit_GeneratesValidResult()
    {
        using var simulator = new QuantumSimulator();
        var result = await QCK.Quantum.RandomBit.Run(simulator);

        Assert.True(result == Microsoft.Quantum.Primitive.Result.Zero || 
                    result == Microsoft.Quantum.Primitive.Result.One);
    }

    [Fact]
    public async void RandomBits_GeneratesCorrectCount()
    {
        using var simulator = new QuantumSimulator();
        int count = 32;
        var results = await QCK.Quantum.RandomBits.Run(simulator, count);

        Assert.Equal(count, results.Length);
    }

    [Fact]
    public async void RandomBits_GeneratesValidResults()
    {
        using var simulator = new QuantumSimulator();
        var results = await QCK.Quantum.RandomBits.Run(simulator, 64);

        foreach (var result in results)
        {
            Assert.True(result == Microsoft.Quantum.Primitive.Result.Zero || 
                        result == Microsoft.Quantum.Primitive.Result.One);
        }
    }

    [Fact]
    public async void RandomBits_ZeroCount_ReturnsEmptyArray()
    {
        using var simulator = new QuantumSimulator();
        var results = await QCK.Quantum.RandomBits.Run(simulator, 0);

        Assert.Empty(results);
    }
}
