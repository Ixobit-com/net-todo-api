using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Models;
using Todo.Common.Constants;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Base;

namespace Todo.API.Controllers.Base {
    public abstract class BaseCrudController<TService, TEntity, TKey, TDto, TFilters, TModel, TDetailsModel, TCreateModel, TUpdateModel> : BaseUpdateController<TService, TEntity, TKey, TDto, TFilters, TModel, TDetailsModel, TUpdateModel>
        where TService : IBaseCrudService<TEntity, TKey, TFilters>
        where TDto : BaseEntityDto<TKey>
        where TKey : IEquatable<TKey>, new()
        where TUpdateModel : class {
        protected readonly IValidator<TCreateModel> _validatorCreate;
        protected readonly string _policyCreate;
        protected readonly string _policyDelete;

        public BaseCrudController(
            IAuthorizationService authorizationService,
            TService entityService,
            IMapper mapper,
            IValidator<TCreateModel> validatorCreate,
            IValidator<TUpdateModel> validatorUpdate,
            string policyCreate, string policyRead, string policyUpdate, string policyDelete)
            : base(authorizationService, entityService, mapper, validatorUpdate, policyRead, policyUpdate) {
            _validatorCreate = validatorCreate;

            _policyCreate = policyCreate;
            _policyDelete = policyDelete;
        }

        /// <summary>
        /// Create new item
        /// </summary>
        /// <param name="data">Item data</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseResultModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public virtual async Task<ActionResult<OkResponseResultModel<TDetailsModel>>> Post([FromBody] TCreateModel data) {
            if (!await IsAuthorizedAsync(_policyCreate)) {
                return GetForbiddenResponseResult();
            }

            if (data == null) {
                return GetErrorResponseResult(ResultCodes.Common.INCORRECT_INPUT_DATA_ERROR, "Incorrect input data");
            }

            PrepareCreateModel(data);

            var valid = _validatorCreate.Validate(data);
            if (!valid.IsValid) {
                return GetErrorResponseResult(valid);
            }

            var dto = _mapper.Map<TDto>(data);

            var result = await _entityService.CreateAsync<TDetailsModel, TDto>(dto);

            return GetResponseResult(result);
        }

        /// <summary>
        /// Delete item by id
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OkResponseResultModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseResultModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public virtual async Task<IActionResult> Delete(TKey id) {
            if (!await IsAuthorizedAsync(_policyDelete)) {
                return GetForbiddenResponseResult();
            }

            var result = await _entityService.DeleteAsync(id);

            return GetResponseResult(result);
        }

        /// <summary>
        /// Prepares input data to create a new item
        /// </summary>
        /// <param name="data">Item data</param>
        protected virtual void PrepareCreateModel(TCreateModel data) { }
    }
}