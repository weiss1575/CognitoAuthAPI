using Amazon.CognitoIdentityProvider;
using CognitoAuthAPI.BLL.Helpers;

namespace CognitoAuthAPI.BLL.Interfaces;

public interface ICognitoErrorHandler
{
    (ErrorType errorType, string errorMessage) GetCognitoErrorInfo(AmazonCognitoIdentityProviderException ex);
}
