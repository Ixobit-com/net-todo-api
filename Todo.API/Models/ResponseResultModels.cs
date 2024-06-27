namespace Todo.API.Models {
    /// <summary>
    /// Represents base class for response result
    /// </summary>
    public abstract class BaseResponseResultModel {
        /// <summary>
        /// Gets or sets response code
        /// </summary>
        public string? Code { get; set; }
    }

    /// <summary>
    /// Represents OK response model
    /// </summary>
    public class OkResponseResultModel : BaseResponseResultModel {
        /// <summary>
        /// Gets or sets response message
        /// </summary>
        public string? Message { get; set; }
    }

    /// <summary>
    /// Represents OK response model
    /// </summary>
    /// <typeparam name="T">Response data type</typeparam>
    public class OkResponseResultModel<T> : OkResponseResultModel {
        /// <summary>
        /// Gets or sets response data
        /// </summary>
        public T? Data { get; set; }
    }

    /// <summary>
    /// Represents Bad Request response model
    /// </summary>
    public class ErrorResponseResultModel : BaseResponseResultModel {
        /// <summary>
        /// Gets or sets response error message
        /// </summary>
        public string? Error { get; set; }
    }
}
