using System;

namespace QCK.Crypto;

public class SharedKey
{
    public byte[] Key { get; set; }
    public int Length => Key.Length;

    public SharedKey(byte[] key)
    {
        Key = key ?? throw new ArgumentNullException(nameof(key));
        if (key.Length == 0)
        {
            throw new ArgumentException("Key cannot be empty", nameof(key));
        }
    }
}

public class ExchangeResult
{
    public int[] AliceBases { get; set; }
    public int[] BobBases { get; set; }
    public SharedKey SharedKey { get; set; }
    public int MatchedBases { get; set; }
    public int TotalBits { get; set; }

    public ExchangeResult(int[] aliceBases, int[] bobBases, SharedKey sharedKey)
    {
        AliceBases = aliceBases ?? throw new ArgumentNullException(nameof(aliceBases));
        BobBases = bobBases ?? throw new ArgumentNullException(nameof(bobBases));
        SharedKey = sharedKey ?? throw new ArgumentNullException(nameof(sharedKey));
        TotalBits = aliceBases.Length;
        MatchedBases = sharedKey.Length;
    }
}

public class EncryptionResult
{
    public byte[] CipherText { get; set; }
    public byte[] Nonce { get; set; }
    public byte[] Tag { get; set; }

    public EncryptionResult(byte[] cipherText, byte[] nonce, byte[] tag)
    {
        CipherText = cipherText ?? throw new ArgumentNullException(nameof(cipherText));
        Nonce = nonce ?? throw new ArgumentNullException(nameof(nonce));
        Tag = tag ?? throw new ArgumentNullException(nameof(tag));
    }
}
