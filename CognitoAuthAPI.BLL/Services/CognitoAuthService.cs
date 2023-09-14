using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using CognitoAuthAPI.BLL.Helpers;
using CognitoAuthAPI.BLL.Interfaces;
using CognitoAuthAPI.Model.Requests;
using CognitoAuthAPI.Model.Responses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CognitoAuthAPI.BLL.Services;

public class CognitoAuthService : IAuthService
{
    private readonly IConfigurationSection _awsConfig;
    private readonly IAmazonCognitoIdentityProvider _cognitoClient;
    private readonly ICognitoErrorHandler _errorHandler;
    private readonly ILogger<CognitoAuthService> _logger;

    public CognitoAuthService(
        IConfiguration config,
        IAmazonCognitoIdentityProvider cognitoClient,
        ICognitoErrorHandler errorHandler,
        ILogger<CognitoAuthService> logger)
    {
        _awsConfig = config.GetSection("AWSCognito");
        _cognitoClient = cognitoClient;
        _errorHandler = errorHandler;
        _logger = logger;
    }

    public async Task<ServiceResult<UserSignUpResponse>> SignUpAsync(UserSignUpRequest userRequest)
    {
        try
        {
            _logger.LogInformation($"Sign up request received. username: {userRequest.Username}, email: {userRequest.Email}");

            SignUpRequest cognitoRequest = new SignUpRequest
            {
                ClientId = _awsConfig["AppClientId"],
                Username = userRequest.Username,
                Password = userRequest.Password
            };
            cognitoRequest.UserAttributes.Add(new AttributeType { Name = "email", Value = userRequest.Email });
            SignUpResponse cognitoResponse = await _cognitoClient.SignUpAsync(cognitoRequest);

            _logger.LogInformation($"Sign up successful. userSub: {cognitoResponse.UserSub}, email: {userRequest.Email}");

            return ServiceResult<UserSignUpResponse>.SuccessResult(new UserSignUpResponse
            {
                UserId = cognitoResponse.UserSub,
                Email = userRequest.Email,
                CodeDeliveryMessage = $"A Confirmation Code has been sent to {cognitoResponse.CodeDeliveryDetails.Destination} via {cognitoResponse.CodeDeliveryDetails.DeliveryMedium.Value}"
            });
        }
        catch (AmazonCognitoIdentityProviderException ex)
        {
            _logger.LogError($"An AWS Cognito error occurred: {ex}");
            var error = _errorHandler.GetCognitoErrorInfo(ex);
            return ServiceResult<UserSignUpResponse>.ErrorResult(error.errorType, error.errorMessage);
        }
    }

    public async Task<ServiceResult> ConfirmSignUpAsync(UserConfirmSignUpRequest userRequest)
    {
        try
        {
            _logger.LogInformation($"Confirm sign up request received. username: {userRequest.Username}, confirmationCode: {userRequest.ConfirmationCode}");

            ConfirmSignUpRequest cognitoRequest = new ConfirmSignUpRequest
            {
                ClientId = _awsConfig["AppClientId"],
                Username = userRequest.Username,
                ConfirmationCode = userRequest.ConfirmationCode
            };
            await _cognitoClient.ConfirmSignUpAsync(cognitoRequest);

            _logger.LogInformation($"Sign up confirmed. username: {userRequest.Username}");

            return ServiceResult.SuccessResult();
        }
        catch (AmazonCognitoIdentityProviderException ex)
        {
            _logger.LogError($"An AWS Cognito error occurred: {ex}");
            var error = _errorHandler.GetCognitoErrorInfo(ex);
            return ServiceResult.ErrorResult(error.errorType, error.errorMessage);
        }
    }

    public async Task<ServiceResult<UserSignInResponse>> SignInAsync(UserSignInRequest userRequest)
    {
        try
        {
            _logger.LogInformation($"Sign in request received. username: {userRequest.Username}");

            InitiateAuthRequest cognitoRequest = new InitiateAuthRequest
            {
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                ClientId = _awsConfig["AppClientId"]
            };
            cognitoRequest.AuthParameters.Add("USERNAME", userRequest.Username);
            cognitoRequest.AuthParameters.Add("PASSWORD", userRequest.Password);
            InitiateAuthResponse cognitoResponse = await _cognitoClient.InitiateAuthAsync(cognitoRequest);
            AuthenticationResultType cognitoAuthResult = cognitoResponse.AuthenticationResult;

            _logger.LogInformation($"User signed in. username: {userRequest.Username}");

            return ServiceResult<UserSignInResponse>.SuccessResult(new UserSignInResponse
            {
                IdToken = cognitoAuthResult.IdToken,
                AccessToken = cognitoAuthResult.AccessToken,
                ExpiresIn = cognitoAuthResult.ExpiresIn,
                RefreshToken = cognitoAuthResult.RefreshToken
            });
        }
        catch (AmazonCognitoIdentityProviderException ex)
        {
            _logger.LogError($"An AWS Cognito error occurred: {ex}");
            var error = _errorHandler.GetCognitoErrorInfo(ex);
            return ServiceResult<UserSignInResponse>.ErrorResult(error.errorType, error.errorMessage);
        }
    }

    public async Task<ServiceResult<UserForgotPasswordResponse>> SendForgotPasswordCodeAsync(UserForgotPasswordRequest userRequest)
    {
        try
        {
            _logger.LogInformation($"Send forgot password code request received. username: {userRequest.Username}");

            ForgotPasswordRequest cognitoRequest = new ForgotPasswordRequest
            {
                Username = userRequest.Username,
                ClientId = _awsConfig["AppClientId"]
            };
            ForgotPasswordResponse cognitoResponse = await _cognitoClient.ForgotPasswordAsync(cognitoRequest);

            _logger.LogInformation($"Forgot password code sent. username: {userRequest.Username}");

            return ServiceResult<UserForgotPasswordResponse>.SuccessResult(new UserForgotPasswordResponse
            {
                CodeDeliveryMessage = $"A Reset Password Code has been sent to {cognitoResponse.CodeDeliveryDetails.Destination} via {cognitoResponse.CodeDeliveryDetails.DeliveryMedium.Value}"
            });
        }
        catch (AmazonCognitoIdentityProviderException ex)
        {
            _logger.LogError($"An AWS Cognito error occurred: {ex}");
            var error = _errorHandler.GetCognitoErrorInfo(ex);
            return ServiceResult<UserForgotPasswordResponse>.ErrorResult(error.errorType, error.errorMessage);
        }
    }

    public async Task<ServiceResult> ConfirmForgotPasswordAsync(UserConfirmForgotPasswordRequest userRequest)
    {
        try
        {
            _logger.LogInformation($"Confirm forgot password request received. username: {userRequest.Username}, confirmationCode: {userRequest.ConfirmationCode}");

            ConfirmForgotPasswordRequest cognitoRequest = new ConfirmForgotPasswordRequest
            {
                ClientId = _awsConfig["AppClientId"],
                ConfirmationCode = userRequest.ConfirmationCode,
                Username = userRequest.Username,
                Password = userRequest.Password
            };
            await _cognitoClient.ConfirmForgotPasswordAsync(cognitoRequest);

            _logger.LogInformation($"Confirm forgot password code successful. username: {userRequest.Username}");

            return ServiceResult.SuccessResult();
        }
        catch (AmazonCognitoIdentityProviderException ex)
        {
            _logger.LogError($"An AWS Cognito error occurred: {ex}");
            var error = _errorHandler.GetCognitoErrorInfo(ex);
            return ServiceResult.ErrorResult(error.errorType, error.errorMessage);
        }
    }

    public async Task<ServiceResult> ChangePasswordAsync(UserChangePasswordRequest userRequest, string? accessToken)
    {
        try
        {
            _logger.LogInformation($"Change password request received. accessToken: {accessToken}");

            ChangePasswordRequest cognitoRequest = new ChangePasswordRequest
            {
                PreviousPassword = userRequest.PreviousPassword,
                ProposedPassword = userRequest.ProposedPassword,
                AccessToken = accessToken
            };
            await _cognitoClient.ChangePasswordAsync(cognitoRequest);

            _logger.LogInformation($"Change password request received. accessToken:  {accessToken}");

            return ServiceResult.SuccessResult();
        }
        catch (AmazonCognitoIdentityProviderException ex)
        {
            _logger.LogError($"An AWS Cognito error occurred: {ex}");
            var error = _errorHandler.GetCognitoErrorInfo(ex);
            return ServiceResult.ErrorResult(error.errorType, error.errorMessage);
        }
    }

    public async Task<ServiceResult<GetMeResponse>> GetMeAsync(string? accessToken)
    {
        try
        {
            GetUserRequest cognitoRequest = new GetUserRequest
            {
                AccessToken = accessToken
            };
            GetUserResponse cognitoResponse = await _cognitoClient.GetUserAsync(cognitoRequest);

            Dictionary<string, string> attributes = new Dictionary<string, string>();
            foreach (var attribute in cognitoResponse.UserAttributes)
            {
                attributes.Add(attribute.Name, attribute.Value);
            }

            return ServiceResult<GetMeResponse>.SuccessResult(new GetMeResponse
            {
                Username = cognitoResponse.Username,
                PreferredMfaSetting = cognitoResponse.PreferredMfaSetting,
                Attributes = attributes
            });
        }
        catch (AmazonCognitoIdentityProviderException ex)
        {
            _logger.LogError($"An AWS Cognito error occurred: {ex}");
            var error = _errorHandler.GetCognitoErrorInfo(ex);
            return ServiceResult<GetMeResponse>.ErrorResult(error.errorType, error.errorMessage);
        }
    }

    public async Task<ServiceResult> SignOutAsync(string? accessToken)
    {
        try
        {
            GlobalSignOutRequest cognitoRequest = new GlobalSignOutRequest
            {
                AccessToken = accessToken
            };

            await _cognitoClient.GlobalSignOutAsync(cognitoRequest);

            return ServiceResult.SuccessResult();
        }
        catch (AmazonCognitoIdentityProviderException ex)
        {
            _logger.LogError($"An AWS Cognito error occurred: {ex}");
            var error = _errorHandler.GetCognitoErrorInfo(ex);
            return ServiceResult.ErrorResult(error.errorType, error.errorMessage);
        }
    }
}
