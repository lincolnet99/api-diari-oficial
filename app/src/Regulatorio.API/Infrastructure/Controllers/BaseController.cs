using Microsoft.AspNetCore.Mvc;
using Regulatorio.API.Filters;
using Regulatorio.SharedKernel;

namespace Regulatorio.API.Infrastructure.Controllers
{
    [Produces("application/json")]
    [JsonException]
    public class BaseController : Controller
    {
        protected IActionResult Ok(int code)
        {
            return base.Ok(Envelope.Ok(code));
        }

        protected IActionResult Ok<T>(int code, T result)
        {
            return base.Ok(Envelope.Ok(code, result));
        }

        protected IActionResult Created<T>(int code, T result)
        {
            return base.Created(Request.Path, Envelope.Ok(code, result));
        }

        protected IActionResult Error(int code, IList<Error> errors)
        {
            return base.Ok(Envelope.Error(code, errors));
        }

        protected IActionResult Unauthorized(int code, Error error)
        {
            return Unauthorized(Envelope.Error(code, error));
        }
    }
}