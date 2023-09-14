namespace CognitoAuthAPI.Model.Requests;

public class UserConfirmForgotPasswordRequest
{
    public string ConfirmationCode { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

}
