namespace CognitoAuthAPI.Model.Requests;

public class UserConfirmSignUpRequest
{
    public string Username { get; set; }
    public string ConfirmationCode { get; set; }
}
