using AesDemo;
using AesDemo.Models;
DateTime createdOn = DateTime.Now;
DateTime expiresOn = createdOn.AddDays(30);
var originalTokenRequest = new TokenRequest("SHA", "Its a conosle", "2023-1959-789234", "892345678", createdOn, expiresOn);

byte[] key = new byte[32];
byte[] iv = new byte[16];

using (var rng = RandomNumberGenerator.Create())
{
    rng.GetBytes(key);
    rng.GetBytes(iv);
}

Console.WriteLine("--> Original Token Request: " + originalTokenRequest);
byte[] encryptedData = AesHelper.Encrypt<TokenRequest>(originalTokenRequest, key, iv);
Console.WriteLine("--> Encrypted Data: " + Convert.ToBase64String(encryptedData));
var encryptedToken = Convert.ToBase64String(encryptedData);
Console.WriteLine("--> Encrypted Token " + encryptedToken);
var compressedToken = GzipHelper.Compress(encryptedToken);
Console.WriteLine("--> Compressed Token " + compressedToken);
var decompressedToken = GzipHelper.Decompress(compressedToken);
Console.WriteLine("--> DeCompressed Token " + decompressedToken);
if (!string.IsNullOrEmpty(decompressedToken))
{
    var decryptedBytes = Convert.FromBase64String(decompressedToken);
    TokenRequest decryptedTokenRequest = AesHelper.Decrypt<TokenRequest>(decryptedBytes, key, iv);
    Console.WriteLine("--> Decrypted Token Request: " + decryptedTokenRequest);
    Console.WriteLine("--> Is Token Valid: " + decryptedTokenRequest.IsTokenValid());
}