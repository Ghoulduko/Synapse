namespace Synapse.Application.Dtos.Authentication;

public class LoginResponseDto
{
    public string? Username { get; set; }
    public string? AccessToken { get; set; }
    public DateTime Expires { get; set; }
    public string? RefreshToken { get; set; }
}