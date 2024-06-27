using FluentValidation;
using Todo.API.Models;
using Todo.Common.Constants;

namespace Todo.API.Validators {
    public class LabelCreateValidator : AbstractValidator<LabelCreateModel> {
        public LabelCreateValidator() {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.LABEL_NAME_REQUIRED)
                    .WithMessage("Label name is required")
                .MaximumLength(LabelConstants.NameMaxLength)
                    .WithErrorCode(ResultCodes.FieldRestrictions.MAX_LENGTH_REACHED)
                    .WithMessage($"Label name length is greater than {LabelConstants.NameMaxLength}");
        }
    }
}