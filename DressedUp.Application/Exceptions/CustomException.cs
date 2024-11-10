using DressedUp.Application.Common.Enums;

namespace DressedUp.Application.Exceptions;

public class CustomException : Exception
{
    public ErrorCode ErrorCode { get; }

    public CustomException(string message, ErrorCode errorCode) : base(message)
    {
        ErrorCode = errorCode;
    }
}