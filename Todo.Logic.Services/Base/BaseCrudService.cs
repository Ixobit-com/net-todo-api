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
    public abstract class BaseCrudService<TEntity, TKey, TFilters, TOrderBy> : BaseUpdateService<TEntity, TKey, TFilters, TOrderBy>, IBaseCrudService<TEntity, TKey, TFilters>
        where TKey : IEquatable<TKey>, new()
        where TEntity : class, IEntity<TKey>, new()
        where TFilters : BaseFilters<TOrderBy>
        where TOrderBy : Enum {
        public BaseCrudService(
            IUnitOfWork unitOfWork,
            Logger logger,
            IMapper mapper)
            : base(unitOfWork, logger, mapper) { }

        public async virtual Task<ServiceResult<T>> CreateAsync<T, TCreateDto>(TCreateDto dto)
            where TCreateDto : BaseEntityDto<TKey> {
            try {
                TEntity entity = GetEntityToCreate(dto);

                await OnCreatingAsync(entity, dto);

                await ChangeEntityBasedOnIdentityAsync(entity);

                await SaveChangesOnCreateAsync(entity, dto);

                await OnCreatedAsync(entity, dto);

                return ServiceResult<T>.Successed((await FindAsync<T>(entity.Id)).Data);
            }
            catch (CodableException ex) {
                _logger.Error(ex, $"Create entity exception occured. Details: [{JsonConvert.SerializeObject(dto)}]");
                return ServiceResult<T>.Failed(ex.Code, ex.Message);
            }
            catch (Exception ex) {
                _logger.Error(ex, $"Create entity exception occured. Details: [{JsonConvert.SerializeObject(dto)}]");
                return ServiceResult<T>.InternalServerError(ex.Message);
            }
        }

        public async virtual Task<ServiceResult> DeleteAsync(TKey id) {
            try {
                TEntity entity = GetEntityToDelete(id);

                if (entity == null) {
                    return ServiceResult.Failed("Entity not found");
                }

                await OnDeletingAsync(entity);

                await SaveChangesOnDeleteAsync(entity);

                await OnDeletedAsync(entity);

                return ServiceResult.Successed();
            }
            catch (CodableException ex) {
                _logger.Error(ex, $"Delete entity exception occured. Details: [{id}]");
                return ServiceResult.Failed(ex.Code, ex.Message);
            }
            catch (Exception ex) {
                _logger.Error(ex, $"Delete entity exception occured. Delails: [{id}]");
                return ServiceResult.InternalServerError(ex.Message);
            }
        }

        protected virtual TKey GetNewKey() {
            throw new NotImplementedException();
        }

        protected virtual IQueryable<TEntity> GetEntityIncludesToDelete(IQueryable<TEntity> query) {
            return query;
        }

        protected virtual async Task SaveChangesOnCreateAsync<TCreateDto>(TEntity entity, TCreateDto dto)
            where TCreateDto : BaseEntityDto<TKey> {
            _unitOfWork.GetRepository<TEntity>().Add(entity);

            await _unitOfWork.CommitAsync();
        }

        protected virtual async Task SaveChangesOnDeleteAsync(TEntity entity) {
            _unitOfWork.GetRepository<TEntity>().Delete(entity);

            await _unitOfWork.CommitAsync();
        }

        protected virtual async Task OnCreatingAsync<TCreateDto>(TEntity entity, TCreateDto dto)
            where TCreateDto : BaseEntityDto<TKey> { }

        protected virtual async Task OnCreatedAsync<TCreateDto>(TEntity entity, TCreateDto dto)
            where TCreateDto : BaseEntityDto<TKey> { }

        protected virtual async Task OnDeletingAsync(TEntity entity) { }

        protected virtual async Task OnDeletedAsync(TEntity entity) { }

        private TEntity GetEntityToCreate<TCreateDto>(TCreateDto dto)
            where TCreateDto : BaseEntityDto<TKey> {
            dto.Id = GetNewKey();

            TEntity entity = new TEntity();

            _mapper.Map(dto, entity);

            return entity;
        }

        private TEntity GetEntityToDelete(TKey id) {
            var query = _unitOfWork.GetRepository<TEntity>()
                .Get(x => x.Id.Equals(id), true);

            query = GetEntityIncludesToDelete(query);

            query = ApplyConditions(query, GetIdentityConditions());

            return query.FirstOrDefault();
        }
    }
}