using System.Net;
using System.Text.Json;
using SmartBudgett.API.Common;

namespace SmartBudgett.Core.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Unhandled API exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var details = _environment.IsDevelopment()
                    ? exception.Message
                    : "İşlem tamamlanamadı. Lütfen daha sonra tekrar deneyin.";

                var response = ApiResponse.Error(
                    "Beklenmeyen bir hata oluştu.",
                    details);

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
