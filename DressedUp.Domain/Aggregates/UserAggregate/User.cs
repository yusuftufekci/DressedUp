namespace DressedUp.Domain.Aggregates.UserAggregate;

public class User
{
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
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public User(string username, string name, string lastname, string email, string passwordHash)
    {
        Username = username;
        Name = name;
        Lastname = lastname;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProfile(string bio, string profilePicUrl, bool isProfileHidden)
    {
        Bio = bio;
        ProfilePicUrl = profilePicUrl;
        IsProfileHidden = isProfileHidden;
        UpdatedAt = DateTime.UtcNow;
    }
}