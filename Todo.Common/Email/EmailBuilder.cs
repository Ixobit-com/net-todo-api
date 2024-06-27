using HandlebarsDotNet;
using Todo.Common.Email.Providers;
using Todo.Common.Enums;

namespace Todo.Common.Email {
    public class EmailBuilder {
        public EmailResult Build(object data, string subjectFormat, string bodyFormat) {
            string subject = GetResult(subjectFormat, data);
            string body = GetResult(bodyFormat, data);

            return new EmailResult {
                Subject = subject,
                Body = body
            };
        }

        public EmailResult Preview(EmailTemplateType type, string subjectFormat, string bodyFormat) {
            ISampleDataProvider provider = GetSampleDataProvider(type);

            return Build(provider.GetSampleData(), subjectFormat, bodyFormat);
        }

        private string GetResult(string format, object data) {
            var template = Handlebars.Compile(format);

            return template(data);
        }

        private ISampleDataProvider GetSampleDataProvider(EmailTemplateType type) {
            switch (type) {
                case EmailTemplateType.CreateNewClient:
                    return new CreateNewClientSampleDataProvider();
                case EmailTemplateType.ResetUserPassword:
                    return new ResetUserPasswordSampleDataProvider();
                default:
                    throw new ArgumentException($"Unknown email template type [{type}]");
            }
        }
    }
}