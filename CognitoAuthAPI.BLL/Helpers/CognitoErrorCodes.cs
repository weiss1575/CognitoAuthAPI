namespace CognitoAuthAPI.BLL.Helpers;

public static class CognitoErrorCodes
{
    public const string UsernameExists = "UsernameExistsException";
    public const string AliasExists = "AliasExistsException";
    public const string InvalidParameter = "InvalidParameterException";
    public const string InvalidPassword = "InvalidPasswordException";
    public const string CodeMismatch = "CodeMismatchException";
    public const string ExpiredCode = "ExpiredCodeException";
    public const string LimitExceeded = "LimitExceededException";
    public const string TooManyFailedAttempts = "TooManyFailedAttemptsException";
    public const string UserNotFound = "UserNotFoundException";
    public const string ResourceNotFound = "ResourceNotFoundException";
    public const string NotAuthorized = "NotAuthorizedException";
    public const string UserNotConfirmed = "UserNotConfirmedException";
}
