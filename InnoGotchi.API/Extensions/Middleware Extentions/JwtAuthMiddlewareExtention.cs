namespace InnoGotchi.API.Extensions
{
    public static class JwtAuthMiddlewareExtention
    {
        public static IApplicationBuilder UseJwtAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtAuthMiddleware>();
        }
    }
}
