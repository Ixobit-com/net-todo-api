using Todo.Logic.Domain.Models.Filters.Base;

namespace Todo.Logic.Domain.Models.Filters {
    public enum SprintOrderByField {
        Name,
        StartedAt,
        FinishedAt
    }

    public class SprintFilters : BaseFilters<SprintOrderByField> {
        public string? SearchText { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}