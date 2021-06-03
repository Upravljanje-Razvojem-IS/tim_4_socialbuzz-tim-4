using Logistics.API.CustomException;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Logistics.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] //Nakon dodavanja UseExceptionHandler potrebno da bi swagger radio, inace baca grešku
    public class ErrorController : ControllerBase
    {
        [Route("/error-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error as LogisticException;

            string stackTrace = context.Error.StackTrace;
            string message = context.Error.Message;

            if(exception is LogisticException)
            {
                return Problem(
                detail: stackTrace,
                statusCode: exception.StatusCode,
                title: message);
            }

            return Problem(
                detail: stackTrace,
                title: message);

        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exception = context.Error as LogisticException;

            if (exception is LogisticException)
            {
                return Problem(
                statusCode: exception.StatusCode,
                title: exception.Message);
            }

            return Problem(
                title: context.Error.Message,
                statusCode: 500);
        }
    }

}
