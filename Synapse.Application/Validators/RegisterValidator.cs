using FluentValidation;
using Synapse.Application.Dtos.Authentication;

namespace Synapse.Application.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty()
            .Length(4, 12)
            .WithMessage("Username must be between 4 and 12 characters long");
        
        RuleFor(r => r.Password)
            .NotEmpty()
            .Length(8, 24)
            .WithMessage("Password must be between 8 and 24 characters long");
        
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email must be a valid email address");
    }
}