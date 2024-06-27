using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Todo.API.Constants;
using Todo.API.Controllers.Base;
using Todo.API.Models;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.API.Controllers {
    public class RoleController : BaseReadController<IRoleService, Role, Guid, RoleFilters, RoleModel, RoleDetailsModel> {
        public RoleController(
            IAuthorizationService authorizationService,
            IRoleService roleService,
            IMapper mapper)
            : base(authorizationService, roleService, mapper, PolicyNames.RoleRead) { }
    }
}