using Todo.Logic.Domain.Models.Filters.Base;
using Todo.Common.Enums;

namespace Todo.Logic.Domain.Models.Filters {
    public enum TicketOrderByField {
        Subject,
        Status,
        Priority,
        Sprint
    }

    public class TicketFilters : BaseFilters<TicketOrderByField> {
        public string? SearchText { get; set; }
        public TicketStatus? Status { get; set; }
        public TicketPriority? Priority { get; set; }
        public Guid? SprintId { get; set; }
        public Guid? ParentTicketId { get; set; }
        public Guid? AssigneeId { get; set; }
        public IEnumerable<Guid> LabelIds { get; set; }
    }
}