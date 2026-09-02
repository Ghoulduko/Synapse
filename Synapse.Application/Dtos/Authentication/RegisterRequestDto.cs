namespace Synapse.Application.Dtos.Authentication;

public class RegisterRequestDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}