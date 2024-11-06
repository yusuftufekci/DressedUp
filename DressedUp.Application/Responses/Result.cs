namespace DressedUp.Application.Responses;

public abstract class Result
{
    public bool IsSuccess { get; protected set; }
    public string Message { get; protected set; }

    protected Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static SuccessResult Success(string message = null) => new SuccessResult(message);
    public static FailureResult Failure(string message, List<string> errors = null) => new FailureResult(message, errors);
}