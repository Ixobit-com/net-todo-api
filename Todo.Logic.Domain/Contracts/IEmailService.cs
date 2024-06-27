using Todo.Common.Email;
using Todo.Common.Email.Data;

namespace Todo.Logic.Domain.Contracts {
    public interface IEmailService {
        Task SendCreateNewClientEmailAsync(CreateNewClientEmailData data, CancellationToken cancellationToken = default);
        Task SendResetUserPasswordEmailAsync(ResetUserPasswordEmailData data, Recipient recipient, CancellationToken cancellationToken = default);
    }
}