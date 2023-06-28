namespace AesDemo;
public static class AesHelper
{
    public static byte[] Encrypt<T>(T data, byte[] key, byte[] iv)
    {
        string serializedData = JsonSerializer.Serialize(data);
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(serializedData);

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using var encryptor = aes.CreateEncryptor();

        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plaintextBytes, 0, plaintextBytes.Length);
        cryptoStream.FlushFinalBlock();

        return memoryStream.ToArray();
    }
    public static T Decrypt<T>(byte[] cipherText, byte[] key, byte[] iv)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;

        using var decryptor = aes.CreateDecryptor();

        using var memoryStream = new MemoryStream(cipherText);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

        using var plaintextStream = new MemoryStream();
        cryptoStream.CopyTo(plaintextStream);

        byte[] plaintextBytes = plaintextStream.ToArray();
        string serializedData = Encoding.UTF8.GetString(plaintextBytes);
        return JsonSerializer.Deserialize<T>(serializedData);
    }
}