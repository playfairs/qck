using System;
using QCK.Crypto;
using Xunit;

namespace QCK.Tests.Crypto;

public class ModelsTests
{
    [Fact]
    public void SharedKey_ValidKey_CreatesSuccessfully()
    {
        byte[] keyBytes = new byte[32];
        for (int i = 0; i < 32; i++)
        {
            keyBytes[i] = (byte)i;
        }

        var sharedKey = new SharedKey(keyBytes);

        Assert.Equal(32, sharedKey.Length);
        Assert.Equal(keyBytes, sharedKey.Key);
    }

    [Fact]
    public void SharedKey_NullKey_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new SharedKey(null!));
    }

    [Fact]
    public void SharedKey_EmptyKey_ThrowsArgumentException()
    {
        byte[] emptyKey = Array.Empty<byte>();

        Assert.Throws<ArgumentException>(() => new SharedKey(emptyKey));
    }

    [Fact]
    public void ExchangeResult_ValidData_CreatesSuccessfully()
    {
        int[] aliceBases = new int[] { 0, 1, 0, 1 };
        int[] bobBases = new int[] { 0, 0, 1, 1 };
        byte[] keyBytes = new byte[32];
        var sharedKey = new SharedKey(keyBytes);

        var result = new ExchangeResult(aliceBases, bobBases, sharedKey);

        Assert.Equal(aliceBases, result.AliceBases);
        Assert.Equal(bobBases, result.BobBases);
        Assert.Equal(sharedKey, result.SharedKey);
        Assert.Equal(4, result.TotalBits);
        Assert.Equal(32, result.MatchedBases);
    }

    [Fact]
    public void ExchangeResult_NullAliceBases_ThrowsArgumentNullException()
    {
        byte[] keyBytes = new byte[32];
        var sharedKey = new SharedKey(keyBytes);

        Assert.Throws<ArgumentNullException>(() => new ExchangeResult(null!, new int[4], sharedKey));
    }

    [Fact]
    public void EncryptionResult_ValidData_CreatesSuccessfully()
    {
        byte[] cipherText = new byte[] { 1, 2, 3, 4 };
        byte[] nonce = new byte[12];
        byte[] tag = new byte[16];

        var result = new EncryptionResult(cipherText, nonce, tag);

        Assert.Equal(cipherText, result.CipherText);
        Assert.Equal(nonce, result.Nonce);
        Assert.Equal(tag, result.Tag);
    }

    [Fact]
    public void EncryptionResult_NullCipherText_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new EncryptionResult(null!, new byte[12], new byte[16]));
    }
}
