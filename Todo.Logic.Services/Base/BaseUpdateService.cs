using AutoMapper;
using Newtonsoft.Json;
using Todo.Common;
using Todo.Common.Exceptions;
using Todo.Common.Logging;
using Todo.Data.Domain.Contracts;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Base;
using Todo.Logic.Domain.Models.Filters.Base;

namespace Todo.Logic.Services.Base {
    public abstract class BaseUpdateService<TEntity, TKey, TFilters, TOrderBy> : BaseReadService<TEntity, TKey, TFilters, TOrderBy>, IBaseUpdateService<TEntity, TKey, TFilters>
        where TKey : IEquatable<TKey>, new()
        where TEntity : class, IEntity<TKey>, new()
        where TFilters : BaseFilters<TOrderBy>
        where TOrderBy : Enum {
        public BaseUpdateService(
            IUnitOfWork unitOfWork,
            Logger logger,
            IMapper mapper)
            : base(unitOfWork, logger, mapper) { }

        public async virtual Task<ServiceResult<T>> UpdateAsync<T, TUpdateDto>(TUpdateDto dto)
            where TUpdateDto : BaseEntityDto<TKey> {
            try {
                TEntity entity = GetEntityToUpdate(dto.Id);

                if (entity == null) {
                    return ServiceResult<T>.Failed("Entity not found");
                }

                _mapper.Map(dto, entity);

                await OnUpdatingAsync(entity, dto);

                await ChangeEntityBasedOnIdentityAsync(entity);

                await SaveChangesOnUpdateAsync(entity, dto);

                await OnUpdatedAsync(entity, dto);

                return ServiceResult<T>.Successed((await FindAsync<T>(entity.Id)).Data);
            }
            catch (CodableException ex) {
                _logger.Error(ex, $"Update entity exception occured. Details: [{JsonConvert.SerializeObject(dto)}]");
                return ServiceResult<T>.Failed(ex.Code, ex.Message);
            }
            catch (Exception ex) {
                _logger.Error(ex, $"Update entity exception occured. Details: [{JsonConvert.SerializeObject(dto)}]");
                return ServiceResult<T>.InternalServerError(ex.Message);
            }
        }

        protected virtual IQueryable<TEntity> GetEntityIncludesToUpdate(IQueryable<TEntity> query) {
            return query;
        }

        protected virtual async Task OnUpdatingAsync<TUpdateDto>(TEntity entity, TUpdateDto dto)
            where TUpdateDto : BaseEntityDto<TKey> { }

        protected virtual async Task OnUpdatedAsync<TUpdateDto>(TEntity entity, TUpdateDto dto)
            where TUpdateDto : BaseEntityDto<TKey> { }

        protected virtual async Task ChangeEntityBasedOnIdentityAsync(TEntity entity) { }

        protected virtual async Task SaveChangesOnUpdateAsync<TUpdateDto>(TEntity entity, TUpdateDto dto)
            where TUpdateDto : BaseEntityDto<TKey> {
            await _unitOfWork.CommitAsync();
        }

        private TEntity GetEntityToUpdate(TKey id) {
            var query = _unitOfWork.GetRepository<TEntity>()
                .Get(x => x.Id.Equals(id), true);

            query = GetEntityIncludesToUpdate(query);

            query = ApplyConditions(query, GetIdentityConditions());

            return query.FirstOrDefault();
        }
    }
}