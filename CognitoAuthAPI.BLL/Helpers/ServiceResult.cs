namespace CognitoAuthAPI.BLL.Helpers;

public class ServiceResult
{
    public bool Success { get; set; }
    public ErrorType ErrorType { get; set; }
    public string? Message { get; set; }

    public static ServiceResult ErrorResult(ErrorType errorType, string message)
    {
        return new ServiceResult
        {
            Success = false,
            ErrorType = errorType,
            Message = message
        };
    }

    public static ServiceResult SuccessResult()
    {
        return new ServiceResult
        {
            Success = true,
            ErrorType = ErrorType.None,
            Message = null,
        };
    }
}

public class ServiceResult<TData> : ServiceResult where TData : class
{
    public TData? Data { get; set; }

    public ServiceResult()
    {
        Success = true;
        ErrorType = ErrorType.None;
        Message = null;
        Data = default;
    }

    public ServiceResult(TData data)
    {
        Success = true;
        ErrorType = ErrorType.None;
        Message = null;
        Data = data;
    }

    public static ServiceResult<TData> SuccessResult(TData data)
    {
        return new ServiceResult<TData>
        {
            Success = true,
            ErrorType = ErrorType.None,
            Message = null,
            Data = data
        };
    }

    public static ServiceResult<TData> ErrorResult(ErrorType errorType, string message)
    {
        return new ServiceResult<TData>
        {
            Success = false,
            ErrorType = errorType,
            Message = message,
            Data = default
        };
    }
}
