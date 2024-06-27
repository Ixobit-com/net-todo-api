using Todo.Logic.Domain.Models.Filters.Base;

namespace Todo.Logic.Domain.Models.Filters {
    public enum UserOrderByField {
        Email,
        Name
    }

    public class UserFilters : BaseFilters<UserOrderByField> {
        public string? SearchText { get; set; }
        public IEnumerable<Guid>? RoleIds { get; set; }
        public Guid? CompanyId { get; set; }
    }
}