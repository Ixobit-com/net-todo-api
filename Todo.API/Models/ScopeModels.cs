namespace Todo.API.Models {
    /// <summary>
    /// Represents model to provide base scope data
    /// </summary>
    public class ScopeModel {
        /// <summary>
        /// Gets or sets scope id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets scope name
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Represents model to provide detailed scope data
    /// </summary>
    public class ScopeDetailsModel {
        /// <summary>
        /// Gets or sets scope id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets scope name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets scope description
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Represents model to provude short scope data
    /// </summary>
    public class ScopeKeyValueModel {
        /// <summary>
        /// Gets or sets scope id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets scope name
        /// </summary>
        public string? Name { get; set; }
    }
}