namespace DressedUp.Application.Responses;

public class FailureResult : Result
{
    public List<string> Errors { get; }

    public FailureResult(string message, List<string> errors = null) : base(false, message)
    {
        Errors = errors ?? new List<string>();
    }
}