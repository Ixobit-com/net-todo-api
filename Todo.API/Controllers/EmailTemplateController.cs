using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Constants;
using Todo.API.Controllers.Base;
using Todo.API.Filters;
using Todo.API.Models;
using Todo.Data.Domain.Models;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.API.Controllers {
    public class EmailTemplateController : BaseUpdateController<IEmailTemplateService, EmailTemplate, int, EmailTemplateDto, EmailTemplateFilters, EmailTemplateModel, EmailTemplateDetailsModel, EmailTemplateUpdateModel> {
        public EmailTemplateController(
            IAuthorizationService authorizationService,
            IEmailTemplateService emailTemplateService,
            IMapper mapper,
            IValidator<EmailTemplateUpdateModel> validatorUpdate)
            : base(authorizationService, emailTemplateService, mapper, validatorUpdate,
                  PolicyNames.EmailTemplateRead, PolicyNames.EmailTemplateUpdate) { }

        /// <summary>
        /// Show preview for email template
        /// </summary>
        /// <param name="data">Email template</param>
        /// <returns></returns>
        [HttpPost("preview")]
        [Authorize(Policy = PolicyNames.EmailTemplatePreview)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseResultModel<EmailTemplatePreviewOutputModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseResultModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ValidateActionFilter<EmailTemplatePreviewInputModel>]
        public IActionResult Preview([FromBody] EmailTemplatePreviewInputModel data) {
            var result = _entityService.GetPreview(data.Type, data.SubjectFormat, data.BodyFormat);

            return GetResponseResult(result);
        }
    }
}