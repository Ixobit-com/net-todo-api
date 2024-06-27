using FluentValidation;
using Todo.API.Models;
using Todo.Common.Constants;

namespace Todo.API.Validators {
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordModel> {
        public ForgotPasswordValidator() {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.USER_EMAIL_REQUIRED)
                    .WithMessage("User email is required")
                .EmailAddress()
                    .WithErrorCode(ResultCodes.Errors.INCORRECT_EMAIL_FORMAT)
                    .WithMessage("Incorrect email format");
        }
    }
}