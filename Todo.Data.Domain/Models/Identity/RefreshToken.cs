using Todo.Data.Domain.Models.Base;

namespace Todo.Data.Domain.Models.Identity {
    public class RefreshToken : BaseEntity<Guid> {
        public Guid ClientId { get; set; }
        public Guid? UserId { get; set; }
        public string? Token { get; set; }
        public DateTime GeneratedAt { get; set; }

        public virtual Client? Client { get; set; }
        public virtual User? User { get; set; }
    }
}