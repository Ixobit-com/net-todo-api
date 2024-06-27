using Todo.Logic.Domain.Models.Filters.Base;

namespace Todo.Logic.Domain.Models.Filters {
    public enum RoleOrderByField {
        Name
    }

    public class RoleFilters : BaseFilters<RoleOrderByField> {
        public string? SearchText { get; set; }
    }
}