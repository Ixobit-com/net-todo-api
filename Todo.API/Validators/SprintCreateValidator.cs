using FluentValidation;
using Todo.API.Models;
using Todo.Common.Constants;

namespace Todo.API.Validators {
    public class SprintCreateValidator : AbstractValidator<SprintCreateModel> {
        public SprintCreateValidator() {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.SPRINT_NAME_REQUIRED)
                    .WithMessage("Sprint name is required")
                .MaximumLength(SprintConstants.NameMaxLength)
                    .WithErrorCode(ResultCodes.FieldRestrictions.MAX_LENGTH_REACHED)
                    .WithMessage($"Sprint name length is greater than {SprintConstants.NameMaxLength}");

            RuleFor(x => x)
                .Cascade(CascadeMode.Stop)
                .Must(x => x.StartedAt <= x.FinishedAt)
                .When(x => x.StartedAt != null && x.FinishedAt != null)
                    .WithErrorCode(ResultCodes.FieldRestrictions.START_GREATER_THAN_FINISH)
                    .WithMessage("Start date is greater than finish date");
        }
    }
}