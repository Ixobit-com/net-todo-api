namespace Todo.API.Models {
    /// <summary>
    /// Represents model to provide base role data
    /// </summary>
    public class RoleModel {
        /// <summary>
        /// Gets or sets role Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets role name
        /// </summary>
        public string? Name { get; set; }
    }

    /// <summary>
    /// Represents model to provide detailed role data
    /// </summary>
    public class RoleDetailsModel {
        /// <summary>
        /// Gets or sets role Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets role name
        /// </summary>
        public string? Name { get; set; }
    }

    /// <summary>
    /// Represents short scope model
    /// </summary>
    public class RoleKeyValueModel {
        /// <summary>
        /// Gets or sets role id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets role name
        /// </summary>
        public string? Name { get; set; }
    }
}