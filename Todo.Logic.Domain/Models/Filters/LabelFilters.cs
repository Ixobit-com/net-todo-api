using Todo.Logic.Domain.Models.Filters.Base;

namespace Todo.Logic.Domain.Models.Filters {
    public enum LabelOrderByField {
        Name
    }

    public class LabelFilters : BaseFilters<LabelOrderByField> {
        public string? SearchText { get; set; }
    }
}