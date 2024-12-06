using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Regulatorio.API.Infrastructure.Controllers;
using Regulatorio.SharedKernel;
using Serilog;
using System.Net;

namespace Regulatorio.API.Filters
{
    public class JsonExceptionAttribute : TypeFilterAttribute
    {
        public JsonExceptionAttribute()
            : base(typeof(HttpCustomExceptionFilterImpl))
        {
        }

        private class HttpCustomExceptionFilterImpl : IExceptionFilter
        {
            private readonly IWebHostEnvironment _env;
            private readonly ILogger<HttpCustomExceptionFilterImpl> _logger;

            public HttpCustomExceptionFilterImpl(IWebHostEnvironment env, ILogger<HttpCustomExceptionFilterImpl> logger)
            {
                _env = env;
                _logger = logger;
            }

            public void OnException(ExceptionContext context)
            {
                var errors = new List<Error>();
                var eventId = new EventId(context.Exception.HResult);

                if (_env.IsDevelopment())
                {
                    errors.Add(new Error("401", context.Exception.Message));
                }
                else
                {
                    Log.Error($"Message: {context.Exception.Message} - StackTrace: {context.Exception.StackTrace}", context.Exception);
                    _logger.LogError(eventId, context.Exception, context.Exception.Message);
                    errors.Add(new Error("501", "Houve um erro interno, consulte o suporte para mais informações"));
                }

                var exceptionObject = new ObjectResult(Envelope.Error(501, errors)) { StatusCode = (int)HttpStatusCode.InternalServerError };

                context.Result = exceptionObject;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}