using Todo.Common.Enums;
using Todo.Data.Domain.Models.Base;

namespace Todo.Data.Domain.Models {
    public class EmailTemplate : BaseAuditableEntity<int> {
        public string SubjectFormat { get; set; }
        public string BodyFormat { get; set; }

        public static EmailTemplate GetDefault(EmailTemplateType type) {
            string subjectFormat;
            string bodyFormat;

            switch (type) {
                case EmailTemplateType.CreateNewClient:
                    subjectFormat = "New client credentials";
                    bodyFormat = @"
                        <div>
                            <p>Name: {{Name}}</p>
                            <p>Key: {{Key}}</p>
                            <p>Secret: {{Secret}}</p>
                        </div>
                    ";
                    break;
                case EmailTemplateType.ResetUserPassword:
                    subjectFormat = "Reset password";
                    bodyFormat = @"
                        <div>
                            <p>Token: {{Token}}</p>
                        </div>
                    ";
                    break;
                default:
                    throw new ArgumentException($"Unknown email type [{type}]");
            }

            return new EmailTemplate {
                Id = (int)type,
                SubjectFormat = subjectFormat,
                BodyFormat = bodyFormat
            };
        }
    }
}