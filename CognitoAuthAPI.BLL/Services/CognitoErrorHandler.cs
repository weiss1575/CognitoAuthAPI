using Amazon.CognitoIdentityProvider;
using CognitoAuthAPI.BLL.Helpers;
using CognitoAuthAPI.BLL.Interfaces;

namespace CognitoAuthAPI.BLL.Services;

public class CognitoErrorHandler : ICognitoErrorHandler
{
    public (ErrorType errorType, string errorMessage) GetCognitoErrorInfo(AmazonCognitoIdentityProviderException ex)
    {
        switch (ex.ErrorCode)
        {
            case CognitoErrorCodes.UsernameExists:
            case CognitoErrorCodes.AliasExists:
                return (ErrorType.Conflict, ex.Message);
            case CognitoErrorCodes.InvalidParameter:
            case CognitoErrorCodes.InvalidPassword:
            case CognitoErrorCodes.CodeMismatch:
            case CognitoErrorCodes.ExpiredCode:
                return (ErrorType.InvalidParameter, ex.Message);
            case CognitoErrorCodes.LimitExceeded:
                return (ErrorType.LimitExceeded, ex.Message);
            case CognitoErrorCodes.TooManyFailedAttempts:
                return (ErrorType.TooManyFailedAttempts, ex.Message);
            case CognitoErrorCodes.UserNotFound:
            case CognitoErrorCodes.ResourceNotFound:
                return (ErrorType.NotFound, ex.Message);
            case CognitoErrorCodes.NotAuthorized:
            case CognitoErrorCodes.UserNotConfirmed:
                return (ErrorType.Unauthorized, ex.Message);
            default:
                return (ErrorType.Cognito, "An unexpected error occurred. Please try again later.");
        }
    }
}
