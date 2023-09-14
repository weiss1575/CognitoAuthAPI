namespace CognitoAuthAPI.Model.Responses;

public class UserSignUpResponse
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string CodeDeliveryMessage { get; set; }
}
