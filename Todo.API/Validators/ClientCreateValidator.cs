using FluentValidation;
using Todo.API.Models;
using Todo.Common.Constants;

namespace Todo.API.Validators {
    public class ClientCreateValidator : AbstractValidator<ClientCreateModel> {
        public ClientCreateValidator() {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .Must(x => !String.IsNullOrEmpty(x))
                    .WithErrorCode(ResultCodes.RequiredFields.CLIENT_NAME_REQUIRED)
                    .WithMessage("Client name is required")
                .MaximumLength(ClientConstants.NameMaxLength)
                    .WithErrorCode(ResultCodes.FieldRestrictions.MAX_LENGTH_REACHED)
                    .WithMessage($"Client name length is greater than {ClientConstants.NameMaxLength}");
        }
    }
}