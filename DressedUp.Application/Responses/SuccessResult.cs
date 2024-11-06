namespace DressedUp.Application.Responses;

public class SuccessResult : Result
{
    public SuccessResult(string message = null) : base(true, message) { }
}