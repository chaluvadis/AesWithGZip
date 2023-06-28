namespace AesDemo.Models;
public record TokenResponse(string Token);
public record TokenRequest(string ClientCode, string Source, string ClientReferenceNumber, string TempCaseId, DateTime CreatedOn, DateTime ExpiresOn)
{
    public bool IsTokenValid()
    {
        return DateTime.Now <= ExpiresOn && DateTime.Now >= CreatedOn;
    }
}
