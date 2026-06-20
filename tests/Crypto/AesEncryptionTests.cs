using System;
using System.IO;
using QCK.Crypto;
using Xunit;

namespace QCK.Tests.Crypto;

public class AesEncryptionTests
{
    [Fact]
    public void Encrypt_ValidInput_ReturnsEncryptionResult()
    {
        byte[] plaintext = new byte[] { 1, 2, 3, 4, 5 };
        byte[] keyBytes = new byte[32];
        var key = new SharedKey(keyBytes);

        var result = AesEncryption.Encrypt(plaintext, key);

        Assert.NotNull(result);
        Assert.NotNull(result.CipherText);
        Assert.NotNull(result.Nonce);
        Assert.NotNull(result.Tag);
        Assert.Equal(12, result.Nonce.Length);
        Assert.Equal(16, result.Tag.Length);
    }

    [Fact]
    public void Encrypt_NullPlaintext_ThrowsArgumentNullException()
    {
        byte[] keyBytes = new byte[32];
        var key = new SharedKey(keyBytes);

        Assert.Throws<ArgumentNullException>(() => AesEncryption.Encrypt(null!, key));
    }

    [Fact]
    public void Encrypt_NullKey_ThrowsArgumentNullException()
    {
        byte[] plaintext = new byte[] { 1, 2, 3 };

        Assert.Throws<ArgumentNullException>(() => AesEncryption.Encrypt(plaintext, null!));
    }

    [Fact]
    public void Encrypt_InvalidKeySize_ThrowsArgumentException()
    {
        byte[] plaintext = new byte[] { 1, 2, 3 };
        byte[] keyBytes = new byte[16];
        var key = new SharedKey(keyBytes);

        Assert.Throws<ArgumentException>(() => AesEncryption.Encrypt(plaintext, key));
    }

    [Fact]
    public void Decrypt_ValidInput_ReturnsOriginalPlaintext()
    {
        byte[] plaintext = new byte[] { 1, 2, 3, 4, 5 };
        byte[] keyBytes = new byte[32];
        var key = new SharedKey(keyBytes);

        var encrypted = AesEncryption.Encrypt(plaintext, key);
        var decrypted = AesEncryption.Decrypt(encrypted, key);

        Assert.Equal(plaintext, decrypted);
    }

    [Fact]
    public void Decrypt_NullResult_ThrowsArgumentNullException()
    {
        byte[] keyBytes = new byte[32];
        var key = new SharedKey(keyBytes);

        Assert.Throws<ArgumentNullException>(() => AesEncryption.Decrypt(null!, key));
    }

    [Fact]
    public void Decrypt_NullKey_ThrowsArgumentNullException()
    {
        byte[] cipherText = new byte[] { 1, 2, 3 };
        byte[] nonce = new byte[12];
        byte[] tag = new byte[16];
        var result = new EncryptionResult(cipherText, nonce, tag);

        Assert.Throws<ArgumentNullException>(() => AesEncryption.Decrypt(result, null!));
    }

    [Fact]
    public void EncryptDecrypt_RoundTrip_Succeeds()
    {
        byte[] original = new byte[100];
        for (int i = 0; i < 100; i++)
        {
            original[i] = (byte)i;
        }

        byte[] keyBytes = new byte[32];
        var key = new SharedKey(keyBytes);

        var encrypted = AesEncryption.Encrypt(original, key);
        var decrypted = AesEncryption.Decrypt(encrypted, key);

        Assert.Equal(original, decrypted);
    }
}
