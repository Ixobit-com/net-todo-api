using Todo.Common.Enums;
using Todo.Common.Extensions;

namespace Todo.API.Models {
    /// <summary>
    /// Represents model to provide base email template data
    /// </summary>
    public class EmailTemplateModel {
        /// <summary>
        /// Gets or sets email template type
        /// </summary>
        public EmailTemplateType Type { get; set; }
        /// <summary>
        /// Gets email template description
        /// </summary>
        public string Description => Type.GetDescription();
    }

    /// <summary>
    /// Represents model to provide detailed vendor data
    /// </summary>
    public class EmailTemplateDetailsModel {
        /// <summary>
        /// Gets or sets email template type
        /// </summary>
        public EmailTemplateType Type { get; set; }
        /// <summary>
        /// Gets email template description
        /// </summary>
        public string Description => Type.GetDescription();
        /// <summary>
        /// Gets or sets email template subject format
        /// </summary>
        public string SubjectFormat { get; set; }
        /// <summary>
        /// Gets or sets email template body format
        /// </summary>
        public string BodyFormat { get; set; }
    }

    /// <summary>
    /// Represents model to update email template
    /// </summary>
    public class EmailTemplateUpdateModel {
        /// <summary>
        /// Gets or sets email template type
        /// </summary>
        public EmailTemplateType Type { get; set; }
        /// <summary>
        /// Gets or sets email template subject format
        /// </summary>
        public string SubjectFormat { get; set; }
        /// <summary>
        /// Gets or sets email template body format
        /// </summary>
        public string BodyFormat { get; set; }
    }

    /// <summary>
    /// Represents model for input data to get email template preview
    /// </summary>
    public class EmailTemplatePreviewInputModel {
        /// <summary>
        /// Gets or sets email template type
        /// </summary>
        public EmailTemplateType Type { get; set; }
        /// <summary>
        /// Gets or sets email template subject format
        /// </summary>
        public string SubjectFormat { get; set; }
        /// <summary>
        /// Gets or sets email template body format
        /// </summary>
        public string BodyFormat { get; set; }
    }

    /// <summary>
    /// Represents model to show email template preview
    /// </summary>
    public class EmailTemplatePreviewOutputModel {
        /// <summary>
        /// Gets or sets email template subject preview
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Gets or sets email template body preview
        /// </summary>
        public string Body { get; set; }
    }
}