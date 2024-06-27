using Todo.Common;
using Todo.Common.Email;
using Todo.Common.Enums;
using Todo.Data.Domain.Models;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.Logic.Domain.Contracts {
    public interface IEmailTemplateService : IBaseUpdateService<EmailTemplate, int, EmailTemplateFilters> {
        ServiceResult<EmailResult> GetPreview(EmailTemplateType type, string subjectFormat, string bodyFormat);
    }
}