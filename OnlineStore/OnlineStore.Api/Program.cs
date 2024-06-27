using OnlineStore.Api.ActionFilters;
using OnlineStore.Application.DependencyInjection;
using OnlineStore.DAL.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationLayer();
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddScoped<ValidationFilterAttribute>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
