using CoreFX.Auth.Contracts.Login;
using FluentValidation;

namespace Hello6.Domain.SDK.Services.AuthServices.Login
{
    public class AuthLogin_RequestValidator : AbstractValidator<AuthLogin_RequestDto>
    {
        public AuthLogin_RequestValidator()
        {
            RuleFor(c => c.Username).NotEmpty().Length(4, 255);
            RuleFor(c => c.Password).NotEmpty().Length(6, 255);
        }
    }
}
