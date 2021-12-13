namespace Apollo.Api.Models;

public class ErrorCodes
{
    protected readonly int ErrorCode;
    protected readonly string Message;
    protected ErrorCodes(int code, string message)
    {
        ErrorCode = code;
        Message = message;
    }
    public static implicit operator int(ErrorCodes @enum)
    {
        return @enum.ErrorCode;
    }

    public static implicit operator string(ErrorCodes @enum)
    {
        return @enum.Message;
    }

    public static readonly ErrorCodes ArgumentNull = new ErrorCodes(4000, "Text can not be empty or null.");
    public static readonly ErrorCodes ServiceNotFound = new ErrorCodes(4040, "Encryption service api not found.");
    public static readonly ErrorCodes GenericError = new ErrorCodes(5000, "An error has been occured.");
    public static readonly ErrorCodes EncryptedDataNull = new ErrorCodes(5002, "Unable to get encrypted data from api.");
    public static readonly ErrorCodes DecryptedDataNull = new ErrorCodes(5003, "Unable to get decrypted data from api.");

}