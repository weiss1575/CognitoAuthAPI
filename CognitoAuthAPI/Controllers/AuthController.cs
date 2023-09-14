using BreweryAPI.Extensions;
using CognitoAuthAPI.BLL.Interfaces;
using CognitoAuthAPI.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CognitoAuthAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signUp")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> SignUp(UserSignUpRequest userRequest)
    {
        var result = await _authService.SignUpAsync(userRequest);
        return result.Success ? Ok(result.Data) : this.FromErrorResult(result);
    }

    [HttpPost("confirmSignUp")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmSignUp(UserConfirmSignUpRequest userRequest)
    {
        var result = await _authService.ConfirmSignUpAsync(userRequest);
        return result.Success ? Ok() : this.FromErrorResult(result);
    }

    [HttpPost("signIn")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SignIn([FromBody] UserSignInRequest userRequest)
    {
        var result = await _authService.SignInAsync(userRequest);
        return result.Success ? Ok(result.Data) : this.FromErrorResult(result);
    }

    [HttpGet("forgotPasswordCode")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendForgotPasswordCode([FromQuery] UserForgotPasswordRequest userRequest)
    {
        var result = await _authService.SendForgotPasswordCodeAsync(userRequest);
        return result.Success ? Ok(result.Data) : this.FromErrorResult(result);
    }

    [HttpPost("forgotPassword")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ConfirmForgotPassword(UserConfirmForgotPasswordRequest userRequest)
    {
        var result = await _authService.ConfirmForgotPasswordAsync(userRequest);
        return result.Success ? Ok() : this.FromErrorResult(result);
    }

    [HttpPost("changePassword")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePasswordAsync(UserChangePasswordRequest userRequest)
    {
        string? accessToken = GetAccessToken();
        var result = await _authService.ChangePasswordAsync(userRequest, accessToken);
        return result.Success ? Ok() : this.FromErrorResult(result);
    }

    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMe()
    {
        string? accessToken = GetAccessToken();
        var result = await _authService.GetMeAsync(accessToken);
        return result.Success ? Ok(result.Data) : this.FromErrorResult(result);
    }

    [HttpDelete("signOut")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignOut()
    {
        string? accessToken = GetAccessToken();
        var result = await _authService.SignOutAsync(accessToken);
        return result.Success ? Ok() : this.FromErrorResult(result);
    }

    private string? GetAccessToken()
    {
        return HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
    }
}
