namespace CognitoAuthAPI.Model.Requests;

public class UserChangePasswordRequest
{
    public string PreviousPassword { get; set; }
    public string ProposedPassword { get; set; }
}
