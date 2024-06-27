using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Todo.Common;
using Todo.Common.Logging;
using Todo.Data.Domain.Contracts;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Filters.Base;

namespace Todo.Logic.Services.Base {
    public abstract class BaseReadService<TEntity, TKey, TFilters, TOrderBy> : BaseService, IBaseReadService<TEntity, TKey, TFilters>
        where TKey : IEquatable<TKey>, new()
        where TEntity : class, IEntity<TKey>, new()
        where TFilters : BaseFilters<TOrderBy>
        where TOrderBy : Enum {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly Logger _logger;
        protected readonly IMapper _mapper;

        public BaseReadService(
            IUnitOfWork unitOfWork,
            Logger logger,
            IMapper mapper)
            : base() {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PagingResult<T>>> GetAsync<T>(TFilters filters) {
            try {
                // Get entities
                IQueryable<TEntity> query = _unitOfWork.GetRepository<TEntity>()
                    .Get();

                // Apply identity conditions
                query = ApplyConditions(query, GetIdentityConditions());

                // Apply general conditions
                query = ApplyConditions(query, GetGeneralConditions(filters));

                // Get total items
                int totalItems = query.Count();

                // Apply advanced conditions
                query = ApplyConditions(query, GetAdvancedConditions(filters));

                // Apply ordering
                query = OrderBy(query, filters.OrderBy, filters.DescOrder);

                // Get filtered items
                int filteredItems = query.Count();

                // Get items for specific page
                query = query.Skip(filters.Skip);
                if (filters.Take > 0) {
                    query = query.Take(filters.Take);
                }

                // Map and return result
                return ServiceResult<PagingResult<T>>.Successed(new PagingResult<T> {
                    TotalItems = totalItems,
                    FilteredItems = filteredItems,
                    Items = query
                        .ProjectTo<T>(_mapper.ConfigurationProvider)
                        .ToList()
                });
            }
            catch (Exception ex) {
                _logger.Error(ex, $"Get entities exception occured.  Details: [{JsonConvert.SerializeObject(filters)}]");
                return ServiceResult<PagingResult<T>>.InternalServerError(ex.Message);
            }
        }

        public async Task<ServiceResult<T>> FindAsync<T>(TKey id) {
            try {
                // Get entity by id
                IQueryable<TEntity> query = _unitOfWork.GetRepository<TEntity>()
                    .Get(x => x.Id.Equals(id));

                // Apply identity conditions
                query = ApplyConditions(query, GetIdentityConditions());

                return ServiceResult<T>.Successed(
                    query
                        .ProjectTo<T>(_mapper.ConfigurationProvider)
                        .FirstOrDefault());
            }
            catch (Exception ex) {
                _logger.Error(ex, $"Get entity exception occured.  Details: [{JsonConvert.SerializeObject(id)}]");
                return ServiceResult<T>.InternalServerError(ex.Message);
            }
        }

        protected virtual IQueryable<TEntity> OrderBy(IQueryable<TEntity> query, TOrderBy orderBy, bool desc) {
            return query;
        }

        protected IQueryable<TEntity> ApplyConditions(IQueryable<TEntity> query, List<Expression<Func<TEntity, bool>>> conditions) {
            if (conditions != null) {
                foreach (var condition in conditions) {
                    query = query.Where(condition);
                }
            }

            return query;
        }

        protected virtual List<Expression<Func<TEntity, bool>>>? GetIdentityConditions() {
            return null;
        }

        protected virtual List<Expression<Func<TEntity, bool>>>? GetGeneralConditions(TFilters filters) {
            return null;
        }

        protected virtual List<Expression<Func<TEntity, bool>>>? GetAdvancedConditions(TFilters filters) {
            return null;
        }
    }
}