using FluentValidation;
using Todo.API.Models;
using Todo.Common.Constants;

namespace Todo.API.Validators {
    public class UserCreateValidator : AbstractValidator<UserCreateModel> {
        public UserCreateValidator() {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.USER_EMAIL_REQUIRED)
                    .WithMessage("User email is required")
                .EmailAddress()
                    .WithErrorCode(ResultCodes.Errors.INCORRECT_EMAIL_FORMAT)
                    .WithMessage("Incorrect email format")
                .MaximumLength(UserConstants.EmailMaxLength)
                    .WithErrorCode(ResultCodes.FieldRestrictions.MAX_LENGTH_REACHED)
                    .WithMessage($"User email length is greater than {UserConstants.EmailMaxLength}");

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.USER_FIRSTNAME_REQUIRED)
                    .WithMessage("User first name is required")
                .MaximumLength(UserConstants.FirstNameMaxLength)
                    .WithErrorCode(ResultCodes.FieldRestrictions.MAX_LENGTH_REACHED)
                    .WithMessage($"User first name length is greater than {UserConstants.FirstNameMaxLength}");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.USER_LASTNAME_REQUIRED)
                    .WithMessage("User last name is required")
                .MaximumLength(UserConstants.LastNameMaxLength)
                    .WithErrorCode(ResultCodes.FieldRestrictions.MAX_LENGTH_REACHED)
                    .WithMessage($"User last name length is greater than {UserConstants.LastNameMaxLength}");
        }
    }
}