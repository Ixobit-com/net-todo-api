using AutoMapper;
using System.Linq.Expressions;
using Todo.Common;
using Todo.Common.Email;
using Todo.Common.Enums;
using Todo.Common.Logging;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Filters;
using Todo.Logic.Services.Base;

namespace Todo.Logic.Services {
    public class EmailTemplateService : BaseUpdateService<EmailTemplate, int, EmailTemplateFilters, EmailTemplateOrderByField>, IEmailTemplateService {
        private readonly EmailBuilder _emailBuilder;

        public EmailTemplateService(
            IUnitOfWork unitOfWork,
            Logger logger,
            IMapper mapper)
            : base(unitOfWork, logger, mapper) {
            _emailBuilder = new EmailBuilder();
        }

        public ServiceResult<EmailResult> GetPreview(EmailTemplateType type, string subjectFormat, string bodyFormat) {
            try {
                var result = _emailBuilder.Preview(type, subjectFormat, bodyFormat);

                return ServiceResult<EmailResult>.Successed(result);
            }
            catch (Exception ex) {
                _logger.Error(ex, $"Get email template preview exception occured. Type: [{type}], subject format: [{subjectFormat}], body format: [{bodyFormat}]");
                return ServiceResult<EmailResult>.InternalServerError(ex.Message);
            }
        }

        protected override List<Expression<Func<EmailTemplate, bool>>>? GetAdvancedConditions(EmailTemplateFilters filters) {
            var conditions = new List<Expression<Func<EmailTemplate, bool>>>();

            if (!String.IsNullOrEmpty(filters.SearchText)) {
                conditions.Add(x =>
                    x.SubjectFormat.ToLower().Contains(filters.SearchText.ToLower()) || x.BodyFormat.ToLower().Contains(filters.SearchText.ToLower()));
            }

            return conditions;
        }

        protected override IQueryable<EmailTemplate> OrderBy(IQueryable<EmailTemplate> query, EmailTemplateOrderByField orderBy, bool desc) {
            switch (orderBy) {
                case EmailTemplateOrderByField.Type:
                    return (desc) ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id);
                default:
                    return base.OrderBy(query, orderBy, desc);
            }
        }
    }
}