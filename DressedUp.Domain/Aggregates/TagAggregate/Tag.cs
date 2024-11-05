namespace DressedUp.Domain.Aggregates.TagAggregate;

public class Tag
{
    public int TagId { get; private set; }
    public string TagName { get; private set; }

    public Tag(string tagName)
    {
        TagName = tagName;
    }
}