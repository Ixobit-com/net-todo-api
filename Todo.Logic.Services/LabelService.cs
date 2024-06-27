using AutoMapper;
using System.Linq.Expressions;
using Todo.Common.Logging;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Filters;
using Todo.Logic.Services.Base;

namespace Todo.Logic.Services {
    public class LabelService : BaseCrudService<Label, Guid, LabelFilters, LabelOrderByField>, ILabelService {
        public LabelService(
            IUnitOfWork unitOfWork,
            Logger logger,
            IMapper mapper)
            : base(unitOfWork, logger, mapper) { }

        protected override Guid GetNewKey() {
            return Guid.NewGuid();
        }

        protected override List<Expression<Func<Label, bool>>>? GetAdvancedConditions(LabelFilters filters) {
            var conditions = new List<Expression<Func<Label, bool>>>();

            if (!String.IsNullOrEmpty(filters.SearchText)) {
                conditions.Add(x => x.Name.ToLower().Contains(filters.SearchText.ToLower()));
            }

            return conditions;
        }

        protected override IQueryable<Label> OrderBy(IQueryable<Label> query, LabelOrderByField orderBy, bool desc) {
            switch (orderBy) {
                case LabelOrderByField.Name:
                    return (desc) ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                default:
                    return base.OrderBy(query, orderBy, desc);
            }
        }
    }
}