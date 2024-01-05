using ILogger = Serilog.ILogger;

namespace InventoryManagementSystem.Api.Middlewares
{
    public class ExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(ILogger logger) => _logger = logger;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{

                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            var error = new Error(exception);

            _logger.Error(exception, exception.Message, exception.StackTrace);

            context.Response.StatusCode = (int)error.StatusCode;

            return context.Response.WriteAsJsonAsync(error.Result);
        }
    }
}
