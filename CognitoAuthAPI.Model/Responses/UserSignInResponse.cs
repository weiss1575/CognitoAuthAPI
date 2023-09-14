namespace CognitoAuthAPI.Model.Responses;

public class UserSignInResponse
{
    public string IdToken { get; set; }
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
}
