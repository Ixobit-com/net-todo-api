using FluentValidation;
using Todo.API.Models;
using Todo.Common.Constants;

namespace Todo.API.Validators {
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordModel> {
        public ResetPasswordValidator() {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.USER_EMAIL_REQUIRED)
                    .WithMessage("User email is required")
                .EmailAddress()
                    .WithErrorCode(ResultCodes.Errors.INCORRECT_EMAIL_FORMAT)
                    .WithMessage("Incorrect email format");

            RuleFor(x => x.Token)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.RESET_TOKEN_REQUIRED)
                    .WithMessage("Reset token is required");

            RuleFor(x => x.NewPassword)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.NEW_PASSWORD_REQUIRED)
                    .WithMessage("New password is required");
        }
    }
}