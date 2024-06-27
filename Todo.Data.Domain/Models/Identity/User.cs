using Microsoft.AspNetCore.Identity;
using Todo.Data.Domain.Contracts;

namespace Todo.Data.Domain.Models.Identity {
    public class User : IdentityUser<Guid>, IEntity<Guid>, IAuditableEntity {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}