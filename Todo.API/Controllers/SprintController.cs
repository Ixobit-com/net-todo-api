using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Todo.API.Constants;
using Todo.API.Controllers.Base;
using Todo.API.Models;
using Todo.Data.Domain.Models;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.API.Controllers {
    public class SprintController : BaseCrudController<ISprintService, Sprint, Guid, SprintDto, SprintFilters, SprintModel, SprintDetailsModel, SprintCreateModel, SprintUpdateModel> {
        public SprintController(
            IAuthorizationService authorizationService,
            ISprintService sprintService,
            IMapper mapper,
            IValidator<SprintCreateModel> validatorCreate,
            IValidator<SprintUpdateModel> validatorUpdate)
            : base(authorizationService, sprintService, mapper, validatorCreate, validatorUpdate,
                  PolicyNames.SprintCreate, PolicyNames.SprintRead, PolicyNames.SprintUpdate, PolicyNames.SprintDelete) { }
    }
}