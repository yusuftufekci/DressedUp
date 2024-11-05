namespace DressedUp.Domain.Aggregates.TagAggregate;

public class SubTag
{
    public int SubTagId { get; private set; }
    public int TagId { get; private set; }
    public string SubTagName { get; private set; }

    public SubTag(int tagId, string subTagName)
    {
        TagId = tagId;
        SubTagName = subTagName;
    }
}