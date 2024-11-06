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
    /// Any error messages related to the operation.
    /// </summary>
    public List<string> Errors { get; } // Errors özelliğini ekledik

    private Result(T data, bool isSuccess, string message, List<string> errors = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
        Errors = errors ?? new List<string>(); // Errors özelliği hata durumlarında dolacak
    }

    // Başarılı sonuç için factory metodu
    public static Result<T> Success(T data, string message = null) => new Result<T>(data, true, message);

    // Başarısız sonuç için factory metodu
    public static Result<T> Failure(string message, List<string> errors = null) => new Result<T>(default, false, message, errors);
}