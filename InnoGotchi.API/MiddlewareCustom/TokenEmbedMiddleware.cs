namespace InnoGotchi.API.Extensions
{
    public class TokenEmbedMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenEmbedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies["AspNetCore.Application.Id"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }

            await _next(context);
        }
    }
}
