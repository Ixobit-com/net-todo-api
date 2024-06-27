using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Todo.Common.Logging;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Filters;
using Todo.Logic.Services.Base;

namespace Todo.Logic.Services {
    public class TicketService : BaseCrudService<Ticket, Guid, TicketFilters, TicketOrderByField>, ITicketService {
        public TicketService(
            IUnitOfWork unitOfWork,
            Logger logger,
            IMapper mapper)
            : base(unitOfWork, logger, mapper) { }

        protected override Guid GetNewKey() {
            return Guid.NewGuid();
        }

        protected override List<Expression<Func<Ticket, bool>>>? GetAdvancedConditions(TicketFilters filters) {
            var conditions = new List<Expression<Func<Ticket, bool>>>();

            if (!String.IsNullOrEmpty(filters.SearchText)) {
                conditions.Add(x => x.Subject.ToLower().Contains(filters.SearchText.ToLower()));
            }

            if (filters.Status.HasValue) {
                conditions.Add(x => x.Status == filters.Status.Value);
            }

            if (filters.Priority.HasValue) {
                conditions.Add(x => x.Priority == filters.Priority.Value);
            }

            if (filters.SprintId.HasValue) {
                conditions.Add(x => x.SprintId == filters.SprintId.Value);
            }

            if (filters.ParentTicketId.HasValue) {
                conditions.Add(x => x.ParentTicketId == filters.ParentTicketId.Value);
            }

            if (filters.AssigneeId.HasValue) {
                conditions.Add(x => x.AssigneeId == filters.AssigneeId.Value);
            }

            if (filters.LabelIds?.Any() ?? false) {
                conditions.Add(x => filters.LabelIds.All(id => x.Labels.Any(l => l.LabelId == id)));
            }

            return conditions;
        }

        protected override IQueryable<Ticket> OrderBy(IQueryable<Ticket> query, TicketOrderByField orderBy, bool desc) {
            switch (orderBy) {
                case TicketOrderByField.Subject:
                    return (desc) ? query.OrderByDescending(x => x.Subject) : query.OrderBy(x => x.Subject);
                case TicketOrderByField.Status:
                    return (desc) ? query.OrderByDescending(x => x.Status) : query.OrderBy(x => x.Status);
                case TicketOrderByField.Priority:
                    return (desc) ? query.OrderByDescending(x => x.Priority) : query.OrderBy(x => x.Priority);
                case TicketOrderByField.Sprint:
                    return (desc) ? query.OrderByDescending(x => x.Sprint.Name) : query.OrderBy(x => x.Sprint.Name);
                default:
                    return base.OrderBy(query, orderBy, desc);
            }
        }

        protected override IQueryable<Ticket> GetEntityIncludesToUpdate(IQueryable<Ticket> query) {
            return query
                .Include(x => x.Labels)
                    .ThenInclude(x => x.Label);
        }
    }
}