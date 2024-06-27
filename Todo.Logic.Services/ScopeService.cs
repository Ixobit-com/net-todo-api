using AutoMapper;
using System.Linq.Expressions;
using Todo.Common.Logging;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Filters;
using Todo.Logic.Services.Base;

namespace Todo.Logic.Services {
    public class ScopeService : BaseReadService<Scope, Guid, ScopeFilters, ScopeOrderByField>, IScopeService {
        public ScopeService(
            IUnitOfWork unitOfWork,
            Logger logger,
            IMapper mapper)
            : base(unitOfWork, logger, mapper) { }

        protected override List<Expression<Func<Scope, bool>>>? GetAdvancedConditions(ScopeFilters filters) {
            var conditions = new List<Expression<Func<Scope, bool>>>();

            if (!String.IsNullOrEmpty(filters.SearchText)) {
                conditions.Add(x => x.Name.ToLower().Contains(filters.SearchText.ToLower()));
            }

            return conditions;
        }

        protected override IQueryable<Scope> OrderBy(IQueryable<Scope> query, ScopeOrderByField orderBy, bool desc) {
            switch (orderBy) {
                case ScopeOrderByField.Name:
                    return (desc) ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                default:
                    return base.OrderBy(query, orderBy, desc);
            }
        }
    }
}