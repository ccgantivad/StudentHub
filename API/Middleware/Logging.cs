using System.Diagnostics;

namespace API.Middleware
{
    public class Logging
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Logging> _logger;

        public Logging(RequestDelegate next, ILogger<Logging> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");

            await _next(context);

            stopwatch.Stop();
            _logger.LogInformation($"Completed request: {context.Request.Method} {context.Request.Path} in {stopwatch.ElapsedMilliseconds}ms with status code {context.Response.StatusCode}");
        }
    }
}
