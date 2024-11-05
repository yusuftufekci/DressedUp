namespace DressedUp.Domain.Aggregates.UserAggregate.ValueObjects;

public class Email
{
    public string Value { get; private set; }

    public Email(string value)
    {
        if (!IsValidEmail(value))
            throw new ArgumentException("Invalid email format.");

        Value = value;
    }

    private bool IsValidEmail(string email)
    {
        return email.Contains("@");  // Basit bir doğrulama, burada daha kapsamlı bir kontrol yapılabilir.
    }

    public override bool Equals(object obj)
    {
        return obj is Email email && Value == email.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }
}