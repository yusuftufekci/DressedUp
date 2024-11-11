namespace DressedUp.Domain.Aggregates.UserAggregate;

public class RefreshToken
{
    public int Id { get; private set; }
    public string Token { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public bool IsRevoked { get; private set; }
    public bool IsUsed { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public string CreatedByIp { get; private set; }
    public DateTime? RevokedDate { get; private set; }
    public string? RevokedByIp { get; private set; }
    public string? ReplacedByToken { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }
    public string DeviceId { get; set; }


    public RefreshToken(int userId, string token, DateTime expiryDate, string createdByIp, string deviceId)
    {
        UserId = userId;
        Token = token;
        ExpiryDate = expiryDate;
        CreatedDate = DateTime.UtcNow;
        CreatedByIp = createdByIp;
        IsRevoked = false;
        IsUsed = false;
        DeviceId = deviceId;
    }

    public void Revoke(string revokedByIp)
    {
        IsRevoked = true;
        RevokedDate = DateTime.UtcNow;
        RevokedByIp = revokedByIp;
    }

    public void MarkAsUsed(string replacedByToken)
    {
        IsUsed = true;
        ReplacedByToken = replacedByToken;
    }
}
