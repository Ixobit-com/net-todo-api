using Todo.Logic.Domain.Models.Filters.Base;

namespace Todo.Logic.Domain.Models.Filters {
    public enum EmailTemplateOrderByField {
        Type
    }

    public class EmailTemplateFilters : BaseFilters<EmailTemplateOrderByField> {
        public string SearchText { get; set; }
    }
}