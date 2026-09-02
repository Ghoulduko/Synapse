namespace Synapse.Application.Dtos.Authentication;

public class LoginRequestDto
{
    public required string Password { get; set; }
    public required string Email { get; set; }
}