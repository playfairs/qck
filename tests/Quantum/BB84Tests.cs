using System;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using Xunit;

namespace QCK.Tests.Quantum;

public class BB84Tests
{
    [Fact]
    public async void GenerateSharedKey_GeneratesCorrectStructure()
    {
        using var simulator = new QuantumSimulator();
        int bitCount = 256;
        var (aliceBits, aliceBases, bobBases, bobBits, sharedKey) = 
            await QCK.Quantum.GenerateSharedKey.Run(simulator, bitCount);

        Assert.Equal(bitCount, aliceBits.Length);
        Assert.Equal(bitCount, aliceBases.Length);
        Assert.Equal(bitCount, bobBases.Length);
        Assert.Equal(bitCount, bobBits.Length);
        Assert.NotNull(sharedKey);
    }

    [Fact]
    public async void GenerateSharedKey_BasesAreValid()
    {
        using var simulator = new QuantumSimulator();
        var (aliceBits, aliceBases, bobBases, bobBits, sharedKey) = 
            await QCK.Quantum.GenerateSharedKey.Run(simulator, 64);

        foreach (var basis in aliceBases)
        {
            Assert.True(basis == 0 || basis == 1);
        }

        foreach (var basis in bobBases)
        {
            Assert.True(basis == 0 || basis == 1);
        }
    }

    [Fact]
    public async void GenerateSharedKey_BitsAreValid()
    {
        using var simulator = new QuantumSimulator();
        var (aliceBits, aliceBases, bobBases, bobBits, sharedKey) = 
            await QCK.Quantum.GenerateSharedKey.Run(simulator, 64);

        foreach (var bit in aliceBits)
        {
            Assert.True(bit == Microsoft.Quantum.Primitive.Result.Zero || 
                        bit == Microsoft.Quantum.Primitive.Result.One);
        }

        foreach (var bit in bobBits)
        {
            Assert.True(bit == Microsoft.Quantum.Primitive.Result.Zero || 
                        bit == Microsoft.Quantum.Primitive.Result.One);
        }
    }

    [Fact]
    public async void GenerateSharedKey_SharedKeyLengthLessThanTotal()
    {
        using var simulator = new QuantumSimulator();
        int bitCount = 256;
        var (aliceBits, aliceBases, bobBases, bobBits, sharedKey) = 
            await QCK.Quantum.GenerateSharedKey.Run(simulator, bitCount);

        Assert.True(sharedKey.Length <= bitCount);
    }

    [Fact]
    public async void GenerateSharedKey_SharedKeyContainsOnlyMatchingBases()
    {
        using var simulator = new QuantumSimulator();
        int bitCount = 128;
        var (aliceBits, aliceBases, bobBases, bobBits, sharedKey) = 
            await QCK.Quantum.GenerateSharedKey.Run(simulator, bitCount);

        int matchedCount = 0;
        for (int i = 0; i < bitCount; i++)
        {
            if (aliceBases[i] == bobBases[i])
            {
                matchedCount++;
            }
        }

        Assert.Equal(matchedCount, sharedKey.Length);
    }
}
