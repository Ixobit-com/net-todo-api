using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Models;
using Todo.Common;
using Todo.Common.Constants;

namespace Todo.API.Controllers.Base {
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class BaseController : ControllerBase {
        /// <summary>
        /// Generates 200 OK response
        /// </summary>
        /// <param name="code">Response code</param>
        /// <param name="message">Response message</param>
        /// <returns>OK request result</returns>
        protected OkObjectResult GetOkResponseResult(string code, string message) {
            return Ok(new OkResponseResultModel {
                Code = code,
                Message = message
            });
        }

        /// <summary>
        /// Generates 200 OK response
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <param name="code">Response code</param>
        /// <param name="message">Response message</param>
        /// <param name="data">Response data</param>
        /// <returns>OK request result</returns>
        protected OkObjectResult GetOkResponseResult<T>(string code, string message, T data) {
            return Ok(new OkResponseResultModel<T> {
                Code = code,
                Message = message,
                Data = data
            });
        }

        /// <summary>
        /// Generates 200 OK response
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <param name="data">Response data</param>
        /// <returns>OK request result</returns>
        protected OkObjectResult GetOkResponseResult<T>(T data) {
            return Ok(new OkResponseResultModel<T> {
                Code = ResultCodes.Common.OK,
                Message = String.Empty,
                Data = data
            });
        }

        /// <summary>
        /// Generates 400 Bad Request response
        /// </summary>
        /// <param name="code">Response code</param>
        /// <param name="error">Error message</param>
        /// <returns>Bad request result</returns>
        protected BadRequestObjectResult GetErrorResponseResult(string code, string error) {
            return BadRequest(new ErrorResponseResultModel {
                Code = code,
                Error = error
            });
        }

        /// <summary>
        /// Generates 400 Bad Request response
        /// </summary>
        /// <param name="result">Validation result</param>
        /// <returns>Bad request result</returns>
        protected BadRequestObjectResult GetErrorResponseResult(ValidationResult result) {
            var error = result.Errors.First();

            return BadRequest(new ErrorResponseResultModel {
                Code = error.ErrorCode,
                Error = error.ErrorMessage
            });
        }

        /// <summary>
        /// Generates response based on service result
        /// </summary>
        /// <param name="result">Service result</param>
        /// <returns>Request result</returns>
        protected ObjectResult GetResponseResult(ServiceResult result) {
            if (result.IsSuccessful) {
                return Ok(new OkResponseResultModel {
                    Code = result.Code,
                    Message = result.Message
                });
            }

            return BadRequest(new ErrorResponseResultModel {
                Code = result.Code,
                Error = result.Message
            });
        }

        /// <summary>
        /// Generates response based on service result
        /// </summary>
        /// <typeparam name="T">Response data type</typeparam>
        /// <param name="result">Service result</param>
        /// <returns>Request result</returns>
        protected ObjectResult GetResponseResult<T>(ServiceResult<T> result) {
            if (result.IsSuccessful) {
                return Ok(new OkResponseResultModel<T> {
                    Code = result.Code,
                    Message = result.Message,
                    Data = result.Data
                });
            }

            return BadRequest(new ErrorResponseResultModel {
                Code = result.Code,
                Error = result.Message
            });
        }

        /// <summary>
        /// Generates 403 Forbidden response
        /// </summary>
        /// <returns>Forbidden result</returns>
        protected StatusCodeResult GetForbiddenResponseResult() {
            return StatusCode(StatusCodes.Status403Forbidden);
        }
    }
}