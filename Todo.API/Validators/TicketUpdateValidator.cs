using FluentValidation;
using Todo.API.Models;
using Todo.Common.Constants;

namespace Todo.API.Validators {
    public class TicketUpdateValidator : AbstractValidator<TicketUpdateModel> {
        public TicketUpdateValidator() {
            RuleFor(x => x.Subject)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.SUBJECT_NAME_REQUIRED)
                    .WithMessage("Ticket subject is required")
                .MaximumLength(TicketConstants.SubjectMaxLength)
                    .WithErrorCode(ResultCodes.FieldRestrictions.MAX_LENGTH_REACHED)
                    .WithMessage($"Ticket subject length is greater than {TicketConstants.SubjectMaxLength}");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(TicketConstants.DescriptionMaxLength)
                .When(x => !String.IsNullOrEmpty(x.Description))
                    .WithErrorCode(ResultCodes.FieldRestrictions.MAX_LENGTH_REACHED)
                    .WithMessage($"Ticket description length is greater than {TicketConstants.DescriptionMaxLength}");
        }
    }
}