using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using Todo.Common.Logging;

namespace Todo.API.Filters {
    public class LogRequestActionFilterAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext context) {
            var logger = context.HttpContext.RequestServices.GetRequiredService<Logger>();

            string httpMethod = context.HttpContext.Request.Method.ToUpper();
            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
            string arguments = JsonConvert.SerializeObject(context.ActionArguments);

            logger.Info($"[{httpMethod}] {controller}/{action} - {arguments}");
        }

        public override void OnActionExecuted(ActionExecutedContext context) {
            var logger = context.HttpContext.RequestServices.GetRequiredService<Logger>();

            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();

            string statusCode = "N/A";
            string value = null;

            if (context.Result is ObjectResult) {
                statusCode = (context.Result as ObjectResult).StatusCode.ToString();
                value = JsonConvert.SerializeObject((context.Result as ObjectResult).Value);
            }
            else if (context.Result is StatusCodeResult) {
                statusCode = (context.Result as StatusCodeResult).StatusCode.ToString();
            }
            else if (context.Result is FileContentResult) {
                statusCode = HttpStatusCode.OK.ToString();
            }
            else if (context.Result is RedirectResult) {
                statusCode = HttpStatusCode.Redirect.ToString();
            }

            logger.Info($"[{statusCode}] {controller}/{action} - {value}");
        }
    }
}