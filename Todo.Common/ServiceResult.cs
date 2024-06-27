using Todo.Common.Constants;

namespace Todo.Common {
    public class ServiceResult {
        public bool IsSuccessful { get; set; }
        public string? Code { get; set; }
        public string? Message { get; set; }

        public ServiceResult(
            bool isSuccessful,
            string code,
            string message) {
            IsSuccessful = isSuccessful;
            Code = code;
            Message = message;
        }

        public static ServiceResult Successed() {
            return Successed(ResultCodes.Common.OK, String.Empty);
        }

        public static ServiceResult Successed(string message) {
            return Successed(ResultCodes.Common.OK, message);
        }

        public static ServiceResult Successed(string code, string message) {
            return new ServiceResult(true, code, message);
        }

        public static ServiceResult Failed() {
            return Failed(ResultCodes.Common.ERROR, String.Empty);
        }

        public static ServiceResult Failed(string code) {
            return Failed(code, String.Empty);
        }

        public static ServiceResult InternalServerError(string details) {
            return Failed(ResultCodes.Common.INTERNAL_SERVER_ERROR, $"Internal server error: {details}");
        }

        public static ServiceResult Failed(string code, string message) {
            return new ServiceResult(false, code, message);
        }
    }

    public class ServiceResult<T> : ServiceResult {
        public T? Data { get; set; }

        public ServiceResult(
            bool isSuccessful,
            string code,
            string message,
            T data)
            : base(isSuccessful, code, message) {
            Data = data;
        }

        public static new ServiceResult<T> Successed() {
            return Successed(ResultCodes.Common.OK, String.Empty, default);
        }

        public static ServiceResult<T> Successed(T data) {
            return Successed(ResultCodes.Common.OK, String.Empty, data);
        }

        public static new ServiceResult<T> Successed(string message) {
            return Successed(ResultCodes.Common.OK, message, default);
        }

        public static ServiceResult<T> Successed(string message, T data) {
            return Successed(ResultCodes.Common.OK, message, data);
        }

        public static new ServiceResult<T> Successed(string code, string message) {
            return Successed(code, message, default);
        }

        public static ServiceResult<T> Successed(string code, string message, T data) {
            return new ServiceResult<T>(true, code, message, data);
        }

        public static new ServiceResult<T> Failed() {
            return Failed(ResultCodes.Common.ERROR, String.Empty);
        }

        public static new ServiceResult<T> Failed(string code) {
            return Failed(code, String.Empty);
        }

        public static ServiceResult<T> Failed(ServiceResult serviceResult) {
            return Failed(serviceResult.Code, serviceResult.Message);
        }

        public static ServiceResult<T> InternalServerError(string details) {
            return Failed(ResultCodes.Common.INTERNAL_SERVER_ERROR, $"Internal server error: {details}");
        }

        public static ServiceResult<T> Failed(string code, string message) {
            return new ServiceResult<T>(false, code, message, default);
        }
    }
}