using AutoMapper;
using System.Linq.Expressions;
using Todo.Common.Logging;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Filters;
using Todo.Logic.Services.Base;

namespace Todo.Logic.Services {
    public class SprintService : BaseCrudService<Sprint, Guid, SprintFilters, SprintOrderByField>, ISprintService {
        public SprintService(
            IUnitOfWork unitOfWork,
            Logger logger,
            IMapper mapper)
            : base(unitOfWork, logger, mapper) { }

        protected override Guid GetNewKey() {
            return Guid.NewGuid();
        }

        protected override List<Expression<Func<Sprint, bool>>>? GetAdvancedConditions(SprintFilters filters) {
            var conditions = new List<Expression<Func<Sprint, bool>>>();

            if (!String.IsNullOrEmpty(filters.SearchText)) {
                conditions.Add(x => x.Name.ToLower().Contains(filters.SearchText.ToLower()));
            }

            if (filters.From.HasValue) {
                conditions.Add(x => !x.FinishedAt.HasValue || x.FinishedAt >= filters.From.Value);
            }

            if (filters.To.HasValue) {
                conditions.Add(x => !x.StartedAt.HasValue || x.StartedAt <= filters.To.Value);
            }

            return conditions;
        }

        protected override IQueryable<Sprint> OrderBy(IQueryable<Sprint> query, SprintOrderByField orderBy, bool desc) {
            switch (orderBy) {
                case SprintOrderByField.Name:
                    return (desc) ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                case SprintOrderByField.StartedAt:
                    return (desc) ? query.OrderByDescending(x => x.StartedAt) : query.OrderBy(x => x.StartedAt);
                case SprintOrderByField.FinishedAt:
                    return (desc) ? query.OrderByDescending(x => x.FinishedAt) : query.OrderBy(x => x.FinishedAt);
                default:
                    return base.OrderBy(query, orderBy, desc);
            }
        }
    }
}