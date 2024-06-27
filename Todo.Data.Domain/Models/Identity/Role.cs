using Microsoft.AspNetCore.Identity;
using Todo.Data.Domain.Contracts;

namespace Todo.Data.Domain.Models.Identity {
    public class Role : IdentityRole<Guid>, IEntity<Guid> {
        public virtual ICollection<UserRole>? UserRoles { get; set; }
    }
}