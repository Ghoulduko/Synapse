using FluentValidation;
using Synapse.Application.Dtos.Authentication;

namespace Synapse.Application.Validators;

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
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