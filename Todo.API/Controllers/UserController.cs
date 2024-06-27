using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Todo.API.Constants;
using Todo.API.Controllers.Base;
using Todo.API.Models;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.API.Controllers {
    public class UserController : BaseCrudController<IUserService, User, Guid, UserDto, UserFilters, UserModel, UserDetailsModel, UserCreateModel, UserUpdateModel> {
        public UserController(
            IAuthorizationService authorizationService,
            IUserService userService,
            IMapper mapper,
            IValidator<UserCreateModel> validatorCreate,
            IValidator<UserUpdateModel> validatorUpdate)
            : base(authorizationService, userService, mapper, validatorCreate, validatorUpdate,
                  PolicyNames.UserCreate, PolicyNames.UserRead, PolicyNames.UserUpdate, PolicyNames.UserDelete) { }
    }
}