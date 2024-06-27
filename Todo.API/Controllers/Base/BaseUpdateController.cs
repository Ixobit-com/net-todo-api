using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Models;
using Todo.Common.Constants;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Base;

namespace Todo.API.Controllers.Base {
    public abstract class BaseUpdateController<TService, TEntity, TKey, TDto, TFilters, TModel, TDetailsModel, TUpdateModel> : BaseReadController<TService, TEntity, TKey, TFilters, TModel, TDetailsModel>
        where TService : IBaseUpdateService<TEntity, TKey, TFilters>
        where TDto : BaseEntityDto<TKey>
        where TKey : IEquatable<TKey>, new()
        where TUpdateModel : class {
        protected readonly IValidator<TUpdateModel> _validatorUpdate;

        protected readonly string _policyUpdate;

        public BaseUpdateController(
            IAuthorizationService authorizationService,
            TService entityService,
            IMapper mapper,
            IValidator<TUpdateModel> validatorUpdate,
            string policyRead,
            string policyUpdate) : base(authorizationService, entityService, mapper, policyRead) {
            _validatorUpdate = validatorUpdate;
            _policyUpdate = policyUpdate;
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="data">Item data</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseResultModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async virtual Task<ActionResult<OkResponseResultModel<TDetailsModel>>> Put([FromBody] TUpdateModel data) {
            if (!await IsAuthorizedAsync(_policyUpdate)) {
                return GetForbiddenResponseResult();
            }

            if (data == null) {
                return GetErrorResponseResult(ResultCodes.Common.INCORRECT_INPUT_DATA_ERROR, "Incorrect input data");
            }

            PrepareUpdateModel(data);

            var valid = _validatorUpdate.Validate(data);
            if (!valid.IsValid) {
                return GetErrorResponseResult(valid);
            }

            var dto = _mapper.Map<TDto>(data);

            var result = await _entityService.UpdateAsync<TDetailsModel, TDto>(dto);

            return GetResponseResult(result);
        }

        /// <summary>
        /// Update item partially
        /// </summary>
        /// <param name="data">Item data</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseResultModel))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async virtual Task<ActionResult<OkResponseResultModel<TDetailsModel>>> Patch(TKey id, [FromBody] JsonPatchDocument<TUpdateModel> data) {
            if (!await IsAuthorizedAsync(_policyUpdate)) {
                return GetForbiddenResponseResult();
            }

            if (data == null) {
                return GetErrorResponseResult(ResultCodes.Common.INCORRECT_INPUT_DATA_ERROR, "Incorrect input data");
            }

            var existing = await _entityService.FindAsync<TUpdateModel>(id);
            if (!existing.IsSuccessful) {
                return GetErrorResponseResult(existing.Code, existing.Message);
            }

            data.ApplyTo(existing.Data);

            PrepareUpdateModel(existing.Data);

            var valid = _validatorUpdate.Validate(existing.Data);
            if (!valid.IsValid) {
                return GetErrorResponseResult(valid);
            }

            var dto = _mapper.Map<TDto>(existing.Data);

            var result = await _entityService.UpdateAsync<TDetailsModel, TDto>(dto);

            return GetResponseResult(result);
        }

        /// <summary>
        /// Prepares input data to update an existing item
        /// </summary>
        /// <param name="data">Item data</param>
        protected virtual void PrepareUpdateModel(TUpdateModel data) { }
    }
}