using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.ViewModels;

namespace WebApi.Helpers
{
    public static class ValidationHandler
    {
        public static ActionResult<T> HandleBadRequest<T>(this ControllerBase controller, string requestPath, string message)
            where T : ServiceRequestResultModel, new()
        {

            var viewModel = new T
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = message,
                ExceptionMessage = null,
                Path = requestPath,
            };

            return controller.StatusCode((int)HttpStatusCode.BadRequest, viewModel);
        }

        public static ActionResult<T> HandleNotFound<T>(this ControllerBase controller, string requestPath, string message)
            where T : ServiceRequestResultModel, new()
        {
            var viewModel = new T
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = message,
                ExceptionMessage = null,
                Path = requestPath,
            };

            return controller.StatusCode((int)HttpStatusCode.NotFound, viewModel);
        }
    }
}
