using Todo.Logic.Domain.Models.Filters.Base;

namespace Todo.Logic.Domain.Models.Filters {
    public enum ClientOrderByField {
        Name,
        IsActive
    }

    public class ClientFilters : BaseFilters<ClientOrderByField> {
        public string? SearchText { get; set; }
        public bool? IsActive { get; set; }
    }
}