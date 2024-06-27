using AutoMapper;
using System.Linq.Expressions;
using Todo.Common.Logging;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Filters;
using Todo.Logic.Services.Base;

namespace Todo.Logic.Services {
    public class RoleService : BaseReadService<Role, Guid, RoleFilters, RoleOrderByField>, IRoleService {
        public RoleService(
            IUnitOfWork unitOfWork,
            Logger logger,
            IMapper mapper)
            : base(unitOfWork, logger, mapper) { }

        protected override List<Expression<Func<Role, bool>>>? GetAdvancedConditions(RoleFilters filters) {
            var conditions = new List<Expression<Func<Role, bool>>>();

            if (!String.IsNullOrEmpty(filters.SearchText)) {
                conditions.Add(x => x.Name.ToLower().Contains(filters.SearchText.ToLower()));
            }

            return conditions;
        }

        protected override IQueryable<Role> OrderBy(IQueryable<Role> query, RoleOrderByField orderBy, bool desc) {
            switch (orderBy) {
                case RoleOrderByField.Name:
                    return (desc) ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                default:
                    return base.OrderBy(query, orderBy, desc);
            }
        }
    }
}