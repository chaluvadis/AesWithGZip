namespace AesDemo;
public static class GzipHelper
{
    public static string Compress(string inputString)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
        using var memoryStream = new MemoryStream();
        using var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress);
        gzipStream.Write(inputBytes, 0, inputBytes.Length);
        gzipStream.Close();
        return Convert.ToBase64String(memoryStream.ToArray());
    }
    public static string Decompress(string compressedString)
    {
        byte[] compressedBytes = Convert.FromBase64String(compressedString);
        using var memoryStream = new MemoryStream(compressedBytes);
        using var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress);
        using var resultStream = new MemoryStream();
        gzipStream.CopyTo(resultStream);
        return Encoding.UTF8.GetString(resultStream.ToArray());
    }
}