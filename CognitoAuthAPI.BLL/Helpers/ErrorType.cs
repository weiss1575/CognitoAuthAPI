namespace CognitoAuthAPI.BLL.Helpers;

public enum ErrorType
{
    None,
    InvalidParameter,
    NotFound,
    Unauthorized,
    Conflict,
    TooManyFailedAttempts,
    LimitExceeded,
    Cognito
}
