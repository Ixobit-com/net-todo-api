using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Constants;
using Todo.API.Controllers.Base;
using Todo.API.Models;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models;
using Todo.Logic.Domain.Models.Filters;

namespace Todo.API.Controllers {
    public class ClientController : BaseCrudController<IClientService, Client, Guid, ClientDto, ClientFilters, ClientModel, ClientDetailsModel, ClientCreateModel, ClientUpdateModel> {
        public ClientController(
            IAuthorizationService authorizationService,
            IClientService clientService,
            IMapper mapper,
            IValidator<ClientCreateModel> validatorCreate,
            IValidator<ClientUpdateModel> validatorUpdate)
            : base(authorizationService, clientService, mapper, validatorCreate, validatorUpdate,
                  PolicyNames.ClientCreate, PolicyNames.ClientRead, PolicyNames.ClientUpdate, PolicyNames.ClientDelete) { }

        /// <summary>
        /// Reset client credentials
        /// </summary>
        /// <param name="id">Client id</param>
        /// <returns></returns>
        [HttpPut("reset/{id}")]
        [Authorize(Policy = PolicyNames.ClientReset)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseResultModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseResultModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Reset(Guid id) {
            var result = await _entityService.ResetAsync(id);

            return GetResponseResult(result);
        }
    }
}