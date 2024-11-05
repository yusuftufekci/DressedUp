namespace DressedUp.Domain.Aggregates.PostAggregate;

public class PostTag
{
    public int PostId { get; private set; }
    public int TagId { get; private set; }
    public int SubTagId { get; private set; }

    public PostTag(int postId, int tagId, int subTagId)
    {
        PostId = postId;
        TagId = tagId;
        SubTagId = subTagId;
    }
}