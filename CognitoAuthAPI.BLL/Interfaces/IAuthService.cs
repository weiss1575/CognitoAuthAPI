using CognitoAuthAPI.BLL.Helpers;
using CognitoAuthAPI.Model.Requests;
using CognitoAuthAPI.Model.Responses;

namespace CognitoAuthAPI.BLL.Interfaces;

public interface IAuthService
{
    Task<ServiceResult<UserSignUpResponse>> SignUpAsync(UserSignUpRequest userRequest);
    Task<ServiceResult> ConfirmSignUpAsync(UserConfirmSignUpRequest userRequest);
    Task<ServiceResult<UserSignInResponse>> SignInAsync(UserSignInRequest userRequest);
    Task<ServiceResult<UserForgotPasswordResponse>> SendForgotPasswordCodeAsync(UserForgotPasswordRequest userRequest);
    Task<ServiceResult> ConfirmForgotPasswordAsync(UserConfirmForgotPasswordRequest userRequest);
    Task<ServiceResult> ChangePasswordAsync(UserChangePasswordRequest userRequest, string? accessToken);
    Task<ServiceResult<GetMeResponse>> GetMeAsync(string? accessToken);
    Task<ServiceResult> SignOutAsync(string? accessToken);
}
