using IdentityServer.Api.Extensions;
using IdentityServer.Application.DependencyInjection;
using IdentityServer.Application.Settings;
using IdentityServer.DAL.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.DefaultSection));

builder.Services.ConfigureSwagger();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddApplicationLayer();
builder.Services.AddDataAccessLayer(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
