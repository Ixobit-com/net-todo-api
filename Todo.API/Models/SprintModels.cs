namespace Todo.API.Models {
    /// <summary>
    /// Represents model to provide base sprint data
    /// </summary>
    public class SprintModel {
        /// <summary>
        /// Gets or sets sprint id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets sprint name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets sprint started at
        /// </summary>
        public DateTime? StartedAt { get; set; }
        /// <summary>
        /// Gets or sets sprint finished at
        /// </summary>
        public DateTime? FinishedAt { get; set; }
    }

    /// <summary>
    /// Represents model to provide detailed sprint data
    /// </summary>
    public class SprintDetailsModel {
        /// <summary>
        /// Gets or sets sprint id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets sprint name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets sprint started at
        /// </summary>
        public DateTime? StartedAt { get; set; }
        /// <summary>
        /// Gets or sets sprint finished at
        /// </summary>
        public DateTime? FinishedAt { get; set; }
    }

    /// <summary>
    /// Represents model to create new sprint
    /// </summary>
    public class SprintCreateModel {
        /// <summary>
        /// Gets or sets sprint name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets sprint started at
        /// </summary>
        public DateTime? StartedAt { get; set; }
        /// <summary>
        /// Gets or sets sprint finished at
        /// </summary>
        public DateTime? FinishedAt { get; set; }
    }

    /// <summary>
    /// Represents model to update sprint
    /// </summary>
    public class SprintUpdateModel : SprintCreateModel {
        /// <summary>
        /// Gets or sets sprint id
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents short sprint model
    /// </summary>
    public class SprintKeyValueModel {
        /// <summary>
        /// Gets or sets sprint id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets sprint name
        /// </summary>
        public string? Name { get; set; }
    }
}