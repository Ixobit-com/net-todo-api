using Microsoft.AspNetCore.Identity;

namespace Todo.Data.Domain.Models.Identity {
    public class UserRole : IdentityUserRole<Guid> {
        public virtual Role? Role { get; set; }
        public virtual User? User { get; set; }
    }
}