using FluentValidation;
using Todo.API.Constants;
using Todo.API.Models;
using Todo.Common.Constants;

namespace Todo.API.Validators {
    public class AuthValidator : AbstractValidator<AuthModel> {
        public AuthValidator() {
            RuleFor(x => x.GrantType)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.GRANT_TYPE_REQUIRED)
                    .WithMessage("Grant type is required");

            RuleFor(x => x.ClientKey)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.CLIENT_KEY_REQUIRED)
                    .WithMessage("Client key is required");

            RuleFor(x => x.ClientSecret)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.CLIENT_SECRET_REQUIRED)
                    .WithMessage("Client secret is required");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.EMAIL_REQUIRED)
                    .WithMessage("Email is required")
                .EmailAddress()
                    .WithErrorCode(ResultCodes.Errors.INCORRECT_EMAIL_FORMAT)
                    .WithMessage("Incorrect email format")
                .When(x => x.GrantType == AccessTokenGrantTypes.ResourceOwner);

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.PASSWORD_REQUIRED)
                    .WithMessage("Password is required")
                .When(x => x.GrantType == AccessTokenGrantTypes.ResourceOwner);

            RuleFor(x => x.RefreshToken)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.REFRESH_TOKEN_REQUIRED)
                    .WithMessage("Refresh token is required")
                .When(x => x.GrantType == AccessTokenGrantTypes.RefreshToken);
        }
    }
}