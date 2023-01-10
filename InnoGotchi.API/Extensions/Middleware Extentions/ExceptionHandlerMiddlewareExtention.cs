using InnoGotchi.API.Entities.ErrorModel;

namespace InnoGotchi.API.Extensions
{
    public static class ExceptionHandlerMiddlewareExtention
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
