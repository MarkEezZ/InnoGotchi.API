using FluentValidation;
using FluentValidation.AspNetCore;
using InnoGotchi.API;
using InnoGotchi.API.Extensions;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 44344;
});

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddPolicyAuthorization();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseTokenEmbedderMiddleware();
app.UseAuthentication();
app.UseJwtAuthMiddleware();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints => 
{ 
    endpoints.MapControllers(); 
});

app.Run();

