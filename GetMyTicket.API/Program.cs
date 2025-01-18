using GetMyTicket.API.ServiceExtensions;
using GetMyTicket.Common.Entities;
using GetMyTicket.Persistance.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer
(builder.Configuration.GetConnectionString("DefaultConnection")));

//TODO: once we have a sufficient custom implementation, remove Identity Api endpoints; 

builder.Services.AddIdentity<User, ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints()
    .AddDefaultTokenProviders();

builder.Services.AddApplicationServices();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("React Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000/") 
              .AllowAnyHeader()                                              
              .AllowAnyMethod();                                            
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Get my ticket",
        Version = "v1",
        Description = "An example API for .NET 9"
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("React Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = string.Empty; // Serve the UI at the app's root
    });
}

using (var scope = app.Services.CreateScope())
{
    try
    {
        var DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await DbContext.Database.EnsureCreatedAsync();
    }
    catch (Exception)
    {
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapIdentityApi<User>();

app.Run();
