using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Todo.API.Constants;
using Todo.API.Controllers.Base;
using Todo.API.Models;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.API.Controllers {
    public class ScopeController : BaseReadController<IScopeService, Scope, Guid, ScopeFilters, ScopeModel, ScopeDetailsModel> {
        public ScopeController(
            IAuthorizationService authorizationService,
            IScopeService scopeService,
            IMapper mapper)
            : base(authorizationService, scopeService, mapper, PolicyNames.ScopeRead) { }
    }
}