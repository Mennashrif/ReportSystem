using ReportSystem.Application.Features.Common.DTO;
using System.Net;

namespace ReportSystem.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (FluentValidation.ValidationException ex)
            {
                await HandleValidationExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string response = new ResponseDTO()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage =  ex.Message 
                }.ToString();
                _logger.LogError(ex.InnerException, $"{response}");
                await httpContext.Response.WriteAsync(response);
            }
        }
        private async Task HandleValidationExceptionAsync(HttpContext context, FluentValidation.ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(new ResponseDTO()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = exception.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
            }.ToString());
        }
    }

}
