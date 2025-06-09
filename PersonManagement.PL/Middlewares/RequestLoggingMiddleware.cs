namespace PersonManagement.PL.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;

            _logger.LogInformation("HTTP {Method} {Path} from {IP}",
                request.Method,
                request.Path,
                context.Connection.RemoteIpAddress?.ToString());

            await _next(context);

            _logger.LogInformation("HTTP Response {StatusCode}", context.Response.StatusCode);
        }
    }

}
