namespace CognitoAuthAPI.Model.Requests;

public class UserSignUpRequest
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
