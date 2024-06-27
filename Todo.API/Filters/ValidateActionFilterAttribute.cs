using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Todo.API.Models;
using Todo.Common.Constants;

namespace Todo.API.Filters {
    public class ValidateActionFilterAttribute<T> : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext context) {
            if (context.ActionArguments == null || context.ActionArguments.Count == 0 || !context.ActionArguments.Any(x => x.Value is T)) {
                context.Result = new BadRequestObjectResult(new ErrorResponseResultModel {
                    Code = ResultCodes.Common.INCORRECT_INPUT_DATA_ERROR,
                    Error = "Incorrect input data"
                });

                return;
            }

            var arg = context.ActionArguments.First(x => x.Value is T);

            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            if (validator == null) {
                return;
            }

            var result = validator.Validate((T)arg.Value);
            if (!result.IsValid) {
                var error = result.Errors.First();

                context.Result = new BadRequestObjectResult(new ErrorResponseResultModel {
                    Code = error.ErrorCode,
                    Error = error.ErrorMessage
                });
            }
        }
    }
}