using DressedUp.Application.Common.Enums;

namespace DressedUp.Application.Responses;

public class Result<T>
{
    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }
    /// <summary>
    /// An optional message providing additional context.
    /// </summary>
    public string Message { get; }
    /// <summary>
    /// The main data returned by the operation.
    /// </summary>
    public T Data { get; }
    /// <summary>
    /// A code representing the specific type of error, if applicable.
    /// </summary>
    public ErrorCode? ErrorCode { get; set; }
    /// <summary>
    /// Any error messages related to the operation.
    /// </summary>
    public List<string> Errors { get; set; }  // Çoklu hata mesajları için liste

    private Result(T data, bool isSuccess, string message, ErrorCode? errorCode = null, List<string> errors = null)
    {
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
        ErrorCode = errorCode;
        Errors = errors ?? new List<string>();  // Hata durumlarında Errors dolacak
    }

    public static Result<T> Success(T data, string message = "") 
        => new Result<T>(data, true, message);

    public static Result<T> Failure(string message, ErrorCode errorCode, List<string> errors = null) 
        => new Result<T>(default, false, message, errorCode, errors);
}