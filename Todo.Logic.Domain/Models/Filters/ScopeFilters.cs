using Todo.Logic.Domain.Models.Filters.Base;

namespace Todo.Logic.Domain.Models.Filters {
    public enum ScopeOrderByField {
        Name
    }

    public class ScopeFilters : BaseFilters<ScopeOrderByField> {
        public string? SearchText { get; set; }
    }
}