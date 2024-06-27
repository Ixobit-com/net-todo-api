namespace Todo.API.Models {
    /// <summary>
    /// Represents model to provide base label data
    /// </summary>
    public class LabelModel {
        /// <summary>
        /// Gets or sets label id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets label name
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Represents model to provide detailed label data
    /// </summary>
    public class LabelDetailsModel {
        /// <summary>
        /// Gets or sets label id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets label name
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Represents model to create new label
    /// </summary>
    public class LabelCreateModel {
        /// <summary>
        /// Gets or sets label name
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Represents model to update label
    /// </summary>
    public class LabelUpdateModel : LabelCreateModel {
        /// <summary>
        /// Gets or sets label id
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    /// Represents short label model
    /// </summary>
    public class LabelKeyValueModel {
        /// <summary>
        /// Gets or sets label id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets label name
        /// </summary>
        public string? Name { get; set; }
    }
}