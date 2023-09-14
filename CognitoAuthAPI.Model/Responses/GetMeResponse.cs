namespace CognitoAuthAPI.Model.Responses;

public class GetMeResponse
{
    public string Username { get; set; }
    public string PreferredMfaSetting { get; set; }
    public Dictionary<string, string> Attributes { get; set; }
}
