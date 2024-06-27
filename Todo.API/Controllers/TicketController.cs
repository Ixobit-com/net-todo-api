using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Todo.API.Constants;
using Todo.API.Controllers.Base;
using Todo.API.Models;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Filters;
using Todo.Logic.Domain.Models;
using Todo.Data.Domain.Models;

namespace Todo.API.Controllers {
    public class TicketController : BaseCrudController<ITicketService, Ticket, Guid, TicketDto, TicketFilters, TicketModel, TicketDetailsModel, TicketCreateModel, TicketUpdateModel> {
        public TicketController(
            IAuthorizationService authorizationService,
            ITicketService ticketService,
            IMapper mapper,
            IValidator<TicketCreateModel> validatorCreate,
            IValidator<TicketUpdateModel> validatorUpdate)
            : base(authorizationService, ticketService, mapper, validatorCreate, validatorUpdate,
                  PolicyNames.TicketCreate, PolicyNames.TicketRead, PolicyNames.TicketUpdate, PolicyNames.TicketDelete) { }
    }
}