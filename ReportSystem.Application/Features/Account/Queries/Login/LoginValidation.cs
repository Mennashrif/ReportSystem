using FluentValidation;

namespace ReportSystem.Application.Features.Account.Queries.Login
{
    public class LoginValidation:AbstractValidator<LoginQuery>
    {
        public LoginValidation()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email is required");

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
