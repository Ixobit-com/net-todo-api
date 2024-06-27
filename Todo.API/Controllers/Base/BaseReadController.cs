using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Models;
using Todo.Common;
using Todo.Logic.Domain.Contracts;

namespace Todo.API.Controllers.Base {
    public abstract class BaseReadController<TService, TEntity, TKey, TFilters, TModel, TDetailsModel> : BaseController
        where TService : IBaseReadService<TEntity, TKey, TFilters> {
        protected readonly IAuthorizationService _authorizationService;
        protected readonly TService _entityService;
        protected readonly IMapper _mapper;

        protected readonly string _policyRead;

        public BaseReadController(
            IAuthorizationService authorizationService,
            TService entityService,
            IMapper mapper,
            string policyRead) {
            _authorizationService = authorizationService;
            _entityService = entityService;
            _mapper = mapper;

            _policyRead = policyRead;
        }

        /// <summary>
        /// Get items by filters
        /// </summary>
        /// <param name="filters">Input filters</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseResultModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public virtual async Task<ActionResult<OkResponseResultModel<PagingResult<TModel>>>> Get([FromQuery] TFilters filters) {
            if (!await IsAuthorizedAsync(_policyRead)) {
                return GetForbiddenResponseResult();
            }

            var result = await _entityService.GetAsync<TModel>(filters);

            return GetResponseResult(result);
        }

        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseResultModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public virtual async Task<ActionResult<OkResponseResultModel<TDetailsModel>>> Get(TKey id) {
            if (!await IsAuthorizedAsync(_policyRead)) {
                return GetForbiddenResponseResult();
            }

            var result = await _entityService.FindAsync<TDetailsModel>(id);

            return GetResponseResult(result);
        }

        protected async Task<bool> IsAuthorizedAsync(string policyName) {
            var result = await _authorizationService.AuthorizeAsync(User, policyName);

            return result.Succeeded;
        }
    }
}