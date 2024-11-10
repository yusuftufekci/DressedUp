using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DressedUp.Domain.Exceptions;

namespace DressedUp.Domain.Aggregates.UserAggregate;

public class User
{
    [Key]
    public int UserId { get; private set; }
    public string Username { get; private set; }
    public string Name { get; private set; }
    public string Lastname { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string? Bio { get; private set; }
    public string? MobileNo { get; private set; }
    public string? ProfilePicUrl { get; private set; }
    public bool IsProfileHidden { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private List<RefreshToken> _refreshTokens = new List<RefreshToken>();
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

    public User()
    {
        
    }
    public User(string username, string name, string lastname, string email, string passwordHash)
    {
        if (string.IsNullOrEmpty(username)) throw new DomainException("Username is required.");
        if (string.IsNullOrEmpty(email)) throw new DomainException("Email is required.");
        if (!IsValidEmail(email)) throw new DomainException("Invalid email format.");
        Username = username;
        Name = name;
        Lastname = lastname;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    private bool IsValidEmail(string email) =>
        // Basit bir email format kontrolü yapılabilir
        email.Contains("@");

    public void UpdateProfile(string bio, string profilePicUrl, bool isProfileHidden)
    {
        Bio = bio;
        ProfilePicUrl = profilePicUrl;
        IsProfileHidden = isProfileHidden;
        UpdatedAt = DateTime.UtcNow;
    }
    public void AddRefreshToken(string token, DateTime expiryDate, string createdByIp)
    {
        _refreshTokens.Add(new RefreshToken(UserId, token, expiryDate, createdByIp));
    }

    public void RevokeRefreshToken(string token, string revokedByIp)
    {
        var refreshToken = _refreshTokens.SingleOrDefault(rt => rt.Token == token);
        if (refreshToken != null)
        {
            refreshToken.Revoke(revokedByIp);
        }
    }
}