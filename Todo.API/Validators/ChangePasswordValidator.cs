using FluentValidation;
using Todo.API.Models;
using Todo.Common.Constants;

namespace Todo.API.Validators {
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel> {
        public ChangePasswordValidator() {
            RuleFor(x => x.CurrentPassword)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.CURRENT_PASSWORD_REQUIRED)
                    .WithMessage("Current password is required");

            RuleFor(x => x.NewPassword)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.NEW_PASSWORD_REQUIRED)
                    .WithMessage("New password is required");
        }
    }
}