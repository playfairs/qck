using System;
using System.IO;
using QCK.Crypto;
using Xunit;

namespace QCK.Tests.Crypto;

public class KeyManagerTests : IDisposable
{
    private const string TestKeyFile = ".test_qck_key";

    public KeyManagerTests()
    {
        if (File.Exists(TestKeyFile))
        {
            File.Delete(TestKeyFile);
        }
    }

    public void Dispose()
    {
        if (File.Exists(TestKeyFile))
        {
            File.Delete(TestKeyFile);
        }
    }

    [Fact]
    public void GenerateKey_CreatesValidKey()
    {
        var key = KeyManager.GenerateKey();

        Assert.NotNull(key);
        Assert.Equal(32, key.Length);
        Assert.NotNull(key.Key);
        Assert.Equal(32, key.Key.Length);
    }

    [Fact]
    public void SaveKey_ValidKey_SavesSuccessfully()
    {
        byte[] keyBytes = new byte[32];
        for (int i = 0; i < 32; i++)
        {
            keyBytes[i] = (byte)i;
        }
        var key = new SharedKey(keyBytes);

        KeyManager.SaveKey(key, Directory.GetCurrentDirectory());

        Assert.True(File.Exists(TestKeyFile));
    }

    [Fact]
    public void LoadKey_ExistingKey_LoadsSuccessfully()
    {
        byte[] keyBytes = new byte[32];
        for (int i = 0; i < 32; i++)
        {
            keyBytes[i] = (byte)i;
        }
        var originalKey = new SharedKey(keyBytes);
        KeyManager.SaveKey(originalKey, Directory.GetCurrentDirectory());

        var loadedKey = KeyManager.LoadKey(Directory.GetCurrentDirectory());

        Assert.Equal(originalKey.Key, loadedKey.Key);
    }

    [Fact]
    public void LoadKey_NonexistentKey_ThrowsFileNotFoundException()
    {
        Assert.Throws<FileNotFoundException>(() => KeyManager.LoadKey(Directory.GetCurrentDirectory()));
    }

    [Fact]
    public void KeyExists_ExistingKey_ReturnsTrue()
    {
        byte[] keyBytes = new byte[32];
        var key = new SharedKey(keyBytes);
        KeyManager.SaveKey(key, Directory.GetCurrentDirectory());

        Assert.True(KeyManager.KeyExists(Directory.GetCurrentDirectory()));
    }

    [Fact]
    public void KeyExists_NonexistentKey_ReturnsFalse()
    {
        Assert.False(KeyManager.KeyExists(Directory.GetCurrentDirectory()));
    }

    [Fact]
    public void DeleteKey_ExistingKey_DeletesSuccessfully()
    {
        byte[] keyBytes = new byte[32];
        var key = new SharedKey(keyBytes);
        KeyManager.SaveKey(key, Directory.GetCurrentDirectory());

        KeyManager.DeleteKey(Directory.GetCurrentDirectory());

        Assert.False(KeyManager.KeyExists(Directory.GetCurrentDirectory()));
    }

    [Fact]
    public void DeleteKey_NonexistentKey_DoesNotThrow()
    {
        KeyManager.DeleteKey(Directory.GetCurrentDirectory());

        Assert.False(KeyManager.KeyExists(Directory.GetCurrentDirectory()));
    }
}
