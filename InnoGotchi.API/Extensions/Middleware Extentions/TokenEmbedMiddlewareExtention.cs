namespace InnoGotchi.API.Extensions
{
    public static class TokenEmbedMiddlewareExtention
    {
        public static IApplicationBuilder UseTokenEmbedderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenEmbedMiddleware>();
        }
    }
}
