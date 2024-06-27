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
    public class LabelController : BaseCrudController<ILabelService, Label, Guid, LabelDto, LabelFilters, LabelModel, LabelDetailsModel, LabelCreateModel, LabelUpdateModel> {
        public LabelController(
            IAuthorizationService authorizationService,
            ILabelService labelService,
            IMapper mapper,
            IValidator<LabelCreateModel> validatorCreate,
            IValidator<LabelUpdateModel> validatorUpdate)
            : base(authorizationService, labelService, mapper, validatorCreate, validatorUpdate,
                  PolicyNames.LabelCreate, PolicyNames.LabelRead, PolicyNames.LabelUpdate, PolicyNames.LabelDelete) { }
    }
}