namespace DressedUp.Domain.Aggregates.PostAggregate.ValueObjects;

public class MediaUrl
{
    public string Url { get; private set; }

    public MediaUrl(string url)
    {
        if (string.IsNullOrEmpty(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
            throw new ArgumentException("Invalid URL format.");

        Url = url;
    }

    public override bool Equals(object obj)
    {
        return obj is MediaUrl mediaUrl && Url == mediaUrl.Url;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Url);
    }
}