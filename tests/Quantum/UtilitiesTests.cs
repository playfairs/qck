using System;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using Xunit;

namespace QCK.Tests.Quantum;

public class UtilitiesTests
{
    [Fact]
    public void ResultArrayToInt_AllZeros_ReturnsZero()
    {
        var results = new Microsoft.Quantum.Primitive.Result[] 
        { 
            Microsoft.Quantum.Primitive.Result.Zero, 
            Microsoft.Quantum.Primitive.Result.Zero,
            Microsoft.Quantum.Primitive.Result.Zero 
        };

        var result = QCK.Quantum.ResultArrayToInt(results);

        Assert.Equal(0, result);
    }

    [Fact]
    public void ResultArrayToInt_AllOnes_ReturnsCorrectValue()
    {
        var results = new Microsoft.Quantum.Primitive.Result[] 
        { 
            Microsoft.Quantum.Primitive.Result.One, 
            Microsoft.Quantum.Primitive.Result.One,
            Microsoft.Quantum.Primitive.Result.One 
        };

        var result = QCK.Quantum.ResultArrayToInt(results);

        Assert.Equal(7, result);
    }

    [Fact]
    public void ResultArrayToBoolArray_ConvertsCorrectly()
    {
        var results = new Microsoft.Quantum.Primitive.Result[] 
        { 
            Microsoft.Quantum.Primitive.Result.Zero, 
            Microsoft.Quantum.Primitive.Result.One,
            Microsoft.Quantum.Primitive.Result.Zero 
        };

        var boolArray = QCK.Quantum.ResultArrayToBoolArray(results);

        Assert.Equal(3, boolArray.Length);
        Assert.False(boolArray[0]);
        Assert.True(boolArray[1]);
        Assert.False(boolArray[2]);
    }

    [Fact]
    public void BoolArrayToResultArray_ConvertsCorrectly()
    {
        var boolArray = new bool[] { false, true, false };

        var results = QCK.Quantum.BoolArrayToResultArray(boolArray);

        Assert.Equal(3, results.Length);
        Assert.Equal(Microsoft.Quantum.Primitive.Result.Zero, results[0]);
        Assert.Equal(Microsoft.Quantum.Primitive.Result.One, results[1]);
        Assert.Equal(Microsoft.Quantum.Primitive.Result.Zero, results[2]);
    }

    [Fact]
    public void ResultArrayToByte_ConvertsCorrectly()
    {
        var results = new Microsoft.Quantum.Primitive.Result[8];
        for (int i = 0; i < 8; i++)
        {
            results[i] = Microsoft.Quantum.Primitive.Result.One;
        }

        var result = QCK.Quantum.ResultArrayToByte(results);

        Assert.Equal(255, result);
    }
}
