using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Todo.Common.Constants;
using Todo.Common.Email;
using Todo.Common.Email.Data;
using Todo.Common.Enums;
using Todo.Common.Logging;
using Todo.Common.Settings;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Contracts;

namespace Todo.Logic.Services {
    public class EmailService : IEmailService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Logger _logger;
        private readonly SmtpSettings _smtpSettings;
        private readonly EmailBuilder _emailBuilder;

        public EmailService(
            IUnitOfWork unitOfWork,
            Logger logger,
            IOptions<SmtpSettings> smtpSettings) {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _smtpSettings = smtpSettings.Value;

            _emailBuilder = new EmailBuilder();
        }

        public async Task SendCreateNewClientEmailAsync(CreateNewClientEmailData data, CancellationToken cancellationToken = default) {
            await SendEmailAsync(EmailTemplateType.CreateNewClient, data, GetAdministratorMailboxAddresses(), cancellationToken);
        }

        public async Task SendResetUserPasswordEmailAsync(ResetUserPasswordEmailData data, Recipient recipient, CancellationToken cancellationToken = default) {
            await SendEmailAsync(EmailTemplateType.ResetUserPassword, data, new MailboxAddress(recipient.Name, recipient.Email), cancellationToken);
        }

        private async Task SendEmailAsync(EmailTemplateType type, object data, MailboxAddress recipient, CancellationToken cancellationToken = default) {
            if (recipient == null) {
                return;
            }

            await SendEmailAsync(type, data, new List<MailboxAddress> { recipient }, cancellationToken);
        }

        private async Task SendEmailAsync(EmailTemplateType type, object data, IEnumerable<MailboxAddress> recipients, CancellationToken cancellationToken = default) {
            try {
                if (recipients?.Any() ?? false) {
                    return;
                }

                using (var message = new MimeMessage()) {
                    message.To.AddRange(recipients);

                    var emailTemplate = _unitOfWork.GetRepository<EmailTemplate>()
                        .Get(x => x.Id == (int)type)
                        .FirstOrDefault() ?? EmailTemplate.GetDefault(type);

                    var result = _emailBuilder.Build(data, emailTemplate.SubjectFormat, emailTemplate.BodyFormat);

                    message.Subject = result.Subject;
                    message.Body = new TextPart(TextFormat.Html) {
                        Text = result.Body
                    };

                    message.From.Add(new MailboxAddress(_smtpSettings.Name, _smtpSettings.Username));

                    using (var client = new SmtpClient()) {
                        await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls, cancellationToken);
                        await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password, cancellationToken);
                        await client.SendAsync(message, cancellationToken);
                        await client.DisconnectAsync(true, cancellationToken);
                    }
                }
            }
            catch (Exception ex) {
                _logger.Error(ex, "Send mail exception occured");
            }
        }

        private IEnumerable<MailboxAddress> GetAdministratorMailboxAddresses() {
#if DEBUG
            return new List<MailboxAddress> {
                new MailboxAddress("Vitaly Koltsov", "vitaly.koltsov@sibers.com")
            };
#endif

            var admins = _unitOfWork.GetRepository<User>()
                .Get(x => x.UserRoles.Any(ur => ur.Role.Name == RoleNames.Administrator))
                .Select(x => new { x.FirstName, x.LastName, x.Email })
                .ToList();

            if (!admins?.Any() ?? false) {
                return null;
            }

            return admins
                .Select(x => new MailboxAddress($"{x.FirstName} {x.LastName}", x.Email));
        }
    }
}